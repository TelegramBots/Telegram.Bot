using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Requests.Abstractions;
using Xunit.Abstractions;
using Xunit.Sdk;

#nullable enable

namespace Telegram.Bot.Tests.Integ.Framework;

#if WTB
#pragma warning disable CS9113, CA1822
internal class RetryTelegramBotClient(TestConfiguration configuration, IMessageSink diagnosticMessageSink, CancellationToken ct = default)
    : WTelegramBotClient(MakeOptions(configuration.ApiToken, configuration.ClientApiToken.Split(':')), cancellationToken: ct)
{
    private static StreamWriter WTelegramLogs = new StreamWriter("WTelegramBot.log", true, System.Text.Encoding.UTF8) { AutoFlush = true };
    private static WTelegramBotClientOptions MakeOptions(string botToken, string[] api)
    {
        var connection = new Microsoft.Data.Sqlite.SqliteConnection($"Data Source=WTelegramBot.{botToken.Split(':')[0]}.sqlite");
        WTelegram.Helpers.Log = (lvl, str) => System.Diagnostics.Trace.WriteLine(str);
        WTelegram.Helpers.Log += (lvl, str) => WTelegramLogs.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{"TDIWE!"[lvl]}] {str}");
        return new(botToken, int.Parse(api[0]), api[1], connection);
    }
    public void WithStreams(Stream[] _) { }
}
#else
internal class RetryTelegramBotClient(TestConfiguration configuration, IMessageSink diagnosticMessageSink, CancellationToken ct = default)
    : TelegramBotClient(new TelegramBotClientOptions(configuration.ApiToken), cancellationToken: ct)
{
    public int RetryMax { get; } = configuration.RetryCount;
    public TimeSpan DefaultTimeout { get; } = TimeSpan.FromSeconds(configuration.DefaultRetryTimeout);

    private Stream[]? _testStreams;
    public void WithStreams(Stream[] streams) => _testStreams = streams;

    public override async Task<TResponse> SendRequest<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        RequestException apiRequestException = new RequestException("Should never been fired");

        try
        {
            for (var i = 0; i < RetryMax; i++)
            {
                try
                {
                    return await base.SendRequest(request, cancellationToken);
                }
                catch (ApiRequestException e) when (e.ErrorCode == 429)
                {
                    apiRequestException = e;

                    var timeout = e.Parameters?.RetryAfter is null
                        ? DefaultTimeout
                        : TimeSpan.FromSeconds(e.Parameters.RetryAfter.Value);

                    var message = $"Retry attempt {i + 1}. Waiting for {timeout} seconds before retrying.";
                    diagnosticMessageSink.OnMessage(new DiagnosticMessage(message));
                    await Task.Delay(timeout, cancellationToken);
                    if (_testStreams != null)
                        foreach (var stream in _testStreams)
                            stream.Position = 0;
                }
            }
        }
        finally
        {
            _testStreams = null;
        }
        throw apiRequestException;
    }
}
#endif

internal static class RetryTelegramBotClientExtensions
{
    public static ITelegramBotClient WithStreams(this ITelegramBotClient botClient, params System.IO.Stream[] streams)
    {
        if (botClient is RetryTelegramBotClient retry) retry.WithStreams(streams);
        return botClient;
    }
}

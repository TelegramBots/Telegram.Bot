using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Requests.Abstractions;
using Xunit.Abstractions;
using Xunit.Sdk;

#nullable enable

namespace Telegram.Bot.Tests.Integ.Framework;

internal class TestClientOptions(
    string token,
    string? baseUrl,
    bool useTestEnvironment,
    int retryCount,
    TimeSpan defaultTimeout)
    : TelegramBotClientOptions(token, baseUrl, useTestEnvironment)
{
    public int RetryCount { get; } = retryCount;
    public TimeSpan DefaultTimeout { get; } = defaultTimeout;
};

internal class RetryTelegramBotClient(
    IMessageSink diagnosticMessageSink,
    TestClientOptions options)
    : TelegramBotClient(options)
{
    public override async Task<TResponse> MakeRequestAsync<TResponse>(
        IRequest<TResponse> request,
        CancellationToken cancellationToken = default)
    {
        RequestException apiRequestException = new RequestException("Should never been fired");

        for (var i = 0; i < options.RetryCount; i++)
        {
            try
            {
                return await base.MakeRequestAsync(request, cancellationToken);
            }
            catch (ApiRequestException e) when (e.ErrorCode == 429)
            {
                apiRequestException = e;

                var timeout = e.Parameters?.RetryAfter is null
                    ? options.DefaultTimeout
                    : TimeSpan.FromSeconds(e.Parameters.RetryAfter.Value);

                var message = $"Retry attempt {i + 1}. Waiting for {timeout} seconds before retrying.";
                diagnosticMessageSink.OnMessage(new DiagnosticMessage(message));
                await Task.Delay(timeout, cancellationToken);
            }
        }

        throw apiRequestException;
    }
}

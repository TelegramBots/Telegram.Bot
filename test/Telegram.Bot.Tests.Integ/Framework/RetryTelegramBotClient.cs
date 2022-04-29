using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Requests.Abstractions;
using Xunit.Abstractions;
using Xunit.Sdk;

#nullable enable

namespace Telegram.Bot.Tests.Integ.Framework;

internal class TestClientOptions : TelegramBotClientOptions
{
    public int RetryCount { get; }
    public TimeSpan DefaultTimeout { get; }

    public TestClientOptions(
        string token,
        string? baseUrl,
        bool useTestEnvironment,
        int retryCount,
        TimeSpan defaultTimeout)
        : base(token, baseUrl, useTestEnvironment)
    {
        RetryCount = retryCount;
        DefaultTimeout = defaultTimeout;
    }
};

internal class RetryTelegramBotClient : TelegramBotClient
{
    readonly IMessageSink _diagnosticMessageSink;
    readonly TestClientOptions _options;

    public RetryTelegramBotClient(
        IMessageSink diagnosticMessageSink,
        TestClientOptions options)
        : base(options)
    {
        _diagnosticMessageSink = diagnosticMessageSink;
        _options = options;
    }

    public override async Task<TResponse> MakeRequestAsync<TResponse>(
        IRequest<TResponse> request,
        CancellationToken cancellationToken = default)
    {
        ApiRequestException apiRequestException = default!;

        for (var i = 0; i < _options.RetryCount; i++)
        {
            try
            {
                return await base.MakeRequestAsync(request, cancellationToken);
            }
            catch (ApiRequestException e) when (e.ErrorCode == 429)
            {
                apiRequestException = e;

                var timeout = e.Parameters?.RetryAfter is null
                    ? _options.DefaultTimeout
                    : TimeSpan.FromSeconds(e.Parameters.RetryAfter.Value);

                var message = $"Retry attempt {i + 1}. Waiting for {timeout} seconds before retrying.";
                _diagnosticMessageSink.OnMessage(new DiagnosticMessage(message));
                await Task.Delay(timeout, cancellationToken);
            }
        }

        throw apiRequestException;
    }
}

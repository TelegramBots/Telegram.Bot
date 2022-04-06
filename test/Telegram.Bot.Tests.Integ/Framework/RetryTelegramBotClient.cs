using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Requests.Abstractions;
using Xunit.Abstractions;
using Xunit.Sdk;

#nullable enable

namespace Telegram.Bot.Tests.Integ.Framework;

internal record TestClientOptions(
    int RetryCount,
    TimeSpan DefaultTimeout,
    string Token
);

internal class RetryTelegramBotClient : TelegramBotClient
{
    private readonly IMessageSink _diagnosticMessageSink;
    private readonly TestClientOptions _options;

    public RetryTelegramBotClient(
        IMessageSink diagnosticMessageSink,
        TestClientOptions options)
        : base(token: options.Token, httpClient: default, baseUrl: default)
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

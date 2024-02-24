using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
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
    TimeSpan defaultTimeout) : TelegramBotClientOptions(token, baseUrl, useTestEnvironment)
{
    public int RetryCount { get; } = retryCount;
    public TimeSpan DefaultTimeout { get; } = defaultTimeout;
};

internal class RetryTelegramBotClient(
    IMessageSink diagnosticMessageSink,
    TestClientOptions options) : TelegramBotClient(options)
{
    readonly IMessageSink _diagnosticMessageSink = diagnosticMessageSink;
    readonly TestClientOptions _options = options;

    public override async Task<TResponse> MakeRequestAsync<TResponse>(
        IRequest<TResponse> request,
        CancellationToken cancellationToken = default)
    {
        ApiRequestException apiRequestException = default!;

        var url = $"{_options.BaseRequestUrl}/{request.MethodName}";
        var httpRequest = new HttpRequestMessage(method: request.Method, requestUri: url)
        {
            Content = request.ToHttpContent()
        };

        for (var i = 0; i < _options.RetryCount; i++)
        {
            try
            {
                HttpRequestMessage? clonedContent = await CloneHttpRequestMessageAsync(httpRequest);
                return await MakeRequestAsync<TResponse>(request, clonedContent, cancellationToken);
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

    internal static async Task<HttpRequestMessage> CloneHttpRequestMessageAsync(HttpRequestMessage req)
    {
        HttpRequestMessage clone = new HttpRequestMessage(req.Method, req.RequestUri);

        // Copy the request's content (via a MemoryStream) into the cloned object
        var ms = new MemoryStream();
        if (req.Content != null)
        {
            await req.Content.CopyToAsync(ms).ConfigureAwait(false);
            ms.Position = 0;
            clone.Content = new StreamContent(ms);

            // Copy the content headers
            foreach (var h in req.Content.Headers)
                clone.Content.Headers.Add(h.Key, h.Value);
        }


        clone.Version = req.Version;

        foreach (KeyValuePair<string, object?> option in req.Options)
            clone.Options.Set(new HttpRequestOptionsKey<object?>(option.Key), option.Value);

        foreach (KeyValuePair<string, IEnumerable<string>> header in req.Headers)
            clone.Headers.TryAddWithoutValidation(header.Key, header.Value);

        return clone;
    }
}

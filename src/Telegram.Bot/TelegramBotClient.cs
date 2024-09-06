using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Telegram.Bot.Args;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Requests;

namespace Telegram.Bot;

/// <summary>
/// A client to use the Telegram Bot API
/// </summary>
[PublicAPI]
public class TelegramBotClient : ITelegramBotClient
{
    readonly TelegramBotClientOptions _options;

    readonly HttpClient _httpClient;

    /// <inheritdoc/>
    public long BotId => _options.BotId;

    /// <inheritdoc />
    public bool LocalBotServer => _options.LocalBotServer;

    /// <summary>
    /// Timeout for requests
    /// </summary>
    public TimeSpan Timeout
    {
        get => _httpClient.Timeout;
        set => _httpClient.Timeout = value;
    }

    #region For testing purposes
    internal string BaseRequestUrl => _options.BaseRequestUrl;
    internal string BaseFileUrl => _options.BaseFileUrl;
    #endregion

#pragma warning disable CS1591
    public delegate Task OnUpdateHandler(Update update);
    public delegate Task OnMessageHandler(Message message, UpdateType type);
    public delegate Task OnErrorHandler(Exception exception, Polling.HandleErrorSource source);
    OnUpdateHandler? _onUpdate;
    OnMessageHandler? _onMessage;
    CancellationTokenSource? _receivingEvents;
    /// <summary>Handler to be called when there is an incoming update</summary>
    public event OnUpdateHandler? OnUpdate { add { _onUpdate += value; StartEventReceiving(); } remove { _onUpdate -= value; StopEventReceiving(); } }
    /// <summary>Handler to be called when there is an incoming message or edited message</summary>
    public event OnMessageHandler? OnMessage { add { _onMessage += value; StartEventReceiving(); } remove { _onMessage -= value; StopEventReceiving(); } }
    /// <summary>Handler to be called when there was a polling error or an exception in your handlers</summary>
    public event OnErrorHandler? OnError;
#pragma warning restore CS1591

    /// <summary>
    /// Global cancellation token
    /// </summary>
    public CancellationToken GlobalCancelToken { get; }

    /// <inheritdoc />
    public IExceptionParser ExceptionsParser { get; set; } = new DefaultExceptionParser();

    /// <summary>
    /// Occurs before sending a request to API
    /// </summary>
    public event AsyncEventHandler<ApiRequestEventArgs>? OnMakingApiRequest;

    /// <summary>
    /// Occurs after receiving the response to an API request
    /// </summary>
    public event AsyncEventHandler<ApiResponseEventArgs>? OnApiResponseReceived;

    /// <summary>
    /// Create a new <see cref="TelegramBotClient"/> instance.
    /// </summary>
    /// <param name="options">Configuration for <see cref="TelegramBotClient" /></param>
    /// <param name="httpClient">A custom <see cref="HttpClient"/></param>
    /// <param name="cancellationToken">Global cancellation token</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="options"/> is <see langword="null"/>
    /// </exception>
    public TelegramBotClient(
        TelegramBotClientOptions options,
        HttpClient? httpClient = default,
        CancellationToken cancellationToken = default)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
#if NET6_0_OR_GREATER
        _httpClient = httpClient ?? new HttpClient(new SocketsHttpHandler { PooledConnectionLifetime = TimeSpan.FromMinutes(3) });
#else
        _httpClient = httpClient ?? new HttpClient();
#endif
        GlobalCancelToken = cancellationToken;
    }

    /// <summary>
    /// Create a new <see cref="TelegramBotClient"/> instance.
    /// </summary>
    /// <param name="token"></param>
    /// <param name="httpClient">A custom <see cref="HttpClient"/></param>
    /// <param name="cancellationToken">Global cancellation token</param>
    /// <exception cref="ArgumentException">
    /// Thrown if <paramref name="token"/> format is invalid
    /// </exception>
    public TelegramBotClient(
        string token,
        HttpClient? httpClient = null,
        CancellationToken cancellationToken = default) :
        this(new TelegramBotClientOptions(token), httpClient, cancellationToken)
    { }

    /// <inheritdoc />
    public virtual async Task<TResponse> MakeRequestAsync<TResponse>(
        IRequest<TResponse> request,
        CancellationToken cancellationToken = default)
    {
        if (request is null) { throw new ArgumentNullException(nameof(request)); }

        using var cts = CancellationTokenSource.CreateLinkedTokenSource(GlobalCancelToken, cancellationToken);
        var url = $"{_options.BaseRequestUrl}/{request.MethodName}";
        using var httpContent = request.ToHttpContent();
        for (int attempt = 1; ; attempt++)
        {
            var httpRequest = new HttpRequestMessage(request.Method, url) { Content = httpContent };
            ApiRequestEventArgs? requestEventArgs = null;
            if (OnMakingApiRequest is not null)
            {
                requestEventArgs ??= new(request, httpRequest);
                await OnMakingApiRequest.Invoke(this, requestEventArgs, cts.Token).ConfigureAwait(false);
            }
            // httpContent.Headers.ContentLength must be called after OnMakingApiRequest, because it enforces the
            // final ContentLength header, and OnMakingApiRequest might modify the content, leading to discrepancy
            if (httpContent != null && _options.RetryThreshold > 0 && _options.RetryCount > 1 && httpContent.Headers.ContentLength == null)
                await httpContent.LoadIntoBufferAsync().ConfigureAwait(false);

            using var httpResponse = await SendRequestAsync(_httpClient, httpRequest, cts.Token).ConfigureAwait(false);

            if (OnApiResponseReceived is not null)
            {
                requestEventArgs ??= new(request, httpRequest);
                ApiResponseEventArgs responseEventArgs = new(httpResponse, requestEventArgs);
                await OnApiResponseReceived.Invoke(this, responseEventArgs, cts.Token).ConfigureAwait(false);
            }

            if (httpResponse.StatusCode != HttpStatusCode.OK)
            {
                var failedApiResponse = await httpResponse.DeserializeContentAsync<ApiResponse>(
                    response => response.ErrorCode == default || response.Description is null, cts.Token).ConfigureAwait(false);

                if (failedApiResponse.ErrorCode == 429 && _options.RetryThreshold > 0 && attempt < _options.RetryCount &&
                    failedApiResponse.Parameters?.RetryAfter <= _options.RetryThreshold)
                {
                    await Task.Delay(failedApiResponse.Parameters.RetryAfter.Value * 1000, cts.Token).ConfigureAwait(false);
                    continue; // retry attempt
                }
                throw ExceptionsParser.Parse(failedApiResponse);
            }

            var apiResponse = await httpResponse.DeserializeContentAsync<ApiResponse<TResponse>>(
                    response => !response.Ok || response.Result is null, cts.Token).ConfigureAwait(false);
            return apiResponse.Result!;
        }

        [MethodImpl(methodImplOptions: MethodImplOptions.AggressiveInlining)]
        static async Task<HttpResponseMessage> SendRequestAsync(
            HttpClient httpClient,
            HttpRequestMessage httpRequest,
            CancellationToken cancellationToken)
        {
            HttpResponseMessage? httpResponse;
            try
            {
                httpResponse = await httpClient
                    .SendAsync(request: httpRequest, cancellationToken: cancellationToken)
                    .ConfigureAwait(continueOnCapturedContext: false);
            }
            catch (TaskCanceledException exception)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    throw;
                }

                throw new RequestException(message: "Request timed out", innerException: exception);
            }
            catch (Exception exception)
            {
                throw new RequestException(
                    message: "Exception during making request",
                    innerException: exception
                );
            }

            return httpResponse;
        }
    }

    /// <summary>
    /// Test the API token
    /// </summary>
    /// <returns><see langword="true"/> if token is valid</returns>
    public async Task<bool> TestApiAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await MakeRequestAsync(request: new GetMeRequest(), cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            return true;
        }
        catch (ApiRequestException e)
            when (e.ErrorCode == 401)
        {
            return false;
        }
    }

    /// <inheritdoc />
    public async Task DownloadFileAsync(
        string filePath,
        Stream destination,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(filePath) || filePath.Length < 2)
        {
            throw new ArgumentException(message: "Invalid file path", paramName: nameof(filePath));
        }

        if (destination is null) { throw new ArgumentNullException(nameof(destination)); }

        using var cts = CancellationTokenSource.CreateLinkedTokenSource(GlobalCancelToken, cancellationToken);
        var fileUri = $"{_options.BaseFileUrl}/{filePath}";
        using HttpResponseMessage httpResponse = await GetResponseAsync(
            httpClient: _httpClient,
            fileUri: fileUri,
            cancellationToken: cts.Token
        ).ConfigureAwait(false);

        if (!httpResponse.IsSuccessStatusCode)
        {
            var failedApiResponse = await httpResponse.DeserializeContentAsync<ApiResponse>(
                    response => response.ErrorCode == default || response.Description is null, cts.Token).ConfigureAwait(false);

            throw ExceptionsParser.Parse(failedApiResponse);
        }

        if (httpResponse.Content is null)
        {
            throw new RequestException(
                message: "Response doesn't contain any content",
                httpResponse.StatusCode
            );
        }

        try
        {
#if NET6_0_OR_GREATER
            await httpResponse.Content.CopyToAsync(destination, cts.Token)
                .ConfigureAwait(false);
#else
            await httpResponse.Content.CopyToAsync(destination)
                .ConfigureAwait(false);
#endif
        }
        catch (Exception exception)
        {
            throw new RequestException(
                message: "Exception during file download",
                httpResponse.StatusCode,
                exception
            );
        }

        [MethodImpl(methodImplOptions: MethodImplOptions.AggressiveInlining)]
        static async Task<HttpResponseMessage> GetResponseAsync(
            HttpClient httpClient,
            string fileUri,
            CancellationToken cancellationToken)
        {
            HttpResponseMessage? httpResponse;
            try
            {
                httpResponse = await httpClient
                    .GetAsync(
                        requestUri: fileUri,
                        completionOption: HttpCompletionOption.ResponseHeadersRead,
                        cancellationToken: cancellationToken
                    )
                    .ConfigureAwait(false);
            }
            catch (TaskCanceledException exception)
            {
                if (cancellationToken.IsCancellationRequested) { throw; }

                throw new RequestException(
                    message: "Request timed out",
                    innerException: exception
                );
            }
            catch (Exception exception)
            {
                throw new RequestException(
                    message: "Exception during file download",
                    innerException: exception
                );
            }

            return httpResponse;
        }
    }

    private void StartEventReceiving()
    {
        lock (_options)
        {
            if (_receivingEvents != null) return;
            _receivingEvents ??= new CancellationTokenSource();
        }
        this.StartReceiving(async (bot, update, ct) =>
        {
            var task = _onMessage == null ? _onUpdate?.Invoke(update) : update switch
            {
                { Message: { } m } => _onMessage?.Invoke(m, UpdateType.Message),
                { EditedMessage: { } em } => _onMessage?.Invoke(em, UpdateType.EditedMessage),
                { ChannelPost: { } cp } => _onMessage?.Invoke(cp, UpdateType.ChannelPost),
                { EditedChannelPost: { } ecp } => _onMessage?.Invoke(ecp, UpdateType.EditedChannelPost),
                { BusinessMessage: { } bm } => _onMessage?.Invoke(bm, UpdateType.BusinessMessage),
                { EditedBusinessMessage: { } ebm } => _onMessage?.Invoke(ebm, UpdateType.EditedBusinessMessage),
                _ => _onUpdate?.Invoke(update) // if OnMessage is set, we call OnUpdate only for non-message updates
            };
            if (task != null) await task.ConfigureAwait(true);
        }, async(bot, ex, source, ct) =>
        {
            var task = OnError?.Invoke(ex, source);
            if (task != null) await task.ConfigureAwait(true);
            else System.Diagnostics.Trace.WriteLine(ex); // fallback logging if OnError is unset
        }, new() { AllowedUpdates = Update.AllTypes }, _receivingEvents.Token);
    }

    private void StopEventReceiving()
    {
        lock (_options)
        {
            if (_onUpdate != null || _onMessage != null || _receivingEvents == null) return;
            _receivingEvents?.Cancel();
            _receivingEvents = null;
        }
    }
}

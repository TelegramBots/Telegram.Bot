using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Telegram.Bot.Args;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Requests;
#if NET6_0_OR_GREATER
using System.Net.Http.Json;
#endif

#pragma warning disable CA1001 // _receivingEvents isn't used with timer or WaitHandle, so needn't be disposed 

namespace Telegram.Bot;

/// <summary>A client to use the Telegram Bot API</summary>
[PublicAPI]
public class TelegramBotClient : ITelegramBotClient
{
    private readonly TelegramBotClientOptions _options;

    private readonly HttpClient _httpClient;

    /// <summary>Bot token</summary>
    public string Token => _options.Token;

    /// <inheritdoc/>
    public long BotId => _options.BotId;

    /// <inheritdoc/>
    public bool LocalBotServer => _options.LocalBotServer;

    /// <inheritdoc/>
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
    private OnUpdateHandler? _onUpdate;
    private OnMessageHandler? _onMessage;
    private CancellationTokenSource? _receivingEvents;
    /// <summary>Handler to be called when there is an incoming update</summary>
    public event OnUpdateHandler? OnUpdate { add { _onUpdate += value; StartEventReceiving(); } remove { _onUpdate -= value; StopEventReceiving(); } }
    /// <summary>Handler to be called when there is an incoming message or edited message</summary>
    public event OnMessageHandler? OnMessage { add { _onMessage += value; StartEventReceiving(); } remove { _onMessage -= value; StopEventReceiving(); } }
    /// <summary>Handler to be called when there was a polling error or an exception in your handlers</summary>
    public event OnErrorHandler? OnError;
#pragma warning restore CS1591

    /// <summary>Global cancellation token</summary>
    public CancellationToken GlobalCancelToken { get; }

    /// <inheritdoc/>
    public IExceptionParser ExceptionsParser { get; set; } = new DefaultExceptionParser();

    /// <inheritdoc/>
    public event AsyncEventHandler<ApiRequestEventArgs>? OnMakingApiRequest;

    /// <inheritdoc/>
    public event AsyncEventHandler<ApiResponseEventArgs>? OnApiResponseReceived;

    /// <summary>Create a new <see cref="TelegramBotClient"/> instance.</summary>
    /// <param name="options">Configuration for <see cref="TelegramBotClient" /></param>
    /// <param name="httpClient">A custom <see cref="HttpClient"/></param>
    /// <param name="cancellationToken">Global cancellation token</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="options"/> is <see langword="null"/></exception>
    public TelegramBotClient(TelegramBotClientOptions options, HttpClient? httpClient = default, CancellationToken cancellationToken = default)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
#if NET6_0_OR_GREATER
        _httpClient = httpClient ?? new HttpClient(new SocketsHttpHandler { PooledConnectionLifetime = TimeSpan.FromMinutes(3) });
#else
        _httpClient = httpClient ?? new HttpClient();
#endif
        GlobalCancelToken = cancellationToken;
    }

    /// <summary>Create a new <see cref="TelegramBotClient"/> instance.</summary>
    /// <param name="token">The bot token</param>
    /// <param name="httpClient">A custom <see cref="HttpClient"/></param>
    /// <param name="cancellationToken">Global cancellation token</param>
    /// <exception cref="ArgumentException">Thrown if <paramref name="token"/> format is invalid</exception>
    public TelegramBotClient(string token, HttpClient? httpClient = null, CancellationToken cancellationToken = default)
        : this(new TelegramBotClientOptions(token), httpClient, cancellationToken)
    { }

    /// <inheritdoc/>
    public virtual async Task<TResponse> SendRequest<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        if (request is null) { throw new ArgumentNullException(nameof(request)); }

        using var cts = CancellationTokenSource.CreateLinkedTokenSource(GlobalCancelToken, cancellationToken);
        cancellationToken = cts.Token;
        var url = $"{_options.BaseRequestUrl}/{request.MethodName}";
        using var httpContent = request.ToHttpContent();
        for (int attempt = 1; ; attempt++)
        {
            var httpRequest = new HttpRequestMessage(request.HttpMethod, url) { Content = httpContent };
            ApiRequestEventArgs? requestEventArgs = null;
            if (OnMakingApiRequest is not null)
            {
                requestEventArgs ??= new(request, httpRequest);
                await OnMakingApiRequest.Invoke(this, requestEventArgs, cancellationToken).ConfigureAwait(false);
            }
            // httpContent.Headers.ContentLength must be called after OnMakingApiRequest, because it enforces the
            // final ContentLength header, and OnMakingApiRequest might modify the content, leading to discrepancy
            if (httpContent != null && _options.RetryThreshold > 0 && _options.RetryCount > 1 && httpContent.Headers.ContentLength == null)
                await httpContent.LoadIntoBufferAsync().ConfigureAwait(false);

            HttpResponseMessage httpResponse;
            try
            {
                httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);
            }
            catch (TaskCanceledException exception)
            {
                if (cancellationToken.IsCancellationRequested) throw;
                throw new RequestException("Bot API Request timed out", exception);
            }
            catch (Exception exception)
            {
                throw new RequestException($"Bot API Service Failure: {exception.GetType().Name}: {exception.Message}", exception);
            }
            using (httpResponse)
            {
                if (OnApiResponseReceived is not null)
                {
                    requestEventArgs ??= new(request, httpRequest);
                    ApiResponseEventArgs responseEventArgs = new(httpResponse, requestEventArgs);
                    await OnApiResponseReceived.Invoke(this, responseEventArgs, cancellationToken).ConfigureAwait(false);
                }

                if (httpResponse.StatusCode != HttpStatusCode.OK)
                {
                    var failedApiResponse = await DeserializeContent<ApiResponse>(httpResponse,
                        response => response.ErrorCode != default && response.Description != null, cancellationToken).ConfigureAwait(false);

                    if (failedApiResponse.ErrorCode == 429 && _options.RetryThreshold > 0 && attempt < _options.RetryCount &&
                        failedApiResponse.Parameters?.RetryAfter <= _options.RetryThreshold)
                    {
                        await Task.Delay(failedApiResponse.Parameters.RetryAfter.Value * 1000, cancellationToken).ConfigureAwait(false);
                        continue; // retry attempt
                    }
                    throw ExceptionsParser.Parse(failedApiResponse);
                }

                var apiResponse = await DeserializeContent<ApiResponse<TResponse>>(httpResponse,
                        response => response.Ok && response.Result != null, cancellationToken).ConfigureAwait(false);
                return apiResponse.Result!;
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static async Task<T> DeserializeContent<T>(HttpResponseMessage httpResponse, Func<T, bool> validate,
        CancellationToken cancellationToken = default) where T : class
    {
        if (httpResponse.Content is null)
            throw new RequestException("Response doesn't contain any content", httpResponse.StatusCode);
        T? deserializedObject;
        try
        {
#if NET6_0_OR_GREATER
            deserializedObject = await httpResponse.Content.ReadFromJsonAsync<T>(JsonBotAPI.Options, cancellationToken).ConfigureAwait(false);
#else
            using var contentStream = await httpResponse.Content.ReadAsStreamAsync().ConfigureAwait(false);
            deserializedObject = await JsonSerializer.DeserializeAsync<T>(contentStream, JsonBotAPI.Options,
                cancellationToken).ConfigureAwait(false);
#endif
        }
        catch (Exception exception)
        {
            throw new RequestException("There was an exception during deserialization of the response", httpResponse.StatusCode, exception);
        }
        if (deserializedObject is null || !validate(deserializedObject))
            throw new RequestException("Required properties not found in response", httpResponse.StatusCode);
        return deserializedObject;
    }

    /// <inheritdoc/>
    public async Task<bool> TestApi(CancellationToken cancellationToken = default)
    {
        try
        {
            await SendRequest(new GetMeRequest(), cancellationToken).ConfigureAwait(false);
            return true;
        }
        catch (ApiRequestException e) when (e.ErrorCode == 401)
        {
            return false;
        }
    }

    /// <summary>Get type of the file referenced by a FileId string</summary>
    /// <param name="fileId">Identifier of file (Base64)</param>
    public static FileIdType GetFileIdType(string fileId) => (FileIdType)Convert.FromBase64String(fileId[0..4])[0];

    /// <inheritdoc/>
    public Task DownloadFile(TGFile file, Stream destination, CancellationToken cancellationToken = default)
        => DownloadFile(file.FilePath!, destination, cancellationToken);

    /// <inheritdoc/>
    public async Task DownloadFile(string filePath, Stream destination, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(filePath) || filePath.Length < 2)
            throw new ArgumentException(message: "Invalid file path", paramName: nameof(filePath));

        if (destination is null) { throw new ArgumentNullException(nameof(destination)); }

        using var cts = CancellationTokenSource.CreateLinkedTokenSource(GlobalCancelToken, cancellationToken);
        var fileUri = $"{_options.BaseFileUrl}/{filePath}";
        using HttpResponseMessage httpResponse = await GetResponseAsync(_httpClient, fileUri, cts.Token).ConfigureAwait(false);

        if (!httpResponse.IsSuccessStatusCode)
        {
            var failedApiResponse = await DeserializeContent<ApiResponse>(httpResponse,
                    response => response.ErrorCode != default && response.Description != null, cts.Token).ConfigureAwait(false);
            throw ExceptionsParser.Parse(failedApiResponse);
        }

        if (httpResponse.Content is null)
            throw new RequestException("Response doesn't contain any content", httpResponse.StatusCode);

        try
        {
#if NET6_0_OR_GREATER
            await httpResponse.Content.CopyToAsync(destination, cts.Token).ConfigureAwait(false);
#else
            await httpResponse.Content.CopyToAsync(destination).ConfigureAwait(false);
#endif
        }
        catch (Exception exception)
        {
            throw new RequestException("Exception during file download", httpResponse.StatusCode, exception);
        }

        [MethodImpl(methodImplOptions: MethodImplOptions.AggressiveInlining)]
        static async Task<HttpResponseMessage> GetResponseAsync(HttpClient httpClient, string fileUri, CancellationToken cancellationToken)
        {
            HttpResponseMessage? httpResponse;
            try
            {
                httpResponse = await httpClient.GetAsync(fileUri, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (TaskCanceledException exception)
            {
                if (cancellationToken.IsCancellationRequested) { throw; }

                throw new RequestException("Request timed out", exception);
            }
            catch (Exception exception)
            {
                throw new RequestException("Exception during file download", exception);
            }
            return httpResponse;
        }
    }

    private void StartEventReceiving()
    {
        lock (_options)
        {
            if (_receivingEvents != null) return;
            _receivingEvents = new CancellationTokenSource();
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
        }, async (bot, ex, source, ct) =>
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
            _receivingEvents?.Dispose();
            _receivingEvents = null;
        }
    }
}

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Helpers;
using Telegram.Bot.Requests;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using File = Telegram.Bot.Types.File;

namespace Telegram.Bot
{
    /// <summary>
    /// A client to use the Telegram Bot API
    /// </summary>
    public class TelegramBotClient : ITelegramBotClient
    {
        const string BaseTelegramUrl = "https://api.telegram.org";
        static readonly Update[] EmptyUpdates = { };

        static readonly ExceptionParser ExceptionParser = new();

        readonly string _baseFileUrl;
        readonly string _baseRequestUrl;
        readonly bool _localBotServer;
        readonly HttpClient _httpClient;

        CancellationTokenSource? _receivingCancellationTokenSource;


        #region Config Properties

        /// <inheritdoc/>
        public long? BotId { get; }

        /// <summary>
        /// Timeout for requests
        /// </summary>
        public TimeSpan Timeout
        {
            get => _httpClient.Timeout;
            set => _httpClient.Timeout = value;
        }

        /// <summary>
        /// Indicates if receiving updates
        /// </summary>
        [Obsolete("This property will be removed in the next major version. " +
            "Please consider using Telegram.Bot.Extensions.Polling instead.")]
        public bool IsReceiving { get; set; }

        /// <summary>
        /// The current message offset
        /// </summary>
        [Obsolete("This property will be removed in the next major version. " +
            "Please consider using Telegram.Bot.Extensions.Polling instead.")]
        public int MessageOffset { get; set; }

        #endregion Config Properties

        #region Events

        /// <summary>
        /// Occurs before sending a request to API
        /// </summary>
        public event AsyncEventHandler<ApiRequestEventArgs>? OnMakingApiRequest;

        /// <summary>
        /// Occurs after receiving the response to an API request
        /// </summary>
        public event AsyncEventHandler<ApiResponseEventArgs>? OnApiResponseReceived;

        /// <summary>
        /// Raises the <see cref="OnUpdate" />, <see cref="OnMessage"/>,
        /// <see cref="OnInlineQuery"/>, <see cref="OnInlineResultChosen"/> and
        /// <see cref="OnCallbackQuery"/> events.
        /// </summary>
        /// <param name="e">
        /// The <see cref="UpdateEventArgs"/> instance containing the event data.
        /// </param>
        protected virtual void OnUpdateReceived(UpdateEventArgs e)
        {
            OnUpdate?.Invoke(this, e);

            switch (e.Update.Type)
            {
                case UpdateType.Message:
                    OnMessage?.Invoke(this, e);
                    break;

                case UpdateType.InlineQuery:
                    OnInlineQuery?.Invoke(this, e);
                    break;

                case UpdateType.ChosenInlineResult:
                    OnInlineResultChosen?.Invoke(this, e);
                    break;

                case UpdateType.CallbackQuery:
                    OnCallbackQuery?.Invoke(this, e);
                    break;

                case UpdateType.EditedMessage:
                    OnMessageEdited?.Invoke(this, e);
                    break;
            }
        }

        /// <summary>
        /// Occurs when an <see cref="Update"/> is received.
        /// </summary>
        [Obsolete("This event will be removed in the next major version. " +
            "Please consider using Telegram.Bot.Extensions.Polling instead.")]
        public event EventHandler<UpdateEventArgs>? OnUpdate;

        /// <summary>
        /// Occurs when a <see cref="Message"/> is received.
        /// </summary>
        [Obsolete("This event will be removed in the next major version. " +
            "Please consider using Telegram.Bot.Extensions.Polling instead.")]
        public event EventHandler<MessageEventArgs>? OnMessage;

        /// <summary>
        /// Occurs when <see cref="Message"/> was edited.
        /// </summary>
        [Obsolete("This event will be removed in the next major version. " +
            "Please consider using Telegram.Bot.Extensions.Polling instead.")]
        public event EventHandler<MessageEventArgs>? OnMessageEdited;

        /// <summary>
        /// Occurs when an <see cref="InlineQuery"/> is received.
        /// </summary>
        [Obsolete("This event will be removed in the next major version. " +
            "Please consider using Telegram.Bot.Extensions.Polling instead.")]
        public event EventHandler<InlineQueryEventArgs>? OnInlineQuery;

        /// <summary>
        /// Occurs when a <see cref="ChosenInlineResult"/> is received.
        /// </summary>
        [Obsolete("This event will be removed in the next major version. " +
            "Please consider using Telegram.Bot.Extensions.Polling instead.")]
        public event EventHandler<ChosenInlineResultEventArgs>? OnInlineResultChosen;

        /// <summary>
        /// Occurs when an <see cref="CallbackQuery"/> is received
        /// </summary>
        [Obsolete("This event will be removed in the next major version. " +
            "Please consider using Telegram.Bot.Extensions.Polling instead.")]
        public event EventHandler<CallbackQueryEventArgs>? OnCallbackQuery;

        /// <summary>
        /// Occurs when an error occurs during the background update pooling.
        /// </summary>
        [Obsolete("This event will be removed in the next major version. " +
            "Please consider using Telegram.Bot.Extensions.Polling instead.")]
        public event EventHandler<ReceiveErrorEventArgs>? OnReceiveError;

        /// <summary>
        /// Occurs when an error occurs during the background update pooling.
        /// </summary>
        [Obsolete("This event will be removed in the next major version. " +
            "Please consider using Telegram.Bot.Extensions.Polling instead.")]
        public event EventHandler<ReceiveGeneralErrorEventArgs>? OnReceiveGeneralError;

        #endregion

        /// <summary>
        /// Create a new <see cref="TelegramBotClient"/> instance.
        /// </summary>
        /// <param name="token">API token</param>
        /// <param name="httpClient">A custom <see cref="HttpClient"/></param>
        /// <param name="baseUrl">
        /// Used to change base url to your private bot api server URL. It looks like
        /// http://localhost:8081. Path, query and fragment will be omitted if present.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="token"/> format is invalid
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="baseUrl"/> format is invalid
        /// </exception>
        public TelegramBotClient(
            string token,
            HttpClient? httpClient = null,
            string? baseUrl = default)
        {
            if (token is null) throw new ArgumentNullException(nameof(token));

            BotId = GetIdFromToken(token);

            _localBotServer = baseUrl is not null;
            var effectiveBaseUrl = _localBotServer
                ? ExtractBaseUrl(baseUrl)
                : BaseTelegramUrl;

            _baseRequestUrl = $"{effectiveBaseUrl}/bot{token}";
            _baseFileUrl = $"{effectiveBaseUrl}/file/bot{token}";
            _httpClient = httpClient ?? new HttpClient();

            static long? GetIdFromToken(string token)
            {
#if NETCOREAPP3_1_OR_GREATER
                var span = token.AsSpan();
                var index = span.IndexOf(':');

                if (index is < 1 or > 16) { return null; }

                var botIdSpan = span[..index];
                if (!long.TryParse(botIdSpan, out var botId)) { return null; }
#else
                var index = token.IndexOf(':');

                if (index is < 1 or > 16) { return null; }

                var botIdSpan = token.Substring(0, index);
                if (!long.TryParse(botIdSpan, out var botId)) { return null; }
#endif

                return botId;
            }
        }

        /// <summary>
        /// Create a new <see cref="TelegramBotClient"/> instance behind a proxy.
        /// </summary>
        /// <param name="token">API token</param>
        /// <param name="webProxy">Use this <see cref="IWebProxy"/> to connect to the API</param>
        /// <param name="baseUrl">
        /// Used to change base url to your private bot api server URL. It looks like
        /// http://localhost:8081. Path, query and fragment will be omitted if present.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="token"/> format is invalid
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="baseUrl"/> format is invalid
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="webProxy"/> is null
        /// </exception>
        [Obsolete("Provide httpClient with configured proxy instead.")]
        public TelegramBotClient(string token, IWebProxy webProxy, string? baseUrl = default)
            : this(token, CreateHttpClient(webProxy), baseUrl)
        { }

        #region Helpers

        /// <inheritdoc />
        public async Task<TResponse> MakeRequestAsync<TResponse>(
            IRequest<TResponse> request,
            CancellationToken cancellationToken = default)
        {
            if (request is null) { throw new ArgumentNullException(nameof(request)); }

            var url = new Uri($"{_baseRequestUrl}{request.MethodName}", UriKind.Absolute);
            var httpRequest = new HttpRequestMessage(request.Method, url)
            {
                Content = request.ToHttpContent()
            };

            ApiRequestEventArgs? requestEventArgs = default;

            if (OnMakingApiRequest != null)
            {
                requestEventArgs = new ApiRequestEventArgs(
                    request.MethodName,
                    httpRequest.Content
                );
                await OnMakingApiRequest.Invoke(this, requestEventArgs, cancellationToken);
            }

            HttpResponseMessage? httpResponse;
            try
            {
                httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (TaskCanceledException exception)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    throw;
                }

                throw new RequestException("Request timed out", exception);
            }
            catch (Exception exception)
            {
                throw new RequestException("Exception during making request", exception);
            }

            try
            {
                if (OnApiResponseReceived != null)
                {
                    requestEventArgs ??= new ApiRequestEventArgs(
                        request.MethodName,
                        httpRequest.Content
                    );
                    var responseEventArgs = new ApiResponseEventArgs(
                        httpResponse,
                        requestEventArgs
                    );
                    await OnApiResponseReceived.Invoke(this, responseEventArgs, cancellationToken);
                }

                if (httpResponse.StatusCode != HttpStatusCode.OK)
                {
                    var failedApiResponse = await httpResponse
                        .DeserializeContentAsync<ApiResponse>(
                            guard: response => response.ErrorCode == default ||
                                               response.Description is null
                        )
                        .ConfigureAwait(false);

                    throw ExceptionParser.Parse(failedApiResponse);
                }

                var apiResponse = await httpResponse
                    .DeserializeContentAsync<ApiResponse<TResponse>>(
                        guard: response => response.Ok == false ||
                                           response.Result is null
                    )
                    .ConfigureAwait(false);

                return apiResponse.Result!;
            }
            finally
            {
                httpResponse?.Dispose();
            }
        }

        /// <summary>
        /// Test the API token
        /// </summary>
        /// <returns><c>true</c> if token is valid</returns>
        public async Task<bool> TestApiAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await MakeRequestAsync(new GetMeRequest(), cancellationToken).ConfigureAwait(false);
                return true;
            }
            catch (ApiRequestException e)
                when (e.ErrorCode == 401)
            {
                return false;
            }
        }

        /// <summary>
        /// Start update receiving
        /// </summary>
        /// <param name="allowedUpdates">List the types of updates you want your bot to receive.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiRequestException"> Thrown if token is invalid</exception>
        [Obsolete("This method will be removed in the next major version. " +
            "Please consider using Telegram.Bot.Extensions.Polling instead.")]
        public void StartReceiving(UpdateType[]? allowedUpdates = default,
                                   CancellationToken cancellationToken = default)
        {
            _receivingCancellationTokenSource = new CancellationTokenSource();

            cancellationToken.Register(() => _receivingCancellationTokenSource.Cancel());

            ReceiveAsync(allowedUpdates, _receivingCancellationTokenSource.Token);
        }

#pragma warning disable AsyncFixer03 // Avoid fire & forget async void methods
        async void ReceiveAsync(
            UpdateType[]? allowedUpdates,
            CancellationToken cancellationToken = default)
        {
            IsReceiving = true;
            while (!cancellationToken.IsCancellationRequested)
            {
                var timeout = Convert.ToInt32(Timeout.TotalSeconds);
                var updates = EmptyUpdates;

                try
                {
                    updates = await MakeRequestAsync(
                        new GetUpdatesRequest
                        {
                            Offset = MessageOffset,
                            Timeout = timeout,
                            AllowedUpdates = allowedUpdates,
                        },
                        cancellationToken: cancellationToken
                    ).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                }
                catch (ApiRequestException apiException)
                {
                    OnReceiveError?.Invoke(this, apiException);
                }
                catch (Exception generalException)
                {
                    OnReceiveGeneralError?.Invoke(this, generalException);
                }

                try
                {
                    foreach (var update in updates)
                    {
                        OnUpdateReceived(new UpdateEventArgs(update));
                        MessageOffset = update.Id + 1;
                    }
                }
                catch
                {
                    IsReceiving = false;
                    throw;
                }
            }

            IsReceiving = false;
        }
#pragma warning restore AsyncFixer03 // Avoid fire & forget async void methods

        /// <summary>
        /// Stop update receiving
        /// </summary>
        [Obsolete("This method will be removed in the next major version. " +
            "Please consider using Telegram.Bot.Extensions.Polling instead.")]
        public void StopReceiving()
        {
            try
            {
                _receivingCancellationTokenSource?.Cancel();
            }
            catch (WebException)
            {
            }
            catch (TaskCanceledException)
            {
            }
            finally
            {
                _receivingCancellationTokenSource?.Dispose();
            }
        }

        #endregion Helpers

        /// <inheritdoc />
        public async Task DownloadFileAsync(
            string filePath,
            Stream destination,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(filePath) || filePath.Length < 2)
            {
                throw new ArgumentException("Invalid file path", nameof(filePath));
            }

            if (destination is null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            var fileUri = new Uri($"{_baseFileUrl}{filePath}", UriKind.Absolute);

            HttpResponseMessage? httpResponse;
            try
            {
                httpResponse = await _httpClient
                    .GetAsync(fileUri, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (TaskCanceledException exception)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    throw;
                }

                throw new RequestException("Request timed out", exception);
            }
            catch (Exception exception)
            {
                throw new RequestException("Exception during file download", exception);
            }

            try
            {
                if (!httpResponse.IsSuccessStatusCode)
                {
                    var failedApiResponse = await httpResponse
                        .DeserializeContentAsync<ApiResponse>(
                            guard: response => response.ErrorCode == default ||
                                               response.Description is null
                        )
                        .ConfigureAwait(false);

                    throw ExceptionParser.Parse(failedApiResponse);
                }

                if (httpResponse.Content is null)
                    throw new RequestException(
                        "Response doesn't contain any content",
                        httpResponse.StatusCode
                    );

                try
                {
                    await httpResponse.Content.CopyToAsync(destination)
                        .ConfigureAwait(false);
                }
                catch (Exception exception)
                {
                    throw new RequestException(
                        "Exception during file download",
                        httpResponse.StatusCode,
                        exception
                    );
                }
            }
            finally
            {
                httpResponse?.Dispose();
            }
        }

        /// <inheritdoc />
        public async Task<File> GetInfoAndDownloadFileAsync(
            string fileId,
            Stream destination,
            CancellationToken cancellationToken = default)
        {
            var file = await MakeRequestAsync(new GetFileRequest(fileId), cancellationToken)
                .ConfigureAwait(false);

            await DownloadFileAsync(file.FilePath!, destination, cancellationToken)
                .ConfigureAwait(false);

            return file;
        }

        static HttpClient CreateHttpClient(IWebProxy webProxy) =>
            new(new HttpClientHandler
            {
                Proxy = webProxy ?? throw new ArgumentNullException(nameof(webProxy)),
                UseProxy = true
            });

        static string ExtractBaseUrl(string? baseUrl)
        {
            if (baseUrl is null) throw new ArgumentNullException(nameof(baseUrl));

            if (!Uri.TryCreate(baseUrl, UriKind.Absolute, out var baseUri)
                || string.IsNullOrEmpty(baseUri.Scheme)
                || string.IsNullOrEmpty(baseUri.Authority))
            {
                throw new ArgumentException(
                    "Invalid format. A valid base url looks \"http://localhost:8081\" ",
                    nameof(baseUrl)
                );
            }

            return $"{baseUri.Scheme}://{baseUri.Authority}";
        }

        #region For testing purposes

        internal string BaseRequestUrl => _baseRequestUrl;
        internal string BaseFileUrl => _baseFileUrl;
        internal bool LocalBotServer => _localBotServer;

        #endregion
    }
}

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Helpers;
using Telegram.Bot.Requests;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;

namespace Telegram.Bot
{
    /// <summary>
    /// A client to use the Telegram Bot API
    /// </summary>
    public class TelegramBotClient : ITelegramBotClient
    {
        const string BaseTelegramUrl = "https://api.telegram.org";

        readonly string _baseFileUrl;
        readonly string _baseRequestUrl;
        readonly bool _localBotServer;
        readonly HttpClient _httpClient;

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
            if (token is null) { throw new ArgumentNullException(nameof(token)); }

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
                var index = token.IndexOf(value: ':');

                if (index is < 1 or > 16) { return null; }

                var botIdSpan = token.Substring(startIndex: 0, length: index);
                if (!long.TryParse(botIdSpan, out var botId)) { return null; }
#endif

                return botId;
            }
        }

        /// <inheritdoc />
        public async Task<TResponse> MakeRequestAsync<TResponse>(
            IRequest<TResponse> request,
            CancellationToken cancellationToken = default)
        {
            if (request is null) { throw new ArgumentNullException(nameof(request)); }

            var url = $"{_baseRequestUrl}/{request.MethodName}";

            using var httpRequest = new HttpRequestMessage(method: request.Method, requestUri: url)
            {
                Content = request.ToHttpContent()
            };

            if (OnMakingApiRequest is not null)
            {
                var requestEventArgs = new ApiRequestEventArgs(
                    request: request,
                    httpRequestMessage: httpRequest
                );
                await OnMakingApiRequest.Invoke(
                    botClient: this,
                    args: requestEventArgs,
                    cancellationToken: cancellationToken
                ).ConfigureAwait(false);
            }

            using var httpResponse = await SendRequestAsync(
                httpClient: _httpClient,
                httpRequest: httpRequest,
                cancellationToken: cancellationToken
            ).ConfigureAwait(false);

            if (OnApiResponseReceived is not null)
            {
                var requestEventArgs = new ApiRequestEventArgs(
                    request: request,
                    httpRequestMessage: httpRequest
                );
                var responseEventArgs = new ApiResponseEventArgs(
                    responseMessage: httpResponse,
                    apiRequestEventArgs: requestEventArgs
                );
                await OnApiResponseReceived.Invoke(
                    botClient: this,
                    args: responseEventArgs,
                    cancellationToken: cancellationToken
                ).ConfigureAwait(false);
            }

            if (httpResponse.StatusCode != HttpStatusCode.OK)
            {
                var failedApiResponse = await httpResponse
                    .DeserializeContentAsync<ApiResponse>(
                        guard: response =>
                            response.ErrorCode == default ||
                            // ReSharper disable ConditionIsAlwaysTrueOrFalse
                            response.Description is null
                            // ReSharper restore ConditionIsAlwaysTrueOrFalse
                    )
                    .ConfigureAwait(false);

                throw ExceptionsParser.Parse(failedApiResponse);
            }

            var apiResponse = await httpResponse
                .DeserializeContentAsync<ApiResponse<TResponse>>(
                    guard: response => response.Ok == false ||
                                       response.Result is null
                )
                .ConfigureAwait(false);

            return apiResponse.Result!;

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
        /// <returns><c>true</c> if token is valid</returns>
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

            var fileUri = $"{_baseFileUrl}/{filePath}";
            using HttpResponseMessage httpResponse = await GetResponseAsync(
                httpClient: _httpClient,
                fileUri: fileUri,
                cancellationToken: cancellationToken
            ).ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode)
            {
                var failedApiResponse = await httpResponse
                    .DeserializeContentAsync<ApiResponse>(
                        guard: response =>
                            response.ErrorCode == default ||
                            // ReSharper disable ConditionIsAlwaysTrueOrFalse
                            response.Description is null
                            // ReSharper restore ConditionIsAlwaysTrueOrFalse
                    )
                    .ConfigureAwait(false);

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
                await httpResponse.Content.CopyToAsync(destination)
                    .ConfigureAwait(false);
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
                        .ConfigureAwait(continueOnCapturedContext: false);
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

        static string ExtractBaseUrl(string? baseUrl)
        {
            if (baseUrl is null) { throw new ArgumentNullException(paramName: nameof(baseUrl)); }

            if (!Uri.TryCreate(uriString: baseUrl, uriKind: UriKind.Absolute, out var baseUri)
                || string.IsNullOrEmpty(value: baseUri.Scheme)
                || string.IsNullOrEmpty(value: baseUri.Authority))
            {
                throw new ArgumentException(
                    message: "Invalid format. A valid base url looks \"http://localhost:8081\" ",
                    paramName: nameof(baseUrl)
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

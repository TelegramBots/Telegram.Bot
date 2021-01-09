using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telegram.Bot.Args;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Requests;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Requests.Parameters;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.Payments;
using Telegram.Bot.Types.ReplyMarkups;
using File = Telegram.Bot.Types.File;

namespace Telegram.Bot
{
    /// <summary>
    ///     A client to use the Telegram Bot API
    /// </summary>
    public class TelegramBotClient : ITelegramBotClient
    {
        private const string BaseUrl = "https://api.telegram.org/bot";

        private const string BaseFileUrl = "https://api.telegram.org/file/bot";

        private static readonly Update[] EmptyUpdates = { };

        private readonly string _baseRequestUrl;

        private readonly HttpClient _httpClient;

        private readonly string _token;

        /// <summary>
        ///     Create a new <see cref="TelegramBotClient" /> instance.
        /// </summary>
        /// <param name="token">API token</param>
        /// <param name="httpClient">A custom <see cref="HttpClient" /></param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="token" /> format is invalid</exception>
        public TelegramBotClient(string token, HttpClient httpClient = null)
        {
            _token = token ?? throw new ArgumentNullException(nameof(token));
            var parts = _token.Split(':');
            if (parts.Length > 1 && int.TryParse(parts[0], out var id))
                BotId = id;
            else
                throw new ArgumentException(
                    "Invalid format. A valid token looks like \"1234567:4TT8bAc8GHUspu3ERYn-KGcvsvGB9u_n4ddy\".",
                    nameof(token)
                );

            _baseRequestUrl = $"{BaseUrl}{_token}/";
            _httpClient = httpClient ?? new HttpClient();
        }

        /// <summary>
        ///     Create a new <see cref="TelegramBotClient" /> instance behind a proxy.
        /// </summary>
        /// <param name="token">API token</param>
        /// <param name="webProxy">Use this <see cref="IWebProxy" /> to connect to the API</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="token" /> format is invalid</exception>
        public TelegramBotClient(string token, IWebProxy webProxy)
        {
            _token = token ?? throw new ArgumentNullException(nameof(token));
            var parts = _token.Split(':');
            if (int.TryParse(parts[0], out var id))
                BotId = id;
            else
                throw new ArgumentException(
                    "Invalid format. A valid token looks like \"1234567:4TT8bAc8GHUspu3ERYn-KGcvsvGB9u_n4ddy\".",
                    nameof(token)
                );

            _baseRequestUrl = $"{BaseUrl}{_token}/";
            var httpClientHander = new HttpClientHandler
            {
                Proxy = webProxy,
                UseProxy = true
            };
            _httpClient = new HttpClient(httpClientHander);
        }

        /// <inheritdoc />
        public int BotId { get; }

        #region Inline mode

        /// <inheritdoc />
        public Task AnswerInlineQueryAsync(
            string inlineQueryId,
            IEnumerable<InlineQueryResultBase> results,
            int? cacheTime = default,
            bool isPersonal = default,
            string nextOffset = default,
            string switchPmText = default,
            string switchPmParameter = default,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new AnswerInlineQueryRequest(inlineQueryId, results)
            {
                CacheTime = cacheTime,
                IsPersonal = isPersonal,
                NextOffset = nextOffset,
                SwitchPmText = switchPmText,
                SwitchPmParameter = switchPmParameter
            }, cancellationToken);
        }

        # endregion Inline mode

        #region Config Properties

        /// <summary>
        ///     Timeout for requests
        /// </summary>
        public TimeSpan Timeout
        {
            get => _httpClient.Timeout;
            set => _httpClient.Timeout = value;
        }

        /// <summary>
        ///     Indicates if receiving updates
        /// </summary>
        public bool IsReceiving { get; set; }

        private CancellationTokenSource _receivingCancellationTokenSource;

        /// <summary>
        ///     The current message offset
        /// </summary>
        public int MessageOffset { get; set; }

        #endregion Config Properties

        #region Events

        /// <summary>
        ///     Occurs before sending a request to API
        /// </summary>
        public event EventHandler<ApiRequestEventArgs> MakingApiRequest;

        /// <summary>
        ///     Occurs after receiving the response to an API request
        /// </summary>
        public event EventHandler<ApiResponseEventArgs> ApiResponseReceived;

        /// <summary>
        ///     Raises the <see cref="OnUpdate" />, <see cref="OnMessage" />, <see cref="OnInlineQuery" />,
        ///     <see cref="OnInlineResultChosen" /> and <see cref="OnCallbackQuery" /> events.
        /// </summary>
        /// <param name="e">The <see cref="UpdateEventArgs" /> instance containing the event data.</param>
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
        ///     Occurs when an <see cref="Update" /> is received.
        /// </summary>
        public event EventHandler<UpdateEventArgs> OnUpdate;

        /// <summary>
        ///     Occurs when a <see cref="Message" /> is received.
        /// </summary>
        public event EventHandler<MessageEventArgs> OnMessage;

        /// <summary>
        ///     Occurs when <see cref="Message" /> was edited.
        /// </summary>
        public event EventHandler<MessageEventArgs> OnMessageEdited;

        /// <summary>
        ///     Occurs when an <see cref="InlineQuery" /> is received.
        /// </summary>
        public event EventHandler<InlineQueryEventArgs> OnInlineQuery;

        /// <summary>
        ///     Occurs when a <see cref="ChosenInlineResult" /> is received.
        /// </summary>
        public event EventHandler<ChosenInlineResultEventArgs> OnInlineResultChosen;

        /// <summary>
        ///     Occurs when an <see cref="CallbackQuery" /> is received
        /// </summary>
        public event EventHandler<CallbackQueryEventArgs> OnCallbackQuery;

        /// <summary>
        ///     Occurs when an error occurs during the background update pooling.
        /// </summary>
        public event EventHandler<ReceiveErrorEventArgs> OnReceiveError;

        /// <summary>
        ///     Occurs when an error occurs during the background update pooling.
        /// </summary>
        public event EventHandler<ReceiveGeneralErrorEventArgs> OnReceiveGeneralError;

        #endregion

        #region Helpers

        /// <inheritdoc />
        public async Task<TResponse> MakeRequestAsync<TResponse>(
            IRequest<TResponse> request,
            CancellationToken cancellationToken = default)
        {
            var url = _baseRequestUrl + request.MethodName;

            var httpRequest = new HttpRequestMessage(request.Method, url)
            {
                Content = request.ToHttpContent()
            };

            var reqDataArgs = new ApiRequestEventArgs
            {
                MethodName = request.MethodName,
                HttpContent = httpRequest.Content
            };
            MakingApiRequest?.Invoke(this, reqDataArgs);

            HttpResponseMessage httpResponse;
            try
            {
                httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (TaskCanceledException e)
            {
                if (cancellationToken.IsCancellationRequested)
                    throw;

                throw new ApiRequestException("Request timed out", 408, e);
            }

            // required since user might be able to set new status code using following event arg
            var actualResponseStatusCode = httpResponse.StatusCode;
            var responseJson = await httpResponse.Content.ReadAsStringAsync()
                .ConfigureAwait(false);

            ApiResponseReceived?.Invoke(this, new ApiResponseEventArgs
            {
                ResponseMessage = httpResponse,
                ApiRequestEventArgs = reqDataArgs
            });

            switch (actualResponseStatusCode)
            {
                case HttpStatusCode.OK:
                    break;
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.BadRequest when !string.IsNullOrWhiteSpace(responseJson):
                case HttpStatusCode.Forbidden when !string.IsNullOrWhiteSpace(responseJson):
                case HttpStatusCode.Conflict when !string.IsNullOrWhiteSpace(responseJson):
                    // Do NOT throw here, an ApiRequestException will be thrown next
                    break;
                default:
                    httpResponse.EnsureSuccessStatusCode();
                    break;
            }

            var apiResponse =
                JsonConvert.DeserializeObject<ApiResponse<TResponse>>(responseJson)
                ?? new ApiResponse<TResponse> // ToDo is required? unit test
                {
                    Ok = false,
                    Description = "No response received"
                };

            if (!apiResponse.Ok)
                throw ApiExceptionParser.Parse(apiResponse);

            return apiResponse.Result;
        }

        /// <summary>
        ///     Test the API token
        /// </summary>
        /// <returns><c>true</c> if token is valid</returns>
        public async Task<bool> TestApiAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await GetMeAsync(cancellationToken).ConfigureAwait(false);
                return true;
            }
            catch (ApiRequestException e)
                when (e.ErrorCode == 401)
            {
                return false;
            }
        }

        /// <summary>
        ///     Start update receiving
        /// </summary>
        /// <param name="allowedUpdates">List the types of updates you want your bot to receive.</param>
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of
        ///     cancellation.
        /// </param>
        /// <exception cref="ApiRequestException"> Thrown if token is invalid</exception>
        public void StartReceiving(UpdateType[] allowedUpdates = null,
                                   CancellationToken cancellationToken = default)
        {
            _receivingCancellationTokenSource = new CancellationTokenSource();

            cancellationToken.Register(() => _receivingCancellationTokenSource.Cancel());

            ReceiveAsync(allowedUpdates, _receivingCancellationTokenSource.Token);
        }

#pragma warning disable AsyncFixer03 // Avoid fire & forget async void methods
        private async void ReceiveAsync(
            UpdateType[] allowedUpdates,
            CancellationToken cancellationToken = default)
        {
            IsReceiving = true;
            while (!cancellationToken.IsCancellationRequested)
            {
                var timeout = Convert.ToInt32(Timeout.TotalSeconds);
                var updates = EmptyUpdates;

                try
                {
                    updates = await GetUpdatesAsync(
                        MessageOffset,
                        timeout: timeout,
                        allowedUpdates: allowedUpdates,
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
        ///     Stop update receiving
        /// </summary>
        public void StopReceiving()
        {
            try
            {
                _receivingCancellationTokenSource.Cancel();
            }
            catch (WebException)
            {
            }
            catch (TaskCanceledException)
            {
            }
        }

        #endregion Helpers

        #region Getting updates

        /// <inheritdoc />
        public Task<Update[]> GetUpdatesAsync(
            int offset = default,
            int limit = default,
            int timeout = default,
            IEnumerable<UpdateType> allowedUpdates = default,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new GetUpdatesRequest
            {
                Offset = offset,
                Limit = limit,
                Timeout = timeout,
                AllowedUpdates = allowedUpdates
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task SetWebhookAsync(
            string url,
            InputFileStream certificate = default,
            int maxConnections = default,
            IEnumerable<UpdateType> allowedUpdates = default,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new SetWebhookRequest(url, certificate)
            {
                MaxConnections = maxConnections,
                AllowedUpdates = allowedUpdates
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task DeleteWebhookAsync(CancellationToken cancellationToken = default)
        {
            return MakeRequestAsync(new DeleteWebhookRequest(), cancellationToken);
        }

        /// <inheritdoc />
        public Task<WebhookInfo> GetWebhookInfoAsync(CancellationToken cancellationToken = default)
        {
            return MakeRequestAsync(new GetWebhookInfoRequest(), cancellationToken);
        }

        #endregion Getting updates

        #region Available methods

        /// <inheritdoc />
        public Task<User> GetMeAsync(CancellationToken cancellationToken = default)
        {
            return MakeRequestAsync(new GetMeRequest(), cancellationToken);
        }

        /// <inheritdoc />
        public Task<Message> SendTextMessageAsync(
            ChatId chatId,
            string text,
            ParseMode parseMode = default,
            bool disableWebPagePreview = default,
            bool disableNotification = default,
            int replyToMessageId = default,
            IReplyMarkup replyMarkup = default,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new SendMessageRequest(chatId, text)
            {
                ParseMode = parseMode,
                DisableWebPagePreview = disableWebPagePreview,
                DisableNotification = disableNotification,
                ReplyToMessageId = replyToMessageId,
                ReplyMarkup = replyMarkup
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<Message> ForwardMessageAsync(
            ChatId chatId,
            ChatId fromChatId,
            int messageId,
            bool disableNotification = default,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new ForwardMessageRequest(chatId, fromChatId, messageId)
            {
                DisableNotification = disableNotification
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<Message> SendPhotoAsync(
            ChatId chatId,
            InputOnlineFile photo,
            string caption = default,
            ParseMode parseMode = default,
            bool disableNotification = default,
            int replyToMessageId = default,
            IReplyMarkup replyMarkup = default,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new SendPhotoRequest(chatId, photo)
            {
                Caption = caption,
                ParseMode = parseMode,
                ReplyToMessageId = replyToMessageId,
                DisableNotification = disableNotification,
                ReplyMarkup = replyMarkup
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<Message> SendAudioAsync(
            ChatId chatId,
            InputOnlineFile audio,
            string caption = default,
            ParseMode parseMode = default,
            int duration = default,
            string performer = default,
            string title = default,
            bool disableNotification = default,
            int replyToMessageId = default,
            IReplyMarkup replyMarkup = default,
            CancellationToken cancellationToken = default,
            InputMedia thumb = default
        )
        {
            return MakeRequestAsync(new SendAudioRequest(chatId, audio)
            {
                Caption = caption,
                ParseMode = parseMode,
                Duration = duration,
                Performer = performer,
                Title = title,
                Thumb = thumb,
                DisableNotification = disableNotification,
                ReplyToMessageId = replyToMessageId,
                ReplyMarkup = replyMarkup
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<Message> SendDocumentAsync(
            ChatId chatId,
            InputOnlineFile document,
            string caption = default,
            ParseMode parseMode = default,
            bool disableNotification = default,
            int replyToMessageId = default,
            IReplyMarkup replyMarkup = default,
            CancellationToken cancellationToken = default,
            InputMedia thumb = default
        )
        {
            return MakeRequestAsync(new SendDocumentRequest(chatId, document)
            {
                Caption = caption,
                Thumb = thumb,
                ParseMode = parseMode,
                DisableNotification = disableNotification,
                ReplyToMessageId = replyToMessageId,
                ReplyMarkup = replyMarkup
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<Message> SendStickerAsync(
            ChatId chatId,
            InputOnlineFile sticker,
            bool disableNotification = default,
            int replyToMessageId = default,
            IReplyMarkup replyMarkup = default,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new SendStickerRequest(chatId, sticker)
            {
                DisableNotification = disableNotification,
                ReplyToMessageId = replyToMessageId,
                ReplyMarkup = replyMarkup
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<Message> SendVideoAsync(
            ChatId chatId,
            InputOnlineFile video,
            int duration = default,
            int width = default,
            int height = default,
            string caption = default,
            ParseMode parseMode = default,
            bool supportsStreaming = default,
            bool disableNotification = default,
            int replyToMessageId = default,
            IReplyMarkup replyMarkup = default,
            CancellationToken cancellationToken = default,
            InputMedia thumb = default
        )
        {
            return MakeRequestAsync(new SendVideoRequest(chatId, video)
            {
                Duration = duration,
                Width = width,
                Height = height,
                Thumb = thumb,
                Caption = caption,
                ParseMode = parseMode,
                SupportsStreaming = supportsStreaming,
                DisableNotification = disableNotification,
                ReplyToMessageId = replyToMessageId,
                ReplyMarkup = replyMarkup
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<Message> SendAnimationAsync(
            ChatId chatId,
            InputOnlineFile animation,
            int duration = default,
            int width = default,
            int height = default,
            InputMedia thumb = default,
            string caption = default,
            ParseMode parseMode = default,
            bool disableNotification = default,
            int replyToMessageId = default,
            IReplyMarkup replyMarkup = default,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new SendAnimationRequest(chatId, animation)
            {
                Duration = duration,
                Width = width,
                Height = height,
                Thumb = thumb,
                Caption = caption,
                ParseMode = parseMode,
                DisableNotification = disableNotification,
                ReplyToMessageId = replyToMessageId,
                ReplyMarkup = replyMarkup
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<Message> SendVoiceAsync(
            ChatId chatId,
            InputOnlineFile voice,
            string caption = default,
            ParseMode parseMode = default,
            int duration = default,
            bool disableNotification = default,
            int replyToMessageId = default,
            IReplyMarkup replyMarkup = default,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new SendVoiceRequest(chatId, voice)
            {
                Caption = caption,
                ParseMode = parseMode,
                Duration = duration,
                DisableNotification = disableNotification,
                ReplyToMessageId = replyToMessageId,
                ReplyMarkup = replyMarkup
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<Message> SendVideoNoteAsync(
            ChatId chatId,
            InputTelegramFile videoNote,
            int duration = default,
            int length = default,
            bool disableNotification = default,
            int replyToMessageId = default,
            IReplyMarkup replyMarkup = default,
            CancellationToken cancellationToken = default,
            InputMedia thumb = default
        )
        {
            return MakeRequestAsync(new SendVideoNoteRequest(chatId, videoNote)
            {
                Duration = duration,
                Length = length,
                Thumb = thumb,
                DisableNotification = disableNotification,
                ReplyToMessageId = replyToMessageId,
                ReplyMarkup = replyMarkup
            }, cancellationToken);
        }

        /// <inheritdoc />
        [Obsolete("Use the other overload of this method instead. Only photo and video input types are allowed.")]
        public Task<Message[]> SendMediaGroupAsync(
            ChatId chatId,
            IEnumerable<InputMediaBase> media,
            bool disableNotification = default,
            int replyToMessageId = default,
            CancellationToken cancellationToken = default
        )
        {
            var inputMedia = media
                .Select(m => m as IAlbumInputMedia)
                .Where(m => m != null)
                .ToArray();
            return MakeRequestAsync(new SendMediaGroupRequest(chatId, inputMedia)
            {
                DisableNotification = disableNotification,
                ReplyToMessageId = replyToMessageId
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<Message[]> SendMediaGroupAsync(
            IEnumerable<IAlbumInputMedia> inputMedia,
            ChatId chatId,
            bool disableNotification = default,
            int replyToMessageId = default,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new SendMediaGroupRequest(chatId, inputMedia)
            {
                DisableNotification = disableNotification,
                ReplyToMessageId = replyToMessageId
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<Message> SendLocationAsync(
            ChatId chatId,
            float latitude,
            float longitude,
            int livePeriod = default,
            bool disableNotification = default,
            int replyToMessageId = default,
            IReplyMarkup replyMarkup = default,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new SendLocationRequest(chatId, latitude, longitude)
            {
                LivePeriod = livePeriod,
                DisableNotification = disableNotification,
                ReplyToMessageId = replyToMessageId,
                ReplyMarkup = replyMarkup
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<Message> SendVenueAsync(
            ChatId chatId,
            float latitude,
            float longitude,
            string title,
            string address,
            string foursquareId = default,
            bool disableNotification = default,
            int replyToMessageId = default,
            IReplyMarkup replyMarkup = default,
            CancellationToken cancellationToken = default,
            string foursquareType = default
        )
        {
            return MakeRequestAsync(new SendVenueRequest(chatId, latitude, longitude, title, address)
            {
                FoursquareId = foursquareId,
                FoursquareType = foursquareType,
                DisableNotification = disableNotification,
                ReplyToMessageId = replyToMessageId,
                ReplyMarkup = replyMarkup
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<Message> SendContactAsync(
            ChatId chatId,
            string phoneNumber,
            string firstName,
            string lastName = default,
            bool disableNotification = default,
            int replyToMessageId = default,
            IReplyMarkup replyMarkup = default,
            CancellationToken cancellationToken = default,
            string vCard = default
        )
        {
            return MakeRequestAsync(new SendContactRequest(chatId, phoneNumber, firstName)
            {
                LastName = lastName,
                Vcard = vCard,
                DisableNotification = disableNotification,
                ReplyToMessageId = replyToMessageId,
                ReplyMarkup = replyMarkup
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<Message> SendPollAsync(
            ChatId chatId,
            string question,
            IEnumerable<string> options,
            bool disableNotification = default,
            int replyToMessageId = default,
            IReplyMarkup replyMarkup = default,
            CancellationToken cancellationToken = default,
            bool? isAnonymous = default,
            PollType? type = default,
            bool? allowsMultipleAnswers = default,
            int? correctOptionId = default,
            bool? isClosed = default,
            string explanation = default,
            ParseMode explanationParseMode = default,
            int? openPeriod = default,
            DateTime? closeDate = default
        )
        {
            return MakeRequestAsync(new SendPollRequest(chatId, question, options)
            {
                DisableNotification = disableNotification,
                ReplyToMessageId = replyToMessageId,
                ReplyMarkup = replyMarkup,
                IsAnonymous = isAnonymous,
                Type = type,
                AllowsMultipleAnswers = allowsMultipleAnswers,
                CorrectOptionId = correctOptionId,
                IsClosed = isClosed,
                OpenPeriod = openPeriod,
                CloseDate = closeDate,
                Explanation = explanation,
                ExplanationParseMode = explanationParseMode
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<Message> SendDiceAsync(
            ChatId chatId,
            bool disableNotification = default,
            int replyToMessageId = default,
            IReplyMarkup replyMarkup = default,
            CancellationToken cancellationToken = default,
            Emoji? emoji = default)
        {
            return MakeRequestAsync(
                new SendDiceRequest(chatId)
                {
                    DisableNotification = disableNotification,
                    ReplyToMessageId = replyToMessageId,
                    ReplyMarkup = replyMarkup,
                    Emoji = emoji
                },
                cancellationToken
            );
        }

        /// <inheritdoc />
        public Task SendChatActionAsync(
            ChatId chatId,
            ChatAction chatAction,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new SendChatActionRequest(chatId, chatAction), cancellationToken);
        }

        /// <inheritdoc />
        public Task<UserProfilePhotos> GetUserProfilePhotosAsync(
            int userId,
            int offset = default,
            int limit = default,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new GetUserProfilePhotosRequest(userId)
            {
                Offset = offset,
                Limit = limit
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<File> GetFileAsync(
            string fileId,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new GetFileRequest(fileId), cancellationToken);
        }

        /// <inheritdoc />
        [Obsolete("This method will be removed in next major release. Use its overload instead.")]
        public async Task<Stream> DownloadFileAsync(
            string filePath,
            CancellationToken cancellationToken = default
        )
        {
            var stream = new MemoryStream();
            await DownloadFileAsync(filePath, stream, cancellationToken)
                .ConfigureAwait(false);
            return stream;
        }

        /// <inheritdoc />
        public async Task DownloadFileAsync(
            string filePath,
            Stream destination,
            CancellationToken cancellationToken = default
        )
        {
            if (string.IsNullOrWhiteSpace(filePath) || filePath.Length < 2)
                throw new ArgumentException("Invalid file path", nameof(filePath));

            if (destination == null) throw new ArgumentNullException(nameof(destination));

            var fileUri = new Uri($"{BaseFileUrl}{_token}/{filePath}");

            var response = await _httpClient
                .GetAsync(fileUri, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            using (response)
            {
                await response.Content.CopyToAsync(destination)
                    .ConfigureAwait(false);
            }
        }

        /// <inheritdoc />
        public async Task<File> GetInfoAndDownloadFileAsync(
            string fileId,
            Stream destination,
            CancellationToken cancellationToken = default
        )
        {
            var file = await GetFileAsync(fileId, cancellationToken)
                .ConfigureAwait(false);

            await DownloadFileAsync(file.FilePath, destination, cancellationToken)
                .ConfigureAwait(false);

            return file;
        }

        /// <inheritdoc />
        public Task KickChatMemberAsync(
            ChatId chatId,
            int userId,
            DateTime untilDate = default,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new KickChatMemberRequest(chatId, userId)
            {
                UntilDate = untilDate
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task LeaveChatAsync(
            ChatId chatId,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new LeaveChatRequest(chatId), cancellationToken);
        }

        /// <inheritdoc />
        public Task UnbanChatMemberAsync(
            ChatId chatId,
            int userId,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new UnbanChatMemberRequest(chatId, userId), cancellationToken);
        }

        /// <inheritdoc />
        public Task<Chat> GetChatAsync(
            ChatId chatId,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new GetChatRequest(chatId), cancellationToken);
        }

        /// <inheritdoc />
        public Task<ChatMember[]> GetChatAdministratorsAsync(
            ChatId chatId,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new GetChatAdministratorsRequest(chatId), cancellationToken);
        }

        /// <inheritdoc />
        public Task<int> GetChatMembersCountAsync(
            ChatId chatId,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new GetChatMembersCountRequest(chatId), cancellationToken);
        }

        /// <inheritdoc />
        public Task<ChatMember> GetChatMemberAsync(
            ChatId chatId,
            int userId,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new GetChatMemberRequest(chatId, userId), cancellationToken);
        }

        /// <inheritdoc />
        public Task AnswerCallbackQueryAsync(
            string callbackQueryId,
            string text = default,
            bool showAlert = default,
            string url = default,
            int cacheTime = default,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new AnswerCallbackQueryRequest(callbackQueryId)
            {
                Text = text,
                ShowAlert = showAlert,
                Url = url,
                CacheTime = cacheTime
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task RestrictChatMemberAsync(
            ChatId chatId,
            int userId,
            ChatPermissions permissions,
            DateTime untilDate = default,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(
                new RestrictChatMemberRequest(chatId, userId, permissions)
                {
                    UntilDate = untilDate
                },
                cancellationToken);
        }

        /// <inheritdoc />
        public Task PromoteChatMemberAsync(
            ChatId chatId,
            int userId,
            bool? canChangeInfo = default,
            bool? canPostMessages = default,
            bool? canEditMessages = default,
            bool? canDeleteMessages = default,
            bool? canInviteUsers = default,
            bool? canRestrictMembers = default,
            bool? canPinMessages = default,
            bool? canPromoteMembers = default,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new PromoteChatMemberRequest(chatId, userId)
            {
                CanChangeInfo = canChangeInfo,
                CanPostMessages = canPostMessages,
                CanEditMessages = canEditMessages,
                CanDeleteMessages = canDeleteMessages,
                CanInviteUsers = canInviteUsers,
                CanRestrictMembers = canRestrictMembers,
                CanPinMessages = canPinMessages,
                CanPromoteMembers = canPromoteMembers
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task SetChatAdministratorCustomTitleAsync(
            ChatId chatId,
            int userId,
            string customTitle,
            CancellationToken cancellationToken = default)
        {
            return MakeRequestAsync(
                new SetChatAdministratorCustomTitleRequest(chatId, userId, customTitle),
                cancellationToken);
        }

        /// <inheritdoc />
        public Task SetChatPermissionsAsync(
            ChatId chatId,
            ChatPermissions permissions,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new SetChatPermissionsRequest(chatId, permissions), cancellationToken);
        }

        /// <inheritdoc />
        public Task<BotCommand[]> GetMyCommandsAsync(CancellationToken cancellationToken = default)
        {
            return MakeRequestAsync(new GetMyCommandsRequest(), cancellationToken);
        }

        /// <inheritdoc />
        public Task SetMyCommandsAsync(
            IEnumerable<BotCommand> commands,
            CancellationToken cancellationToken = default)
        {
            return MakeRequestAsync(new SetMyCommandsRequest(commands), cancellationToken);
        }

        /// <inheritdoc />
        public Task<Message> StopMessageLiveLocationAsync(
            ChatId chatId,
            int messageId,
            InlineKeyboardMarkup replyMarkup = default,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new StopMessageLiveLocationRequest(chatId, messageId)
            {
                ReplyMarkup = replyMarkup
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task StopMessageLiveLocationAsync(
            string inlineMessageId,
            InlineKeyboardMarkup replyMarkup = default,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new StopInlineMessageLiveLocationRequest(inlineMessageId)
            {
                ReplyMarkup = replyMarkup
            }, cancellationToken);
        }

        #endregion Available methods

        #region Available methods with parameters

        /// <summary>
        ///     Send a request to Bot API
        /// </summary>
        /// <param name="makeRequestParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<TResponse> MakeRequestAsync<TResponse>(
            MakeRequestParameters<TResponse> makeRequestParameters, CancellationToken cancellationToken = default)
        {
            return MakeRequestAsync(makeRequestParameters.Request, cancellationToken);
        }

        /// <summary>
        ///     Start update receiving
        /// </summary>
        /// <param name="startReceivingParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual void StartReceiving(StartReceivingParameters startReceivingParameters,
                                           CancellationToken cancellationToken = default)
        {
            StartReceiving(startReceivingParameters.AllowedUpdates, cancellationToken);
        }

        /// <summary>
        ///     Use this method to receive incoming updates using long polling (wiki).
        /// </summary>
        /// <param name="getUpdatesParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<Update[]> GetUpdatesAsync(GetUpdatesParameters getUpdatesParameters,
                                                      CancellationToken cancellationToken = default)
        {
            return GetUpdatesAsync(getUpdatesParameters.Offset, getUpdatesParameters.Limit,
                getUpdatesParameters.Timeout, getUpdatesParameters.AllowedUpdates, cancellationToken);
        }

        /// <summary>
        ///     Use this method to specify a url and receive incoming updates via an outgoing webhook.
        ///     Whenever there is an <see cref="Update" /> for the bot, we will send an HTTPS POST request to the specified url,
        ///     containing a JSON-serialized Update. In case of an unsuccessful request, we will give up after a reasonable
        ///     amount of attempts.
        ///     If you'd like to make sure that the Webhook request comes from Telegram, we recommend using a secret path
        ///     in the URL, e.g. https://www.example.com/&lt;token&gt;. Since nobody else knows your bot's token, you can be
        ///     pretty sure it's us.
        /// </summary>
        /// <param name="setWebhookParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task SetWebhookAsync(SetWebhookParameters setWebhookParameters,
                                            CancellationToken cancellationToken = default)
        {
            return SetWebhookAsync(setWebhookParameters.Url, setWebhookParameters.Certificate,
                setWebhookParameters.MaxConnections, setWebhookParameters.AllowedUpdates, cancellationToken);
        }

        /// <summary>
        ///     Use this method to send text messages. On success, the sent Description is returned.
        /// </summary>
        /// <param name="sendTextMessageParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<Message> SendTextMessageAsync(SendTextMessageParameters sendTextMessageParameters,
                                                          CancellationToken cancellationToken = default)
        {
            return SendTextMessageAsync(sendTextMessageParameters.ChatId, sendTextMessageParameters.Text,
                sendTextMessageParameters.ParseMode, sendTextMessageParameters.DisableWebPagePreview,
                sendTextMessageParameters.DisableNotification, sendTextMessageParameters.ReplyToMessageId,
                sendTextMessageParameters.ReplyMarkup, cancellationToken);
        }

        /// <summary>
        ///     Use this method to forward messages of any kind. On success, the sent Description is returned.
        /// </summary>
        /// <param name="forwardMessageParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<Message> ForwardMessageAsync(ForwardMessageParameters forwardMessageParameters,
                                                         CancellationToken cancellationToken = default)
        {
            return ForwardMessageAsync(forwardMessageParameters.ChatId, forwardMessageParameters.FromChatId,
                forwardMessageParameters.MessageId, forwardMessageParameters.DisableNotification, cancellationToken);
        }

        /// <summary>
        ///     Use this method to send photos. On success, the sent Description is returned.
        /// </summary>
        /// <param name="sendPhotoParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<Message> SendPhotoAsync(SendPhotoParameters sendPhotoParameters,
                                                    CancellationToken cancellationToken = default)
        {
            return SendPhotoAsync(sendPhotoParameters.ChatId, sendPhotoParameters.Photo, sendPhotoParameters.Caption,
                sendPhotoParameters.ParseMode, sendPhotoParameters.DisableNotification,
                sendPhotoParameters.ReplyToMessageId, sendPhotoParameters.ReplyMarkup, cancellationToken);
        }

        /// <summary>
        ///     Use this method to send audio files, if you want Telegram clients to display them in the music player. Your
        ///     audio must be in the .mp3 format. On success, the sent Description is returned. Bots can currently send
        ///     audio files of up to 50 MB in size, this limit may be changed in the future.
        /// </summary>
        /// <param name="sendAudioParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<Message> SendAudioAsync(SendAudioParameters sendAudioParameters,
                                                    CancellationToken cancellationToken = default)
        {
            return SendAudioAsync(sendAudioParameters.ChatId, sendAudioParameters.Audio, sendAudioParameters.Caption,
                sendAudioParameters.ParseMode, sendAudioParameters.Duration, sendAudioParameters.Performer,
                sendAudioParameters.Title, sendAudioParameters.DisableNotification,
                sendAudioParameters.ReplyToMessageId, sendAudioParameters.ReplyMarkup, thumb: sendAudioParameters.Thumb,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        ///     Use this method to send general files. On success, the sent Description is returned. Bots can send files of
        ///     any type of up to 50 MB in size.
        /// </summary>
        /// <param name="sendDocumentParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<Message> SendDocumentAsync(SendDocumentParameters sendDocumentParameters,
                                                       CancellationToken cancellationToken = default)
        {
            return SendDocumentAsync(sendDocumentParameters.ChatId, sendDocumentParameters.Document,
                sendDocumentParameters.Caption, sendDocumentParameters.ParseMode,
                sendDocumentParameters.DisableNotification, sendDocumentParameters.ReplyToMessageId,
                sendDocumentParameters.ReplyMarkup, thumb: sendDocumentParameters.Thumb,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        ///     Use this method to send .webp stickers. On success, the sent Description is returned.
        /// </summary>
        /// <param name="sendStickerParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<Message> SendStickerAsync(SendStickerParameters sendStickerParameters,
                                                      CancellationToken cancellationToken = default)
        {
            return SendStickerAsync(sendStickerParameters.ChatId, sendStickerParameters.Sticker,
                sendStickerParameters.DisableNotification, sendStickerParameters.ReplyToMessageId,
                sendStickerParameters.ReplyMarkup, cancellationToken);
        }

        /// <summary>
        ///     Use this method to send video files, Telegram clients support mp4 videos (other formats may be sent as
        ///     Document). On success, the sent Description is returned. Bots can send video files of up to 50 MB in size.
        /// </summary>
        /// <param name="sendVideoParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<Message> SendVideoAsync(SendVideoParameters sendVideoParameters,
                                                    CancellationToken cancellationToken = default)
        {
            return SendVideoAsync(sendVideoParameters.ChatId, sendVideoParameters.Video, sendVideoParameters.Duration,
                sendVideoParameters.Width, sendVideoParameters.Height, sendVideoParameters.Caption,
                sendVideoParameters.ParseMode, sendVideoParameters.SupportsStreaming,
                sendVideoParameters.DisableNotification, sendVideoParameters.ReplyToMessageId,
                sendVideoParameters.ReplyMarkup, thumb: sendVideoParameters.Thumb,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        ///     Use this method to send animation files (GIF or H.264/MPEG-4 AVC video without sound). On success, the sent
        ///     Message is returned. Bots can currently send animation files of up to 50 MB in size, this limit may be
        ///     changed in the future.
        /// </summary>
        /// <param name="sendAnimationParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<Message> SendAnimationAsync(SendAnimationParameters sendAnimationParameters,
                                                        CancellationToken cancellationToken = default)
        {
            return SendAnimationAsync(sendAnimationParameters.ChatId, sendAnimationParameters.Animation,
                sendAnimationParameters.Duration, sendAnimationParameters.Width, sendAnimationParameters.Height,
                sendAnimationParameters.Thumb, sendAnimationParameters.Caption, sendAnimationParameters.ParseMode,
                sendAnimationParameters.DisableNotification, sendAnimationParameters.ReplyToMessageId,
                sendAnimationParameters.ReplyMarkup, cancellationToken);
        }

        /// <summary>
        ///     Use this method to send audio files, if you want Telegram clients to display the file as a playable voice message.
        ///     For this to work, your audio must be in an .ogg file encoded with OPUS (other formats may be sent as Audio or
        ///     Document). On success, the sent Description is returned. Bots can currently send voice messages of up to 50 MB in
        ///     size, this limit may be changed in the future.
        /// </summary>
        /// <param name="sendVoiceParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<Message> SendVoiceAsync(SendVoiceParameters sendVoiceParameters,
                                                    CancellationToken cancellationToken = default)
        {
            return SendVoiceAsync(sendVoiceParameters.ChatId, sendVoiceParameters.Voice, sendVoiceParameters.Caption,
                sendVoiceParameters.ParseMode, sendVoiceParameters.Duration, sendVoiceParameters.DisableNotification,
                sendVoiceParameters.ReplyToMessageId, sendVoiceParameters.ReplyMarkup, cancellationToken);
        }

        /// <summary>
        ///     As of v.4.0, Telegram clients support rounded square mp4 videos of up to 1 minute long. Use this method to
        ///     send video messages.
        /// </summary>
        /// <param name="sendVideoNoteParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<Message> SendVideoNoteAsync(SendVideoNoteParameters sendVideoNoteParameters,
                                                        CancellationToken cancellationToken = default)
        {
            return SendVideoNoteAsync(sendVideoNoteParameters.ChatId, sendVideoNoteParameters.VideoNote,
                sendVideoNoteParameters.Duration, sendVideoNoteParameters.Length,
                sendVideoNoteParameters.DisableNotification, sendVideoNoteParameters.ReplyToMessageId,
                sendVideoNoteParameters.ReplyMarkup, thumb: sendVideoNoteParameters.Thumb,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        ///     Use this method to send a group of photos or videos as an album. On success, an array of the sent Messages is
        ///     returned.
        /// </summary>
        /// <param name="sendMediaGroupParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<Message[]> SendMediaGroupAsync(SendMediaGroupParameters sendMediaGroupParameters,
                                                           CancellationToken cancellationToken = default)
        {
            return SendMediaGroupAsync(sendMediaGroupParameters.ChatId, sendMediaGroupParameters.Media,
                sendMediaGroupParameters.DisableNotification, sendMediaGroupParameters.ReplyToMessageId,
                cancellationToken);
        }

        /// <summary>
        ///     Use this method to send a group of photos or videos as an album. On success, an array of the sent Messages is
        ///     returned.
        /// </summary>
        /// <param name="sendMediaGroupExtendedParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<Message[]> SendMediaGroupAsync(
            SendMediaGroupExtendedParameters sendMediaGroupExtendedParameters,
            CancellationToken cancellationToken = default)
        {
            return SendMediaGroupAsync(sendMediaGroupExtendedParameters.InputMedia,
                sendMediaGroupExtendedParameters.ChatId, sendMediaGroupExtendedParameters.DisableNotification,
                sendMediaGroupExtendedParameters.ReplyToMessageId, cancellationToken);
        }

        /// <summary>
        ///     Use this method to send point on the map. On success, the sent Description is returned.
        /// </summary>
        /// <param name="sendLocationParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<Message> SendLocationAsync(SendLocationParameters sendLocationParameters,
                                                       CancellationToken cancellationToken = default)
        {
            return SendLocationAsync(sendLocationParameters.ChatId, sendLocationParameters.Latitude,
                sendLocationParameters.Longitude, sendLocationParameters.LivePeriod,
                sendLocationParameters.DisableNotification, sendLocationParameters.ReplyToMessageId,
                sendLocationParameters.ReplyMarkup, cancellationToken);
        }

        /// <summary>
        ///     Use this method to send information about a venue.
        /// </summary>
        /// <param name="sendVenueParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<Message> SendVenueAsync(SendVenueParameters sendVenueParameters,
                                                    CancellationToken cancellationToken = default)
        {
            return SendVenueAsync(sendVenueParameters.ChatId, sendVenueParameters.Latitude,
                sendVenueParameters.Longitude, sendVenueParameters.Title, sendVenueParameters.Address,
                sendVenueParameters.FoursquareId, sendVenueParameters.DisableNotification,
                sendVenueParameters.ReplyToMessageId, sendVenueParameters.ReplyMarkup,
                foursquareType: sendVenueParameters.FoursquareType, cancellationToken: cancellationToken);
        }

        /// <summary>
        ///     Use this method to send phone contacts.
        /// </summary>
        /// <param name="sendContactParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<Message> SendContactAsync(SendContactParameters sendContactParameters,
                                                      CancellationToken cancellationToken = default)
        {
            return SendContactAsync(sendContactParameters.ChatId, sendContactParameters.PhoneNumber,
                sendContactParameters.FirstName, sendContactParameters.LastName,
                sendContactParameters.DisableNotification, sendContactParameters.ReplyToMessageId,
                sendContactParameters.ReplyMarkup, vCard: sendContactParameters.VCard,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        ///     Use this method to send a native poll. A native poll can't be sent to a private chat. On success, the sent
        ///     <see cref="Message" /> is returned.
        /// </summary>
        /// <param name="sendPollParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<Message> SendPollAsync(SendPollParameters sendPollParameters,
                                                   CancellationToken cancellationToken = default)
        {
            return SendPollAsync(sendPollParameters.ChatId, sendPollParameters.Question, sendPollParameters.Options,
                sendPollParameters.DisableNotification, sendPollParameters.ReplyToMessageId,
                sendPollParameters.ReplyMarkup, isAnonymous: sendPollParameters.IsAnonymous,
                type: sendPollParameters.Type, allowsMultipleAnswers: sendPollParameters.AllowsMultipleAnswers,
                correctOptionId: sendPollParameters.CorrectOptionId, isClosed: sendPollParameters.IsClosed,
                explanation: sendPollParameters.Explanation,
                explanationParseMode: sendPollParameters.ExplanationParseMode,
                openPeriod: sendPollParameters.OpenPeriod, closeDate: sendPollParameters.CloseDate,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        ///     Use this request to send a dice, which will have a random value from 1 to 6. On success, the sent
        ///     <see cref="Message" /> is returned
        /// </summary>
        /// <param name="sendDiceParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<Message> SendDiceAsync(SendDiceParameters sendDiceParameters,
                                                   CancellationToken cancellationToken = default)
        {
            return SendDiceAsync(sendDiceParameters.ChatId, sendDiceParameters.DisableNotification,
                sendDiceParameters.ReplyToMessageId, sendDiceParameters.ReplyMarkup, emoji: sendDiceParameters.Emoji,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        ///     Use this method when you need to tell the user that something is happening on the bot's side. The status is set for
        ///     5 seconds or less (when a message arrives from your bot, Telegram clients clear its typing status).
        /// </summary>
        /// <param name="sendChatActionParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task SendChatActionAsync(SendChatActionParameters sendChatActionParameters,
                                                CancellationToken cancellationToken = default)
        {
            return SendChatActionAsync(sendChatActionParameters.ChatId, sendChatActionParameters.ChatAction,
                cancellationToken);
        }

        /// <summary>
        ///     Use this method to get a list of profile pictures for a user. Returns a UserProfilePhotos object.
        /// </summary>
        /// <param name="getUserProfilePhotosParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<UserProfilePhotos> GetUserProfilePhotosAsync(
            GetUserProfilePhotosParameters getUserProfilePhotosParameters,
            CancellationToken cancellationToken = default)
        {
            return GetUserProfilePhotosAsync(getUserProfilePhotosParameters.UserId,
                getUserProfilePhotosParameters.Offset, getUserProfilePhotosParameters.Limit, cancellationToken);
        }

        /// <summary>
        ///     Use this method to get information about a file. For the moment, bots can download files of up to 20MB in size.
        /// </summary>
        /// <param name="getFileParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<File> GetFileAsync(GetFileParameters getFileParameters,
                                               CancellationToken cancellationToken = default)
        {
            return GetFileAsync(getFileParameters.FileId, cancellationToken);
        }

        /// <summary>
        ///     Use this method to download a file.
        /// </summary>
        /// <param name="downloadFileParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<Stream> DownloadFileAsync(DownloadFileParameters downloadFileParameters,
                                                      CancellationToken cancellationToken = default)
        {
            return DownloadFileAsync(downloadFileParameters.FilePath, cancellationToken);
        }

        /// <summary>
        ///     Use this method to download a file.
        /// </summary>
        /// <param name="downloadFileExtendedParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task DownloadFileAsync(DownloadFileExtendedParameters downloadFileExtendedParameters,
                                              CancellationToken cancellationToken = default)
        {
            return DownloadFileAsync(downloadFileExtendedParameters.FilePath,
                downloadFileExtendedParameters.Destination, cancellationToken);
        }

        /// <summary>
        ///     Use this method to get basic info about a file and download it.
        /// </summary>
        /// <param name="getInfoAndDownloadFileParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<File> GetInfoAndDownloadFileAsync(
            GetInfoAndDownloadFileParameters getInfoAndDownloadFileParameters,
            CancellationToken cancellationToken = default)
        {
            return GetInfoAndDownloadFileAsync(getInfoAndDownloadFileParameters.FileId,
                getInfoAndDownloadFileParameters.Destination, cancellationToken);
        }

        /// <summary>
        ///     Use this method to kick a user from a group or a supergroup. In the case of supergroups, the user will not be able
        ///     to return to the group on their own using invite links, etc., unless unbanned first. The bot must be an
        ///     administrator in the group for this to work.
        /// </summary>
        /// <param name="kickChatMemberParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task KickChatMemberAsync(KickChatMemberParameters kickChatMemberParameters,
                                                CancellationToken cancellationToken = default)
        {
            return KickChatMemberAsync(kickChatMemberParameters.ChatId, kickChatMemberParameters.UserId,
                kickChatMemberParameters.UntilDate, cancellationToken);
        }

        /// <summary>
        ///     Use this method for your bot to leave a group, supergroup or channel.
        /// </summary>
        /// <param name="leaveChatParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task LeaveChatAsync(LeaveChatParameters leaveChatParameters,
                                           CancellationToken cancellationToken = default)
        {
            return LeaveChatAsync(leaveChatParameters.ChatId, cancellationToken);
        }

        /// <summary>
        ///     Use this method to unban a previously kicked user in a supergroup. The user will not return to the group
        ///     automatically, but will be able to join via link, etc. The bot must be an administrator in the group for this to
        ///     work.
        /// </summary>
        /// <param name="unbanChatMemberParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task UnbanChatMemberAsync(UnbanChatMemberParameters unbanChatMemberParameters,
                                                 CancellationToken cancellationToken = default)
        {
            return UnbanChatMemberAsync(unbanChatMemberParameters.ChatId, unbanChatMemberParameters.UserId,
                cancellationToken);
        }

        /// <summary>
        ///     Use this method to get up to date information about the chat (current name of the user for one-on-one
        ///     conversations, current username of a user, group or channel, etc.).
        /// </summary>
        /// <param name="getChatParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<Chat> GetChatAsync(GetChatParameters getChatParameters,
                                               CancellationToken cancellationToken = default)
        {
            return GetChatAsync(getChatParameters.ChatId, cancellationToken);
        }

        /// <summary>
        ///     Use this method to get a list of administrators in a chat.
        /// </summary>
        /// <param name="getChatAdministratorsParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<ChatMember[]> GetChatAdministratorsAsync(
            GetChatAdministratorsParameters getChatAdministratorsParameters,
            CancellationToken cancellationToken = default)
        {
            return GetChatAdministratorsAsync(getChatAdministratorsParameters.ChatId, cancellationToken);
        }

        /// <summary>
        ///     Use this method to get the number of members in a chat.
        /// </summary>
        /// <param name="getChatMembersCountParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<int> GetChatMembersCountAsync(GetChatMembersCountParameters getChatMembersCountParameters,
                                                          CancellationToken cancellationToken = default)
        {
            return GetChatMembersCountAsync(getChatMembersCountParameters.ChatId, cancellationToken);
        }

        /// <summary>
        ///     Use this method to get information about a member of a chat.
        /// </summary>
        /// <param name="getChatMemberParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<ChatMember> GetChatMemberAsync(GetChatMemberParameters getChatMemberParameters,
                                                           CancellationToken cancellationToken = default)
        {
            return GetChatMemberAsync(getChatMemberParameters.ChatId, getChatMemberParameters.UserId,
                cancellationToken);
        }

        /// <summary>
        ///     Use this method to send answers to callback queries sent from inline keyboards. The answer will be displayed to the
        ///     user as a notification at the top of the chat screen or as an alert.
        /// </summary>
        /// <param name="answerCallbackQueryParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task AnswerCallbackQueryAsync(AnswerCallbackQueryParameters answerCallbackQueryParameters,
                                                     CancellationToken cancellationToken = default)
        {
            return AnswerCallbackQueryAsync(answerCallbackQueryParameters.CallbackQueryId,
                answerCallbackQueryParameters.Text, answerCallbackQueryParameters.ShowAlert,
                answerCallbackQueryParameters.Url, answerCallbackQueryParameters.CacheTime, cancellationToken);
        }

        /// <summary>
        ///     Use this method to restrict a user in a supergroup. The bot must be an administrator in the supergroup for this to
        ///     work and must have the appropriate admin rights.
        /// </summary>
        /// <param name="restrictChatMemberParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task RestrictChatMemberAsync(RestrictChatMemberParameters restrictChatMemberParameters,
                                                    CancellationToken cancellationToken = default)
        {
            return RestrictChatMemberAsync(restrictChatMemberParameters.ChatId, restrictChatMemberParameters.UserId,
                restrictChatMemberParameters.Permissions, restrictChatMemberParameters.UntilDate, cancellationToken);
        }

        /// <summary>
        ///     Use this method to promote or demote a user in a supergroup or a channel. The bot must be an administrator in the
        ///     chat for this to work and must have the appropriate admin rights.
        /// </summary>
        /// <param name="promoteChatMemberParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task PromoteChatMemberAsync(PromoteChatMemberParameters promoteChatMemberParameters,
                                                   CancellationToken cancellationToken = default)
        {
            return PromoteChatMemberAsync(promoteChatMemberParameters.ChatId, promoteChatMemberParameters.UserId,
                promoteChatMemberParameters.CanChangeInfo, promoteChatMemberParameters.CanPostMessages,
                promoteChatMemberParameters.CanEditMessages, promoteChatMemberParameters.CanDeleteMessages,
                promoteChatMemberParameters.CanInviteUsers, promoteChatMemberParameters.CanRestrictMembers,
                promoteChatMemberParameters.CanPinMessages, promoteChatMemberParameters.CanPromoteMembers,
                cancellationToken);
        }

        /// <summary>
        ///     <inheritdoc cref="Telegram.Bot.Requests.SetChatAdministratorCustomTitleRequest" />
        /// </summary>
        /// <param name="setChatAdministratorCustomTitleParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task SetChatAdministratorCustomTitleAsync(
            SetChatAdministratorCustomTitleParameters setChatAdministratorCustomTitleParameters,
            CancellationToken cancellationToken = default)
        {
            return SetChatAdministratorCustomTitleAsync(setChatAdministratorCustomTitleParameters.ChatId,
                setChatAdministratorCustomTitleParameters.UserId, setChatAdministratorCustomTitleParameters.CustomTitle,
                cancellationToken);
        }

        /// <summary>
        ///     Use this method to set default chat permissions for all members. The bot must be an administrator in the group or a
        ///     supergroup for this to work and must have the can_restrict_members admin rights. Returns True on success.
        /// </summary>
        /// <param name="setChatPermissionsParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task SetChatPermissionsAsync(SetChatPermissionsParameters setChatPermissionsParameters,
                                                    CancellationToken cancellationToken = default)
        {
            return SetChatPermissionsAsync(setChatPermissionsParameters.ChatId,
                setChatPermissionsParameters.Permissions, cancellationToken);
        }

        /// <summary>
        ///     Use this method to change the list of the bot's commands. Returns True on success.
        /// </summary>
        /// <param name="setMyCommandsParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task SetMyCommandsAsync(SetMyCommandsParameters setMyCommandsParameters,
                                               CancellationToken cancellationToken = default)
        {
            return SetMyCommandsAsync(setMyCommandsParameters.Commands, cancellationToken);
        }

        /// <summary>
        ///     Use this method to edit text messages sent by the bot or via the bot (for inline bots).
        /// </summary>
        /// <param name="editMessageTextParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<Message> EditMessageTextAsync(EditMessageTextParameters editMessageTextParameters,
                                                          CancellationToken cancellationToken = default)
        {
            return EditMessageTextAsync(editMessageTextParameters.ChatId, editMessageTextParameters.MessageId,
                editMessageTextParameters.Text, editMessageTextParameters.ParseMode,
                editMessageTextParameters.DisableWebPagePreview, editMessageTextParameters.ReplyMarkup,
                cancellationToken);
        }

        /// <summary>
        ///     Use this method to edit text messages sent by the bot or via the bot (for inline bots).
        /// </summary>
        /// <param name="editMessageTextInlineParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task EditMessageTextAsync(EditMessageTextInlineParameters editMessageTextInlineParameters,
                                                 CancellationToken cancellationToken = default)
        {
            return EditMessageTextAsync(editMessageTextInlineParameters.InlineMessageId,
                editMessageTextInlineParameters.Text, editMessageTextInlineParameters.ParseMode,
                editMessageTextInlineParameters.DisableWebPagePreview, editMessageTextInlineParameters.ReplyMarkup,
                cancellationToken);
        }

        /// <summary>
        ///     Use this method to stop updating a live location message sent via the bot (for inline bots) before live_period
        ///     expires.
        /// </summary>
        /// <param name="stopMessageLiveLocationParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<Message> StopMessageLiveLocationAsync(
            StopMessageLiveLocationParameters stopMessageLiveLocationParameters,
            CancellationToken cancellationToken = default)
        {
            return StopMessageLiveLocationAsync(stopMessageLiveLocationParameters.ChatId,
                stopMessageLiveLocationParameters.MessageId, stopMessageLiveLocationParameters.ReplyMarkup,
                cancellationToken);
        }

        /// <summary>
        ///     Use this method to stop updating a live location message sent via the bot (for inline bots) before live_period
        ///     expires.
        /// </summary>
        /// <param name="stopMessageLiveLocationInlineParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task StopMessageLiveLocationAsync(
            StopMessageLiveLocationInlineParameters stopMessageLiveLocationInlineParameters,
            CancellationToken cancellationToken = default)
        {
            return StopMessageLiveLocationAsync(stopMessageLiveLocationInlineParameters.InlineMessageId,
                stopMessageLiveLocationInlineParameters.ReplyMarkup, cancellationToken);
        }

        /// <summary>
        ///     Use this method to edit captions of messages sent by the bot or via the bot (for inline bots).
        /// </summary>
        /// <param name="editMessageCaptionParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<Message> EditMessageCaptionAsync(EditMessageCaptionParameters editMessageCaptionParameters,
                                                             CancellationToken cancellationToken = default)
        {
            return EditMessageCaptionAsync(editMessageCaptionParameters.ChatId, editMessageCaptionParameters.MessageId,
                editMessageCaptionParameters.Caption, editMessageCaptionParameters.ReplyMarkup,
                parseMode: editMessageCaptionParameters.ParseMode, cancellationToken: cancellationToken);
        }

        /// <summary>
        ///     Use this method to edit captions of messages sent by the bot or via the bot (for inline bots).
        /// </summary>
        /// <param name="editMessageCaptionInlineParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task EditMessageCaptionAsync(
            EditMessageCaptionInlineParameters editMessageCaptionInlineParameters,
            CancellationToken cancellationToken = default)
        {
            return EditMessageCaptionAsync(editMessageCaptionInlineParameters.InlineMessageId,
                editMessageCaptionInlineParameters.Caption, editMessageCaptionInlineParameters.ReplyMarkup,
                parseMode: editMessageCaptionInlineParameters.ParseMode, cancellationToken: cancellationToken);
        }

        /// <summary>
        ///     Use this method to edit audio, document, photo, or video inline messages.
        /// </summary>
        /// <param name="editMessageMediaParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<Message> EditMessageMediaAsync(EditMessageMediaParameters editMessageMediaParameters,
                                                           CancellationToken cancellationToken = default)
        {
            return EditMessageMediaAsync(editMessageMediaParameters.ChatId, editMessageMediaParameters.MessageId,
                editMessageMediaParameters.Media, editMessageMediaParameters.ReplyMarkup, cancellationToken);
        }

        /// <summary>
        ///     Use this method to edit audio, document, photo, or video inline messages.
        /// </summary>
        /// <param name="editMessageMediaInlineParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task EditMessageMediaAsync(EditMessageMediaInlineParameters editMessageMediaInlineParameters,
                                                  CancellationToken cancellationToken = default)
        {
            return EditMessageMediaAsync(editMessageMediaInlineParameters.InlineMessageId,
                editMessageMediaInlineParameters.Media, editMessageMediaInlineParameters.ReplyMarkup,
                cancellationToken);
        }

        /// <summary>
        ///     Use this method to edit only the reply markup of messages sent by the bot or via the bot (for inline bots).
        /// </summary>
        /// <param name="editMessageReplyMarkupParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<Message> EditMessageReplyMarkupAsync(
            EditMessageReplyMarkupParameters editMessageReplyMarkupParameters,
            CancellationToken cancellationToken = default)
        {
            return EditMessageReplyMarkupAsync(editMessageReplyMarkupParameters.ChatId,
                editMessageReplyMarkupParameters.MessageId, editMessageReplyMarkupParameters.ReplyMarkup,
                cancellationToken);
        }

        /// <summary>
        ///     Use this method to edit only the reply markup of messages sent by the bot or via the bot (for inline bots).
        /// </summary>
        /// <param name="editMessageReplyMarkupInlineParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task EditMessageReplyMarkupAsync(
            EditMessageReplyMarkupInlineParameters editMessageReplyMarkupInlineParameters,
            CancellationToken cancellationToken = default)
        {
            return EditMessageReplyMarkupAsync(editMessageReplyMarkupInlineParameters.InlineMessageId,
                editMessageReplyMarkupInlineParameters.ReplyMarkup, cancellationToken);
        }

        /// <summary>
        ///     Use this method to edit live location messages sent via the bot (for inline bots).
        /// </summary>
        /// <param name="editMessageLiveLocationParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<Message> EditMessageLiveLocationAsync(
            EditMessageLiveLocationParameters editMessageLiveLocationParameters,
            CancellationToken cancellationToken = default)
        {
            return EditMessageLiveLocationAsync(editMessageLiveLocationParameters.ChatId,
                editMessageLiveLocationParameters.MessageId, editMessageLiveLocationParameters.Latitude,
                editMessageLiveLocationParameters.Longitude, editMessageLiveLocationParameters.ReplyMarkup,
                cancellationToken);
        }

        /// <summary>
        ///     Use this method to edit live location messages sent via the bot (for inline bots).
        /// </summary>
        /// <param name="editMessageLiveLocationInlineParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task EditMessageLiveLocationAsync(
            EditMessageLiveLocationInlineParameters editMessageLiveLocationInlineParameters,
            CancellationToken cancellationToken = default)
        {
            return EditMessageLiveLocationAsync(editMessageLiveLocationInlineParameters.InlineMessageId,
                editMessageLiveLocationInlineParameters.Latitude, editMessageLiveLocationInlineParameters.Longitude,
                editMessageLiveLocationInlineParameters.ReplyMarkup, cancellationToken);
        }

        /// <summary>
        ///     Use this method to send a native poll. A native poll can't be sent to a private chat. On success, the sent
        ///     <see cref="Message" /> is returned.
        /// </summary>
        /// <param name="stopPollParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<Poll> StopPollAsync(StopPollParameters stopPollParameters,
                                                CancellationToken cancellationToken = default)
        {
            return StopPollAsync(stopPollParameters.ChatId, stopPollParameters.MessageId,
                stopPollParameters.ReplyMarkup, cancellationToken);
        }

        /// <summary>
        ///     Use this method to delete a message. A message can only be deleted if it was sent less than 48 hours ago. Any such
        ///     recently sent outgoing message may be deleted. Additionally, if the bot is an administrator in a group chat, it can
        ///     delete any message. If the bot is an administrator in a supergroup, it can delete messages from any other user and
        ///     service messages about people joining or leaving the group (other types of service messages may only be removed by
        ///     the group creator). In channels, bots can only remove their own messages.
        /// </summary>
        /// <param name="deleteMessageParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task DeleteMessageAsync(DeleteMessageParameters deleteMessageParameters,
                                               CancellationToken cancellationToken = default)
        {
            return DeleteMessageAsync(deleteMessageParameters.ChatId, deleteMessageParameters.MessageId,
                cancellationToken);
        }

        /// <summary>
        ///     Use this method to send answers to an inline query.
        /// </summary>
        /// <param name="answerInlineQueryParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task AnswerInlineQueryAsync(AnswerInlineQueryParameters answerInlineQueryParameters,
                                                   CancellationToken cancellationToken = default)
        {
            return AnswerInlineQueryAsync(answerInlineQueryParameters.InlineQueryId,
                answerInlineQueryParameters.Results, answerInlineQueryParameters.CacheTime,
                answerInlineQueryParameters.IsPersonal, answerInlineQueryParameters.NextOffset,
                answerInlineQueryParameters.SwitchPmText, answerInlineQueryParameters.SwitchPmParameter,
                cancellationToken);
        }

        /// <summary>
        ///     Use this method to send invoices.
        /// </summary>
        /// <param name="sendInvoiceParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<Message> SendInvoiceAsync(SendInvoiceParameters sendInvoiceParameters,
                                                      CancellationToken cancellationToken = default)
        {
            return SendInvoiceAsync(sendInvoiceParameters.ChatId, sendInvoiceParameters.Title,
                sendInvoiceParameters.Description, sendInvoiceParameters.Payload, sendInvoiceParameters.ProviderToken,
                sendInvoiceParameters.StartParameter, sendInvoiceParameters.Currency, sendInvoiceParameters.Prices,
                sendInvoiceParameters.ProviderData, sendInvoiceParameters.PhotoUrl, sendInvoiceParameters.PhotoSize,
                sendInvoiceParameters.PhotoWidth, sendInvoiceParameters.PhotoHeight, sendInvoiceParameters.NeedName,
                sendInvoiceParameters.NeedPhoneNumber, sendInvoiceParameters.NeedEmail,
                sendInvoiceParameters.NeedShippingAddress, sendInvoiceParameters.IsFlexible,
                sendInvoiceParameters.DisableNotification, sendInvoiceParameters.ReplyToMessageId,
                sendInvoiceParameters.ReplyMarkup,
                sendPhoneNumberToProvider: sendInvoiceParameters.SendPhoneNumberToProvider,
                sendEmailToProvider: sendInvoiceParameters.SendEmailToProvider, cancellationToken: cancellationToken);
        }

        /// <summary>
        ///     Use this method to reply to shipping queries with failure and error message. If you sent an invoice requesting a
        ///     shipping address and the parameter is_flexible was specified, the Bot API will send an Update with a shipping_query
        ///     field to the bot.
        /// </summary>
        /// <param name="answerShippingQueryParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task AnswerShippingQueryAsync(AnswerShippingQueryParameters answerShippingQueryParameters,
                                                     CancellationToken cancellationToken = default)
        {
            return AnswerShippingQueryAsync(answerShippingQueryParameters.ShippingQueryId,
                answerShippingQueryParameters.ShippingOptions, cancellationToken);
        }

        /// <summary>
        ///     Use this method to reply to shipping queries with failure and error message. If you sent an invoice requesting a
        ///     shipping address and the parameter is_flexible was specified, the Bot API will send an Update with a shipping_query
        ///     field to the bot.
        /// </summary>
        /// <param name="answerShippingQueryExtendedParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task AnswerShippingQueryAsync(
            AnswerShippingQueryExtendedParameters answerShippingQueryExtendedParameters,
            CancellationToken cancellationToken = default)
        {
            return AnswerShippingQueryAsync(answerShippingQueryExtendedParameters.ShippingQueryId,
                answerShippingQueryExtendedParameters.ErrorMessage, cancellationToken);
        }

        /// <summary>
        ///     Respond to a pre-checkout query with failure and error message
        /// </summary>
        /// <param name="answerPreCheckoutQueryParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task AnswerPreCheckoutQueryAsync(
            AnswerPreCheckoutQueryParameters answerPreCheckoutQueryParameters,
            CancellationToken cancellationToken = default)
        {
            return AnswerPreCheckoutQueryAsync(answerPreCheckoutQueryParameters.PreCheckoutQueryId, cancellationToken);
        }

        /// <summary>
        ///     Respond to a pre-checkout query with failure and error message
        /// </summary>
        /// <param name="answerPreCheckoutQueryExtendedParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task AnswerPreCheckoutQueryAsync(
            AnswerPreCheckoutQueryExtendedParameters answerPreCheckoutQueryExtendedParameters,
            CancellationToken cancellationToken = default)
        {
            return AnswerPreCheckoutQueryAsync(answerPreCheckoutQueryExtendedParameters.PreCheckoutQueryId,
                answerPreCheckoutQueryExtendedParameters.ErrorMessage, cancellationToken);
        }

        /// <summary>
        ///     Use this method to send a game.
        /// </summary>
        /// <param name="sendGameParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<Message> SendGameAsync(SendGameParameters sendGameParameters,
                                                   CancellationToken cancellationToken = default)
        {
            return SendGameAsync(sendGameParameters.ChatId, sendGameParameters.GameShortName,
                sendGameParameters.DisableNotification, sendGameParameters.ReplyToMessageId,
                sendGameParameters.ReplyMarkup, cancellationToken);
        }

        /// <summary>
        ///     Use this method to set the score of the specified user in a game.
        /// </summary>
        /// <param name="setGameScoreParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<Message> SetGameScoreAsync(SetGameScoreParameters setGameScoreParameters,
                                                       CancellationToken cancellationToken = default)
        {
            return SetGameScoreAsync(setGameScoreParameters.UserId, setGameScoreParameters.Score,
                setGameScoreParameters.ChatId, setGameScoreParameters.MessageId, setGameScoreParameters.Force,
                setGameScoreParameters.DisableEditMessage, cancellationToken);
        }

        /// <summary>
        ///     Use this method to set the score of the specified user in a game.
        /// </summary>
        /// <param name="setGameScoreInlineParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task SetGameScoreAsync(SetGameScoreInlineParameters setGameScoreInlineParameters,
                                              CancellationToken cancellationToken = default)
        {
            return SetGameScoreAsync(setGameScoreInlineParameters.UserId, setGameScoreInlineParameters.Score,
                setGameScoreInlineParameters.InlineMessageId, setGameScoreInlineParameters.Force,
                setGameScoreInlineParameters.DisableEditMessage, cancellationToken);
        }

        /// <summary>
        ///     Use this method to get data for high score tables.
        /// </summary>
        /// <param name="getGameHighScoresParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<GameHighScore[]> GetGameHighScoresAsync(
            GetGameHighScoresParameters getGameHighScoresParameters, CancellationToken cancellationToken = default)
        {
            return GetGameHighScoresAsync(getGameHighScoresParameters.UserId, getGameHighScoresParameters.ChatId,
                getGameHighScoresParameters.MessageId, cancellationToken);
        }

        /// <summary>
        ///     Use this method to get data for high score tables.
        /// </summary>
        /// <param name="getGameHighScoresInlineParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<GameHighScore[]> GetGameHighScoresAsync(
            GetGameHighScoresInlineParameters getGameHighScoresInlineParameters,
            CancellationToken cancellationToken = default)
        {
            return GetGameHighScoresAsync(getGameHighScoresInlineParameters.UserId,
                getGameHighScoresInlineParameters.InlineMessageId, cancellationToken);
        }

        /// <summary>
        ///     Use this method to get a sticker set.
        /// </summary>
        /// <param name="getStickerSetParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<StickerSet> GetStickerSetAsync(GetStickerSetParameters getStickerSetParameters,
                                                           CancellationToken cancellationToken = default)
        {
            return GetStickerSetAsync(getStickerSetParameters.Name, cancellationToken);
        }

        /// <summary>
        ///     Use this method to upload a .png file with a sticker for later use in createNewStickerSet and addStickerToSet
        ///     methods (can be used multiple times).
        /// </summary>
        /// <param name="uploadStickerFileParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<File> UploadStickerFileAsync(UploadStickerFileParameters uploadStickerFileParameters,
                                                         CancellationToken cancellationToken = default)
        {
            return UploadStickerFileAsync(uploadStickerFileParameters.UserId, uploadStickerFileParameters.PngSticker,
                cancellationToken);
        }

        /// <summary>
        ///     Use this method to create new sticker set owned by a user. The bot will be able to edit the created sticker set.
        /// </summary>
        /// <param name="createNewStickerSetParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task CreateNewStickerSetAsync(CreateNewStickerSetParameters createNewStickerSetParameters,
                                                     CancellationToken cancellationToken = default)
        {
            return CreateNewStickerSetAsync(createNewStickerSetParameters.UserId, createNewStickerSetParameters.Name,
                createNewStickerSetParameters.Title, createNewStickerSetParameters.PngSticker,
                createNewStickerSetParameters.Emojis, createNewStickerSetParameters.IsMasks,
                createNewStickerSetParameters.MaskPosition, cancellationToken);
        }

        /// <summary>
        ///     Use this method to add a new sticker to a set created by the bot.
        /// </summary>
        /// <param name="addStickerToSetParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task AddStickerToSetAsync(AddStickerToSetParameters addStickerToSetParameters,
                                                 CancellationToken cancellationToken = default)
        {
            return AddStickerToSetAsync(addStickerToSetParameters.UserId, addStickerToSetParameters.Name,
                addStickerToSetParameters.PngSticker, addStickerToSetParameters.Emojis,
                addStickerToSetParameters.MaskPosition, cancellationToken);
        }

        /// <summary>
        ///     Use this method to create new sticker set owned by a user. The bot will be able to edit the created sticker set.
        /// </summary>
        /// <param name="createNewAnimatedStickerSetParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task CreateNewAnimatedStickerSetAsync(
            CreateNewAnimatedStickerSetParameters createNewAnimatedStickerSetParameters,
            CancellationToken cancellationToken = default)
        {
            return CreateNewAnimatedStickerSetAsync(createNewAnimatedStickerSetParameters.UserId,
                createNewAnimatedStickerSetParameters.Name, createNewAnimatedStickerSetParameters.Title,
                createNewAnimatedStickerSetParameters.TgsSticker, createNewAnimatedStickerSetParameters.Emojis,
                createNewAnimatedStickerSetParameters.IsMasks, createNewAnimatedStickerSetParameters.MaskPosition,
                cancellationToken);
        }

        /// <summary>
        ///     Use this method to add a new sticker to a set created by the bot.
        /// </summary>
        /// <param name="addAnimatedStickerToSetParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task AddAnimatedStickerToSetAsync(
            AddAnimatedStickerToSetParameters addAnimatedStickerToSetParameters,
            CancellationToken cancellationToken = default)
        {
            return AddAnimatedStickerToSetAsync(addAnimatedStickerToSetParameters.UserId,
                addAnimatedStickerToSetParameters.Name, addAnimatedStickerToSetParameters.TgsSticker,
                addAnimatedStickerToSetParameters.Emojis, addAnimatedStickerToSetParameters.MaskPosition,
                cancellationToken);
        }

        /// <summary>
        ///     Use this method to move a sticker in a set created by the bot to a specific position.
        /// </summary>
        /// <param name="setStickerPositionInSetParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task SetStickerPositionInSetAsync(
            SetStickerPositionInSetParameters setStickerPositionInSetParameters,
            CancellationToken cancellationToken = default)
        {
            return SetStickerPositionInSetAsync(setStickerPositionInSetParameters.Sticker,
                setStickerPositionInSetParameters.Position, cancellationToken);
        }

        /// <summary>
        ///     Use this method to delete a sticker from a set created by the bot.
        /// </summary>
        /// <param name="deleteStickerFromSetParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task DeleteStickerFromSetAsync(DeleteStickerFromSetParameters deleteStickerFromSetParameters,
                                                      CancellationToken cancellationToken = default)
        {
            return DeleteStickerFromSetAsync(deleteStickerFromSetParameters.Sticker, cancellationToken);
        }

        /// <summary>
        ///     Use this method to set the thumbnail of a sticker set. Animated thumbnails can be set for animated sticker sets
        ///     only. Returns True on success.
        /// </summary>
        /// <param name="setStickerSetThumbParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task SetStickerSetThumbAsync(SetStickerSetThumbParameters setStickerSetThumbParameters,
                                                    CancellationToken cancellationToken = default)
        {
            return SetStickerSetThumbAsync(setStickerSetThumbParameters.Name, setStickerSetThumbParameters.UserId,
                setStickerSetThumbParameters.Thumb, cancellationToken);
        }

        /// <summary>
        ///     Use this method to export an invite link to a supergroup or a channel. The bot must be an administrator in the chat
        ///     for this to work and must have the appropriate admin rights.
        /// </summary>
        /// <param name="exportChatInviteLinkParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task<string> ExportChatInviteLinkAsync(
            ExportChatInviteLinkParameters exportChatInviteLinkParameters,
            CancellationToken cancellationToken = default)
        {
            return ExportChatInviteLinkAsync(exportChatInviteLinkParameters.ChatId, cancellationToken);
        }

        /// <summary>
        ///     Use this method to set a new profile photo for the chat. Photos can't be changed for private chats. The bot must be
        ///     an administrator in the chat for this to work and must have the appropriate admin rights.
        /// </summary>
        /// <param name="setChatPhotoParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task SetChatPhotoAsync(SetChatPhotoParameters setChatPhotoParameters,
                                              CancellationToken cancellationToken = default)
        {
            return SetChatPhotoAsync(setChatPhotoParameters.ChatId, setChatPhotoParameters.Photo, cancellationToken);
        }

        /// <summary>
        ///     Use this method to delete a chat photo. Photos can't be changed for private chats. The bot must be an administrator
        ///     in the chat for this to work and must have the appropriate admin rights.
        /// </summary>
        /// <param name="deleteChatPhotoParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task DeleteChatPhotoAsync(DeleteChatPhotoParameters deleteChatPhotoParameters,
                                                 CancellationToken cancellationToken = default)
        {
            return DeleteChatPhotoAsync(deleteChatPhotoParameters.ChatId, cancellationToken);
        }

        /// <summary>
        ///     Use this method to change the title of a chat. Titles can't be changed for private chats. The bot must be an
        ///     administrator in the chat for this to work and must have the appropriate admin rights.
        /// </summary>
        /// <param name="setChatTitleParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task SetChatTitleAsync(SetChatTitleParameters setChatTitleParameters,
                                              CancellationToken cancellationToken = default)
        {
            return SetChatTitleAsync(setChatTitleParameters.ChatId, setChatTitleParameters.Title, cancellationToken);
        }

        /// <summary>
        ///     Use this method to change the description of a supergroup or a channel. The bot must be an administrator in the
        ///     chat for this to work and must have the appropriate admin rights.
        /// </summary>
        /// <param name="setChatDescriptionParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task SetChatDescriptionAsync(SetChatDescriptionParameters setChatDescriptionParameters,
                                                    CancellationToken cancellationToken = default)
        {
            return SetChatDescriptionAsync(setChatDescriptionParameters.ChatId,
                setChatDescriptionParameters.Description, cancellationToken);
        }

        /// <summary>
        ///     Use this method to pin a message in a supergroup. The bot must be an administrator in the chat for this to work and
        ///     must have the appropriate admin rights.
        /// </summary>
        /// <param name="pinChatMessageParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task PinChatMessageAsync(PinChatMessageParameters pinChatMessageParameters,
                                                CancellationToken cancellationToken = default)
        {
            return PinChatMessageAsync(pinChatMessageParameters.ChatId, pinChatMessageParameters.MessageId,
                pinChatMessageParameters.DisableNotification, cancellationToken);
        }

        /// <summary>
        ///     Use this method to unpin a message in a supergroup chat. The bot must be an administrator in the chat for this to
        ///     work and must have the appropriate admin rights.
        /// </summary>
        /// <param name="unpinChatMessageParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task UnpinChatMessageAsync(UnpinChatMessageParameters unpinChatMessageParameters,
                                                  CancellationToken cancellationToken = default)
        {
            return UnpinChatMessageAsync(unpinChatMessageParameters.ChatId, cancellationToken);
        }

        /// <summary>
        ///     Use this method to set a new group sticker set for a supergroup.
        /// </summary>
        /// <param name="setChatStickerSetParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task SetChatStickerSetAsync(SetChatStickerSetParameters setChatStickerSetParameters,
                                                   CancellationToken cancellationToken = default)
        {
            return SetChatStickerSetAsync(setChatStickerSetParameters.ChatId,
                setChatStickerSetParameters.StickerSetName, cancellationToken);
        }

        /// <summary>
        ///     Use this method to delete a group sticker set from a supergroup.
        /// </summary>
        /// <param name="deleteChatStickerSetParameters">Request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public virtual Task DeleteChatStickerSetAsync(DeleteChatStickerSetParameters deleteChatStickerSetParameters,
                                                      CancellationToken cancellationToken = default)
        {
            return DeleteChatStickerSetAsync(deleteChatStickerSetParameters.ChatId, cancellationToken);
        }

        #endregion

        #region Updating messages

        /// <inheritdoc />
        public Task<Message> EditMessageTextAsync(
            ChatId chatId,
            int messageId,
            string text,
            ParseMode parseMode = default,
            bool disableWebPagePreview = default,
            InlineKeyboardMarkup replyMarkup = default,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new EditMessageTextRequest(chatId, messageId, text)
            {
                ParseMode = parseMode,
                DisableWebPagePreview = disableWebPagePreview,
                ReplyMarkup = replyMarkup
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task EditMessageTextAsync(
            string inlineMessageId,
            string text,
            ParseMode parseMode = default,
            bool disableWebPagePreview = default,
            InlineKeyboardMarkup replyMarkup = default,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new EditInlineMessageTextRequest(inlineMessageId, text)
            {
                DisableWebPagePreview = disableWebPagePreview,
                ReplyMarkup = replyMarkup,
                ParseMode = parseMode
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<Message> EditMessageCaptionAsync(
            ChatId chatId,
            int messageId,
            string caption,
            InlineKeyboardMarkup replyMarkup = default,
            CancellationToken cancellationToken = default,
            ParseMode parseMode = default
        )
        {
            return MakeRequestAsync(new EditMessageCaptionRequest(chatId, messageId, caption)
            {
                ParseMode = parseMode,
                ReplyMarkup = replyMarkup
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task EditMessageCaptionAsync(
            string inlineMessageId,
            string caption,
            InlineKeyboardMarkup replyMarkup = default,
            CancellationToken cancellationToken = default,
            ParseMode parseMode = default
        )
        {
            return MakeRequestAsync(new EditInlineMessageCaptionRequest(inlineMessageId, caption)
            {
                ParseMode = parseMode,
                ReplyMarkup = replyMarkup
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<Message> EditMessageMediaAsync(
            ChatId chatId,
            int messageId,
            InputMediaBase media,
            InlineKeyboardMarkup replyMarkup = default,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new EditMessageMediaRequest(chatId, messageId, media)
            {
                ReplyMarkup = replyMarkup
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task EditMessageMediaAsync(
            string inlineMessageId,
            InputMediaBase media,
            InlineKeyboardMarkup replyMarkup = default,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new EditInlineMessageMediaRequest(inlineMessageId, media)
            {
                ReplyMarkup = replyMarkup
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<Message> EditMessageReplyMarkupAsync(
            ChatId chatId,
            int messageId,
            InlineKeyboardMarkup replyMarkup = default,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(
                new EditMessageReplyMarkupRequest(chatId, messageId, replyMarkup),
                cancellationToken);
        }

        /// <inheritdoc />
        public Task EditMessageReplyMarkupAsync(
            string inlineMessageId,
            InlineKeyboardMarkup replyMarkup = default,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(
                new EditInlineMessageReplyMarkupRequest(inlineMessageId, replyMarkup),
                cancellationToken);
        }

        /// <inheritdoc />
        public Task<Message> EditMessageLiveLocationAsync(
            ChatId chatId,
            int messageId,
            float latitude,
            float longitude,
            InlineKeyboardMarkup replyMarkup = default,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new EditMessageLiveLocationRequest(chatId, messageId, latitude, longitude)
            {
                ReplyMarkup = replyMarkup
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task EditMessageLiveLocationAsync(
            string inlineMessageId,
            float latitude,
            float longitude,
            InlineKeyboardMarkup replyMarkup = default,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new EditInlineMessageLiveLocationRequest(inlineMessageId, latitude, longitude)
            {
                ReplyMarkup = replyMarkup
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<Poll> StopPollAsync(
            ChatId chatId,
            int messageId,
            InlineKeyboardMarkup replyMarkup = default,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new StopPollRequest(chatId, messageId)
            {
                ReplyMarkup = replyMarkup
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task DeleteMessageAsync(
            ChatId chatId,
            int messageId,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new DeleteMessageRequest(chatId, messageId), cancellationToken);
        }

        #endregion Updating messages

        #region Payments

        /// <inheritdoc />
        public Task<Message> SendInvoiceAsync(
            int chatId,
            string title,
            string description,
            string payload,
            string providerToken,
            string startParameter,
            string currency,
            IEnumerable<LabeledPrice> prices,
            string providerData = default,
            string photoUrl = default,
            int photoSize = default,
            int photoWidth = default,
            int photoHeight = default,
            bool needName = default,
            bool needPhoneNumber = default,
            bool needEmail = default,
            bool needShippingAddress = default,
            bool isFlexible = default,
            bool disableNotification = default,
            int replyToMessageId = default,
            InlineKeyboardMarkup replyMarkup = default,
            CancellationToken cancellationToken = default,
            bool sendPhoneNumberToProvider = default,
            bool sendEmailToProvider = default
        )
        {
            return MakeRequestAsync(new SendInvoiceRequest(
                chatId,
                title,
                description,
                payload,
                providerToken,
                startParameter,
                currency,
                // ReSharper disable once PossibleMultipleEnumeration
                prices
            )
            {
                ProviderData = providerData,
                PhotoUrl = photoUrl,
                PhotoSize = photoSize,
                PhotoWidth = photoWidth,
                PhotoHeight = photoHeight,
                NeedName = needName,
                NeedPhoneNumber = needPhoneNumber,
                NeedEmail = needEmail,
                NeedShippingAddress = needShippingAddress,
                SendPhoneNumberToProvider = sendPhoneNumberToProvider,
                SendEmailToProvider = sendEmailToProvider,
                IsFlexible = isFlexible,
                DisableNotification = disableNotification,
                ReplyToMessageId = replyToMessageId,
                ReplyMarkup = replyMarkup
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task AnswerShippingQueryAsync(
            string shippingQueryId,
            IEnumerable<ShippingOption> shippingOptions,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new AnswerShippingQueryRequest(shippingQueryId, shippingOptions),
                cancellationToken);
        }

        /// <inheritdoc />
        public Task AnswerShippingQueryAsync(
            string shippingQueryId,
            string errorMessage,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new AnswerShippingQueryRequest(shippingQueryId, errorMessage), cancellationToken);
        }

        /// <inheritdoc />
        public Task AnswerPreCheckoutQueryAsync(
            string preCheckoutQueryId,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new AnswerPreCheckoutQueryRequest(preCheckoutQueryId), cancellationToken);
        }

        /// <inheritdoc />
        public Task AnswerPreCheckoutQueryAsync(
            string preCheckoutQueryId,
            string errorMessage,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new AnswerPreCheckoutQueryRequest(preCheckoutQueryId, errorMessage),
                cancellationToken);
        }

        #endregion Payments

        #region Games

        /// <inheritdoc />
        public Task<Message> SendGameAsync(
            long chatId,
            string gameShortName,
            bool disableNotification = default,
            int replyToMessageId = default,
            InlineKeyboardMarkup replyMarkup = default,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new SendGameRequest(chatId, gameShortName)
            {
                DisableNotification = disableNotification,
                ReplyToMessageId = replyToMessageId,
                ReplyMarkup = replyMarkup
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<Message> SetGameScoreAsync(
            int userId,
            int score,
            long chatId,
            int messageId,
            bool force = default,
            bool disableEditMessage = default,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new SetGameScoreRequest(userId, score, chatId, messageId)
            {
                Force = force,
                DisableEditMessage = disableEditMessage
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task SetGameScoreAsync(
            int userId,
            int score,
            string inlineMessageId,
            bool force = default,
            bool disableEditMessage = default,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new SetInlineGameScoreRequest(userId, score, inlineMessageId)
            {
                Force = force,
                DisableEditMessage = disableEditMessage
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<GameHighScore[]> GetGameHighScoresAsync(
            int userId,
            long chatId,
            int messageId,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(
                new GetGameHighScoresRequest(userId, chatId, messageId),
                cancellationToken);
        }

        /// <inheritdoc />
        public Task<GameHighScore[]> GetGameHighScoresAsync(
            int userId,
            string inlineMessageId,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(
                new GetInlineGameHighScoresRequest(userId, inlineMessageId),
                cancellationToken);
        }

        #endregion Games

        #region Group and channel management

        /// <inheritdoc />
        public Task<string> ExportChatInviteLinkAsync(
            ChatId chatId,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new ExportChatInviteLinkRequest(chatId), cancellationToken);
        }

        /// <inheritdoc />
        public Task SetChatPhotoAsync(
            ChatId chatId,
            InputFileStream photo,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new SetChatPhotoRequest(chatId, photo), cancellationToken);
        }

        /// <inheritdoc />
        public Task DeleteChatPhotoAsync(
            ChatId chatId,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new DeleteChatPhotoRequest(chatId), cancellationToken);
        }

        /// <inheritdoc />
        public Task SetChatTitleAsync(
            ChatId chatId,
            string title,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new SetChatTitleRequest(chatId, title), cancellationToken);
        }

        /// <inheritdoc />
        public Task SetChatDescriptionAsync(
            ChatId chatId,
            string description = default,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new SetChatDescriptionRequest(chatId, description), cancellationToken);
        }

        /// <inheritdoc />
        public Task PinChatMessageAsync(
            ChatId chatId,
            int messageId,
            bool disableNotification = default,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new PinChatMessageRequest(chatId, messageId)
            {
                DisableNotification = disableNotification
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task UnpinChatMessageAsync(
            ChatId chatId,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new UnpinChatMessageRequest(chatId), cancellationToken);
        }

        /// <inheritdoc />
        public Task SetChatStickerSetAsync(
            ChatId chatId,
            string stickerSetName,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new SetChatStickerSetRequest(chatId, stickerSetName), cancellationToken);
        }

        /// <inheritdoc />
        public Task DeleteChatStickerSetAsync(
            ChatId chatId,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new DeleteChatStickerSetRequest(chatId), cancellationToken);
        }

        #endregion

        #region Stickers

        /// <inheritdoc />
        public Task<StickerSet> GetStickerSetAsync(
            string name,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new GetStickerSetRequest(name), cancellationToken);
        }

        /// <inheritdoc />
        public Task<File> UploadStickerFileAsync(
            int userId,
            InputFileStream pngSticker,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new UploadStickerFileRequest(userId, pngSticker), cancellationToken);
        }

        /// <inheritdoc />
        public Task CreateNewStickerSetAsync(
            int userId,
            string name,
            string title,
            InputOnlineFile pngSticker,
            string emojis,
            bool isMasks = default,
            MaskPosition maskPosition = default,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new CreateNewStickerSetRequest(userId, name, title, pngSticker, emojis)
            {
                ContainsMasks = isMasks,
                MaskPosition = maskPosition
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task AddStickerToSetAsync(
            int userId,
            string name,
            InputOnlineFile pngSticker,
            string emojis,
            MaskPosition maskPosition = default,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new AddStickerToSetRequest(userId, name, pngSticker, emojis)
            {
                MaskPosition = maskPosition
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task CreateNewAnimatedStickerSetAsync(
            int userId,
            string name,
            string title,
            InputFileStream tgsSticker,
            string emojis,
            bool isMasks = default,
            MaskPosition maskPosition = default,
            CancellationToken cancellationToken = default)
        {
            return MakeRequestAsync(
                new CreateNewAnimatedStickerSetRequest(userId, name, title, tgsSticker, emojis)
                {
                    ContainsMasks = isMasks,
                    MaskPosition = maskPosition
                },
                cancellationToken
            );
        }

        /// <inheritdoc />
        public Task AddAnimatedStickerToSetAsync(
            int userId,
            string name,
            InputFileStream tgsSticker,
            string emojis,
            MaskPosition maskPosition = default,
            CancellationToken cancellationToken = default)
        {
            return MakeRequestAsync(
                new AddAnimatedStickerToSetRequest(userId, name, tgsSticker, emojis)
                {
                    MaskPosition = maskPosition
                },
                cancellationToken
            );
        }

        /// <inheritdoc />
        public Task SetStickerPositionInSetAsync(
            string sticker,
            int position,
            CancellationToken cancellationToken = default)
        {
            return MakeRequestAsync(
                new SetStickerPositionInSetRequest(sticker, position),
                cancellationToken
            );
        }

        /// <inheritdoc />
        public Task DeleteStickerFromSetAsync(
            string sticker,
            CancellationToken cancellationToken = default
        )
        {
            return MakeRequestAsync(new DeleteStickerFromSetRequest(sticker), cancellationToken);
        }

        /// <inheritdoc />
        public Task SetStickerSetThumbAsync(
            string name,
            int userId,
            InputOnlineFile thumb = default,
            CancellationToken cancellationToken = default)
        {
            return MakeRequestAsync(
                new SetStickerSetThumbRequest(name, userId, thumb),
                cancellationToken
            );
        }

        #endregion
    }
}

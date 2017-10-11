using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Telegram.Bot.Args;
using Telegram.Bot.Converters;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.Payments;
using Telegram.Bot.Types.ReplyMarkups;

using File = Telegram.Bot.Types.File;
using Telegram.Bot.Types.Requests;

namespace Telegram.Bot
{
    /// <summary>
    /// A client to use the Telegram Bot API
    /// </summary>
    public class TelegramBotClient : ITelegramBotClient
    {
        private const string BaseUrl = "https://api.telegram.org/bot";
        private const string BaseFileUrl = "https://api.telegram.org/file/bot";

        private readonly string _token;
        private bool _invalidToken;
        private readonly HttpClient _httpClient;

        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter>
            {
                new ChatIdConverter(),
                new FileToSendConverter(),
                new InlineQueryResultTypeConverter(),
                new ParseModeConverter(),
                new PhotoSizeConverter(),
                new UnixDateTimeConverter(),
            },
        };

        #region Config Properties

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
        public bool IsReceiving { get; set; }

        private CancellationTokenSource _receivingCancellationTokenSource = default(CancellationTokenSource);

        /// <summary>
        /// The current message offset
        /// </summary>
        public int MessageOffset { get; set; }

        #endregion Config Properties

        #region Events

        public event EventHandler<HttpContent> OnMakingRequest;

        public event EventHandler<HttpResponseMessage> OnResponseReceived;

        /// <summary>
        /// Raises the <see cref="OnUpdate" />, <see cref="OnMessage"/>, <see cref="OnInlineQuery"/>, <see cref="OnInlineResultChosen"/> and <see cref="OnCallbackQuery"/> events.
        /// </summary>
        /// <param name="e">The <see cref="UpdateEventArgs"/> instance containing the event data.</param>
        protected virtual void OnUpdateReceived(UpdateEventArgs e)
        {
            OnUpdate?.Invoke(this, e);

            switch (e.Update.Type)
            {
                case UpdateType.MessageUpdate:
                    OnMessage?.Invoke(this, e);
                    break;

                case UpdateType.InlineQueryUpdate:
                    OnInlineQuery?.Invoke(this, e);
                    break;

                case UpdateType.ChosenInlineResultUpdate:
                    OnInlineResultChosen?.Invoke(this, e);
                    break;

                case UpdateType.CallbackQueryUpdate:
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
        public event EventHandler<UpdateEventArgs> OnUpdate;

        /// <summary>
        /// Occurs when a <see cref="Message"/> is received.
        /// </summary>
        public event EventHandler<MessageEventArgs> OnMessage;

        /// <summary>
        /// Occurs when <see cref="Message"/> was edited.
        /// </summary>
        public event EventHandler<MessageEventArgs> OnMessageEdited;

        /// <summary>
        /// Occurs when an <see cref="InlineQuery"/> is received.
        /// </summary>
        public event EventHandler<InlineQueryEventArgs> OnInlineQuery;

        /// <summary>
        /// Occurs when a <see cref="ChosenInlineResult"/> is received.
        /// </summary>
        public event EventHandler<ChosenInlineResultEventArgs> OnInlineResultChosen;

        /// <summary>
        /// Occurs when an <see cref="CallbackQuery"/> is received
        /// </summary>
        public event EventHandler<CallbackQueryEventArgs> OnCallbackQuery;

        /// <summary>
        /// Occurs when an error occurs during the background update pooling.
        /// </summary>
        public event EventHandler<ReceiveErrorEventArgs> OnReceiveError;

        /// <summary>
        /// Occurs when an error occurs during the background update pooling.
        /// </summary>
        public event EventHandler<ReceiveGeneralErrorEventArgs> OnReceiveGeneralError;

        #endregion

        /// <summary>
        /// Create a new <see cref="TelegramBotClient"/> instance.
        /// </summary>
        /// <param name="token">API token</param>
        /// <param name="httpClient">A custom <see cref="HttpClient"/></param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="token"/> format is invalid</exception>
        public TelegramBotClient(string token, HttpClient httpClient = null)
        {
            if (!Regex.IsMatch(token, @"^\d*:[\w\d-_]{35}$"))
                throw new ArgumentException("Invalid token format", nameof(token));

            _token = token;
            _httpClient = httpClient ?? new HttpClient();
        }

        /// <summary>
        /// Create a new <see cref="TelegramBotClient"/> instance behind a proxy.
        /// </summary>
        /// <param name="token">API token</param>
        /// <param name="webProxy">Use this <see cref="IWebProxy"/> to connect to the API</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="token"/> format is invalid</exception>
        public TelegramBotClient(string token, IWebProxy webProxy)
        {
            if (!Regex.IsMatch(token, @"^\d*:[\w\d-_]{35}$"))
                throw new ArgumentException("Invalid token format", nameof(token));

            var httpClientHander = new HttpClientHandler
            {
                Proxy = webProxy,
                UseProxy = true
            };

            _token = token;
            _httpClient = new HttpClient(httpClientHander);
        }

        #region Helpers

        /// <summary>
        /// Test the API token
        /// </summary>
        /// <returns><c>true</c> if token is valid</returns>
        public async Task<bool> TestApiAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                await GetMeAsync(cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (ApiRequestException e) when (e.ErrorCode == 401)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Start update receiving
        /// </summary>
        /// <param name="allowedUpdates">List the types of updates you want your bot to receive.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiRequestException"> Thrown if token is invalid</exception>
        public void StartReceiving(UpdateType[] allowedUpdates = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (_invalidToken)
                throw new ApiRequestException("Invalid token", 401);

            _receivingCancellationTokenSource = new CancellationTokenSource();

            cancellationToken.Register(() => _receivingCancellationTokenSource.Cancel());

            ReceiveAsync(allowedUpdates, _receivingCancellationTokenSource.Token);
        }

#pragma warning disable AsyncFixer03 // Avoid fire & forget async void methods
        private async void ReceiveAsync(UpdateType[] allowedUpdates, CancellationToken cancellationToken = default(CancellationToken))
        {
            IsReceiving = true;

            while (!cancellationToken.IsCancellationRequested)
            {
                var timeout = Convert.ToInt32(Timeout.TotalSeconds);

                try
                {
                    var updates =
                        await
                        GetUpdatesAsync(MessageOffset, timeout: timeout, allowedUpdates: allowedUpdates, cancellationToken: cancellationToken)
                            .ConfigureAwait(false);

                    foreach (var update in updates)
                    {
                        OnUpdateReceived(new UpdateEventArgs(update));
                        MessageOffset = update.Id + 1;
                    }
                }
                catch (OperationCanceledException) { }
                catch (ApiRequestException apiException)
                {
                    OnReceiveError?.Invoke(this, apiException);
                }
                catch (Exception generalException)
                {
                    OnReceiveGeneralError?.Invoke(this, generalException);
                }
            }

            IsReceiving = false;
        }
#pragma warning restore AsyncFixer03 // Avoid fire & forget async void methods

        /// <summary>
        /// Stop update receiving
        /// </summary>
        public void StopReceiving()
        {
            _receivingCancellationTokenSource.Cancel();
        }

        #endregion Helpers

        #region Getting updates


        /// <summary>
        /// Use this method to receive incoming updates using long polling (wiki).
        /// </summary>
        /// <param name="offset">
        /// Identifier of the first <see cref="Update"/> to be returned.
        /// Must be greater by one than the highest among the identifiers of previously received updates.
        /// By default, updates starting with the earliest unconfirmed update are returned. An update is considered
        /// confirmed as soon as <see cref="GetUpdatesAsync"/> is called with an offset higher than its <see cref="Update.Id"/>.
        /// The negative offset can be specified to retrieve updates starting from -offset update from the end of the updates queue. All previous updates will forgotten.
        /// </param>
        /// <param name="limit">
        /// Limits the number of updates to be retrieved. Values between 1—100 are accepted.
        /// </param>
        /// <param name="timeout">
        /// Timeout in seconds for long polling. Defaults to 0, i.e. usual short polling. Should be positive, short polling should be used for testing purposes only.
        /// </param>
        /// <param name="allowedUpdates">
        /// List the <see cref="UpdateType"/> of updates you want your bot to receive. See <see cref="UpdateType"/> for a complete list of available update types. Specify an empty list to receive all updates regardless of type (default).
        /// If not specified, the previous setting will be used.
        /// 
        /// Please note that this parameter doesn't affect updates created before the call to the <see cref="GetUpdatesAsync"/>, so unwanted updates may be received for a short period of time.
        /// </param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <remarks>
        /// 1. This method will not work if an outgoing webhook is set up.
        /// 2. In order to avoid getting duplicate updates, recalculate offset after each server response.
        ///
        /// Telegram Docs <see href="https://core.telegram.org/bots/api#getupdates"/>
        /// </remarks>
        /// <returns>An Array of <see cref="Update"/> is returned.</returns>
        /// <see href="https://core.telegram.org/bots/api#getupdates"/>
        public Task<Update[]> GetUpdatesAsync(int offset = 0, int limit = 100, int timeout = 0,
            UpdateType[] allowedUpdates = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<Update[]>(new GetUpdatesRequest(offset, limit, timeout, allowedUpdates), cancellationToken);

        /// <summary>
        /// Use this method to specify a url and receive incoming updates via an outgoing webhook.
        /// Whenever there is an <see cref="Update"/> for the bot, we will send an HTTPS POST request to the specified url,
        /// containing a JSON-serialized Update. In case of an unsuccessful request, we will give up after a reasonable
        /// amount of attempts.
        ///
        /// If you'd like to make sure that the Webhook request comes from Telegram, we recommend using a secret path
        /// in the URL, e.g. https://www.example.com/&lt;token&gt;. Since nobody else knows your bot's token, you can be
        /// pretty sure it's us.
        /// </summary>
        /// <param name="url">HTTPS url to send updates to. Use an empty string to remove webhook integration</param>
        /// <param name="certificate">
        /// Upload your public key certificate so that the root certificate in use can be checked.
        /// See the <see href="https://core.telegram.org/bots/self-signed">self-signed guide</see> for details.
        /// </param>
        /// <param name="maxConnections">Maximum allowed number of simultaneous HTTPS connections to the webhook for update delivery, 1-100. Defaults to 40. Use lower values to limit the load on your bot‘s server, and higher values to increase your bot’s throughput.</param>
        /// <param name="allowedUpdates">
        /// List the <see cref="UpdateType"/> of updates you want your bot to receive. See <see cref="UpdateType"/> for a complete list of available update types. Specify an empty list to receive all updates regardless of type (default).
        /// If not specified, the previous setting will be used.
        /// 
        /// Please note that this parameter doesn't affect updates created before the call to the <see cref="GetUpdatesAsync"/>, so unwanted updates may be received for a short period of time.
        /// </param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><c>true</c></returns>
        /// <remarks>
        /// 1. You will not be able to receive updates using getUpdates for as long as an outgoing webhook is set up.
        /// 2. We currently do not support self-signed certificates.
        /// 3. For the moment, the only supported port for Webhooks is 443. We may support additional ports later.
        /// 
        /// If you're having any trouble setting up webhooks, please check out this <see href="https://core.telegram.org/bots/webhooks">amazing guide to Webhooks</see>.
        /// </remarks>
        /// <see href="https://core.telegram.org/bots/api#setwebhook"/>
        public Task SetWebhookAsync(string url = "", FileToSend? certificate = null,
            int maxConnections = 40,
            UpdateType[] allowedUpdates = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<bool>(new SetWebhookRequest(url, certificate, maxConnections, allowedUpdates), cancellationToken);

        /// <summary>
        /// Use this method to remove webhook integration if you decide to switch back to getUpdates.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns true on success</returns>
        /// <see href="https://core.telegram.org/bots/api#deletewebhook"/>
        public Task<bool> DeleteWebhookAsync(CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<bool>(new DeleteWebhookRequest(), cancellationToken);

        /// <summary>
        /// Use this method to get current webhook status.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, returns <see cref="WebhookInfo"/>.</returns>
        /// <see href="https://core.telegram.org/bots/api#getwebhookinfo"/>
        public Task<WebhookInfo> GetWebhookInfoAsync(CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<WebhookInfo>(new GetWebhookInfoRequest(), cancellationToken);

        #endregion Getting updates

        #region Available methods

        /// <summary>
        /// A simple method for testing your bot's auth token.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns basic information about the bot in form of <see cref="User"/> object</returns>
        /// <see href="https://core.telegram.org/bots/api#getme"/>
        public Task<User> GetMeAsync(CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<User>(new GetMeRequest(), cancellationToken: cancellationToken);

        /// <summary>
        /// Use this method to send text messages. On success, the sent Message is returned.
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        /// <param name="text">Text of the message to be sent</param>
        /// <param name="parseMode">Change, if you want Telegram apps to show bold, italic, fixed-width text or inline URLs in your bot's message.</param>
        /// <param name="disableWebPagePreview">Disables link previews for links in this message</param>
        /// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification, Android users will receive a notification with no sound.</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        /// <see href="https://core.telegram.org/bots/api#sendmessage"/>
        public Task<Message> SendTextMessageAsync(ChatId chatId, string text, ParseMode parseMode = ParseMode.Default,
            bool disableWebPagePreview = false,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<Message>(new SendTextMessageRequest(chatId, text, parseMode, 
                disableWebPagePreview, disableNotification, replyToMessageId, replyMarkup), cancellationToken);

        /// <summary>
        /// Use this method to forward messages of any kind. On success, the sent Message is returned.
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        /// <param name="fromChatId"><see cref="ChatId"/> for the chat where the original message was sent</param>
        /// <param name="messageId">Unique message identifier</param>
        /// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification, Android users will receive a notification with no sound.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        /// <see href="https://core.telegram.org/bots/api#forwardmessage"/>
        public Task<Message> ForwardMessageAsync(ChatId chatId, ChatId fromChatId, int messageId,
            bool disableNotification = false,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<Message>(new ForwardMessageRequest(chatId, fromChatId, messageId, disableNotification), cancellationToken);

        /// <summary>
        /// Use this method to send photos. On success, the sent Message is returned.
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        /// <param name="photo">Photo to send.</param>
        /// <param name="caption">Photo caption (may also be used when resending photos by file_id).</param>
        /// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification, Android users will receive a notification with no sound.</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        /// <see href="https://core.telegram.org/bots/api#sendphoto"/>
        public Task<Message> SendPhotoAsync(ChatId chatId, FileToSend photo, string caption = "",
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<Message>(new SendPhotoRequest(chatId, photo, caption, disableNotification,
                replyToMessageId, replyMarkup), cancellationToken);

        /// <summary>
        /// Use this method to send audio files, if you want Telegram clients to display them in the music player. Your audio must be in the .mp3 format. On success, the sent Message is returned. Bots can currently send audio files of up to 50 MB in size, this limit may be changed in the future.
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        /// <param name="audio">Audio file to send.</param>
        /// <param name="caption">Audio caption, 0-200 characters</param>
        /// <param name="duration">Duration of the audio in seconds</param>
        /// <param name="performer">Performer</param>
        /// <param name="title">Track name</param>
        /// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification, Android users will receive a notification with no sound.</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        /// <see href="https://core.telegram.org/bots/api#sendaudio"/>
        public Task<Message> SendAudioAsync(ChatId chatId, FileToSend audio, string caption, int duration, string performer, string title,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<Message>(new SendAudioRequest(chatId, audio, caption, duration, 
                performer, title, disableNotification, replyToMessageId, replyMarkup), cancellationToken);


        /// <summary>
        /// Use this method to send general files. On success, the sent Message is returned. Bots can send files of any type of up to 50 MB in size.
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        /// <param name="document">File to send.</param>
        /// <param name="caption">Document caption</param>
        /// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification, Android users will receive a notification with no sound.</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        /// <see href="https://core.telegram.org/bots/api#senddocument"/>
        public Task<Message> SendDocumentAsync(ChatId chatId, FileToSend document, string caption = "",
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<Message>(new SendDocumentRequest(chatId, document, caption, disableNotification,
                replyToMessageId, replyMarkup), cancellationToken);

        /// <summary>
        /// Use this method to send .webp stickers. On success, the sent Message is returned.
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        /// <param name="sticker">Sticker to send.</param>
        /// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification, Android users will receive a notification with no sound.</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        /// <see href="https://core.telegram.org/bots/api#sendsticker"/>
        public Task<Message> SendStickerAsync(ChatId chatId, FileToSend sticker,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<Message>(new SendStickerRequest(chatId, sticker, disableNotification, 
                replyToMessageId, replyMarkup), cancellationToken);

        /// <summary>
        /// Use this method to send video files, Telegram clients support mp4 videos (other formats may be sent as Document). On success, the sent Message is returned. Bots can send video files of up to 50 MB in size.
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        /// <param name="video">Video to send.</param>
        /// <param name="duration">Duration of sent video in seconds</param>
        /// <param name="width">Video width</param>
        /// <param name="height">Video height</param>
        /// <param name="caption">Video caption</param>
        /// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification, Android users will receive a notification with no sound.</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        /// <see href="https://core.telegram.org/bots/api#sendvideo"/>
        public Task<Message> SendVideoAsync(ChatId chatId, FileToSend video, int duration = 0,
            int width = 0,
            int height = 0,
            string caption = "",
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<Message>(new SendVideoRequest(chatId, video, duration, width, height, caption, disableNotification,
                replyToMessageId, replyMarkup), cancellationToken);

        /// <summary>
        /// Use this method to send audio files, if you want Telegram clients to display the file as a playable voice message. For this to work, your audio must be in an .ogg file encoded with OPUS (other formats may be sent as Audio or Document). On success, the sent Message is returned. Bots can currently send voice messages of up to 50 MB in size, this limit may be changed in the future.
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        /// <param name="voice">Audio file to send.</param>
        /// <param name="caption">Voice message caption, 0-200 characters</param>
        /// <param name="duration">Duration of sent audio in seconds</param>
        /// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification, Android users will receive a notification with no sound.</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        /// <see href="https://core.telegram.org/bots/api#sendvoice"/>
        public Task<Message> SendVoiceAsync(ChatId chatId, FileToSend voice,
            string caption = "",
            int duration = 0,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<Message>(new SendVoiceRequest(chatId, voice, caption, duration, disableNotification,
                replyToMessageId, replyMarkup), cancellationToken);

        /// <summary>
        /// As of v.4.0, Telegram clients support rounded square mp4 videos of up to 1 minute long. Use this method to send video messages.
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        /// <param name="videoNote">Video note to send.</param>
        /// <param name="duration">Duration of sent video in seconds</param>
        /// <param name="length">Video width and height</param>
        /// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification, Android users will receive a notification with no sound.</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
        /// <see href="https://core.telegram.org/bots/api#sendvideonote"/>
        public Task<Message> SendVideoNoteAsync(ChatId chatId, FileToSend videoNote,
            int duration = 0,
            int length = 0,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<Message>(new SendVideoNoteRequest(chatId, videoNote, duration, length, disableNotification,
                replyToMessageId, replyMarkup), cancellationToken);


        /// <summary>
        /// Use this method to send point on the map. On success, the sent Message is returned.
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        /// <param name="latitude">Latitude of location</param>
        /// <param name="longitude">Longitude of location</param>
        /// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification, Android users will receive a notification with no sound.</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        /// <see href="https://core.telegram.org/bots/api#sendlocation"/>
        public Task<Message> SendLocationAsync(ChatId chatId, float latitude, float longitude,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<Message>(new SendLocationRequest(chatId, latitude, longitude, disableNotification,
                replyToMessageId, replyMarkup), cancellationToken);

        /// <summary>
        /// Use this method to send information about a venue.
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        /// <param name="latitude">Latitude of the venue</param>
        /// <param name="longitude">Longitude of the venue</param>
        /// <param name="title">Name of the venue</param>
        /// <param name="address">Address of the venue</param>
        /// <param name="foursquareId">Foursquare identifier of the venue</param>
        /// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification, Android users will receive a notification with no sound.</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
        /// <see href="https://core.telegram.org/bots/api#sendvenue"/>
        public Task<Message> SendVenueAsync(ChatId chatId, float latitude, float longitude, string title, string address,
            string foursquareId = null,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<Message>(new SendVenueRequest(chatId, latitude, longitude, title, address, foursquareId,
                disableNotification, replyToMessageId, replyMarkup), cancellationToken);

        /// <summary>
        /// Use this method to send phone contacts.
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        /// <param name="phoneNumber">Contact's phone number</param>
        /// <param name="firstName">Contact's first name</param>
        /// <param name="lastName">Contact's last name</param>
        /// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification, Android users will receive a notification with no sound.</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Additional interface options. A JSON-serialized object for an inline keyboard, custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        /// <see href="https://core.telegram.org/bots/api#sendcontact"/>
        public Task<Message> SendContactAsync(ChatId chatId, string phoneNumber, string firstName, string lastName = null,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<Message>(new SendContactRequest(chatId, phoneNumber, firstName, lastName, disableNotification,
                replyToMessageId, replyMarkup), cancellationToken);

        /// <summary>
        /// Use this method when you need to tell the user that something is happening on the bot's side. The status is set for 5 seconds or less (when a message arrives from your bot, Telegram clients clear its typing status).
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        /// <param name="chatAction">Type of action to broadcast. Choose one, depending on what the user is about to receive.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <remarks>We only recommend using this method when a response from the bot will take a noticeable amount of time to arrive.</remarks>
        /// <see href="https://core.telegram.org/bots/api#sendchataction"/>
        public Task SendChatActionAsync(ChatId chatId, ChatAction chatAction,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<bool>(new SendChatActionRequest(chatId, chatAction), cancellationToken);

        /// <summary>
        /// Use this method to get a list of profile pictures for a user. Returns a UserProfilePhotos object.
        /// </summary>
        /// <param name="userId">Unique identifier of the target user</param>
        /// <param name="offset">Sequential number of the first photo to be returned. By default, all photos are returned.</param>
        /// <param name="limit">Limits the number of photos to be retrieved. Values between 1—100 are accepted. Defaults to 100.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns a <see cref="UserProfilePhotos"/> object</returns>
        /// <see href="https://core.telegram.org/bots/api#getuserprofilephotos"/>
        public Task<UserProfilePhotos> GetUserProfilePhotosAsync(int userId, int? offset = null, int limit = 100,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<UserProfilePhotos>(new GetUserProfilePhotosRequest(userId, offset, limit), cancellationToken);

        /// <summary>
        /// Use this method to download a file. For the moment, bots can download files of up to 20MB in size.
        /// </summary>
        /// <param name="fileId">File identifier</param>
        /// <param name="destination">The destination stream</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The File object. If <paramref name="destination"/> stream in not provided, stream is embedded in the <see cref="File"/> object</returns>
        /// <see href="https://core.telegram.org/bots/api#getfile"/>
        public async Task<File> GetFileAsync(string fileId, Stream destination = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var fileInfo = await SendWebRequestAsync<File>(new GetFileRequest(fileId), cancellationToken)
                          .ConfigureAwait(false);

            var fileUri = new Uri(BaseFileUrl + _token + "/" + fileInfo.FilePath);

            if (destination == null)
                destination = fileInfo.FileStream = new MemoryStream();

            using (var response = await _httpClient.GetAsync(fileUri, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                                    .ConfigureAwait(false))
            {
                await response.Content.CopyToAsync(destination)
                                .ConfigureAwait(false);
                destination.Position = 0;
            }

            return fileInfo;
        }

        /// <summary>
        /// Use this method to kick a user from a group or a supergroup. In the case of supergroups, the user will not be able to return to the group on their own using invite links, etc., unless unbanned first. The bot must be an administrator in the group for this to work.
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target group</param>
        /// <param name="userId">Unique identifier of the target user</param>
        /// <param name="untilDate"><see cref="DateTime"/> when the user will be unbanned. If user is banned for more than 366 days or less than 30 seconds from the current time they are considered to be banned forever</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><c>true</c> on success.</returns>
        /// <see href="https://core.telegram.org/bots/api#kickchatmember"/>
        public Task<bool> KickChatMemberAsync(ChatId chatId, int userId,
            DateTime untilDate = default(DateTime),
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<bool>(new KickChatMemberRequest(chatId, userId, untilDate), cancellationToken);

        /// <summary>
        /// Use this method for your bot to leave a group, supergroup or channel.
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns a Chat object on success.</returns>
        /// <see href="https://core.telegram.org/bots/api#leavechat"/>
        public Task<bool> LeaveChatAsync(ChatId chatId, CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<bool>(new LeaveChatRequest(chatId), cancellationToken);

        /// <summary>
        /// Use this method to unban a previously kicked user in a supergroup. The user will not return to the group automatically, but will be able to join via link, etc. The bot must be an administrator in the group for this to work. 
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target group</param>
        /// <param name="userId">Unique identifier of the target user</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><c>true</c> on success.</returns>
        /// <see href="https://core.telegram.org/bots/api#unbanchatmember"/>
        public Task<bool> UnbanChatMemberAsync(ChatId chatId, int userId,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<bool>(new UnbanChatMemberRequest(chatId, userId), cancellationToken);

        /// <summary>
        /// Use this method to get up to date information about the chat (current name of the user for one-on-one conversations, current username of a user, group or channel, etc.).
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns a Chat object on success.</returns>
        /// <see href="https://core.telegram.org/bots/api#getchat"/>
        public Task<Chat> GetChatAsync(ChatId chatId, CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<Chat>(new GetChatRequest(chatId), cancellationToken);

        /// <summary>
        /// Use this method to get a list of administrators in a chat.
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, returns an Array of <see cref="ChatMember"/> objects that contains information about all chat administrators except other bots. If the chat is a group or a supergroup and no administrators were appointed, only the creator will be returned.</returns>
        /// <see href="https://core.telegram.org/bots/api#getchatadministrators"/>
        public Task<ChatMember[]> GetChatAdministratorsAsync(ChatId chatId,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<ChatMember[]>(new GetChatAdministratorsRequest(chatId), cancellationToken);

        /// <summary>
        /// Use this method to get the number of members in a chat.
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns Int on success.</returns>
        /// <see href="https://core.telegram.org/bots/api#getchatmemberscount"/>
        public Task<int> GetChatMembersCountAsync(ChatId chatId,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<int>(new GetChatMembersCountRequest(chatId), cancellationToken);

        /// <summary>
        /// Use this method to get information about a member of a chat.
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        /// <param name="userId">Unique identifier of the target user</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns a ChatMember object on success.</returns>
        /// <see href="https://core.telegram.org/bots/api#getchatmember"/>
        public Task<ChatMember> GetChatMemberAsync(ChatId chatId, int userId,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<ChatMember>(new GetChatMemberRequest(chatId, userId), cancellationToken);

        /// <summary>
        /// Use this method to send answers to callback queries sent from inline keyboards. The answer will be displayed to the user as a notification at the top of the chat screen or as an alert.
        /// </summary>
        /// <param name="callbackQueryId">Unique identifier for the query to be answered</param>
        /// <param name="text">Text of the notification. If not specified, nothing will be shown to the user</param>
        /// <param name="showAlert">If true, an alert will be shown by the client instead of a notification at the top of the chat screen. Defaults to false.</param>
        /// <param name="url">
        /// URL that will be opened by the user's client. If you have created a Game and accepted the conditions via @Botfather, specify the URL that opens your game – note that this will only work if the query comes from a callback_game button.
        /// Otherwise, you may use links like telegram.me/your_bot? start = XXXX that open your bot with a parameter.
        /// </param>
        /// <param name="cacheTime">The maximum amount of time in seconds that the result of the callback query may be cached client-side. Telegram apps will support caching starting in version 3.14.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, <c>true</c> is returned.</returns>
        /// <remarks>
        /// Alternatively, the user can be redirected to the specified Game URL. For this option to work, you must first create a game for your bot via BotFather and accept the terms.
        /// Otherwise, you may use links like telegram.me/your_bot?start=XXXX that open your bot with a parameter.
        /// </remarks>
        /// <see href="https://core.telegram.org/bots/api#answercallbackquery"/>
        public Task<bool> AnswerCallbackQueryAsync(string callbackQueryId, string text = null,
            bool showAlert = false,
            string url = null,
            int cacheTime = 0,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<bool>(new AnswerCallbackQueryRequest(callbackQueryId, text, showAlert, url, cacheTime), cancellationToken);

        /// <summary>
        /// Use this method to restrict a user in a supergroup. The bot must be an administrator in the supergroup for this to work and must have the appropriate admin rights.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup</param>
        /// <param name="userId">Unique identifier of the target user</param>
        /// <param name="untilDate"><see cref="DateTime"/> when restrictions will be lifted for the user. If user is restricted for more than 366 days or less than 30 seconds from the current time, they are considered to be restricted forever</param>
        /// <param name="canSendMessages">Pass True, if the user can send text messages, contacts, locations and venues</param>
        /// <param name="canSendMediaMessages">Pass True, if the user can send audios, documents, photos, videos, video notes and voice notes, implies can_send_messages</param>
        /// <param name="canSendOtherMessages">Pass True, if the user can send animations, games, stickers and use inline bots, implies can_send_media_messages</param>
        /// <param name="canAddWebPagePreviews">Pass True, if the user may add web page previews to their messages, implies can_send_media_messages</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, <c>true</c> is returned</returns>
        /// <remarks>Pass True for all boolean parameters to lift restrictions from a user.</remarks>
        /// <see href="https://core.telegram.org/bots/api#restrictchatmember"/>
        public Task<bool> RestrictChatMemberAsync(ChatId chatId, int userId, DateTime untilDate = default(DateTime),
            bool? canSendMessages = null, bool? canSendMediaMessages = null, bool? canSendOtherMessages = null,
            bool? canAddWebPagePreviews = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<bool>(new RestrictChatMemberRequest(chatId, userId, untilDate, canSendMessages,
                canSendMediaMessages, canSendOtherMessages, canAddWebPagePreviews), cancellationToken);

        /// <summary>
        /// Use this method to promote or demote a user in a supergroup or a channel. The bot must be an administrator in the chat for this to work and must have the appropriate admin rights.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="userId">Unique identifier of the target user</param>
        /// <param name="canChangeInfo">Pass True, if the administrator can change chat title, photo and other settings</param>
        /// <param name="canPostMessages">Pass True, if the administrator can create channel posts, channels only</param>
        /// <param name="canEditMessages">Pass True, if the administrator can edit messages of other users, channels only</param>
        /// <param name="canDeleteMessages">Pass True, if the administrator can delete messages of other users</param>
        /// <param name="canInviteUsers">Pass True, if the administrator can invite new users to the chat</param>
        /// <param name="canRestrictMembers">Pass True, if the administrator can restrict, ban or unban chat members</param>
        /// <param name="canPinMessages">Pass True, if the administrator can pin messages, supergroups only</param>
        /// <param name="canPromoteMembers">Pass True, if the administrator can add new administrators with a subset of his own privileges or demote administrators that he has promoted, directly or indirectly (promoted by administrators that were appointed by him)</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns True on success.</returns>
        /// <remarks>Pass False for all boolean parameters to demote a user.</remarks>
        /// <see href="https://core.telegram.org/bots/api#promotechatmember"/>
        public Task<bool> PromoteChatMemberAsync(ChatId chatId, int userId, bool? canChangeInfo = null,
            bool? canPostMessages = null, bool? canEditMessages = null, bool? canDeleteMessages = null,
            bool? canInviteUsers = null, bool? canRestrictMembers = null, bool? canPinMessages = null,
            bool? canPromoteMembers = null, CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<bool>(new PromoteChatMemberRequest(chatId, userId, canChangeInfo, canPostMessages, canEditMessages,
                canDeleteMessages, canInviteUsers, canRestrictMembers, canPinMessages, canPromoteMembers), cancellationToken);

        #endregion Available methods

        #region Updating messages

        /// <summary>
        /// Use this method to edit text messages sent by the bot or via the bot (for inline bots).
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        /// <param name="messageId">Unique identifier of the sent message</param>
        /// <param name="text">New text of the message</param>
        /// <param name="parseMode">Send Markdown or HTML, if you want Telegram apps to show bold, italic, fixed-width text or inline URLs in your bot's message.</param>
        /// <param name="disableWebPagePreview">Disables link previews for links in this message</param>
        /// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, the edited Message is returned.</returns>
        /// <see href="https://core.telegram.org/bots/api#editmessagetext"/>
        public Task<Message> EditMessageTextAsync(ChatId chatId, int messageId, string text,
            ParseMode parseMode = ParseMode.Default,
            bool disableWebPagePreview = false,
            IReplyMarkup replyMarkup = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<Message>(new EditMessageTextRequest(chatId, messageId, text, parseMode,
                disableWebPagePreview, replyMarkup), cancellationToken);

        /// <summary>
        /// Use this method to edit text messages sent by the bot or via the bot (for inline bots).
        /// </summary>
        /// <param name="inlineMessageId">Identifier of the inline message</param>
        /// <param name="text">New text of the message</param>
        /// <param name="parseMode">Send Markdown or HTML, if you want Telegram apps to show bold, italic, fixed-width text or inline URLs in your bot's message.</param>
        /// <param name="disableWebPagePreview">Disables link previews for links in this message</param>
        /// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>><c>true</c> on success.</returns>
        /// <see href="https://core.telegram.org/bots/api#editmessagetext"/>
        public Task<bool> EditInlineMessageTextAsync(string inlineMessageId, string text,
            ParseMode parseMode = ParseMode.Default,
            bool disableWebPagePreview = false,
            IReplyMarkup replyMarkup = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<bool>(new EditInlineMessageTextRequest(inlineMessageId, text, parseMode,
                disableWebPagePreview, replyMarkup), cancellationToken);

        /// <summary>
        /// Use this method to edit captions of messages sent by the bot or via the bot (for inline bots).
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        /// <param name="messageId">Unique identifier of the sent message</param>
        /// <param name="caption">New caption of the message</param>
        /// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, the edited Message is returned.</returns>
        /// <see href="https://core.telegram.org/bots/api#editmessagecaption"/>
        public Task<Message> EditMessageCaptionAsync(ChatId chatId, int messageId, string caption,
            IReplyMarkup replyMarkup = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<Message>(new EditMessageCaptionRequest(chatId, messageId, caption,
                replyMarkup), cancellationToken);

        /// <summary>
        /// Use this method to edit captions of messages sent by the bot or via the bot (for inline bots).
        /// </summary>
        /// <param name="inlineMessageId">Unique identifier of the sent message</param>
        /// <param name="caption">New caption of the message</param>
        /// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>><c>true</c> on success.</returns>
        /// <see href="https://core.telegram.org/bots/api#editmessagecaption"/>
        public Task<bool> EditInlineMessageCaptionAsync(string inlineMessageId, string caption,
            IReplyMarkup replyMarkup = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<bool>(new EditInlineMessageCaptionRequest(inlineMessageId, caption,
                replyMarkup), cancellationToken);

        /// <summary>
        /// Use this method to edit only the reply markup of messages sent by the bot or via the bot (for inline bots).
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        /// <param name="messageId">Unique identifier of the sent message</param>
        /// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, the edited Message is returned.</returns>
        /// <see href="https://core.telegram.org/bots/api#editmessagereplymarkup"/>
        public Task<Message> EditMessageReplyMarkupAsync(ChatId chatId, int messageId,
            IReplyMarkup replyMarkup = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<Message>(new EditMessageReplyMarkupRequest(chatId, messageId,
                replyMarkup), cancellationToken);

        /// <summary>
        /// Use this method to edit only the reply markup of messages sent by the bot or via the bot (for inline bots).
        /// </summary>
        /// <param name="inlineMessageId">Unique identifier of the sent message</param>
        /// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>><c>true</c> on success.</returns>
        /// <see href="https://core.telegram.org/bots/api#editmessagereplymarkup"/>
        public Task<bool> EditInlineMessageReplyMarkupAsync(string inlineMessageId,
            IReplyMarkup replyMarkup = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<bool>(new EditInlineMessageReplyMarkupRequest(inlineMessageId,
                replyMarkup), cancellationToken);

        /// <summary>
        /// Use this method to delete a message. A message can only be deleted if it was sent less than 48 hours ago. Any such recently sent outgoing message may be deleted. Additionally, if the bot is an administrator in a group chat, it can delete any message. If the bot is an administrator in a supergroup, it can delete messages from any other user and service messages about people joining or leaving the group (other types of service messages may only be removed by the group creator). In channels, bots can only remove their own messages.
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> Unique identifier for the target chat or username of the target channel (in the format @channelusername)</param>
        /// <param name="messageId">Unique identifier of the message to delete</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><c>true</c> on success.</returns>
        /// <see href="https://core.telegram.org/bots/api#deletemessage"/>
        public Task<bool> DeleteMessageAsync(ChatId chatId, int messageId,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<bool>(new DeleteMessageRequest(chatId, messageId), cancellationToken);

        #endregion Updating messages

        #region Inline mode

        /// <summary>
        /// Use this method to send answers to an inline query.
        /// </summary>
        /// <param name="inlineQueryId">Unique identifier for answered query</param>
        /// <param name="results">A array of results for the inline query</param>
        /// <param name="cacheTime">The maximum amount of time in seconds the result of the inline query may be cached on the server</param>
        /// <param name="isPersonal">Pass <c>true</c>, if results may be cached on the server side only for the user that sent the query. By default, results may be returned to any user who sends the same query</param>
        /// <param name="nextOffset">Pass the offset that a client should send in the next query with the same text to receive more results. Pass an empty string if there are no more results or if you don't support pagination. Offset length can't exceed 64 bytes.</param>
        /// <param name="switchPmText">If passed, clients will display a button with specified text that switches the user to a private chat with the bot and sends the bot a start message with the parameter switch_pm_parameter</param>
        /// <param name="switchPmParameter">Parameter for the start message sent to the bot when user presses the switch button</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, <c>true</c> is returned.</returns>
        /// <see href="https://core.telegram.org/bots/api#answerinlinequery"/>
        public Task<bool> AnswerInlineQueryAsync(string inlineQueryId, InlineQueryResult[] results,
            int? cacheTime = null,
            bool isPersonal = false, string nextOffset = null,
            string switchPmText = null,
            string switchPmParameter = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<bool>(new AnswerInlineQueryRequest(inlineQueryId, results, cacheTime, isPersonal,
                nextOffset, switchPmText, switchPmParameter), cancellationToken);

        # endregion Inline mode

        #region Payments

        /// <summary>
        /// Use this method to send invoices.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target private chat</param>
        /// <param name="title">Product name</param>
        /// <param name="description">Product description</param>
        /// <param name="payload">Bot-defined invoice payload, 1-128 bytes. This will not be displayed to the user, use for your internal processes.</param>
        /// <param name="providerToken">Payments provider token, obtained via Botfather</param>
        /// <param name="startParameter">Unique deep-linking parameter that can be used to generate this invoice when used as a start parameter</param>
        /// <param name="currency">Three-letter ISO 4217 currency code, see more on currencies</param>
        /// <param name="prices">Price breakdown, a list of components (e.g. product price, tax, discount, delivery cost, delivery tax, bonus, etc.)</param>
        /// <param name="photoUrl">URL of the product photo for the invoice. Can be a photo of the goods or a marketing image for a service.</param>
        /// <param name="photoSize">Photo size</param>
        /// <param name="photoWidth">Photo width</param>
        /// <param name="photoHeight">Photo height</param>
        /// <param name="needName">Pass True, if you require the user's full name to complete the order</param>
        /// <param name="needPhoneNumber">Pass True, if you require the user's phone number to complete the order</param>
        /// <param name="needEmail">Pass True, if you require the user's email to complete the order</param>
        /// <param name="needShippingAddress">Pass True, if you require the user's shipping address to complete the order</param>
        /// <param name="isFlexible">Pass True, if the final price depends on the shipping method</param>
        /// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification, Android users will receive a notification with no sound.</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
        /// <see href="https://core.telegram.org/bots/api#sendinvoice"/>
        public Task<Message> SendInvoiceAsync(ChatId chatId, string title, string description,
            string payload, string providerToken, string startParameter, string currency,
            LabeledPrice[] prices, string photoUrl = null, int photoSize = 0, int photoWidth = 0,
            int photoHeight = 0, bool needName = false, bool needPhoneNumber = false,
            bool needEmail = false, bool needShippingAddress = false, bool isFlexible = false,
            bool disableNotification = false, int replyToMessageId = 0, InlineKeyboardMarkup replyMarkup = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<Message>(new SendInvoiceRequest(chatId, title, description, payload, providerToken,
                startParameter, currency, prices, photoUrl, photoSize, photoWidth, photoHeight, needName, needPhoneNumber,
                needEmail, needShippingAddress, isFlexible, disableNotification, replyToMessageId, replyMarkup), cancellationToken);

        /// <summary>
        /// If you sent an invoice requesting a shipping address and the parameter is_flexible was specified, the Bot API will send an Update with a shipping_query field to the bot. Use this method to reply to shipping queries.
        /// </summary>
        /// <param name="shippingQueryId">Unique identifier for the query to be answered</param>
        /// <param name="ok">Specify True if delivery to the specified address is possible and False if there are any problems</param>
        /// <param name="shippingOptions">Required if ok is True.</param>
        /// <param name="errorMessage">Required if ok is False. Error message in human readable form that explains why it is impossible to complete the order </param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, True is returned.</returns>
        /// <see href="https://core.telegram.org/bots/api#answershippingquery"/>
        public Task<bool> AnswerShippingQueryAsync(string shippingQueryId, bool ok,
            ShippingOption[] shippingOptions = null,
            string errorMessage = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<bool>(new AnswerShippingQueryRequest(shippingQueryId, ok, shippingOptions, errorMessage), cancellationToken);

        /// <summary>
        /// Use this method to respond to such pre-checkout queries.
        /// </summary>
        /// <param name="preCheckoutQueryId">Unique identifier for the query to be answered</param>
        /// <param name="ok">Specify True if everything is alright</param>
        /// <param name="errorMessage">Required if ok is False. Error message in human readable form that explains the reason for failure to proceed with the checkout</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, True is returned.</returns>
        /// <remarks>Note: The Bot API must receive an answer within 10 seconds after the pre-checkout query was sent.</remarks>
        /// <see href="https://core.telegram.org/bots/api#answerprecheckoutquery"/>
        public Task<bool> AnswerPreCheckoutQueryAsync(string preCheckoutQueryId, bool ok,
            string errorMessage = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<bool>(new AnswerPreCheckoutQueryRequest(preCheckoutQueryId, ok, errorMessage), cancellationToken);

        #endregion Payments

        #region Games

        /// <summary>
        /// Use this method to send a game.
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        /// <param name="gameShortName">Short name of the game, serves as the unique identifier for the game.</param>
        /// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification, Android users will receive a notification with no sound.</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        /// <see href="https://core.telegram.org/bots/api#sendgame"/>
        public Task<Message> SendGameAsync(ChatId chatId, string gameShortName,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<Message>(new SendGameRequest(chatId, gameShortName, disableNotification, replyToMessageId,
                replyMarkup), cancellationToken);

        /// <summary>
        /// Use this method to set the score of the specified user in a game.
        /// </summary>
        /// <param name="userId">Unique identifier of the target user.</param>
        /// <param name="score">The score.</param>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat.</param>
        /// <param name="messageId">Unique identifier of the sent message.</param>
        /// <param name="force">Pass True, if the high score is allowed to decrease. This can be useful when fixing mistakes or banning cheaters</param>
        /// <param name="disableEditMessage">Pass True, if the game message should not be automatically edited to include the current scoreboard</param>
        /// <param name="editMessage">Pass True, if the game message should be automatically edited to include the current scoreboard.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, if the message was sent by the bot, returns the edited <see cref="Message"/></returns>
        /// <see href="https://core.telegram.org/bots/api#setgamescore"/>
        public Task<Message> SetGameScoreAsync(int userId, int score, ChatId chatId, int messageId,
            bool force = false,
            bool disableEditMessage = false,
            bool editMessage = false,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<Message>(new SetGameScoreRequest(userId, score, chatId, messageId, force, disableEditMessage,
                editMessage), cancellationToken);

        /// <summary>
        /// Use this method to set the score of the specified user in a game.
        /// </summary>
        /// <param name="userId">Unique identifier of the target user.</param>
        /// <param name="score">The score.</param>
        /// <param name="inlineMessageId">Identifier of the inline message.</param>
        /// <param name="force">Pass True, if the high score is allowed to decrease. This can be useful when fixing mistakes or banning cheaters</param>
        /// <param name="disableEditMessage">Pass True, if the game message should not be automatically edited to include the current scoreboard</param>
        /// <param name="editMessage">Pass True, if the game message should be automatically edited to include the current scoreboard.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, if the message was sent by the bot, returns the edited Message</returns>
        /// <see href="https://core.telegram.org/bots/api#setgamescore"/>
        public async Task<Message> SetGameScoreAsync(int userId, int score, string inlineMessageId,
            bool force = false,
            bool disableEditMessage = false,
            bool editMessage = false,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            object response = await SendWebRequestAsync<object>(new SetGameScoreRequest(userId, score, inlineMessageId,
                force, disableEditMessage, editMessage), cancellationToken)
                .ConfigureAwait(false);

            Message message;
            switch (response)
            {
                case Message m:
                    message = m;
                    break;
                case bool b when b:
                    message = default(Message);
                    break;
                default:
                    throw new Exception($"Unexpected response: {response}");
            }
            return message;
        }

        /// <summary>
        /// Use this method to get data for high score tables.
        /// </summary>
        /// <param name="userId">Unique identifier of the target user.</param>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat.</param>
        /// <param name="messageId">Unique identifier of the sent message.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, returns an Array of <see cref="GameHighScore"/> objects</returns>
        /// <remarks>
        /// This method will currently return scores for the target user, plus two of his closest neighbors on each side.
        /// Will also return the top three users if the user and his neighbors are not among them. Please note that this behavior is subject to change.
        /// </remarks>
        /// <see href="https://core.telegram.org/bots/api#getgamehighscores"/>
        public Task<GameHighScore[]> GetGameHighScoresAsync(int userId, ChatId chatId, int messageId,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<GameHighScore[]>(new GetGameHighScoresRequest(userId, chatId, messageId), cancellationToken);

        /// <summary>
        /// Use this method to get data for high score tables.
        /// </summary>
        /// <param name="userId">Unique identifier of the target user.</param>
        /// <param name="inlineMessageId">Unique identifier of the inline message.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, returns an Array of <see cref="GameHighScore"/> objects</returns>
        /// <remarks>
        /// This method will currently return scores for the target user, plus two of his closest neighbors on each side.
        /// Will also return the top three users if the user and his neighbors are not among them. Please note that this behavior is subject to change.
        /// </remarks>
        /// <see href="https://core.telegram.org/bots/api#getgamehighscores"/>
        public Task<GameHighScore[]> GetGameHighScoresAsync(int userId, string inlineMessageId, CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<GameHighScore[]>(new GetGameHighScoresRequest(userId, inlineMessageId), cancellationToken);

        #endregion Games

        #region Group and channel management
        /// <summary>
        /// Use this method to export an invite link to a supergroup or a channel. The bot must be an administrator in the chat for this to work and must have the appropriate admin rights.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns exported invite link as String on success.</returns>
        /// <see href="https://core.telegram.org/bots/api#exportchatinvitelink"/>
        public Task<string> ExportChatInviteLinkAsync(ChatId chatId,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<string>(new ExportChatInviteLinkRequest(chatId), cancellationToken);

        /// <summary>
        /// Use this method to set a new profile photo for the chat. Photos can't be changed for private chats. The bot must be an administrator in the chat for this to work and must have the appropriate admin rights.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="photo">The new profile picture for the chat.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <c>true</c> on success.</returns>
        /// <see href="https://core.telegram.org/bots/api#setchatphoto"/>
        public Task<bool> SetChatPhotoAsync(ChatId chatId, FileToSend photo,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<bool>(new SetChatPhotoRequest(chatId, photo), cancellationToken);

        /// <summary>
        /// Use this method to delete a chat photo. Photos can't be changed for private chats. The bot must be an administrator in the chat for this to work and must have the appropriate admin rights.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns true on success.</returns>
        /// <see href="https://core.telegram.org/bots/api#deletechatphoto"/>
        public Task<bool> DeleteChatPhotoAsync(ChatId chatId, CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<bool>(new DeleteChatPhotoRequest(chatId), cancellationToken);

        /// <summary>
        /// Use this method to change the title of a chat. Titles can't be changed for private chats. The bot must be an administrator in the chat for this to work and must have the appropriate admin rights.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="title">New chat title, 1-255 characters</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns true on success.</returns>
        /// <see href="https://core.telegram.org/bots/api#setchattitle"/>
        public Task<bool> SetChatTitleAsync(ChatId chatId, string title,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<bool>(new SetChatTitleRequest(chatId, title), cancellationToken);

        /// <summary>
        /// Use this method to change the description of a supergroup or a channel. The bot must be an administrator in the chat for this to work and must have the appropriate admin rights.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="description">New chat description, 0-255 characters. Defaults to an empty string, which would clear the description.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns true on success.</returns>
        /// <see href="https://core.telegram.org/bots/api#setchatdescription"/>
        public Task<bool> SetChatDescriptionAsync(ChatId chatId, string description = "",
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<bool>(new SetChatDescriptionRequest(chatId, description), cancellationToken);

        /// <summary>
        /// Use this method to pin a message in a supergroup. The bot must be an administrator in the chat for this to work and must have the appropriate admin rights.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup</param>
        /// <param name="messageId">Identifier of a message to pin</param>
        /// <param name="disableNotification">Pass True, if it is not necessary to send a notification to all group members about the new pinned message</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns true on success.</returns>
        /// <see href="https://core.telegram.org/bots/api#pinchatmessage"/>
        public Task<bool> PinChatMessageAsync(ChatId chatId, int messageId, bool disableNotification = false,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<bool>(new PinChatMessageRequest(chatId, messageId, disableNotification), cancellationToken);

        /// <summary>
        /// Use this method to unpin a message in a supergroup chat. The bot must be an administrator in the chat for this to work and must have the appropriate admin rights.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns true on success</returns>
        /// <see href="https://core.telegram.org/bots/api#unpinchatmessage"/>
        public Task<bool> UnpinChatMessageAsync(ChatId chatId, CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<bool>(new UnpinChatMessageRequest(chatId), cancellationToken);
        #endregion

        #region Stickers
        /// <summary>
        /// Use this method to get a sticker set.
        /// </summary>
        /// <param name="name">Short name of the sticker set that is used in t.me/addstickers/ URLs (e.g., animals)</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, a StickerSet object is returned.</returns>
        /// <see href="https://core.telegram.org/bots/api#getstickerset"/>
        public Task<StickerSet> GetStickerSetAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<StickerSet>(new GetStickerSetRequest(name), cancellationToken);

        /// <summary>
        /// Use this method to upload a .png file with a sticker for later use in createNewStickerSet and addStickerToSet methods (can be used multiple times).
        /// </summary>
        /// <param name="userId">User identifier of sticker file owner</param>
        /// <param name="pngSticker">Png image with the sticker, must be up to 512 kilobytes in size, dimensions must not exceed 512px, and either width or height must be exactly 512px.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns the uploaded File on success.</returns>
        /// <see href="https://core.telegram.org/bots/api#uploadstickerfile"/>
        public Task<File> UploadStickerFileAsync(int userId, FileToSend pngSticker,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<File>(new UploadStickerFileRequest(userId, pngSticker), cancellationToken);

        /// <summary>
        /// Use this method to create new sticker set owned by a user. The bot will be able to edit the created sticker set. 
        /// </summary>
        /// <param name="userId">User identifier of created sticker set owner</param>
        /// <param name="name">Short name of sticker set, to be used in t.me/addstickers/ URLs (e.g., animals). Can contain only english letters, digits and underscores. Must begin with a letter, can't contain consecutive underscores and must end in “_by_&lt;bot_username&gt;”. &lt;bot_username&gt; is case insensitive. 1-64 characters.</param>
        /// <param name="title">Sticker set title, 1-64 characters</param>
        /// <param name="pngSticker">Png image with the sticker, must be up to 512 kilobytes in size, dimensions must not exceed 512px, and either width or height must be exactly 512px.</param>
        /// <param name="emojis">One or more emoji corresponding to the sticker</param>
        /// <param name="isMasks">Pass True, if a set of mask stickers should be created</param>
        /// <param name="maskPosition">Position where the mask should be placed on faces</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns True on success.</returns>
        /// <see href="https://core.telegram.org/bots/api#createnewstickerset"/>
        public Task<bool> CreateNewStickerSetAsnyc(int userId, string name, string title, FileToSend pngSticker,
            string emojis, bool isMasks = false, MaskPosition maskPosition = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<bool>(new CreateNewStickerSetRequest(userId, name, title, pngSticker,
                 emojis, isMasks, maskPosition), cancellationToken);

        /// <summary>
        /// Use this method to add a new sticker to a set created by the bot.
        /// </summary>
        /// <param name="userId">User identifier of sticker set owner</param>
        /// <param name="name">Sticker set name</param>
        /// <param name="pngSticker">Png image with the sticker, must be up to 512 kilobytes in size, dimensions must not exceed 512px, and either width or height must be exactly 512px.</param>
        /// <param name="emojis">One or more emoji corresponding to the sticker</param>
        /// <param name="maskPosition">Position where the mask should be placed on faces</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>True on success</returns>
        /// <see href="https://core.telegram.org/bots/api#addstickertoset"/>
        public Task<bool> AddStickerToSetAsync(int userId, string name, FileToSend pngSticker, string emojis, MaskPosition maskPosition = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<bool>(new AddStickerToSetRequest(userId, name, pngSticker, emojis,
                maskPosition), cancellationToken);

        /// <summary>
        /// Use this method to move a sticker in a set created by the bot to a specific position.
        /// </summary>
        /// <param name="sticker">File identifier of the sticker</param>
        /// <param name="position">New sticker position in the set, zero-based</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>True on success</returns>
        /// <see href="https://core.telegram.org/bots/api#setstickerpositioninset"/>
        public Task<bool> SetStickerPositionInSetAsync(string sticker, int position, CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<bool>(new SetStickerPositionInSetRequest(sticker, position), cancellationToken);

        /// <summary>
        /// Use this method to delete a sticker from a set created by the bot.
        /// </summary>
        /// <param name="sticker">File identifier of the sticker</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns True on success.</returns>
        /// <see href="https://core.telegram.org/bots/api#deletestickerfromset"/>
        public Task<bool> DeleteStickerFromSetAsync(string sticker, CancellationToken cancellationToken = default(CancellationToken))
            => SendWebRequestAsync<bool>(new DeleteStickerFromSetRequest(sticker), cancellationToken);
        #endregion

        #region Support Methods - Private

        /*/// <summary>
        /// Use this method to send any messages. On success, the sent Message is returned.
        /// </summary>
        /// <param name="type">The <see cref="MessageType"/></param>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format @channelusername)</param>
        /// <param name="content">The content of the message. Could be a text, photo, audio, sticker, document, video or location</param>
        /// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification, Android users will receive a notification with no sound.</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <param name="additionalParameters">Optional. if additional Parameters could be send i.e. "disable_web_page_preview" in for a TextMessage</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        private Task<Message> SendMessageAsync(MessageType type, ChatId chatId, object content,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null,
            Dictionary<string, object> additionalParameters = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (additionalParameters == null)
                additionalParameters = new Dictionary<string, object>();

            var typeInfo = type.ToKeyValue();

            additionalParameters.Add("chat_id", chatId);

            if (disableNotification)
                additionalParameters.Add("disable_notification", true);

            if (replyMarkup != null)
                additionalParameters.Add("reply_markup", replyMarkup);

            if (replyToMessageId != 0)
                additionalParameters.Add("reply_to_message_id", replyToMessageId);

            if (!string.IsNullOrEmpty(typeInfo.Value))
                additionalParameters.Add(typeInfo.Value, content);

            return SendWebRequestAsync<Message>(typeInfo.Key, additionalParameters, cancellationToken);
        }

        private async Task<T> SendWebRequestAsync<T>(string method, Dictionary<string, object> parameters = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (_invalidToken)
                throw new ApiRequestException("Invalid token", 401);

            var uri = new Uri(BaseUrl + _token + "/" + method);

            ApiResponse<T> responseObject;
            try
            {
                HttpResponseMessage response;
                if (parameters == null || parameters.Count == 0)
                {
                    // Request with no parameters

                    OnMakingRequest?.Invoke(this, null); // ToDo: Use a struct to hold values such as URI
                    response = await _httpClient.GetAsync(uri, cancellationToken)
                                            .ConfigureAwait(false);
                }
                else if (parameters.Any(p => p.Value is FileToSend && ((FileToSend)p.Value).Type == FileType.Stream))
                {
                    // Request including a file

                    using (var form = new MultipartFormDataContent())
                    {
                        foreach (var parameter in parameters.Where(parameter => parameter.Value != null))
                        {
                            var content = ConvertParameterValue(parameter.Value);

                            if (parameter.Value is FileToSend fts)
                            {
                                content.Headers.Add("Content-Type", "application/octet-stream");
                                string headerValue = $"form-data; name=\"{parameter.Key}\"; filename=\"{fts.Filename}\"";
                                byte[] bytes = Encoding.UTF8.GetBytes(headerValue);
                                headerValue = string.Join("", bytes.Select(b => (char)b));
                                content.Headers.Add("Content-Disposition", headerValue);

                                form.Add(content, parameter.Key, fts.Filename);
                            }
                            else
                            {
                                form.Add(content, parameter.Key);
                            }
                        }

                        OnMakingRequest?.Invoke(this, form);
                        response = await _httpClient.PostAsync(uri, form, cancellationToken)
                                                .ConfigureAwait(false);
                    }
                }
                else
                {
                    // Request with JSON data

                    var payload = JsonConvert.SerializeObject(parameters, SerializerSettings);

                    var httpContent = new StringContent(payload, Encoding.UTF8, "application/json");

                    OnMakingRequest?.Invoke(this, httpContent);
                    response = await _httpClient.PostAsync(uri, httpContent, cancellationToken)
                                            .ConfigureAwait(false);
                }

                string responseString = await response.Content.ReadAsStringAsync()
                    .ConfigureAwait(false);

                // ToDo: Read response content and then fire event with a custom struct containing Request URI, Response status code, and response raw payload
                OnResponseReceived?.Invoke(this, response);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        break;
                    case HttpStatusCode.Unauthorized:
                        _invalidToken = true;
                        throw new ApiRequestException("Invalid token", 401);
                    case HttpStatusCode.BadRequest when !string.IsNullOrWhiteSpace(responseString):
                    case HttpStatusCode.Forbidden when !string.IsNullOrWhiteSpace(responseString):
                    case HttpStatusCode.Conflict when !string.IsNullOrWhiteSpace(responseString):
                        // Do NOT throw here, an ApiRequestException will be thrown next
                        break;
                    default:
                        response.EnsureSuccessStatusCode();
                        break;
                }

                responseObject = JsonConvert.DeserializeObject<ApiResponse<T>>(responseString, SerializerSettings);
            }
            catch (TaskCanceledException e)
            {
                if (cancellationToken.IsCancellationRequested)
                    throw;

                throw new ApiRequestException("Request timed out", 408, e); // ToDo: Put breakpoint, then disconnect from the net and try this
            }

            if (responseObject == null)
                responseObject = new ApiResponse<T> { Ok = false, Message = "No response received" };

            if (!responseObject.Ok)
                throw ApiRequestException.FromApiResponse(responseObject);

            return responseObject.ResultObject;
        }*/

        private async Task<T> SendWebRequestAsync<T>(IApiRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (_invalidToken)
                throw new ApiRequestException("Invalid token", 401);

            var uri = new Uri(BaseUrl + _token + "/" + request.Method);

            ApiResponse<T> responseObject = null;
            try
            {
                HttpResponseMessage response;

                switch (request.Encoding)
                {
                    case RequestEncoding.Multipart:
                        using (var form = new MultipartFormDataContent())
                        {
                            var fContent = new StreamContent(request.FileStream);
                            fContent.Headers.Add("Content-Type", "application/octet-stream");
                            string headerValue = $"form-data; name=\"{request.FileParameterName}\"; filename=\"{request.FileName}\"";
                            byte[] bytes = Encoding.UTF8.GetBytes(headerValue);
                            headerValue = string.Join("", bytes.Select(b => (char)b));
                            fContent.Headers.Add("Content-Disposition", headerValue);
                            form.Add(fContent, request.FileParameterName, request.FileName);

                            foreach (var parameter in request.Parameters.Where(parameter => parameter.Value != null))
                            {
                                var content = ConvertParameterValue(parameter.Value);
                                form.Add(content, parameter.Key);
                            }

                            response = await _httpClient.PostAsync(uri, form, cancellationToken)
                                                .ConfigureAwait(false);
                        }
                        break;
                    case RequestEncoding.Json:
                    default:
                        if (request.Parameters.Count < 1)
                        {
                            response = await _httpClient.GetAsync(uri, cancellationToken)
                                            .ConfigureAwait(false);
                        }
                        else
                        {
                            var payload = JsonConvert.SerializeObject(request.Parameters, SerializerSettings);

                            var httpContent = new StringContent(payload, Encoding.UTF8, "application/json");

                            response = await _httpClient.PostAsync(uri, httpContent, cancellationToken)
                                                    .ConfigureAwait(false);
                        }
                        break;
                }

                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e) when (e.Message.Contains("401"))
            {
                _invalidToken = true;
                throw new ApiRequestException("Invalid token", 401, e);
            }
            catch (TaskCanceledException e)
            {
                if (cancellationToken.IsCancellationRequested)
                    throw;

                throw new ApiRequestException("Request timed out", 408, e);
            }
            catch (HttpRequestException e)
                when (e.Message.Contains("400") || e.Message.Contains("403") || e.Message.Contains("409"))
            { }

            if (responseObject == null)
                responseObject = new ApiResponse<T> { Ok = false, Message = "No response received" };

            if (!responseObject.Ok)
                throw ApiRequestException.FromApiResponse(responseObject);

            return responseObject.ResultObject;
        }

        private static HttpContent ConvertParameterValue(object value)
        {
            HttpContent httpContent;

            switch (value)
            {
                case string str: // Prevent escaping back-slash character: "\r\n" should not be "\\r\\n"
                    httpContent = new StringContent(str);
                    break;
                case FileToSend fileToSend:
                    httpContent = new StreamContent(fileToSend.Content);
                    break;
                default:
                    httpContent = new StringContent(JsonConvert.SerializeObject(value, SerializerSettings).Trim('"'));
                    break;
            }

            return httpContent;
        }
        #endregion
    }
}

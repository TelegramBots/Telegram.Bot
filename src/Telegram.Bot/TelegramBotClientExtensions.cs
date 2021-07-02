using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Helpers;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.Payments;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot
{
    /// <summary>
    /// Extension methods that map to requests from Bot API documentation
    /// </summary>
    public static class TelegramBotClientExtensions
    {
        #region Getting updates

        /// <inheritdoc />
        public static async Task<Update[]> GetUpdatesAsync(
            this ITelegramBotClient botClient,
            int? offset = default,
            int? limit = default,
            int? timeout = default,
            IEnumerable<UpdateType>? allowedUpdates = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new GetUpdatesRequest
                {
                    Offset = offset,
                    Limit = limit,
                    Timeout = timeout,
                    AllowedUpdates = allowedUpdates
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task SetWebhookAsync(
            this ITelegramBotClient botClient,
            string url,
            InputFileStream? certificate = default,
            string? ipAddress = default,
            int? maxConnections = default,
            IEnumerable<UpdateType>? allowedUpdates = default,
            bool? dropPendingUpdates = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new SetWebhookRequest(url)
                {
                    Certificate = certificate,
                    MaxConnections = maxConnections,
                    AllowedUpdates = allowedUpdates,
                    IpAddress = ipAddress,
                    DropPendingUpdates = dropPendingUpdates
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task DeleteWebhookAsync(
            this ITelegramBotClient botClient,
            bool? dropPendingUpdates = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new DeleteWebhookRequest { DropPendingUpdates = dropPendingUpdates },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task<WebhookInfo> GetWebhookInfoAsync(
            this ITelegramBotClient botClient,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient))
                .MakeRequestAsync(new GetWebhookInfoRequest(), cancellationToken);

        #endregion Getting updates

        #region Available methods

        /// <inheritdoc />
        public static async Task<User> GetMeAsync(
            this ITelegramBotClient botClient,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient))
                .MakeRequestAsync(new GetMeRequest(), cancellationToken);

        /// <inheritdoc />
        public static async Task LogOutAsync(
            this ITelegramBotClient botClient,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient))
                .MakeRequestAsync(new LogOutRequest(), cancellationToken);

        /// <inheritdoc />
        public static async Task CloseAsync(
            this ITelegramBotClient botClient,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient))
                .MakeRequestAsync(new CloseRequest(), cancellationToken);

        /// <inheritdoc />
        public static async Task<Message> SendTextMessageAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            string text,
            ParseMode? parseMode = default,
            IEnumerable<MessageEntity>? entities = default,
            bool? disableWebPagePreview = default,
            bool? disableNotification = default,
            int? replyToMessageId = default,
            bool? allowSendingWithoutReply = default,
            IReplyMarkup? replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new SendMessageRequest(chatId, text)
                {
                    ParseMode = parseMode,
                    Entities = entities,
                    DisableWebPagePreview = disableWebPagePreview,
                    DisableNotification = disableNotification,
                    ReplyToMessageId = replyToMessageId,
                    AllowSendingWithoutReply = allowSendingWithoutReply,
                    ReplyMarkup = replyMarkup
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task<Message> ForwardMessageAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            ChatId fromChatId,
            int messageId,
            bool? disableNotification = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new ForwardMessageRequest(chatId, fromChatId, messageId)
                {
                    DisableNotification = disableNotification
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task<MessageId> CopyMessageAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            ChatId fromChatId,
            int messageId,
            string? caption = default,
            ParseMode? parseMode = default,
            IEnumerable<MessageEntity>? captionEntities = default,
            int? replyToMessageId = default,
            bool? disableNotification = default,
            IReplyMarkup? replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new CopyMessageRequest(chatId, fromChatId, messageId)
                {
                    Caption = caption,
                    ParseMode = parseMode,
                    CaptionEntities = captionEntities,
                    ReplyToMessageId = replyToMessageId,
                    DisableNotification = disableNotification,
                    ReplyMarkup = replyMarkup
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task<Message> SendPhotoAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            InputOnlineFile photo,
            string? caption = default,
            ParseMode? parseMode = default,
            IEnumerable<MessageEntity>? captionEntities = default,
            bool? disableNotification = default,
            int? replyToMessageId = default,
            bool? allowSendingWithoutReply = default,
            IReplyMarkup? replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new SendPhotoRequest(chatId, photo)
                {
                    Caption = caption,
                    ParseMode = parseMode,
                    CaptionEntities = captionEntities,
                    ReplyToMessageId = replyToMessageId,
                    DisableNotification = disableNotification,
                    AllowSendingWithoutReply = allowSendingWithoutReply,
                    ReplyMarkup = replyMarkup
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task<Message> SendAudioAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            InputOnlineFile audio,
            string? caption = default,
            ParseMode? parseMode = default,
            IEnumerable<MessageEntity>? captionEntities = default,
            int? duration = default,
            string? performer = default,
            string? title = default,
            InputMedia? thumb = default,
            bool? disableNotification = default,
            int? replyToMessageId = default,
            bool? allowSendingWithoutReply = default,
            IReplyMarkup? replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new SendAudioRequest(chatId, audio)
                {
                    Caption = caption,
                    ParseMode = parseMode,
                    CaptionEntities = captionEntities,
                    Duration = duration,
                    Performer = performer,
                    Title = title,
                    Thumb = thumb,
                    DisableNotification = disableNotification,
                    ReplyToMessageId = replyToMessageId,
                    AllowSendingWithoutReply = allowSendingWithoutReply,
                    ReplyMarkup = replyMarkup
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task<Message> SendDocumentAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            InputOnlineFile document,
            InputMedia? thumb = default,
            string? caption = default,
            ParseMode? parseMode = default,
            IEnumerable<MessageEntity>? captionEntities = default,
            bool? disableContentTypeDetection = default,
            bool? disableNotification = default,
            int? replyToMessageId = default,
            bool? allowSendingWithoutReply = default,
            IReplyMarkup? replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new SendDocumentRequest(chatId, document)
                {
                    Caption = caption,
                    Thumb = thumb,
                    ParseMode = parseMode,
                    CaptionEntities = captionEntities,
                    DisableNotification = disableNotification,
                    ReplyToMessageId = replyToMessageId,
                    AllowSendingWithoutReply = allowSendingWithoutReply,
                    ReplyMarkup = replyMarkup,
                    DisableContentTypeDetection = disableContentTypeDetection,
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task<Message> SendStickerAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            InputOnlineFile sticker,
            bool? disableNotification = default,
            int? replyToMessageId = default,
            bool? allowSendingWithoutReply = default,
            IReplyMarkup? replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new SendStickerRequest(chatId, sticker)
                {
                    DisableNotification = disableNotification,
                    ReplyToMessageId = replyToMessageId,
                    AllowSendingWithoutReply = allowSendingWithoutReply,
                    ReplyMarkup = replyMarkup
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task<Message> SendVideoAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            InputOnlineFile video,
            int? duration = default,
            int? width = default,
            int? height = default,
            InputMedia? thumb = default,
            string? caption = default,
            ParseMode? parseMode = default,
            IEnumerable<MessageEntity>? captionEntities = default,
            bool? supportsStreaming = default,
            bool? disableNotification = default,
            int? replyToMessageId = default,
            bool? allowSendingWithoutReply = default,
            IReplyMarkup? replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new SendVideoRequest(chatId, video)
                {
                    Duration = duration,
                    Width = width,
                    Height = height,
                    Thumb = thumb,
                    Caption = caption,
                    ParseMode = parseMode,
                    CaptionEntities = captionEntities,
                    SupportsStreaming = supportsStreaming,
                    DisableNotification = disableNotification,
                    ReplyToMessageId = replyToMessageId,
                    AllowSendingWithoutReply = allowSendingWithoutReply,
                    ReplyMarkup = replyMarkup
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task<Message> SendAnimationAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            InputOnlineFile animation,
            int? duration = default,
            int? width = default,
            int? height = default,
            InputMedia? thumb = default,
            string? caption = default,
            ParseMode? parseMode = default,
            IEnumerable<MessageEntity>? captionEntities = default,
            bool? disableNotification = default,
            int? replyToMessageId = default,
            bool? allowSendingWithoutReply = default,
            IReplyMarkup? replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new SendAnimationRequest(chatId, animation)
                {
                    Duration = duration,
                    Width = width,
                    Height = height,
                    Thumb = thumb,
                    Caption = caption,
                    ParseMode = parseMode,
                    CaptionEntities = captionEntities,
                    DisableNotification = disableNotification,
                    ReplyToMessageId = replyToMessageId,
                    AllowSendingWithoutReply = allowSendingWithoutReply,
                    ReplyMarkup = replyMarkup,
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task<Message> SendVoiceAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            InputOnlineFile voice,
            string? caption = default,
            ParseMode? parseMode = default,
            IEnumerable<MessageEntity>? captionEntities = default,
            int? duration = default,
            bool? disableNotification = default,
            int? replyToMessageId = default,
            bool? allowSendingWithoutReply = default,
            IReplyMarkup? replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new SendVoiceRequest(chatId, voice)
                {
                    Caption = caption,
                    ParseMode = parseMode,
                    CaptionEntities = captionEntities,
                    Duration = duration,
                    DisableNotification = disableNotification,
                    ReplyToMessageId = replyToMessageId,
                    AllowSendingWithoutReply = allowSendingWithoutReply,
                    ReplyMarkup = replyMarkup
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task<Message> SendVideoNoteAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            InputTelegramFile videoNote,
            int? duration = default,
            int? length = default,
            InputMedia? thumb = default,
            bool? disableNotification = default,
            int? replyToMessageId = default,
            bool? allowSendingWithoutReply = default,
            IReplyMarkup? replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new SendVideoNoteRequest(chatId, videoNote)
                {
                    Duration = duration,
                    Length = length,
                    Thumb = thumb,
                    DisableNotification = disableNotification,
                    ReplyToMessageId = replyToMessageId,
                    AllowSendingWithoutReply = allowSendingWithoutReply,
                    ReplyMarkup = replyMarkup
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task<Message[]> SendMediaGroupAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            IEnumerable<IAlbumInputMedia> media,
            bool? disableNotification = default,
            int? replyToMessageId = default,
            bool? allowSendingWithoutReply = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new SendMediaGroupRequest(chatId, media)
                {
                    DisableNotification = disableNotification,
                    ReplyToMessageId = replyToMessageId,
                    AllowSendingWithoutReply = allowSendingWithoutReply,
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task<Message> SendLocationAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            float latitude,
            float longitude,
            int? livePeriod = default,
            int? heading = default,
            int? proximityAlertRadius = default,
            bool? disableNotification = default,
            int? replyToMessageId = default,
            bool? allowSendingWithoutReply = default,
            IReplyMarkup? replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new SendLocationRequest(chatId, latitude, longitude)
                {
                    LivePeriod = livePeriod,
                    DisableNotification = disableNotification,
                    ReplyToMessageId = replyToMessageId,
                    AllowSendingWithoutReply = allowSendingWithoutReply,
                    ReplyMarkup = replyMarkup,
                    Heading = heading,
                    ProximityAlertRadius = proximityAlertRadius
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task<Message> SendVenueAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            float latitude,
            float longitude,
            string title,
            string address,
            string? foursquareId = default,
            string? foursquareType = default,
            string? googlePlaceId = default,
            string? googlePlaceType = default,
            bool? disableNotification = default,
            int? replyToMessageId = default,
            bool? allowSendingWithoutReply = default,
            IReplyMarkup? replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new SendVenueRequest(chatId, latitude, longitude, title, address)
                {
                    FoursquareId = foursquareId,
                    FoursquareType = foursquareType,
                    DisableNotification = disableNotification,
                    ReplyToMessageId = replyToMessageId,
                    AllowSendingWithoutReply = allowSendingWithoutReply,
                    ReplyMarkup = replyMarkup,
                    GooglePlaceId = googlePlaceId,
                    GooglePlaceType = googlePlaceType
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task<Message> SendContactAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            string phoneNumber,
            string firstName,
            string? lastName = default,
            string? vCard = default,
            bool? disableNotification = default,
            int? replyToMessageId = default,
            bool? allowSendingWithoutReply = default,
            IReplyMarkup? replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new SendContactRequest(chatId, phoneNumber, firstName)
                {
                    LastName = lastName,
                    Vcard = vCard,
                    DisableNotification = disableNotification,
                    ReplyToMessageId = replyToMessageId,
                    AllowSendingWithoutReply = allowSendingWithoutReply,
                    ReplyMarkup = replyMarkup
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task<Message> SendPollAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            string question,
            IEnumerable<string> options,
            bool? isAnonymous = default,
            PollType? type = default,
            bool? allowsMultipleAnswers = default,
            int? correctOptionId = default,
            string? explanation = default,
            ParseMode? explanationParseMode = default,
            IEnumerable<MessageEntity>? explanationEntities = default,
            int? openPeriod = default,
            DateTime? closeDate = default,
            bool? isClosed = default,
            bool? disableNotification = default,
            int? replyToMessageId = default,
            bool? allowSendingWithoutReply = default,
            IReplyMarkup? replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new SendPollRequest(chatId, question, options)
                {
                    DisableNotification = disableNotification,
                    ReplyToMessageId = replyToMessageId,
                    AllowSendingWithoutReply = allowSendingWithoutReply,
                    ReplyMarkup = replyMarkup,
                    IsAnonymous = isAnonymous,
                    Type = type,
                    AllowsMultipleAnswers = allowsMultipleAnswers,
                    CorrectOptionId = correctOptionId,
                    IsClosed = isClosed,
                    OpenPeriod = openPeriod,
                    CloseDate = closeDate,
                    Explanation = explanation,
                    ExplanationParseMode = explanationParseMode,
                    ExplanationEntities = explanationEntities,
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task<Message> SendDiceAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            Emoji? emoji = default,
            bool? disableNotification = default,
            int? replyToMessageId = default,
            bool? allowSendingWithoutReply = default,
            IReplyMarkup? replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new SendDiceRequest(chatId)
                {
                    DisableNotification = disableNotification,
                    ReplyToMessageId = replyToMessageId,
                    AllowSendingWithoutReply = allowSendingWithoutReply,
                    ReplyMarkup = replyMarkup,
                    Emoji = emoji
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task SendChatActionAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            ChatAction chatAction,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient))
                .MakeRequestAsync(new SendChatActionRequest(chatId, chatAction), cancellationToken);

        /// <inheritdoc />
        public static async Task<UserProfilePhotos> GetUserProfilePhotosAsync(
            this ITelegramBotClient botClient,
            long userId,
            int? offset = default,
            int? limit = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new GetUserProfilePhotosRequest(userId)
                {
                    Offset = offset,
                    Limit = limit
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task<File> GetFileAsync(
            this ITelegramBotClient botClient,
            string fileId,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient))
                .MakeRequestAsync(new GetFileRequest(fileId), cancellationToken);

        /// <inheritdoc />
        public static async Task BanChatMemberAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            long userId,
            DateTime? untilDate = default,
            bool? revokeMessages = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new BanChatMemberRequest(chatId, userId)
                {
                    UntilDate = untilDate,
                    RevokeMessages = revokeMessages
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task KickChatMemberAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            long userId,
            DateTime? untilDate = default,
            bool? revokeMessages = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new KickChatMemberRequest(chatId, userId)
                {
                    UntilDate = untilDate,
                    RevokeMessages = revokeMessages
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task LeaveChatAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient))
                .MakeRequestAsync(new LeaveChatRequest(chatId), cancellationToken);

        /// <inheritdoc />
        public static async Task UnbanChatMemberAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            long userId,
            bool? onlyIfBanned = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new UnbanChatMemberRequest(chatId, userId) { OnlyIfBanned = onlyIfBanned },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task<Chat> GetChatAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient))
                .MakeRequestAsync(new GetChatRequest(chatId), cancellationToken);

        /// <inheritdoc />
        public static async Task<ChatMember[]> GetChatAdministratorsAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient))
                .MakeRequestAsync(new GetChatAdministratorsRequest(chatId), cancellationToken);

        /// <inheritdoc />
        public static async Task<int> GetChatMembersCountAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient))
                .MakeRequestAsync(new GetChatMembersCountRequest(chatId), cancellationToken);

        /// <inheritdoc />
        public static async Task<int> GetChatMemberCountAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient))
                .MakeRequestAsync(new GetChatMemberCountRequest(chatId), cancellationToken);

        /// <inheritdoc />
        public static async Task<ChatMember> GetChatMemberAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            long userId,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient))
                .MakeRequestAsync(new GetChatMemberRequest(chatId, userId), cancellationToken);

        /// <inheritdoc />
        public static async Task AnswerCallbackQueryAsync(
            this ITelegramBotClient botClient,
            string callbackQueryId,
            string? text = default,
            bool? showAlert = default,
            string? url = default,
            int? cacheTime = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new AnswerCallbackQueryRequest(callbackQueryId)
            {
                    Text = text,
                    ShowAlert = showAlert,
                    Url = url,
                    CacheTime = cacheTime
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task RestrictChatMemberAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            long userId,
            ChatPermissions permissions,
            DateTime? untilDate = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new RestrictChatMemberRequest(chatId, userId, permissions)
                {
                    UntilDate = untilDate
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task PromoteChatMemberAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            long userId,
            bool? isAnonymous = default,
            bool? canManageChat = default,
            bool? canChangeInfo = default,
            bool? canPostMessages = default,
            bool? canEditMessages = default,
            bool? canDeleteMessages = default,
            bool? canManageVoiceChats = default,
            bool? canInviteUsers = default,
            bool? canRestrictMembers = default,
            bool? canPinMessages = default,
            bool? canPromoteMembers = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new PromoteChatMemberRequest(chatId, userId)
                {
                    IsAnonymous = isAnonymous,
                    CanChangeInfo = canChangeInfo,
                    CanPostMessages = canPostMessages,
                    CanEditMessages = canEditMessages,
                    CanDeleteMessages = canDeleteMessages,
                    CanInviteUsers = canInviteUsers,
                    CanRestrictMembers = canRestrictMembers,
                    CanPinMessages = canPinMessages,
                    CanPromoteMembers = canPromoteMembers,
                    CanManageChat = canManageChat,
                    CanManageVoiceChat = canManageVoiceChats
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task SetChatAdministratorCustomTitleAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            long userId,
            string customTitle,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new SetChatAdministratorCustomTitleRequest(chatId, userId, customTitle),
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task SetChatPermissionsAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            ChatPermissions permissions,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new SetChatPermissionsRequest(chatId, permissions),
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task<BotCommand[]> GetMyCommandsAsync(
            this ITelegramBotClient botClient,
            BotCommandScope? scope = default,
            string? languageCode = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new GetMyCommandsRequest
                {
                    Scope = scope,
                    LanguageCode = languageCode
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task SetMyCommandsAsync(
            this ITelegramBotClient botClient,
            IEnumerable<BotCommand> commands,
            BotCommandScope? scope = default,
            string? languageCode = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new SetMyCommandsRequest(commands)
                {
                    Scope = scope,
                    LanguageCode = languageCode
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task DeleteMyCommandsAsync(
            this ITelegramBotClient botClient,
            BotCommandScope? scope = default,
            string? languageCode = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new DeleteMyCommandsRequest
                {
                    Scope = scope,
                    LanguageCode = languageCode
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task<Message> StopMessageLiveLocationAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            int messageId,
            InlineKeyboardMarkup? replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new StopMessageLiveLocationRequest(chatId, messageId)
                {
                    ReplyMarkup = replyMarkup
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task StopMessageLiveLocationAsync(
            this ITelegramBotClient botClient,
            string inlineMessageId,
            InlineKeyboardMarkup? replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new StopInlineMessageLiveLocationRequest(inlineMessageId)
                {
                    ReplyMarkup = replyMarkup
                },
                cancellationToken
            );

        #endregion Available methods

        #region Updating messages

        /// <inheritdoc />
        public static async Task<Message> EditMessageTextAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            int messageId,
            string text,
            ParseMode? parseMode = default,
            IEnumerable<MessageEntity>? entities = default,
            bool? disableWebPagePreview = default,
            InlineKeyboardMarkup? replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new EditMessageTextRequest(chatId, messageId, text)
                {
                    ParseMode = parseMode,
                    Entities = entities,
                    DisableWebPagePreview = disableWebPagePreview,
                    ReplyMarkup = replyMarkup
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task EditMessageTextAsync(
            this ITelegramBotClient botClient,
            string inlineMessageId,
            string text,
            ParseMode? parseMode = default,
            IEnumerable<MessageEntity>? entities = default,
            bool? disableWebPagePreview = default,
            InlineKeyboardMarkup? replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new EditInlineMessageTextRequest(inlineMessageId, text)
                {
                    DisableWebPagePreview = disableWebPagePreview,
                    ReplyMarkup = replyMarkup,
                    ParseMode = parseMode,
                    Entities = entities
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task<Message> EditMessageCaptionAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            int messageId,
            string caption,
            ParseMode? parseMode = default,
            IEnumerable<MessageEntity>? captionEntities = default,
            InlineKeyboardMarkup? replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new EditMessageCaptionRequest(chatId, messageId)
                {
                    Caption = caption,
                    ParseMode = parseMode,
                    CaptionEntities = captionEntities,
                    ReplyMarkup = replyMarkup
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task EditMessageCaptionAsync(
            this ITelegramBotClient botClient,
            string inlineMessageId,
            string caption,
            ParseMode? parseMode = default,
            IEnumerable<MessageEntity>? captionEntities = default,
            InlineKeyboardMarkup? replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new EditInlineMessageCaptionRequest(inlineMessageId)
                {
                    Caption = caption,
                    ParseMode = parseMode,
                    CaptionEntities = captionEntities,
                    ReplyMarkup = replyMarkup
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task<Message> EditMessageMediaAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            int messageId,
            InputMediaBase media,
            InlineKeyboardMarkup? replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new EditMessageMediaRequest(chatId, messageId, media)
                {
                    ReplyMarkup = replyMarkup
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task EditMessageMediaAsync(
            this ITelegramBotClient botClient,
            string inlineMessageId,
            InputMediaBase media,
            InlineKeyboardMarkup? replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new EditInlineMessageMediaRequest(inlineMessageId, media)
                {
                    ReplyMarkup = replyMarkup
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task<Message> EditMessageReplyMarkupAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            int messageId,
            InlineKeyboardMarkup? replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new EditMessageReplyMarkupRequest(chatId, messageId) { ReplyMarkup = replyMarkup },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task EditMessageReplyMarkupAsync(
            this ITelegramBotClient botClient,
            string inlineMessageId,
            InlineKeyboardMarkup? replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new EditInlineMessageReplyMarkupRequest(inlineMessageId) { ReplyMarkup = replyMarkup },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task<Message> EditMessageLiveLocationAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            int messageId,
            float latitude,
            float longitude,
            float? horizontalAccuracy = default,
            int? heading = default,
            int? proximityAlertRadius = default,
            InlineKeyboardMarkup? replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new EditMessageLiveLocationRequest(chatId, messageId, latitude, longitude)
                {
                    ReplyMarkup = replyMarkup,
                    HorizontalAccuracy = horizontalAccuracy,
                    Heading = heading,
                    ProximityAlertRadius = proximityAlertRadius
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task EditMessageLiveLocationAsync(
            this ITelegramBotClient botClient,
            string inlineMessageId,
            float latitude,
            float longitude,
            float? horizontalAccuracy = default,
            int? heading = default,
            int? proximityAlertRadius = default,
            InlineKeyboardMarkup? replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new EditInlineMessageLiveLocationRequest(inlineMessageId, latitude, longitude)
                {
                    ReplyMarkup = replyMarkup,
                    HorizontalAccuracy = horizontalAccuracy,
                    Heading = heading,
                    ProximityAlertRadius = proximityAlertRadius
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task<Poll> StopPollAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            int messageId,
            InlineKeyboardMarkup? replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new StopPollRequest(chatId, messageId)
                {
                    ReplyMarkup = replyMarkup
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task DeleteMessageAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            int messageId,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient))
                .MakeRequestAsync(new DeleteMessageRequest(chatId, messageId), cancellationToken);

        #endregion Updating messages

        #region Inline mode

        /// <inheritdoc />
        public static async Task AnswerInlineQueryAsync(
            this ITelegramBotClient botClient,
            string inlineQueryId,
            IEnumerable<InlineQueryResultBase> results,
            int? cacheTime = default,
            bool? isPersonal = default,
            string? nextOffset = default,
            string? switchPmText = default,
            string? switchPmParameter = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new AnswerInlineQueryRequest(inlineQueryId, results)
                {
                    CacheTime = cacheTime,
                    IsPersonal = isPersonal,
                    NextOffset = nextOffset,
                    SwitchPmText = switchPmText,
                    SwitchPmParameter = switchPmParameter
                },
                cancellationToken
            );

        # endregion Inline mode

        #region Payments

        /// <inheritdoc />
        public static async Task<Message> SendInvoiceAsync(
            this ITelegramBotClient botClient,
            long chatId,
            string title,
            string description,
            string payload,
            string providerToken,
            string currency,
            IEnumerable<LabeledPrice> prices,
            int? maxTipAmount = default,
            int[] suggestedTipAmounts = default,
            string? startParameter = default,
            string? providerData = default,
            string? photoUrl = default,
            int? photoSize = default,
            int? photoWidth = default,
            int? photoHeight = default,
            bool? needName = default,
            bool? needPhoneNumber = default,
            bool? needEmail = default,
            bool? needShippingAddress = default,
            bool? sendPhoneNumberToProvider = default,
            bool? sendEmailToProvider = default,
            bool? isFlexible = default,
            bool? disableNotification = default,
            int? replyToMessageId = default,
            bool? allowSendingWithoutReply = default,
            InlineKeyboardMarkup? replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new SendInvoiceRequest(
                    chatId,
                    title,
                    description,
                    payload,
                    providerToken,
                    currency,
                    prices)
                {
                    MaxTipAmount = maxTipAmount,
                    SuggestedTipAmounts = suggestedTipAmounts,
                    StartParameter = startParameter,
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
                    AllowSendingWithoutReply = allowSendingWithoutReply,
                    ReplyMarkup = replyMarkup
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task AnswerShippingQueryAsync(
            this ITelegramBotClient botClient,
            string shippingQueryId,
            IEnumerable<ShippingOption> shippingOptions,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new AnswerShippingQueryRequest(shippingQueryId, shippingOptions),
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task AnswerShippingQueryAsync(
            this ITelegramBotClient botClient,
            string shippingQueryId,
            string errorMessage,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new AnswerShippingQueryRequest(shippingQueryId, errorMessage),
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task AnswerPreCheckoutQueryAsync(
            this ITelegramBotClient botClient,
            string preCheckoutQueryId,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new AnswerPreCheckoutQueryRequest(preCheckoutQueryId),
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task AnswerPreCheckoutQueryAsync(
            this ITelegramBotClient botClient,
            string preCheckoutQueryId,
            string errorMessage,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new AnswerPreCheckoutQueryRequest(preCheckoutQueryId, errorMessage),
                cancellationToken
            );

        #endregion Payments

        #region Games

        /// <inheritdoc />
        public static async Task<Message> SendGameAsync(
            this ITelegramBotClient botClient,
            long chatId,
            string gameShortName,
            bool? disableNotification = default,
            int? replyToMessageId = default,
            bool? allowSendingWithoutReply = default,
            InlineKeyboardMarkup? replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new SendGameRequest(chatId, gameShortName)
                {
                    DisableNotification = disableNotification,
                    ReplyToMessageId = replyToMessageId,
                    AllowSendingWithoutReply = allowSendingWithoutReply,
                    ReplyMarkup = replyMarkup
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task<Message> SetGameScoreAsync(
            this ITelegramBotClient botClient,
            long userId,
            int score,
            long chatId,
            int messageId,
            bool? force = default,
            bool? disableEditMessage = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new SetGameScoreRequest(userId, score, chatId, messageId)
                {
                    Force = force,
                    DisableEditMessage = disableEditMessage
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task SetGameScoreAsync(
            this ITelegramBotClient botClient,
            long userId,
            int score,
            string inlineMessageId,
            bool? force = default,
            bool? disableEditMessage = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new SetInlineGameScoreRequest(userId, score, inlineMessageId)
                {
                    Force = force,
                    DisableEditMessage = disableEditMessage
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task<GameHighScore[]> GetGameHighScoresAsync(
            this ITelegramBotClient botClient,
            long userId,
            long chatId,
            int messageId,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new GetGameHighScoresRequest(userId, chatId, messageId),
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task<GameHighScore[]> GetGameHighScoresAsync(
            this ITelegramBotClient botClient,
            long userId,
            string inlineMessageId,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new GetInlineGameHighScoresRequest(userId, inlineMessageId),
                cancellationToken
            );

        #endregion Games

        #region Group and channel management

        /// <inheritdoc />
        public static async Task<string> ExportChatInviteLinkAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient))
                .MakeRequestAsync(new ExportChatInviteLinkRequest(chatId), cancellationToken);

        /// <inheritdoc />
        public static async Task SetChatPhotoAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            InputFileStream photo,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient))
                .MakeRequestAsync(new SetChatPhotoRequest(chatId, photo), cancellationToken);

        /// <inheritdoc />
        public static async Task DeleteChatPhotoAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient))
                .MakeRequestAsync(new DeleteChatPhotoRequest(chatId), cancellationToken);

        /// <inheritdoc />
        public static async Task SetChatTitleAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            string title,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient))
                .MakeRequestAsync(new SetChatTitleRequest(chatId, title), cancellationToken);

        /// <inheritdoc />
        public static async Task SetChatDescriptionAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            string? description = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new SetChatDescriptionRequest(chatId) { Description = description },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task PinChatMessageAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            int messageId,
            bool? disableNotification = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new PinChatMessageRequest(chatId, messageId)
                {
                    DisableNotification = disableNotification
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task UnpinChatMessageAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            int? messageId = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new UnpinChatMessageRequest(chatId) { MessageId = messageId },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task UnpinAllChatMessages(
            this ITelegramBotClient botClient,
            ChatId chatId,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient))
                .MakeRequestAsync(new UnpinAllChatMessagesRequest(chatId), cancellationToken);

        /// <inheritdoc />
        public static async Task SetChatStickerSetAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            string stickerSetName,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new SetChatStickerSetRequest(chatId, stickerSetName),
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task DeleteChatStickerSetAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient))
                .MakeRequestAsync(new DeleteChatStickerSetRequest(chatId), cancellationToken);

        /// <inheritdoc />
        public static async Task<ChatInviteLink> CreateChatInviteLinkAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            DateTime? expireDate = default,
            int? memberLimit = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new CreateChatInviteLinkRequest(chatId)
                {
                    ExpireDate = expireDate,
                    MemberLimit = memberLimit
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task<ChatInviteLink> EditChatInviteLinkAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            string inviteLink,
            DateTime? expireDate = default,
            int? memberLimit = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new EditChatInviteLinkRequest(chatId, inviteLink)
                {
                    ExpireDate = expireDate,
                    MemberLimit = memberLimit
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task<ChatInviteLink> RevokeChatInviteLinkAsync(
            this ITelegramBotClient botClient,
            ChatId chatId,
            string inviteLink,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new RevokeChatInviteLinkRequest(chatId, inviteLink),
                cancellationToken
            );

        #endregion

        #region Stickers

        /// <inheritdoc />
        public static async Task<StickerSet> GetStickerSetAsync(
            this ITelegramBotClient botClient,
            string name,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient))
                .MakeRequestAsync(new GetStickerSetRequest(name), cancellationToken);

        /// <inheritdoc />
        public static async Task<File> UploadStickerFileAsync(
            this ITelegramBotClient botClient,
            long userId,
            InputFileStream pngSticker,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient))
                .MakeRequestAsync(new UploadStickerFileRequest(userId, pngSticker), cancellationToken);

        /// <inheritdoc />
        public static async Task CreateNewStickerSetAsync(
            this ITelegramBotClient botClient,
            long userId,
            string name,
            string title,
            InputOnlineFile pngSticker,
            string emojis,
            bool? isMasks = default,
            MaskPosition? maskPosition = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new CreateNewStickerSetRequest(userId, name, title, pngSticker, emojis)
                {
                    ContainsMasks = isMasks,
                    MaskPosition = maskPosition
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task AddStickerToSetAsync(
            this ITelegramBotClient botClient,
            long userId,
            string name,
            InputOnlineFile pngSticker,
            string emojis,
            MaskPosition? maskPosition = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new AddStickerToSetRequest(userId, name, pngSticker, emojis)
                {
                    MaskPosition = maskPosition
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task CreateNewAnimatedStickerSetAsync(
            this ITelegramBotClient botClient,
            long userId,
            string name,
            string title,
            InputFileStream tgsSticker,
            string emojis,
            bool? isMasks = default,
            MaskPosition? maskPosition = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new CreateNewAnimatedStickerSetRequest(userId, name, title, tgsSticker, emojis)
                {
                    ContainsMasks = isMasks,
                    MaskPosition = maskPosition
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task AddAnimatedStickerToSetAsync(
            this ITelegramBotClient botClient,
            long userId,
            string name,
            InputFileStream tgsSticker,
            string emojis,
            MaskPosition? maskPosition = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new AddAnimatedStickerToSetRequest(userId, name, tgsSticker, emojis)
                {
                    MaskPosition = maskPosition
                },
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task SetStickerPositionInSetAsync(
            this ITelegramBotClient botClient,
            string sticker,
            int position,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new SetStickerPositionInSetRequest(sticker, position),
                cancellationToken
            );

        /// <inheritdoc />
        public static async Task DeleteStickerFromSetAsync(
            this ITelegramBotClient botClient,
            string sticker,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient))
                .MakeRequestAsync(new DeleteStickerFromSetRequest(sticker), cancellationToken);

        /// <inheritdoc />
        public static async Task SetStickerSetThumbAsync(
            this ITelegramBotClient botClient,
            string name,
            long userId,
            InputOnlineFile? thumb = default,
            CancellationToken cancellationToken = default
        ) =>
            await botClient.ThrowIfNull(nameof(botClient)).MakeRequestAsync(
                new SetStickerSetThumbRequest(name, userId) { Thumb = thumb },
                cancellationToken
            );

        #endregion
    }
}

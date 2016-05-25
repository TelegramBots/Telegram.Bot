using System;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;

using File = Telegram.Bot.Types.File;

namespace Telegram.Bot
{
#pragma warning disable CS1591
    public partial class TelegramBotClient {
        private const bool GenerateErrorOnUsage = false;

        #region Support Methods - Public

        [Obsolete("TestApi is deprecated, please use TestApiAsync", GenerateErrorOnUsage)]
        public Task<bool> TestApi() => TestApiAsync();

        #endregion

        #region API Methods - General

        [Obsolete("GetMe is deprecated, please use GetMeAsync", GenerateErrorOnUsage)]
        public Task<User> GetMe() => GetMeAsync();

        [Obsolete("GetUpdates is deprecated, please use GetUpdatesAsync", GenerateErrorOnUsage)]
        public Task<Update[]> GetUpdates(int offset = 0, int limit = 100, int timeout = 0)
            => GetUpdatesAsync(offset, limit, timeout);

        [Obsolete("SetWebhook is deprecated, please use SetWebhookAsync", GenerateErrorOnUsage)]
        public Task SetWebhook(string url = "", FileToSend? certificate = null)
            => SetWebhookAsync(url, certificate);

        #endregion

        #region API Methods - SendMessages

        [Obsolete("SendTextMessage is deprecated, please use SendTextMessageAsync", GenerateErrorOnUsage)]
        public Task<Message> SendTextMessage(long chatId, string text, bool disableWebPagePreview = false,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null,
            ParseMode parseMode = ParseMode.Default)
            => SendTextMessageAsync(chatId, text, disableWebPagePreview, disableNotification, replyToMessageId, replyMarkup, parseMode);

        [Obsolete("SendTextMessage is deprecated, please use SendTextMessageAsync", GenerateErrorOnUsage)]
        public Task<Message> SendTextMessage(string chatId, string text, bool disableWebPagePreview = false,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null,
            ParseMode parseMode = ParseMode.Default)
            => SendTextMessageAsync(chatId, text, disableWebPagePreview, disableNotification, replyToMessageId, replyMarkup, parseMode);

        [Obsolete("ForwardMessage is deprecated, please use ForwardMessageAsync", GenerateErrorOnUsage)]
        public Task<Message> ForwardMessage(long chatId, long fromChatId, int messageId, bool disableNotification = false)
            => ForwardMessageAsync(chatId, fromChatId, messageId, disableNotification);

        [Obsolete("ForwardMessage is deprecated, please use ForwardMessageAsync", GenerateErrorOnUsage)]
        public Task<Message> ForwardMessage(long chatId, string fromChatId, int messageId, bool disableNotification = false)
            => ForwardMessageAsync(chatId, fromChatId, messageId, disableNotification);

        [Obsolete("ForwardMessage is deprecated, please use ForwardMessageAsync", GenerateErrorOnUsage)]
        public Task<Message> ForwardMessage(string chatId, long fromChatId, int messageId, bool disableNotification = false)
            => ForwardMessageAsync(chatId, fromChatId, messageId, disableNotification);

        [Obsolete("ForwardMessage is deprecated, please use ForwardMessageAsync", GenerateErrorOnUsage)]
        public Task<Message> ForwardMessage(string chatId, string fromChatId, int messageId, bool disableNotification = false)
            => ForwardMessageAsync(chatId, fromChatId, messageId, disableNotification);

        [Obsolete("SendPhoto is deprecated, please use SendPhotoAsync", GenerateErrorOnUsage)]
        public Task<Message> SendPhoto(long chatId, FileToSend photo, string caption = "",
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null)
            => SendPhotoAsync(chatId, photo, caption, disableNotification, replyToMessageId, replyMarkup);

        [Obsolete("SendPhoto is deprecated, please use SendPhotoAsync", GenerateErrorOnUsage)]
        public Task<Message> SendPhoto(string chatId, FileToSend photo, string caption = "",
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null)
            => SendPhotoAsync(chatId, photo, caption, disableNotification, replyToMessageId, replyMarkup);

        [Obsolete("SendPhoto is deprecated, please use SendPhotoAsync", GenerateErrorOnUsage)]
        public Task<Message> SendPhoto(long chatId, string photo, string caption = "",
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null)
            => SendPhotoAsync(chatId, photo, caption, disableNotification, replyToMessageId, replyMarkup);

        public Task<Message> SendPhoto(string chatId, string photo, string caption = "",
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null)
            => SendPhotoAsync(chatId, photo, caption, disableNotification, replyToMessageId, replyMarkup);

        [Obsolete("SendAudio is deprecated, please use SendAudioAsync", GenerateErrorOnUsage)]
        public Task<Message> SendAudio(long chatId, FileToSend audio, int duration, string performer, string title,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null)
            => SendAudioAsync(chatId, audio, duration, performer, title, disableNotification, replyToMessageId, replyMarkup);

        [Obsolete("SendAudio is deprecated, please use SendAudioAsync", GenerateErrorOnUsage)]
        public Task<Message> SendAudio(string chatId, FileToSend audio, int duration, string performer, string title,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null)
            => SendAudioAsync(chatId, audio, duration, performer, title, disableNotification, replyToMessageId, replyMarkup);

        [Obsolete("SendAudio is deprecated, please use SendAudioAsync", GenerateErrorOnUsage)]
        public Task<Message> SendAudio(long chatId, string audio, int duration, string performer, string title,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null)
            => SendAudioAsync(chatId, audio, duration, performer, title, disableNotification, replyToMessageId, replyMarkup);

        [Obsolete("SendAudio is deprecated, please use SendAudioAsync", GenerateErrorOnUsage)]
        public Task<Message> SendAudio(string chatId, string audio, int duration, string performer, string title,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null)
            => SendAudioAsync(chatId, audio, duration, performer, title, disableNotification, replyToMessageId, replyMarkup);

        [Obsolete("SendContact is deprecated, please use SendContactAsync", GenerateErrorOnUsage)]
        public Task<Message> SendContact(long chatId, string phoneNumber, string firstName, string lastName = null,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null)
            => SendContactAsync(chatId, phoneNumber, firstName, lastName, disableNotification, replyToMessageId, replyMarkup);

        [Obsolete("SendContact is deprecated, please use SendContactAsync", GenerateErrorOnUsage)]
        public Task<Message> SendContact(string chatId, string phoneNumber, string firstName, string lastName = null,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null)
            => SendContactAsync(chatId, phoneNumber, firstName, lastName, disableNotification, replyToMessageId, replyMarkup);

        [Obsolete("SendDocument is deprecated, please use SendDocumentAsync", GenerateErrorOnUsage)]
        public Task<Message> SendDocument(long chatId, FileToSend document, string caption = "",
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null)
            => SendDocumentAsync(chatId, document, caption, disableNotification, replyToMessageId, replyMarkup);

        [Obsolete("SendDocument is deprecated, please use SendDocumentAsync", GenerateErrorOnUsage)]
        public Task<Message> SendDocument(string chatId, FileToSend document, string caption = "",
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null)
            => SendDocumentAsync(chatId, document, caption, disableNotification, replyToMessageId, replyMarkup);

        [Obsolete("SendDocument is deprecated, please use SendDocumentAsync", GenerateErrorOnUsage)]
        public Task<Message> SendDocument(long chatId, string document, string caption = "",
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null)
            => SendDocumentAsync(chatId, document, caption, disableNotification, replyToMessageId, replyMarkup);

        [Obsolete("SendDocument is deprecated, please use SendDocumentAsync", GenerateErrorOnUsage)]
        public Task<Message> SendDocument(string chatId, string document, string caption = "",
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null)
            => SendDocumentAsync(chatId, document, caption, disableNotification, replyToMessageId, replyMarkup);

        [Obsolete("SendSticker is deprecated, please use SendStickerAsync", GenerateErrorOnUsage)]
        public Task<Message> SendSticker(long chatId, FileToSend sticker,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null)
            => SendStickerAsync(chatId, sticker, disableNotification, replyToMessageId, replyMarkup);

        [Obsolete("SendSticker is deprecated, please use SendStickerAsync", GenerateErrorOnUsage)]
        public Task<Message> SendSticker(string chatId, FileToSend sticker,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null)
            => SendStickerAsync(chatId, sticker, disableNotification, replyToMessageId, replyMarkup);

        [Obsolete("SendSticker is deprecated, please use SendStickerAsync", GenerateErrorOnUsage)]
        public Task<Message> SendSticker(long chatId, string sticker,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null)
            => SendStickerAsync(chatId, sticker, disableNotification, replyToMessageId, replyMarkup);

        [Obsolete("SendSticker is deprecated, please use SendStickerAsync", GenerateErrorOnUsage)]
        public Task<Message> SendSticker(string chatId, string sticker,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null)
            => SendStickerAsync(chatId, sticker, disableNotification, replyToMessageId, replyMarkup);

        [Obsolete("SendVenue is deprecated, please use SendVenueAsync", GenerateErrorOnUsage)]
        public Task<Message> SendVenue(long chatId, float latitude, float longitude, string title, string address,
            string foursquareId = null,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null)
            => SendVenueAsync(chatId, latitude, longitude, title, address, foursquareId, disableNotification, replyToMessageId, replyMarkup);

        [Obsolete("SendVenue is deprecated, please use SendVenueAsync", GenerateErrorOnUsage)]
        public Task<Message> SendVenue(string chatId, float latitude, float longitude, string title, string address,
            string foursquareId = null,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null)
            => SendVenueAsync(chatId, latitude, longitude, title, address, foursquareId, disableNotification, replyToMessageId, replyMarkup);

        [Obsolete("SendVideo is deprecated, please use SendVideoAsync", GenerateErrorOnUsage)]
        public Task<Message> SendVideo(long chatId, FileToSend video, int duration = 0, string caption = "",
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null)
            => SendVideoAsync(chatId, video, duration, caption, disableNotification, replyToMessageId, replyMarkup);

        [Obsolete("SendVideo is deprecated, please use SendVideoAsync", GenerateErrorOnUsage)]
        public Task<Message> SendVideo(string chatId, FileToSend video, int duration = 0, string caption = "",
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null)
            => SendVideoAsync(chatId, video, duration, caption, disableNotification, replyToMessageId, replyMarkup);

        [Obsolete("SendVideo is deprecated, please use SendVideoAsync", GenerateErrorOnUsage)]
        public Task<Message> SendVideo(long chatId, string video, int duration = 0, string caption = "",
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null)
            => SendVideoAsync(chatId, video, duration, caption, disableNotification, replyToMessageId, replyMarkup);

        [Obsolete("SendVideo is deprecated, please use SendVideoAsync", GenerateErrorOnUsage)]
        public Task<Message> SendVideo(string chatId, string video, int duration = 0, string caption = "",
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null)
            => SendVideoAsync(chatId, video, duration, caption, disableNotification, replyToMessageId, replyMarkup);

        [Obsolete("SendVoice is deprecated, please use SendVoiceAsync", GenerateErrorOnUsage)]
        public Task<Message> SendVoice(long chatId, FileToSend audio, int duration = 0,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null)
            => SendVoiceAsync(chatId, audio, duration, disableNotification, replyToMessageId, replyMarkup);

        [Obsolete("SendVoice is deprecated, please use SendVoiceAsync", GenerateErrorOnUsage)]
        public Task<Message> SendVoice(string chatId, FileToSend audio, int duration = 0,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null)
            => SendVoiceAsync(chatId, audio, duration, disableNotification, replyToMessageId, replyMarkup);

        [Obsolete("SendVoice is deprecated, please use SendVoiceAsync", GenerateErrorOnUsage)]
        public Task<Message> SendVoice(long chatId, string audio, int duration = 0,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null)
            => SendVoiceAsync(chatId, audio, duration, disableNotification, replyToMessageId, replyMarkup);

        [Obsolete("SendVoice is deprecated, please use SendVoiceAsync", GenerateErrorOnUsage)]
        public Task<Message> SendVoice(string chatId, string audio, int duration = 0,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null)
            => SendVoiceAsync(chatId, audio, duration, disableNotification, replyToMessageId, replyMarkup);

        [Obsolete("SendLocation is deprecated, please use SendLocationAsync", GenerateErrorOnUsage)]
        public Task<Message> SendLocation(long chatId, float latitude, float longitude,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null)
            => SendLocationAsync(chatId, latitude, longitude, disableNotification, replyToMessageId, replyMarkup);

        [Obsolete("SendLocation is deprecated, please use SendLocationAsync", GenerateErrorOnUsage)]
        public Task<Message> SendLocation(string chatId, float latitude, float longitude,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null)
            => SendLocationAsync(chatId, latitude, longitude, disableNotification, replyToMessageId, replyMarkup);

        [Obsolete("SendChatAction is deprecated, please use SendChatActionAsync", GenerateErrorOnUsage)]
        public Task SendChatAction(long chatId, ChatAction chatAction) => SendChatActionAsync(chatId, chatAction);

        [Obsolete("SendChatAction is deprecated, please use SendChatActionAsync", GenerateErrorOnUsage)]
        public Task SendChatAction(string chatId, ChatAction chatAction) => SendChatActionAsync(chatId, chatAction);

        #endregion

        #region API Methods - Administration

        [Obsolete("GetChatAdministrators is deprecated, please use GetChatAdministratorsAsync", GenerateErrorOnUsage)]
        public Task<ChatMember[]> GetChatAdministrators(long chatId) => GetChatAdministratorsAsync(chatId);

        [Obsolete("GetChatAdministrators is deprecated, please use GetChatAdministratorsAsync", GenerateErrorOnUsage)]
        public Task<ChatMember[]> GetChatAdministrators(string chatId) => GetChatAdministratorsAsync(chatId);

        [Obsolete("GetChatMembersCount is deprecated, please use GetChatMembersCountAsync", GenerateErrorOnUsage)]
        public Task<int> GetChatMembersCount(long chatId) => GetChatMembersCountAsync(chatId);

        [Obsolete("GetChatMembersCount is deprecated, please use GetChatMembersCountAsync", GenerateErrorOnUsage)]
        public Task<int> GetChatMembersCount(string chatId) => GetChatMembersCountAsync(chatId);

        [Obsolete("GetChatMember is deprecated, please use GetChatMemberAsync", GenerateErrorOnUsage)]
        public Task<ChatMember> GetChatMember(long chatId, int userId) => GetChatMemberAsync(chatId, userId);

        [Obsolete("GetChatMember is deprecated, please use GetChatMemberAsync", GenerateErrorOnUsage)]
        public Task<ChatMember> GetChatMember(string chatId, int userId) => GetChatMemberAsync(chatId, userId);

        [Obsolete("GetChat is deprecated, please use GetChatAsync", GenerateErrorOnUsage)]
        public Task<Chat> GetChat(long chatId) => GetChatAsync(chatId);

        [Obsolete("GetChat is deprecated, please use GetChatAsync", GenerateErrorOnUsage)]
        public Task<Chat> GetChat(string chatId) => GetChatAsync(chatId);

        [Obsolete("LeaveChat is deprecated, please use LeaveChatAsync", GenerateErrorOnUsage)]
        public Task<bool> LeaveChat(long chatId) => LeaveChatAsync(chatId);

        [Obsolete("LeaveChat is deprecated, please use LeaveChatAsync", GenerateErrorOnUsage)]
        public Task<bool> LeaveChat(string chatId) => LeaveChatAsync(chatId);

        [Obsolete("KickChatMember is deprecated, please use KickChatMemberAsync", GenerateErrorOnUsage)]
        public Task<bool> KickChatMember(long chatId, int userId) => KickChatMemberAsync(chatId, userId);

        [Obsolete("KickChatMember is deprecated, please use KickChatMemberAsync", GenerateErrorOnUsage)]
        public Task<bool> KickChatMember(string chatId, int userId) => KickChatMemberAsync(chatId, userId);

        [Obsolete("UnbanChatMember is deprecated, please use UnbanChatMemberAsync", GenerateErrorOnUsage)]
        public Task<bool> UnbanChatMember(long chatId, int userId) => UnbanChatMemberAsync(chatId, userId);

        [Obsolete("UnbanChatMember is deprecated, please use UnbanChatMemberAsync", GenerateErrorOnUsage)]
        public Task<bool> UnbanChatMember(string chatId, int userId) => UnbanChatMemberAsync(chatId, userId);

        #endregion

        #region API Methods - Download Content

        [Obsolete("GetUserProfilePhotos is deprecated, please use GetUserProfilePhotosAsync", GenerateErrorOnUsage)]
        public Task<UserProfilePhotos> GetUserProfilePhotos(int userId, int? offset = null, int limit = 100)
            => GetUserProfilePhotosAsync(userId, offset, limit);

        [Obsolete("GetFile is deprecated, please use GetFileAsync", GenerateErrorOnUsage)]
        public Task<File> GetFile(string fileId, Stream destination = null)
            => GetFileAsync(fileId, destination);

        #endregion

        #region API Methods - Inline

        [Obsolete("AnswerInlineQuery is deprecated, please use AnswerInlineQueryAsync", GenerateErrorOnUsage)]
        public Task<bool> AnswerInlineQuery(string inlineQueryId, InlineQueryResult[] results, int? cacheTime = null,
            bool isPersonal = false, string nextOffset = null, string switchPmText = null,
            string switchPmParameter = null)
            => AnswerInlineQueryAsync(inlineQueryId, results, cacheTime, isPersonal, nextOffset, switchPmText, switchPmParameter);

        [Obsolete("AnswerCallbackQuery is deprecated, please use AnswerCallbackQueryAsync", GenerateErrorOnUsage)]
        public Task<bool> AnswerCallbackQuery(string callbackQueryId, string text = null, bool showAlert = false)
            => AnswerCallbackQueryAsync(callbackQueryId, text, showAlert);

        #endregion

        #region API Methods - Edit

        [Obsolete("EditMessageText is deprecated, please use EditMessageTextAsync", GenerateErrorOnUsage)]
        public Task<Message> EditMessageText(long chatId, int messageId, string text,
            ParseMode parseMode = ParseMode.Default, bool disableWebPagePreview = false, IReplyMarkup replyMarkup = null)
            => EditMessageTextAsync(chatId, messageId, text, parseMode, disableWebPagePreview, replyMarkup);

        [Obsolete("EditMessageText is deprecated, please use EditMessageTextAsync", GenerateErrorOnUsage)]
        public Task<Message> EditMessageText(string chatId, int messageId, string text,
            ParseMode parseMode = ParseMode.Default, bool disableWebPagePreview = false, IReplyMarkup replyMarkup = null)
            => EditMessageTextAsync(chatId, messageId, text, parseMode, disableWebPagePreview, replyMarkup);

        [Obsolete("EditInlineMessageText is deprecated, please use EditInlineMessageTextAsync", GenerateErrorOnUsage)]
        public Task<Message> EditInlineMessageText(string inlineMessageId, string text,
            ParseMode parseMode = ParseMode.Default, bool disableWebPagePreview = false, IReplyMarkup replyMarkup = null)
            => EditInlineMessageTextAsync(inlineMessageId, text, parseMode, disableWebPagePreview, replyMarkup);

        [Obsolete("EditMessageCaption is deprecated, please use EditMessageCaptionAsync", GenerateErrorOnUsage)]
        public Task<Message> EditMessageCaption(long chatId, int messageId, string caption, IReplyMarkup replyMarkup = null)
            => EditMessageCaptionAsync(chatId, messageId, caption, replyMarkup);

        [Obsolete("EditMessageCaption is deprecated, please use EditMessageCaptionAsync", GenerateErrorOnUsage)]
        public Task<Message> EditMessageCaption(string chatId, int messageId, string caption, IReplyMarkup replyMarkup = null)
            => EditMessageCaptionAsync(chatId, messageId, caption, replyMarkup);

        [Obsolete("EditInlineMessageCaption is deprecated, please use EditInlineMessageCaptionAsync", GenerateErrorOnUsage)]
        public Task<Message> EditInlineMessageCaption(string inlineMessageId, string caption, IReplyMarkup replyMarkup = null)
            => EditInlineMessageCaptionAsync(inlineMessageId, caption, replyMarkup);

        [Obsolete("EditMessageReplyMarkup is deprecated, please use EditMessageReplyMarkupAsync", GenerateErrorOnUsage)]
        public Task<Message> EditMessageReplyMarkup(long chatId, int messageId, IReplyMarkup replyMarkup = null)
            => EditMessageReplyMarkupAsync(chatId, messageId, replyMarkup);

        [Obsolete("EditMessageReplyMarkup is deprecated, please use EditMessageReplyMarkupAsync", GenerateErrorOnUsage)]
        public Task<Message> EditMessageReplyMarkup(string chatId, int messageId, IReplyMarkup replyMarkup = null)
            => EditMessageReplyMarkupAsync(chatId, messageId, replyMarkup);

        [Obsolete("EditInlineMessageReplyMarkup is deprecated, please use EditInlineMessageReplyMarkupAsync", GenerateErrorOnUsage)]
        public Task<Message> EditInlineMessageReplyMarkup(string inlineMessageId, IReplyMarkup replyMarkup = null)
            => EditInlineMessageReplyMarkupAsync(inlineMessageId, replyMarkup);

        #endregion
    }
#pragma warning restore CS1591
}
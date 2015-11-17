using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace Telegram.Bot
{
    public class Api
    {
        private const string BaseUrl = "https://api.telegram.org/bot";
        private const string BaseFileUrl = "https://api.telegram.org/file/bot";

        private readonly string _token;

        /// <summary>
        /// Timeout for uploading Files/Videos/Documents etc.
        /// </summary>
        public TimeSpan UploadTimeout = TimeSpan.FromMinutes(1);

        public Api(string token)
        {
            _token = token;
        }


        /// <summary>
        /// A simple method for testing your bot's auth token. Requires no parameters. Returns basic information about the bot in form of User object.
        /// </summary>
        /// <returns>Returns basic information about the bot in form of <see cref="User"/> object</returns>
        public Task<User> GetMe() => SendWebRequest<User>("getMe");

        /// <summary>
        /// Use this method to receive incoming updates using long polling.
        /// </summary>
        /// <param name="offset">
        /// Optional. Identifier of the first update to be returned. Must be greater by one than the highest among the identifiers of previously received updates.
        /// By default, updates starting with the earliest unconfirmed update are returned. An update is considered confirmed as soon as GetUpdates is 
        /// called with an offset higher than its Id.
        /// </param>
        /// <param name="limit">
        /// Optional. Limits the number of updates to be retrieved. Values between 1—100 are accepted. Defaults to 100
        /// </param>
        /// <param name="timeout">
        /// Optional. Timeout in seconds for long polling. Defaults to 0, i.e. usual short polling
        /// </param>
        /// <remarks>
        /// 1. This method will not work if an outgoing webhook is set up.
        /// 2. In order to avoid getting duplicate updates, recalculate offset after each server response.
        /// </remarks>
        /// <returns>An Array of Update objects is returned.</returns>
        public Task<Update[]> GetUpdates(int offset = 0, int limit = 100, int timeout = 0)
        {
            var parameters = new Dictionary<string, object>
            {
                {"offset", offset},
                {"limit", limit},
                {"timeout", timeout}
            };

            return SendWebRequest<Update[]>("getUpdates", parameters);
        }


        /// <summary>
        /// Use this method to specify a url and receive incoming updates via an outgoing webhook. Whenever there is an update for the bot, we will
        /// send an HTTPS POST request to the specified url, containing a JSON-serialized Update. In case of an unsuccessful request, we will
        /// give up after a reasonable amount of attempts.
        /// </summary>
        /// <param name="url">Optional. HTTPS url to send updates to. Use an empty string to remove webhook integration</param>
        /// <param name="certificate">Upload your public key certificate so that the root certificate in use can be checked</param>
        /// <remarks>
        /// 1. You will not be able to receive updates using getUpdates for as long as an outgoing webhook is set up.
        /// 2. We currently do not support self-signed certificates.
        /// 3. For the moment, the only supported port for Webhooks is 443. We may support additional ports later.
        /// </remarks>
        public Task SetWebhook(string url = "", FileToSend? certificate = null)
        {
            var parameters = new Dictionary<string, object>
            {
                {"url", url}
            };

            if (certificate != null)
                parameters.Add("certificate", certificate);

            return SendWebRequest<bool>("setWebhook", parameters);
        }

        /// <summary>
        /// Use this method to send any messages. On success, the sent Message is returned.
        /// </summary>
        /// <param name="type">The <see cref="MessageType"/></param>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format @channelusername)</param>
        /// <param name="content">The content of the message. Could be a text, photo, audio, sticker, document, video or location</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <param name="additionalParameters">Optional. if additional Parameters could bei send i.e. "disable_web_page_preview" in for a TextMessage</param>
        /// <returns>On success, the sent Message is returned.</returns>
        private Task<Message> SendMessage(MessageType type, string chatId, object content,
            int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null,
            Dictionary<string, object> additionalParameters = null)
        {
            if (additionalParameters == null)
                additionalParameters = new Dictionary<string, object>();

            var typeInfo = type.ToKeyValue();

            additionalParameters.Add("chat_id", chatId);
            additionalParameters.Add("reply_markup", replyMarkup);

            if (replyToMessageId != 0)
                additionalParameters.Add("reply_to_message_id", replyToMessageId);

            if (!string.IsNullOrEmpty(typeInfo.Value))
                additionalParameters.Add(typeInfo.Value, content);

            return SendWebRequest<Message>(typeInfo.Key, additionalParameters);
        }

        /// <summary>
        /// Use this method to send text messages. On success, the sent Message is returned.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="text">Text of the message to be sent</param>
        /// <param name="disableWebPagePreview">Optional. Disables link previews for links in this message</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <param name="isMarkdown">Optional. Set to true if you want Telegram apps to show bold, italic and inline URLs in your message.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public Task<Message> SendTextMessage(long chatId, string text, bool disableWebPagePreview = false,
            int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null, bool isMarkdown = false)
            => SendTextMessage(chatId.ToString(), text, disableWebPagePreview, replyToMessageId, replyMarkup, isMarkdown);

        /// <summary>
        /// Use this method to send text messages. On success, the sent Message is returned.
        /// </summary>
        /// <param name="chatId">username of the target channel (in the format @channelusername)</param>
        /// <param name="text">Text of the message to be sent</param>
        /// <param name="disableWebPagePreview">Optional. Disables link previews for links in this message</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <param name="isMarkdown">Optional. Set to true if you want Telegram apps to show bold, italic and inline URLs in your message.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public Task<Message> SendTextMessage(string chatId, string text, bool disableWebPagePreview = false, int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null, bool isMarkdown = false)
        {
            var additionalParameters = new Dictionary<string, object>();

            if (disableWebPagePreview)
                additionalParameters.Add("disable_web_page_preview", true);

            if (isMarkdown)
                additionalParameters.Add("parse_mode", "Markdown");

            return SendMessage(MessageType.TextMessage, chatId, text, replyToMessageId, replyMarkup, additionalParameters);
        }

        /// <summary>
        /// Use this method to forward messages of any kind. On success, the sent Message is returned.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="fromChatId">Unique identifier for the chat where the original message was sent</param>
        /// <param name="messageId">Unique message identifier</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public Task<Message> ForwardMessage(long chatId, long fromChatId, int messageId) => ForwardMessage(chatId.ToString(), fromChatId.ToString(), messageId);

        /// <summary>
        /// Use this method to forward messages of any kind. On success, the sent Message is returned.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="fromChatId">channel username in the format @channelusername</param>
        /// <param name="messageId">Unique message identifier</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public Task<Message> ForwardMessage(long chatId, string fromChatId, int messageId) => ForwardMessage(chatId.ToString(), fromChatId, messageId);

        /// <summary>
        /// Use this method to forward messages of any kind. On success, the sent Message is returned.
        /// </summary>
        /// <param name="chatId">username of the target channel (in the format @channelusername)</param>
        /// <param name="fromChatId">Unique identifier for the chat where the original message was sent</param>
        /// <param name="messageId">Unique message identifier</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public Task<Message> ForwardMessage(string chatId, long fromChatId, int messageId) => ForwardMessage(chatId, fromChatId.ToString(), messageId);

        /// <summary>
        /// Use this method to forward messages of any kind. On success, the sent Message is returned.
        /// </summary>
        /// <param name="chatId">username of the target channel (in the format @channelusername)</param>
        /// <param name="fromChatId">channel username in the format @channelusername</param>
        /// <param name="messageId">Unique message identifier</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public Task<Message> ForwardMessage(string chatId, string fromChatId, int messageId)
        {
            var parameters = new Dictionary<string, object>
            {
                {"chat_id", chatId},
                {"from_chat_id", fromChatId},
                {"message_id", messageId},
            };

            return SendWebRequest<Message>("forwardMessage", parameters);
        }

        /// <summary>
        /// Use this method to send photos. On success, the sent Message is returned.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="photo">Photo to send. You can either pass a file_id as String to resend a photo that is already on the Telegram servers, or upload a new photo using multipart/form-data.</param>
        /// <param name="caption">Optional. Photo caption (may also be used when resending photos by file_id).</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public Task<Message> SendPhoto(long chatId, FileToSend photo, string caption = "", int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null)
            => SendPhoto(chatId.ToString(), photo, caption, replyToMessageId, replyMarkup);

        /// <summary>
        /// Use this method to send photos. On success, the sent Message is returned.
        /// </summary>
        /// <param name="chatId">Username of the target channel (in the format @channelusername)</param>
        /// <param name="photo">Photo to send. You can either pass a file_id as String to resend a photo that is already on the Telegram servers, or upload a new photo using multipart/form-data.</param>
        /// <param name="caption">Optional. Photo caption (may also be used when resending photos by file_id).</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public Task<Message> SendPhoto(string chatId, FileToSend photo, string caption = "", int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null)
        {
            var additionalParameters = new Dictionary<string, object>
            {
                {"caption", caption}
            };

            return SendMessage(MessageType.PhotoMessage, chatId, photo, replyToMessageId, replyMarkup, additionalParameters);
        }

        /// <summary>
        /// Use this method to send photos. On success, the sent Message is returned.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="photo">Photo to send. You can either pass a file_id as String to resend a photo that is already on the Telegram servers, or upload a new photo using multipart/form-data.</param>
        /// <param name="caption">Optional. Photo caption (may also be used when resending photos by file_id).</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public Task<Message> SendPhoto(long chatId, string photo, string caption = "", int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null)
            => SendPhoto(chatId.ToString(), photo, caption, replyToMessageId, replyMarkup);

        /// <summary>
        /// Use this method to send photos. On success, the sent Message is returned.
        /// </summary>
        /// <param name="chatId">Username of the target channel (in the format @channelusername)</param>
        /// <param name="photo">Photo to send. You can either pass a file_id as String to resend a photo that is already on the Telegram servers, or upload a new photo using multipart/form-data.</param>
        /// <param name="caption">Optional. Photo caption (may also be used when resending photos by file_id).</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public Task<Message> SendPhoto(string chatId, string photo, string caption = "", int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null)
        {
            var additionalParameters = new Dictionary<string, object>
            {
                {"caption", caption}
            };

            return SendMessage(MessageType.PhotoMessage, chatId, photo, replyToMessageId, replyMarkup, additionalParameters);
        }

        /// <summary>
        /// Use this method to send audio files, if you want Telegram clients to display them in the music player. Your audio must be in the .mp3 format. On success, the sent Message is returned. Bots can currently send audio files of up to 50 MB in size, this limit may be changed in the future.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="audio">Audio file to send. You can either pass a file_id as String to resend an audio that is already on the Telegram servers, or upload a new audio file using multipart/form-data.</param>
        /// <param name="duration">Duration of the audio in seconds</param>
        /// <param name="performer">Performer</param>
        /// <param name="title">Track name</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public Task<Message> SendAudio(long chatId, FileToSend audio, int duration, string performer, string title,
            int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null)
            => SendAudio(chatId.ToString(), audio, duration, performer, title, replyToMessageId, replyMarkup);

        /// <summary>
        /// Use this method to send audio files, if you want Telegram clients to display them in the music player. Your audio must be in the .mp3 format. On success, the sent Message is returned. Bots can currently send audio files of up to 50 MB in size, this limit may be changed in the future.
        /// </summary>
        /// <param name="chatId">Username of the target channel (in the format @channelusername)</param>
        /// <param name="audio">Audio file to send. You can either pass a file_id as String to resend an audio that is already on the Telegram servers, or upload a new audio file using multipart/form-data.</param>
        /// <param name="duration">Duration of the audio in seconds</param>
        /// <param name="performer">Performer</param>
        /// <param name="title">Track name</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public Task<Message> SendAudio(string chatId, FileToSend audio, int duration, string performer, string title, int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null)
        {
            var additionalParameters = new Dictionary<string, object>
            {
                {"duration", duration},
                {"performer", performer},
                {"title", title}
            };

            return SendMessage(MessageType.AudioMessage, chatId, audio, replyToMessageId, replyMarkup, additionalParameters);
        }

        /// <summary>
        /// Use this method to send audio files, if you want Telegram clients to display the file as a playable voice message. For this to work, your audio must be in an .ogg file encoded with OPUS (other formats may be sent as Document). On success, the sent Message is returned. Bots can send audio files of up to 50 MB in size.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="audio">Audio file to send. You can either pass a file_id as String to resend an audio that is already on the Telegram servers, or upload a new audio file using multipart/form-data.</param>
        /// <param name="duration">Duration of the audio in seconds</param>
        /// <param name="performer">Performer</param>
        /// <param name="title">Track name</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public Task<Message> SendAudio(long chatId, string audio, int duration, string performer, string title,
            int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null)
            => SendAudio(chatId.ToString(), audio, duration, performer, title, replyToMessageId, replyMarkup);

        /// <summary>
        /// Use this method to send audio files, if you want Telegram clients to display the file as a playable voice message. For this to work, your audio must be in an .ogg file encoded with OPUS (other formats may be sent as Document). On success, the sent Message is returned. Bots can send audio files of up to 50 MB in size.
        /// </summary>
        /// <param name="chatId">Username of the target channel (in the format @channelusername)</param>
        /// <param name="audio">Audio file to send. You can either pass a file_id as String to resend an audio that is already on the Telegram servers, or upload a new audio file using multipart/form-data.</param>
        /// <param name="duration">Duration of the audio in seconds</param>
        /// <param name="performer">Performer</param>
        /// <param name="title">Track name</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public Task<Message> SendAudio(string chatId, string audio, int duration, string performer, string title, int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null)
        {
            var additionalParameters = new Dictionary<string, object>
            {
                {"duration", duration},
                {"performer", performer},
                {"title", title}
            };

            return SendMessage(MessageType.AudioMessage, chatId, audio, replyToMessageId, replyMarkup, additionalParameters);
        }

        /// <summary>
        /// Use this method to send general files. On success, the sent Message is returned. Bots can send files of any type of up to 50 MB in size.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="document">File to send. You can either pass a file_id as String to resend a file that is already on the Telegram servers, or upload a new file using multipart/form-data.</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public Task<Message> SendDocument(long chatId, FileToSend document, int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null) => SendMessage(MessageType.DocumentMessage, chatId.ToString(), document, replyToMessageId, replyMarkup);

        /// <summary>
        /// Use this method to send general files. On success, the sent Message is returned. Bots can send files of any type of up to 50 MB in size.
        /// </summary>
        /// <param name="chatId">Username of the target channel (in the format @channelusername)</param>
        /// <param name="document">File to send. You can either pass a file_id as String to resend a file that is already on the Telegram servers, or upload a new file using multipart/form-data.</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public Task<Message> SendDocument(string chatId, FileToSend document, int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null) => SendMessage(MessageType.DocumentMessage, chatId, document, replyToMessageId, replyMarkup);

        /// <summary>
        /// Use this method to send general files. On success, the sent Message is returned. Bots can send files of any type of up to 50 MB in size.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="document">File to send. You can either pass a file_id as String to resend a file that is already on the Telegram servers, or upload a new file using multipart/form-data.</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public Task<Message> SendDocument(long chatId, string document, int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null) => SendMessage(MessageType.DocumentMessage, chatId.ToString(), document, replyToMessageId, replyMarkup);

        /// <summary>
        /// Use this method to send general files. On success, the sent Message is returned. Bots can send files of any type of up to 50 MB in size.
        /// </summary>
        /// <param name="chatId">Username of the target channel (in the format @channelusername)</param>
        /// <param name="document">File to send. You can either pass a file_id as String to resend a file that is already on the Telegram servers, or upload a new file using multipart/form-data.</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public Task<Message> SendDocument(string chatId, string document, int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null) => SendMessage(MessageType.DocumentMessage, chatId, document, replyToMessageId, replyMarkup);

        /// <summary>
        /// Use this method to send .webp stickers. On success, the sent Message is returned.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="sticker">Sticker to send. You can either pass a file_id as String to resend a sticker that is already on the Telegram servers, or upload a new sticker using multipart/form-data.</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public Task<Message> SendSticker(long chatId, FileToSend sticker, int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null) => SendMessage(MessageType.StickerMessage, chatId.ToString(), sticker, replyToMessageId, replyMarkup);

        /// <summary>
        /// Use this method to send .webp stickers. On success, the sent Message is returned.
        /// </summary>
        /// <param name="chatId">Username of the target channel (in the format @channelusername)</param>
        /// <param name="sticker">Sticker to send. You can either pass a file_id as String to resend a sticker that is already on the Telegram servers, or upload a new sticker using multipart/form-data.</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public Task<Message> SendSticker(string chatId, FileToSend sticker, int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null) => SendMessage(MessageType.StickerMessage, chatId, sticker, replyToMessageId, replyMarkup);

        /// <summary>
        /// Use this method to send .webp stickers. On success, the sent Message is returned.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="sticker">Sticker to send. You can either pass a file_id as String to resend a sticker that is already on the Telegram servers, or upload a new sticker using multipart/form-data.</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public Task<Message> SendSticker(long chatId, string sticker, int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null) => SendMessage(MessageType.StickerMessage, chatId.ToString(), sticker, replyToMessageId, replyMarkup);

        /// <summary>
        /// Use this method to send .webp stickers. On success, the sent Message is returned.
        /// </summary>
        /// <param name="chatId">Username of the target channel (in the format @channelusername)</param>
        /// <param name="sticker">Sticker to send. You can either pass a file_id as String to resend a sticker that is already on the Telegram servers, or upload a new sticker using multipart/form-data.</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public Task<Message> SendSticker(string chatId, string sticker, int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null) => SendMessage(MessageType.StickerMessage, chatId, sticker, replyToMessageId, replyMarkup);

        /// <summary>
        /// Use this method to send video files, Telegram clients support mp4 videos (other formats may be sent as Document). On success, the sent Message is returned. Bots can send video files of up to 50 MB in size.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="video">Video to send. You can either pass a file_id as String to resend a video that is already on the Telegram servers, or upload a new video file using multipart/form-data.</param>
        /// <param name="duration">Duration of sent video in seconds</param>
        /// <param name="caption">Video caption</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public Task<Message> SendVideo(long chatId, FileToSend video, int duration = 0, string caption = "",
            int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null)
            => SendVideo(chatId.ToString(), video, duration, caption, replyToMessageId, replyMarkup);

        /// <summary>
        /// Use this method to send video files, Telegram clients support mp4 videos (other formats may be sent as Document). On success, the sent Message is returned. Bots can send video files of up to 50 MB in size.
        /// </summary>
        /// <param name="chatId">Username of the target channel (in the format @channelusername)</param>
        /// <param name="video">Video to send. You can either pass a file_id as String to resend a video that is already on the Telegram servers, or upload a new video file using multipart/form-data.</param>
        /// <param name="duration">Duration of sent video in seconds</param>
        /// <param name="caption">Video caption</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public Task<Message> SendVideo(string chatId, FileToSend video, int duration = 0, string caption = "", int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null)
        {
            var additionalParameters = new Dictionary<string, object>
            {
                {"duration", duration},
                {"caption", caption}
            };

            return SendMessage(MessageType.VideoMessage, chatId, video, replyToMessageId, replyMarkup, additionalParameters);
        }

        /// <summary>
        /// Use this method to send video files, Telegram clients support mp4 videos (other formats may be sent as Document). On success, the sent Message is returned. Bots can send video files of up to 50 MB in size.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="video">Video to send. You can either pass a file_id as String to resend a video that is already on the Telegram servers, or upload a new video file using multipart/form-data.</param>
        /// <param name="duration">Duration of sent video in seconds</param>
        /// <param name="caption">Video caption</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public Task<Message> SendVideo(long chatId, string video, int duration = 0, string caption = "",
            int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null)
            => SendVideo(chatId.ToString(), video, duration, caption, replyToMessageId, replyMarkup);

        /// <summary>
        /// Use this method to send video files, Telegram clients support mp4 videos (other formats may be sent as Document). On success, the sent Message is returned. Bots can send video files of up to 50 MB in size.
        /// </summary>
        /// <param name="chatId">Username of the target channel (in the format @channelusername)</param>
        /// <param name="video">Video to send. You can either pass a file_id as String to resend a video that is already on the Telegram servers, or upload a new video file using multipart/form-data.</param>
        /// <param name="duration">Duration of sent video in seconds</param>
        /// <param name="caption">Video caption</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public Task<Message> SendVideo(string chatId, string video, int duration = 0, string caption = "", int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null)
        {
            var additionalParameters = new Dictionary<string, object>
            {
                {"duration", duration},
                {"caption", caption}
            };

            return SendMessage(MessageType.VideoMessage, chatId, video, replyToMessageId, replyMarkup, additionalParameters);
        }

        /// <summary>
        /// Use this method to send audio files, if you want Telegram clients to display the file as a playable voice message. For this to work, your audio must be in an .ogg file encoded with OPUS (other formats may be sent as Audio or Document). On success, the sent Message is returned. Bots can currently send voice messages of up to 50 MB in size, this limit may be changed in the future.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="audio">Audio file to send. You can either pass a file_id as String to resend an audio that is already on the Telegram servers, or upload a new audio file using multipart/form-data.</param>
        /// <param name="duration">Duration of sent audio in seconds</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public Task<Message> SendVoice(long chatId, FileToSend audio, int duration = 0, int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null)
            => SendVoice(chatId.ToString(), audio, duration, replyToMessageId, replyMarkup);

        /// <summary>
        /// Use this method to send audio files, if you want Telegram clients to display the file as a playable voice message. For this to work, your audio must be in an .ogg file encoded with OPUS (other formats may be sent as Audio or Document). On success, the sent Message is returned. Bots can currently send voice messages of up to 50 MB in size, this limit may be changed in the future.
        /// </summary>
        /// <param name="chatId">Username of the target channel (in the format @channelusername)</param>
        /// <param name="audio">Audio file to send. You can either pass a file_id as String to resend an audio that is already on the Telegram servers, or upload a new audio file using multipart/form-data.</param>
        /// <param name="duration">Duration of sent audio in seconds</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public Task<Message> SendVoice(string chatId, FileToSend audio, int duration = 0, int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null)
        {
            var additionalParameters = new Dictionary<string, object>
            {
                {"duration", duration}
            };

            return SendMessage(MessageType.VideoMessage, chatId, audio, replyToMessageId, replyMarkup, additionalParameters);
        }

        /// <summary>
        /// Use this method to send audio files, if you want Telegram clients to display the file as a playable voice message. For this to work, your audio must be in an .ogg file encoded with OPUS (other formats may be sent as Audio or Document). On success, the sent Message is returned. Bots can currently send voice messages of up to 50 MB in size, this limit may be changed in the future.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="audio">Audio file to send. You can either pass a file_id as String to resend an audio that is already on the Telegram servers, or upload a new audio file using multipart/form-data.</param>
        /// <param name="duration">Duration of sent audio in seconds</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public Task<Message> SendVoice(long chatId, string audio, int duration = 0, int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null)
            => SendVoice(chatId.ToString(), audio, duration, replyToMessageId, replyMarkup);

        /// <summary>
        /// Use this method to send audio files, if you want Telegram clients to display the file as a playable voice message. For this to work, your audio must be in an .ogg file encoded with OPUS (other formats may be sent as Audio or Document). On success, the sent Message is returned. Bots can currently send voice messages of up to 50 MB in size, this limit may be changed in the future.
        /// </summary>
        /// <param name="chatId">Username of the target channel (in the format @channelusername)</param>
        /// <param name="audio">Audio file to send. You can either pass a file_id as String to resend an audio that is already on the Telegram servers, or upload a new audio file using multipart/form-data.</param>
        /// <param name="duration">Duration of sent audio in seconds</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public Task<Message> SendVoice(string chatId, string audio, int duration = 0, int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null)
        {
            var additionalParameters = new Dictionary<string, object>
            {
                {"duration", duration}
            };

            return SendMessage(MessageType.VideoMessage, chatId, audio, replyToMessageId, replyMarkup, additionalParameters);
        }

        /// <summary>
        /// Use this method to send point on the map. On success, the sent Message is returned.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="latitude">Latitude of location</param>
        /// <param name="longitude">Longitude of location</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public Task<Message> SendLocation(long chatId, float latitude, float longitude, int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null)
            => SendLocation(chatId.ToString(), latitude, longitude, replyToMessageId, replyMarkup);

        /// <summary>
        /// Use this method to send point on the map. On success, the sent Message is returned.
        /// </summary>
        /// <param name="chatId">Username of the target channel (in the format @channelusername)</param>
        /// <param name="latitude">Latitude of location</param>
        /// <param name="longitude">Longitude of location</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public Task<Message> SendLocation(string chatId, float latitude, float longitude, int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null)
        {
            var additionalParameters = new Dictionary<string, object>
            {
                {"longitude", longitude},
            };

            return SendMessage(MessageType.LocationMessage, chatId, latitude, replyToMessageId, replyMarkup, additionalParameters);
        }

        /// <summary>
        /// Use this method when you need to tell the user that something is happening on the bot's side. The status is set for 5 seconds or less (when a message arrives from your bot, Telegram clients clear its typing status).
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="chatAction">Type of action to broadcast. Choose one, depending on what the user is about to receive.</param>
        /// <remarks>We only recommend using this method when a response from the bot will take a noticeable amount of time to arrive.</remarks>
        public Task SendChatAction(long chatId, ChatAction chatAction)
            => SendChatAction(chatId.ToString(), chatAction);

        /// <summary>
        /// Use this method when you need to tell the user that something is happening on the bot's side. The status is set for 5 seconds or less (when a message arrives from your bot, Telegram clients clear its typing status).
        /// </summary>
        /// <param name="chatId">Username of the target channel (in the format @channelusername)</param>
        /// <param name="chatAction">Type of action to broadcast. Choose one, depending on what the user is about to receive.</param>
        /// <remarks>We only recommend using this method when a response from the bot will take a noticeable amount of time to arrive.</remarks>
        public Task SendChatAction(string chatId, ChatAction chatAction)
        {
            var parameters = new Dictionary<string, object>
            {
                {"chat_id", chatId},
                {"action", chatAction.ToActionString()}
            };

            return SendWebRequest<bool>("sendChatAction", parameters);
        }

        /// <summary>
        /// Use this method to get a list of profile pictures for a user. Returns a UserProfilePhotos object.
        /// </summary>
        /// <param name="userId">Unique identifier of the target user</param>
        /// <param name="offset">Optional. Sequential number of the first photo to be returned. By default, all photos are returned.</param>
        /// <param name="limit">Optional. Limits the number of photos to be retrieved. Values between 1—100 are accepted. Defaults to 100.</param>
        /// <returns></returns>
        public Task<UserProfilePhotos> GetUserProfilePhotos(int userId, int? offset = null, int limit = 100)
        {
            var parameters = new Dictionary<string, object>
            {
                {"user_id", userId},
                {"offset", offset},
                {"limit", limit}
            };

            return SendWebRequest<UserProfilePhotos>("getUserProfilePhotos", parameters);
        }

        /// <summary>
        /// Use this method to get basic info about a file and prepare it for downloading. For the moment, bots can download files of up to 20MB in size.
        /// </summary>
        /// <param name="fileId">File identifier</param>
        /// <param name="destination">The destination stream</param>
        /// <returns>The File object. If destination is empty stream ist embedded in the File Object</returns>
        public async Task<Types.File> GetFile(string fileId, Stream destination = null)
        {
            var parameters = new Dictionary<string, object>
            {
                {"file_id", fileId}
            };

            var fileInfo = await SendWebRequest<Types.File>("getFile", parameters).ConfigureAwait(false);

            var fileUri = new Uri(BaseFileUrl + _token + "/" + fileInfo.FilePath);

            if (destination == null)
                destination = fileInfo.FileStream = new MemoryStream();

            using (var downloader = new HttpClient())
            {
                using (var response = await downloader.GetAsync(fileUri, HttpCompletionOption.ResponseHeadersRead))
                {
                    await response.Content.CopyToAsync(destination).ConfigureAwait(false);
                    destination.Position = 0;
                }
            }

            return fileInfo;
        }

        private async Task<T> SendWebRequest<T>(string method, Dictionary<string, object> parameters = null)
        {
            var uri = new Uri(BaseUrl + _token + "/" + method);

            using (var client = new HttpClient())
            {
                ApiResponse<T> responseObject = null;
                try
                {
                    HttpResponseMessage response;

                    if (parameters != null)
                    {
                        using (var form = new MultipartFormDataContent())
                        {
                            foreach (var parameter in parameters.Where(parameter => parameter.Value != null))
                            {
                                var content = ConvertParameterValue(parameter.Value);

                                if (parameter.Value is FileToSend)
                                {
                                    client.Timeout = UploadTimeout;
                                    form.Add(content, parameter.Key, ((FileToSend) parameter.Value).Filename);
                                }
                                else
                                    form.Add(content, parameter.Key);
                            }

                            response = await client.PostAsync(uri, form).ConfigureAwait(false);
                        }
                    }
                    else
                    {
                        response = await client.GetAsync(uri).ConfigureAwait(false);
                    }

                    responseObject = await response.Content.ReadAsAsync<ApiResponse<T>>().ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();
                }
                catch (HttpRequestException e) when (e.Message.Contains("400"))
                {
                }
                    
                //TODO: catch more exceptions

                if (responseObject == null)
                    responseObject = new ApiResponse<T> {Ok = false, Message = "No response received"};

                if (!responseObject.Ok)
                    throw new ApiRequestException(responseObject.Message, responseObject.Code);

                return responseObject.ResultObject;
            }
        }

        private static HttpContent ConvertParameterValue(object value)
        {
            var type = value.GetType();

            switch (type.Name)
            {
                case "String":
                case "Int32":
                    return new StringContent(value.ToString());
                case "Boolean":
                    return new StringContent((bool)value ? "true" : "false");
                case "FileToSend":
                    return new StreamContent(((FileToSend)value).Content);
                default:
                    return new StringContent(JsonConvert.SerializeObject(value));
            }
        }
    }
}

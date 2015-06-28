using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace Telegram.Bot
{
    public class Api
    {
        private const string BaseUrl = "https://api.telegram.org/bot";

        private readonly string _token;

        public Api(string token)
        {
            _token = token;
        }


        /// <summary>
        /// A simple method for testing your bot's auth token. Requires no parameters. Returns basic information about the bot in form of User object.
        /// </summary>
        /// <returns>Returns basic information about the bot in form of <see cref="User"/> object</returns>
        public async Task<User> GetMe()
        {
            const string method = "getMe";

            var response = await SendWebRequest<User>(method);

            return response;
        }

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
        public async Task<Update[]> GetUpdates(int offset = 0, int limit = 100, int timeout = 0)
        {
            const string method = "getUpdates";

            var parameters = new Dictionary<string, object>()
            {
                {"offset", offset},
                {"limit", limit},
                {"timeout", timeout}
            };

            var response = await SendWebRequest<Update[]>(method, parameters);

            return response;
        }


        /// <summary>
        /// Use this method to specify a url and receive incoming updates via an outgoing webhook. Whenever there is an update for the bot, we will
        /// send an HTTPS POST request to the specified url, containing a JSON-serialized Update. In case of an unsuccessful request, we will
        /// give up after a reasonable amount of attempts.
        /// </summary>
        /// <param name="url">Optimal. HTTPS url to send updates to. Use an empty string to remove webhook integration</param>
        /// <remarks>
        /// 1. You will not be able to receive updates using getUpdates for as long as an outgoing webhook is set up.
        /// 2. We currently do not support self-signed certificates.
        /// 3. For the moment, the only supported port for Webhooks is 443. We may support additional ports later.
        /// </remarks>
        public async void SetWebhook(string url)
        {//TODO: Test
            const string method = "setWebhook";

            var parameters = new Dictionary<string, object>
            {
                {"url", url}
            };

            await SendWebRequest(method, parameters);
        }

        /// <summary>
        /// Use this method to send text messages. On success, the sent Message is returned.
        /// </summary>
        /// <param name="chatId">Unique identifier for the message recipient — User or GroupChat id</param>
        /// <param name="text">Text of the message to be sent</param>
        /// <param name="disableWebPagePreview">Optimal. Disables link previews for links in this message</param>
        /// <param name="replyToMessageId">Optimal. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optimal. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public async Task<Message> SendMessage(int chatId, string text, bool disableWebPagePreview = false, int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null)
        {
            const string method = "sendMessage";

            var parameters = new Dictionary<string, object>()
            {
                {"chat_id", chatId},
                {"text", text},
                {"disable_web_page_preview", disableWebPagePreview},
                {"reply_to_message_id", replyToMessageId},
                {"reply_markup", replyMarkup}
            };

            var response = await SendWebRequest<Message>(method, parameters);

            return response;
        }

        /// <summary>
        /// Use this method to forward messages of any kind. On success, the sent Message is returned.
        /// </summary>
        /// <param name="chatId">Unique identifier for the message recipient — User or GroupChat id</param>
        /// <param name="fromChatId">Unique identifier for the chat where the original message was sent — User or GroupChat id</param>
        /// <param name="messageId">Unique message identifier</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public async Task<Message> ForwardMessage(int chatId, int fromChatId, int messageId)
        {
            const string method = "forwardMessage";

            var parameters = new Dictionary<string, object>()
            {
                {"chat_id", chatId},
                {"from_chat_id", fromChatId},
                {"message_id", messageId},
            };

            var response = await SendWebRequest<Message>(method, parameters);

            return response;
        }

        /// <summary>
        /// Use this method to send photos. On success, the sent Message is returned.
        /// </summary>
        /// <param name="chatId">Unique identifier for the message recipient — User or GroupChat id</param>
        /// <param name="photo">Photo to send. You can either pass a file_id as String to resend a photo that is already on the Telegram servers, or upload a new photo using multipart/form-data.</param>
        /// <param name="caption">Optional. Photo caption (may also be used when resending photos by file_id).</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public async Task<Message> SendPhoto(int chatId, byte[] photo, string caption = "", int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null)
        {
            const string method = "sendPhoto";

            var parameters = new Dictionary<string, object>()
            {
                {"chat_id", chatId},
                {"photo", photo},
                {"caption", caption},
                {"reply_to_message_id", replyToMessageId},
                {"reply_markup", replyMarkup}
            };

            var response = await SendWebRequest<Message>(method, parameters);

            return response;
        }

        /// <summary>
        /// Use this method to send photos. On success, the sent Message is returned.
        /// </summary>
        /// <param name="chatId">Unique identifier for the message recipient — User or GroupChat id</param>
        /// <param name="photo">Photo to send. You can either pass a file_id as String to resend a photo that is already on the Telegram servers, or upload a new photo using multipart/form-data.</param>
        /// <param name="caption">Optional. Photo caption (may also be used when resending photos by file_id).</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public async Task<Message> ReSendPhoto(int chatId, string photo, string caption = "", int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null)
        {
            const string method = "sendPhoto";

            var parameters = new Dictionary<string, object>()
            {
                {"chat_id", chatId},
                {"photo", photo},
                {"caption", caption},
                {"reply_to_message_id", replyToMessageId},
                {"reply_markup", replyMarkup}
            };

            var response = await SendWebRequest<Message>(method, parameters);

            return response;
        }

        /// <summary>
        /// Use this method to send audio files, if you want Telegram clients to display the file as a playable voice message. For this to work, your audio must be in an .ogg file encoded with OPUS (other formats may be sent as Document). On success, the sent Message is returned. Bots can send audio files of up to 50 MB in size.
        /// </summary>
        /// <param name="chatId">Unique identifier for the message recipient — User or GroupChat id</param>
        /// <param name="audio">Audio file to send. You can either pass a file_id as String to resend an audio that is already on the Telegram servers, or upload a new audio file using multipart/form-data.</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public async Task<Message> SendAudio(int chatId, byte[] audio, int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null)
        {
            const string method = "sendAudio";

            var parameters = new Dictionary<string, object>()
            {
                {"chat_id", chatId},
                {"audio", audio},
                {"reply_to_message_id", replyToMessageId},
                {"reply_markup", replyMarkup}
            };

            var response = await SendWebRequest<Message>(method, parameters);

            return response;
        }

        /// <summary>
        /// Use this method to send audio files, if you want Telegram clients to display the file as a playable voice message. For this to work, your audio must be in an .ogg file encoded with OPUS (other formats may be sent as Document). On success, the sent Message is returned. Bots can send audio files of up to 50 MB in size.
        /// </summary>
        /// <param name="chatId">Unique identifier for the message recipient — User or GroupChat id</param>
        /// <param name="audio">Audio file to send. You can either pass a file_id as String to resend an audio that is already on the Telegram servers, or upload a new audio file using multipart/form-data.</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public async Task<Message> ReSendAudio(int chatId, string audio, int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null)
        {
            const string method = "sendAudio";

            var parameters = new Dictionary<string, object>()
            {
                {"chat_id", chatId},
                {"audio", audio},
                {"reply_to_message_id", replyToMessageId},
                {"reply_markup", replyMarkup}
            };

            var response = await SendWebRequest<Message>(method, parameters);

            return response;
        }

        /// <summary>
        /// Use this method to send general files. On success, the sent Message is returned. Bots can send files of any type of up to 50 MB in size.
        /// </summary>
        /// <param name="chatId">Unique identifier for the message recipient — User or GroupChat id</param>
        /// <param name="document">File to send. You can either pass a file_id as String to resend a file that is already on the Telegram servers, or upload a new file using multipart/form-data.</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public async Task<Message> SendDocument(int chatId, byte[] document, int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null)
        {
            const string method = "sendDocument";

            var parameters = new Dictionary<string, object>()
            {
                {"chat_id", chatId},
                {"document", document},
                {"reply_to_message_id", replyToMessageId},
                {"reply_markup", replyMarkup}
            };

            var response = await SendWebRequest<Message>(method, parameters);

            return response;
        }

        /// <summary>
        /// Use this method to send general files. On success, the sent Message is returned. Bots can send files of any type of up to 50 MB in size.
        /// </summary>
        /// <param name="chatId">Unique identifier for the message recipient — User or GroupChat id</param>
        /// <param name="document">File to send. You can either pass a file_id as String to resend a file that is already on the Telegram servers, or upload a new file using multipart/form-data.</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public async Task<Message> ReSendDocument(int chatId, string document, int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null)
        {
            const string method = "sendDocument";

            var parameters = new Dictionary<string, object>()
            {
                {"chat_id", chatId},
                {"document", document},
                {"reply_to_message_id", replyToMessageId},
                {"reply_markup", replyMarkup}
            };

            var response = await SendWebRequest<Message>(method, parameters);

            return response;
        }

        /// <summary>
        /// Use this method to send .webp stickers. On success, the sent Message is returned.
        /// </summary>
        /// <param name="chatId">Unique identifier for the message recipient — User or GroupChat id</param>
        /// <param name="sticker">Sticker to send. You can either pass a file_id as String to resend a sticker that is already on the Telegram servers, or upload a new sticker using multipart/form-data.</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public async Task<Message> SendSticker(int chatId, byte[] sticker, int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null)
        {
            const string method = "sendSticker";

            var parameters = new Dictionary<string, object>()
            {
                {"chat_id", chatId},
                {"sticker", sticker},
                {"reply_to_message_id", replyToMessageId},
                {"reply_markup", replyMarkup}
            };

            var response = await SendWebRequest<Message>(method, parameters);

            return response;
        }

        /// <summary>
        /// Use this method to send .webp stickers. On success, the sent Message is returned.
        /// </summary>
        /// <param name="chatId">Unique identifier for the message recipient — User or GroupChat id</param>
        /// <param name="sticker">Sticker to send. You can either pass a file_id as String to resend a sticker that is already on the Telegram servers, or upload a new sticker using multipart/form-data.</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public async Task<Message> ReSendSticker(int chatId, string sticker, int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null)
        {
            const string method = "sendSticker";

            var parameters = new Dictionary<string, object>()
            {
                {"chat_id", chatId},
                {"sticker", sticker},
                {"reply_to_message_id", replyToMessageId},
                {"reply_markup", replyMarkup}
            };

            var response = await SendWebRequest<Message>(method, parameters);

            return response;
        }

        /// <summary>
        /// Use this method to send video files, Telegram clients support mp4 videos (other formats may be sent as Document). On success, the sent Message is returned. Bots can send video files of up to 50 MB in size.
        /// </summary>
        /// <param name="chatId">Unique identifier for the message recipient — User or GroupChat id</param>
        /// <param name="video">Video to send. You can either pass a file_id as String to resend a video that is already on the Telegram servers, or upload a new video file using multipart/form-data.</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public async Task<Message> SendVideo(int chatId, byte[] video, int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null)
        {
            const string method = "sendVideo";

            var parameters = new Dictionary<string, object>()
            {
                {"chat_id", chatId},
                {"video", video},
                {"reply_to_message_id", replyToMessageId},
                {"reply_markup", replyMarkup}
            };

            var response = await SendWebRequest<Message>(method, parameters);

            return response;
        }

        /// <summary>
        /// Use this method to send video files, Telegram clients support mp4 videos (other formats may be sent as Document). On success, the sent Message is returned. Bots can send video files of up to 50 MB in size.
        /// </summary>
        /// <param name="chatId">Unique identifier for the message recipient — User or GroupChat id</param>
        /// <param name="video">Video to send. You can either pass a file_id as String to resend a video that is already on the Telegram servers, or upload a new video file using multipart/form-data.</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public async Task<Message> ReSendVideo(int chatId, string video, int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null)
        {
            const string method = "sendVideo";

            var parameters = new Dictionary<string, object>()
            {
                {"chat_id", chatId},
                {"video", video},
                {"reply_to_message_id", replyToMessageId},
                {"reply_markup", replyMarkup}
            };

            var response = await SendWebRequest<Message>(method, parameters);

            return response;
        }

        /// <summary>
        /// Use this method to send point on the map. On success, the sent Message is returned.
        /// </summary>
        /// <param name="chatId">Unique identifier for the message recipient — User or GroupChat id</param>
        /// <param name="latitude">Latitude of location</param>
        /// <param name="longitude">Longitude of location</param>
        /// <param name="replyToMessageId">Optional. If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Optional. Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>On success, the sent Message is returned.</returns>
        public async Task<Message> SendLocation(int chatId, float latitude, float longitude, int replyToMessageId = 0,
            ReplyMarkup replyMarkup = null)
        {
            const string method = "sendLocation";

            var parameters = new Dictionary<string, object>()
            {
                {"chat_id", chatId},
                {"latitude", latitude},
                {"longitude", longitude},
                {"reply_to_message_id", replyToMessageId},
                {"reply_markup", replyMarkup}
            };

            var response = await SendWebRequest<Message>(method, parameters);

            return response;
        }


        /// <summary>
        /// Use this method when you need to tell the user that something is happening on the bot's side. The status is set for 5 seconds or less (when a message arrives from your bot, Telegram clients clear its typing status).
        /// </summary>
        /// <param name="chatId">Unique identifier for the message recipient — User or GroupChat id</param>
        /// <param name="chatAction">Type of action to broadcast. Choose one, depending on what the user is about to receive.</param>
        /// <remarks>We only recommend using this method when a response from the bot will take a noticeable amount of time to arrive.</remarks>
        public async Task SendChatAction(int chatId, ChatAction chatAction)
        {
            const string method = "sendChatAction";

            var parameters = new Dictionary<string, object>()
            {
                {"chat_id", chatId},
                {"action", chatAction.ToString()}
            };

            await SendWebRequest(method, parameters);
        }

        /// <summary>
        /// Use this method to get a list of profile pictures for a user. Returns a UserProfilePhotos object.
        /// </summary>
        /// <param name="userId">Unique identifier of the target user</param>
        /// <param name="offset">Optional. Sequential number of the first photo to be returned. By default, all photos are returned.</param>
        /// <param name="limit">Optional. Limits the number of photos to be retrieved. Values between 1—100 are accepted. Defaults to 100.</param>
        /// <returns></returns>
        public async Task<UserProfilePhotos> GetUserProfilePhotos(int userId, int? offset = null, int limit = 100)
        {
            const string method = "getUserProfilePhotos";

            var parameters = new Dictionary<string, object>()
            {
                {"user_id", userId},
                {"offset", offset},
                {"limit", limit}
            };

            var response = await SendWebRequest<UserProfilePhotos>(method, parameters);

            return response;
        }


        private async Task<T> SendWebRequest<T>(string method, Dictionary<string, object> parameters = null)
        {
            var uri = new Uri(BaseUrl + _token + "/" + method);

            var client = new HttpClient();


            var form = new MultipartFormDataContent();

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    if (parameter.Value == null) continue;
                    form.Add(ConvertParameterValue(parameter.Value), parameter.Key);
                }
            }

            var response = await client.PostAsync(uri, form);

            response.EnsureSuccessStatusCode();

            var responseText = await response.Content.ReadAsStringAsync();

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<T>>(responseText);

            if (responseObject.Ok)
                return responseObject.ResultObject;

            throw new Exception(responseObject.Message);
        }

        private async Task SendWebRequest(string method, Dictionary<string, object> parameters = null)
        {
            var uri = new Uri(BaseUrl + _token + "/" + method);

            var client = new HttpClient();

            var form = new MultipartFormDataContent();

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    form.Add(ConvertParameterValue(parameter.Value), parameter.Key);
                }
            }

            var response = await client.PostAsync(uri, form);

            response.EnsureSuccessStatusCode();
        }

        private static HttpContent ConvertParameterValue(object value)
        {
            var type = value.GetType().Name;
            switch (type)
            {
                case "String":
                case "Int32":
                    return new StringContent(value.ToString());
                case "Boolean":
                    return new StringContent((bool)value ? "true" : "false");
                case "Byte[]":
                    return new StreamContent(new MemoryStream((byte[]) value));
                default:
                    return new StringContent(JsonConvert.SerializeObject(value));
            }
        }
    }
}

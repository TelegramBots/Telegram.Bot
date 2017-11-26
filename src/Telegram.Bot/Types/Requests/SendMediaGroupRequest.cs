using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Helpers;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Send a group of photos or videos as an album. On success, an array of the sent Messages is returned.
    /// </summary>
    public class SendMediaGroupRequest : RequestBase<SendMediaGroupResponse>
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target channel (in the format @channelusername)
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        /// A JSON-serialized array describing photos and videos to be sent, must include 2–10 items
        /// </summary>
        public IEnumerable<InputMediaBase> Media { get; set; }

        /// <summary>
        /// Sends the messages silently. Users will receive a notification with no sound.
        /// </summary>
        public bool DisableNotification { get; set; }

        /// <summary>
        /// If the messages are a reply, ID of the original message
        /// </summary>
        public int ReplyToMessageId { get; set; }

        /// <summary>
        /// Initializes a request with chat_id and media
        /// </summary>
        /// <param name="chatId">ID of target chat</param>
        /// <param name="media">Media items to send</param>
        public SendMediaGroupRequest(ChatId chatId, IEnumerable<InputMediaBase> media)
            : this()
        {
            ChatId = chatId;
            Media = media;
        }

        /// <summary>
        /// Initializes a new request
        /// </summary>
        public SendMediaGroupRequest()
            : base("sendMediaGroup")
        {
        }

        /// <summary>
        /// Generate content of HTTP message
        /// </summary>
        /// <param name="serializerSettings">JSON serialization setting</param>
        /// <returns>Content of HTTP request</returns>
        public override HttpContent ToHttpContent(JsonSerializerSettings serializerSettings)
        {
            var multipartContent = new MultipartFormDataContent(Guid.NewGuid().ToString() + DateTime.UtcNow.Ticks)
            {
                { new StringContent(ChatId), "chat_id" }
            };

            string mediaJsonArray = JsonConvert.SerializeObject(Media, serializerSettings);
            multipartContent.Add(new StringContent(mediaJsonArray, Encoding.UTF8, "application/json"), "media");

            foreach (var inputMediaType in Media.Where(m => m.Media.FileType == FileType.Stream).Select(m => m.Media))
            {
                string contentDisposision =
                    $"form-data; name=\"{inputMediaType.FileName}\"; filename=\"{inputMediaType.FileName}\""
                    .EncodeUtf8();

                HttpContent mediaPartContent = new StreamContent(inputMediaType.Content)
                {
                    Headers =
                    {
                        { "Content-Type", "application/octet-stream" },
                        { "Content-Disposition", contentDisposision }
                    }
                };

                multipartContent.Add(mediaPartContent, inputMediaType.FileName, inputMediaType.FileName);
            }

            string GetSankeCasedName(string name) => new SnakeCaseNamingStrategy().GetPropertyName(name, false);
            if (DisableNotification)
            {
                multipartContent.Add(new StringContent(true + ""), GetSankeCasedName(nameof(DisableNotification)));
            }
            if (ReplyToMessageId != default)
            {
                multipartContent.Add(new StringContent(ReplyToMessageId + ""), GetSankeCasedName(nameof(ReplyToMessageId)));
            }

            return multipartContent;
        }
    }
}

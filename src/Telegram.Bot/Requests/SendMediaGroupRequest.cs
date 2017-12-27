using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Telegram.Bot.Helpers;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Send a group of photos or videos as an album. On success, an array of the sent Messages is returned.
    /// </summary>
    public class SendMediaGroupRequest : FileRequestBase<Message[]>,
                                         INotifiableMessage,
                                         IReplyMessage
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target channel (in the format @channelusername)
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        /// A JSON-serialized array describing photos and videos to be sent, must include 2–10 items
        /// </summary>
        public IEnumerable<InputMediaBase> Media { get; set; }

        /// <inheritdoc />
        public bool DisableNotification { get; set; }

        /// <inheritdoc />
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
        { }

        /// <summary>
        /// Generate content of HTTP message
        /// </summary>
        /// <returns>Content of HTTP request</returns>
        public override HttpContent ToHttpContent()
        {
            // ToDo: base.GenerateMultipartFormDataContent();

            var multipartContent = new MultipartFormDataContent(Guid.NewGuid().ToString() + DateTime.UtcNow.Ticks)
            {
                { new StringContent(ChatId), nameof(ChatId).ToSnakeCased() }
            };

            string mediaJsonArray = JsonConvert.SerializeObject(Media);
            multipartContent.Add(
                new StringContent(mediaJsonArray, Encoding.UTF8, "application/json"),
                nameof(Media).ToSnakeCased());

            foreach (var inputMediaType in Media.Where(m => m.Media.FileType == FileType.Stream).Select(m => m.Media))
            {
                multipartContent.AddStreamContent(inputMediaType.Content, inputMediaType.FileName);
            }

            if (DisableNotification)
            {
                multipartContent.Add(new StringContent(true + ""), nameof(DisableNotification).ToSnakeCased());
            }
            if (ReplyToMessageId != default)
            {
                multipartContent.Add(new StringContent(ReplyToMessageId + ""), nameof(ReplyToMessageId).ToSnakeCased());
            }

            return multipartContent;
        }
    }
}

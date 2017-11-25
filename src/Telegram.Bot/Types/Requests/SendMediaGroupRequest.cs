using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Telegram.Bot.Helpers;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types.Requests
{
    public class SendMediaGroupRequest : RequestBase<SendMediaGroupResponse>
    {
        public ChatId ChatId { get; set; }

        public IEnumerable<InputMediaBase> Media { get; set; }

        public bool? DisableNotification { get; set; }

        public int? ReplyToMessageId { get; set; }

        public SendMediaGroupRequest(ChatId chatId, IEnumerable<InputMediaBase> media)
            : this()
        {
            ChatId = chatId;
            Media = media;
        }

        public SendMediaGroupRequest()
            : base("sendMediaGroup")
        {
        }

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

            return multipartContent;
        }
    }
}

using System;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using Telegram.Bot.Helpers;
using Telegram.Bot.Types;

namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Set a new profile photo for the chat. Photos can't be changed for private chats. The bot must be an administrator in the chat for this to work and must have the appropriate admin rights.
    /// </summary>
    public class SetChatPhotoRequest : RequestBase<bool>
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target channel (in the format @channelusername)
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        /// New chat photo
        /// </summary>
        public Stream Photo { get; set; }

        /// <summary>
        /// Initializes a new request with chatId and photo
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="photo">New chat photo</param>
        public SetChatPhotoRequest(ChatId chatId, Stream photo)
            : this()
        {
            ChatId = chatId;
            Photo = photo;
        }

        /// <summary>
        /// Initializes a new request
        /// </summary>
        public SetChatPhotoRequest()
            : base("setChatPhoto")
        { }

        /// <summary>
        /// Generate content of HTTP message
        /// </summary>
        /// <param name="serializerSettings">JSON serialization setting</param>
        /// <returns>Content of HTTP request</returns>
        public override HttpContent ToHttpContent(JsonSerializerSettings serializerSettings)
        {
            var multipartContent = new MultipartFormDataContent(Guid.NewGuid().ToString() + DateTime.UtcNow.Ticks)
            {
                { new StringContent(ChatId.ToString()), nameof(ChatId).ToSnakeCased() }
            };

            if (Photo != null)
            {
                multipartContent.AddStreamContent(Photo, nameof(Photo).ToSnakeCased());
            }

            return multipartContent;
        }
    }
}

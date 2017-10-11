using System.Collections.Generic;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to set a chat's photo
    /// </summary>
    public class SetChatPhotoRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetChatPhotoRequest"/> class
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="photo">The new profile picture for the chat.</param>
        public SetChatPhotoRequest(ChatId chatId, FileToSend photo) : base("setChatPhoto",
            new Dictionary<string, object> { { "chat_id", chatId } })
        {
            switch (photo.Type)
            {
                case FileType.Stream:
                    FileStream = photo.Content;
                    FileName = photo.Filename;
                    FileParameterName = "photo";
                    break;
                default:
                    Parameters.Add("photo", photo);
                    break;
            }
        }
    }
}

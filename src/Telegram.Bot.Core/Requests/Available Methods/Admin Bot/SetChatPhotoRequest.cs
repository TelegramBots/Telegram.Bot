using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Set a new profile photo for the chat. Photos can't be changed for private chats. The bot must be an administrator in the chat for this to work and must have the appropriate admin rights.
    /// </summary>
    public class SetChatPhotoRequest : FileRequestBase<bool>
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target channel (in the format @channelusername)
        /// </summary>
        public ChatId ChatId { get; }

        /// <summary>
        /// New chat photo
        /// </summary>
        public InputFileStream Photo { get; }

        /// <summary>
        /// Initializes a new request with chatId and photo
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="photo">New chat photo</param>
        public SetChatPhotoRequest(ChatId chatId, InputFileStream photo)
            : base("setChatPhoto")
        {
            ChatId = chatId;
            Photo = photo;
        }

        /// <param name="ct"></param>
        /// <inheritdoc />
        public override async ValueTask<HttpContent> ToHttpContentAsync(CancellationToken ct) =>
            Photo.FileType == FileType.Stream
                ? await ToMultipartFormDataContentAsync("photo", Photo, ct)
                : await base.ToHttpContentAsync(ct);
    }
}

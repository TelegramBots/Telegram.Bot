using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Helpers;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
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
        public ChatId ChatId { get; }

        /// <summary>
        /// A JSON-serialized array describing photos and videos to be sent, must include 2–10 items
        /// </summary>
        public IEnumerable<IAlbumInputMedia> Media { get; }

        /// <inheritdoc />
        public bool DisableNotification { get; set; }

        /// <inheritdoc />
        public int ReplyToMessageId { get; set; }

        /// <summary>
        /// Initializes a request with chat_id and media
        /// </summary>
        /// <param name="chatId">ID of target chat</param>
        /// <param name="media">Media items to send</param>
        [Obsolete("Use the other constructor. Only photo and video input types are allowed.")]
        public SendMediaGroupRequest(ChatId chatId, IEnumerable<InputMediaBase> media,
                                     ITelegramBotJsonConverter jsonConverter)
            : base(jsonConverter, "sendMediaGroup")
        {
            ChatId = chatId;
            Media = media
                .Select(m => m as IAlbumInputMedia)
                .Where(m => m != null)
                .ToArray();
        }

        /// <summary>
        /// Initializes a request with chat_id and media
        /// </summary>
        /// <param name="chatId">ID of target chat</param>
        /// <param name="media">Media items to send</param>
        public SendMediaGroupRequest(ChatId chatId, IEnumerable<IAlbumInputMedia> media,
                                     ITelegramBotJsonConverter jsonConverter)
            : base(jsonConverter, "sendMediaGroup")
        {
            ChatId = chatId;
            Media = media;
        }

        // ToDo: If there is no file stream in the request, request content should be string
        /// <param name="ct"></param>
        /// <inheritdoc />
        public override async ValueTask<HttpContent> ToHttpContentAsync(CancellationToken ct)
        {
            var httpContent = await GenerateMultipartFormDataContent(ct);
            httpContent.AddContentIfInputFileStream(Media.Cast<IInputMedia>().ToArray());
            return httpContent;
        }
    }
}

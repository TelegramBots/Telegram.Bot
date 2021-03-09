using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Helpers;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Use this method to send a group of photos, videos, documents or audios as an album. Documents and audio files can be only grouped in an album with messages of the same type. On success, an array of the sent Messages is returned.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class SendMediaGroupRequest : FileRequestBase<Message[]>,
        INotifiableMessage,
        IReplyMessage
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target channel (in the format @channelusername)
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public ChatId ChatId { get; }

        /// <summary>
        /// A JSON-serialized array of InputMediaAudio, InputMediaDocument, InputMediaPhoto and InputMediaVideo. describing messages to be sent, must include 2-10 items
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public IEnumerable<IAlbumInputMedia> Media { get; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool DisableNotification { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int ReplyToMessageId { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool AllowSendingWithoutReply { get; set; }

        /// <summary>
        /// Initializes a request with chat_id and media
        /// </summary>
        /// <param name="chatId">ID of target chat</param>
        /// <param name="media">Media items to send</param>
        [Obsolete("Use the other constructor. Only photo and video input types are allowed.")]
        public SendMediaGroupRequest(ChatId chatId, IEnumerable<InputMediaBase> media)
            : base("sendMediaGroup")
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
        public SendMediaGroupRequest(ChatId chatId, IEnumerable<IAlbumInputMedia> media)
            : base("sendMediaGroup")
        {
            ChatId = chatId;
            Media = media;
        }

        // ToDo: If there is no file stream in the request, request content should be string
        /// <inheritdoc />
        public override HttpContent ToHttpContent()
        {
            var httpContent = GenerateMultipartFormDataContent();
            httpContent.AddContentIfInputFileStream(Media.Cast<IInputMedia>().ToArray());
            return httpContent;
        }
    }
}

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults.Abstractions;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a link to a video animation (H.264/MPEG-4 AVC video without sound).
    /// By default, this animated MPEG-4 file will be sent by the user with optional caption.
    /// Alternatively, you can provide message_text to send it instead of the animation.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class InlineQueryResultMpeg4Gif : InlineQueryResultBase,
        ICaptionInlineQueryResult,
        IThumbnailUrlInlineQueryResult,
        ITitleInlineQueryResult,
        IInputMessageContentResult
    {
        /// <summary>
        /// A valid URL for the MP4 file. File size must not exceed 1MB.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Mpeg4Url { get; set; }

        /// <inheritdoc />
        [JsonProperty(Required = Required.Always)]
        public string ThumbUrl { get; set; }

        /// <summary>
        /// Optional. MIME type of the thumbnail, must be one of “image/jpeg”, “image/gif”, or “video/mp4”. Defaults to “image/jpeg”
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ThumbMimeType { get; set; }

        /// <summary>
        /// Optional. Video width
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Mpeg4Width { get; set; }

        /// <summary>
        /// Optional. Video height
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Mpeg4Height { get; set; }

        /// <summary>
        /// Optional. Duration of the Video
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Mpeg4Duration { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Caption { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ParseMode ParseMode { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public MessageEntity[] CaptionEntities { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Title { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public InputMessageContentBase InputMessageContent { get; set; }

        private InlineQueryResultMpeg4Gif()
            : base(InlineQueryResultType.Mpeg4Gif)
        {
        }

        /// <summary>
        /// Initializes a new inline query result
        /// </summary>
        /// <param name="id">Unique identifier of this result</param>
        /// <param name="mpeg4Url">A valid URL for the MP4 file. File size must not exceed 1MB.</param>
        /// <param name="thumbUrl">Url of the thumbnail for the result.</param>
        public InlineQueryResultMpeg4Gif(string id, string mpeg4Url, string thumbUrl)
            : base(InlineQueryResultType.Mpeg4Gif, id)
        {
            Mpeg4Url = mpeg4Url;
            ThumbUrl = thumbUrl;
        }
    }
}

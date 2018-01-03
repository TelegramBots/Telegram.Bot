using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.InlineQueryResults.Abstractions;
using Telegram.Bot.Types.InputMessageContents;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a link to a video animation (H.264/MPEG-4 AVC video without sound) stored on the Telegram servers. By default, this animated MPEG-4 file will be sent by the user with an optional caption. Alternatively, you can use input_message_content to send a message with the specified content instead of the animation.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class InlineQueryResultCachedMpeg4Gif : InlineQueryResult,
                                                   ICaptionInlineQueryResult,
                                                   ITitleInlineQueryResult,
                                                   IInputMessageContentResult
    {
        /// <summary>
        /// Initializes a new inline query result
        /// </summary>
        public InlineQueryResultCachedMpeg4Gif()
            : base(InlineQueryResultType.CachedMpeg4Gif)
        { }

        /// <summary>
        /// Initializes a new inline query result
        /// </summary>
        /// <param name="id">Unique identifier of this result</param>
        /// <param name="fileId">A valid file identifier for the MP4 file</param>
        public InlineQueryResultCachedMpeg4Gif(string id, string fileId)
            : this()
        {
            Id = id;
            FileId = fileId;
        }

        /// <summary>
        /// A valid file identifier for the MP4 file
        /// </summary>
        [JsonProperty("mpeg4_file_id", Required = Required.Always)]
        public string FileId { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Caption { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Title { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public InputMessageContent InputMessageContent { get; set; }
    }
}

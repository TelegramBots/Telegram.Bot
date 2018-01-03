using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.InlineQueryResults.Abstractions;
using Telegram.Bot.Types.InputMessageContents;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a link to a video file stored on the Telegram servers. By default, this video file will be sent by the user with an optional caption. Alternatively, you can use input_message_content to send a message with the specified content instead of the video.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class InlineQueryResultCachedVideo : InlineQueryResult,
                                                ICaptionInlineQueryResult,
                                                ITitleInlineQueryResult,
                                                IInputMessageContentResult
    {
        /// <summary>
        /// Initializes a new inline query result
        /// </summary>
        public InlineQueryResultCachedVideo()
            : base(InlineQueryResultType.CachedVideo)
        { }

        /// <summary>
        /// Initializes a new inline query result
        /// </summary>
        /// <param name="id">Unique identifier of this result</param>
        /// <param name="fileId">A valid file identifier for the video file</param>
        /// <param name="title">Title of the result</param>
        public InlineQueryResultCachedVideo(string id, string fileId, string title)
            : this()
        {
            FileId = fileId;
            Title = title;
        }

        /// <inheritdoc />
        [JsonProperty(Required = Required.Always)]
        public string Title { get; set; }

        /// <summary>
        /// A valid file identifier for the video file
        /// </summary>
        [JsonProperty("video_file_id", Required = Required.Always)]
        public string FileId { get; set; }

        /// <summary>
        /// Optional. Short description of the result
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Description { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Caption { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public InputMessageContent InputMessageContent { get; set; }
    }
}

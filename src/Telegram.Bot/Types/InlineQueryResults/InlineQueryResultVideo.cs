using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.InlineQueryResults.Abstractions;
using Telegram.Bot.Types.InputMessageContents;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents link to a page containing an embedded video player or a video file.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class InlineQueryResultVideo : InlineQueryResult,
                                          ICaptionInlineQueryResult,
                                          IThumbnailUrlInlineQueryResult,
                                          ITitleInlineQueryResult,
                                          IInputMessageContentResult
    {
        /// <summary>
        /// Initializes a new inline query result
        /// </summary>
        public InlineQueryResultVideo()
            : base(InlineQueryResultType.Video)
        { }

        /// <summary>
        /// Initializes a new inline query result
        /// </summary>
        /// <param name="id">Unique identifier of this result</param>
        /// <param name="videoUrl">A valid URL for the embedded video player or video file</param>
        /// <param name="mimeType">Mime type of the content of video url, i.e. "text/html" or "video/mp4"</param>
        /// <param name="thumbUrl">Url of the thumbnail for the result</param>
        /// <param name="title">Title of the result</param>
        public InlineQueryResultVideo(
            string id,
            Uri videoUrl,
            string mimeType,
            Uri thumbUrl,
            string title)
            : this()
        {
            Id = id;
            Url = videoUrl;
            MimeType = mimeType;
            ThumbUrl = thumbUrl;
            Title = title;
        }

        /// <summary>
        /// A valid URL for the embedded video player or video file
        /// </summary>
        [JsonProperty("video_url", Required = Required.Always)]
        public Uri Url { get; set; }

        /// <summary>
        /// Mime type of the content of video url, i.e. "text/html" or "video/mp4"
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string MimeType { get; set; }

        /// <inheritdoc />
        [JsonProperty(Required = Required.Always)]
        public Uri ThumbUrl { get; set; }

        /// <inheritdoc />
        [JsonProperty(Required = Required.Always)]
        public string Title { get; set; }

        /// <summary>
        /// Optional. Video width
        /// </summary>
        [JsonProperty("video_width", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Width { get; set; }

        /// <summary>
        /// Optional. Video height
        /// </summary>
        [JsonProperty("video_height", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Height { get; set; }

        /// <summary>
        /// Optional. Video duration in seconds
        /// </summary>
        [JsonProperty("video_duration", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Duration { get; set; }

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

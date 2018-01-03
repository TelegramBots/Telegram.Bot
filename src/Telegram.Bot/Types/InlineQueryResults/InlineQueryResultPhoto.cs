using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.InlineQueryResults.Abstractions;
using Telegram.Bot.Types.InputMessageContents;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a link to a photo.
    /// By default, this photo will be sent by the user with optional caption.
    /// Alternatively, you can provide message_text to send it instead of photo.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class InlineQueryResultPhoto : InlineQueryResult,
                                          ICaptionInlineQueryResult,
                                          IThumbnailUrlInlineQueryResult,
                                          ITitleInlineQueryResult,
                                          IInputMessageContentResult
    {
        /// <summary>
        /// Initializes a new inline query result
        /// </summary>
        public InlineQueryResultPhoto()
            : base(InlineQueryResultType.Photo)
        { }

        /// <summary>
        /// Initializes a new inline query result
        /// </summary>
        /// <param name="id">Unique identifier of this result</param>
        /// <param name="photoUrl">A valid URL of the photo. Photo size must not exceed 5MB.</param>
        /// <param name="thumbUrl">Optional. Url of the thumbnail for the result.</param>
        public InlineQueryResultPhoto(string id, Uri photoUrl, Uri thumbUrl)
            : this()
        {
            Id = id;
            Url = photoUrl;
            ThumbUrl = thumbUrl;
        }

        /// <inheritdoc />
        [JsonProperty(Required = Required.Always)]
        public Uri ThumbUrl { get; set; }

        /// <summary>
        /// A valid URL of the photo. Photo size must not exceed 5MB.
        /// </summary>
        [JsonProperty("photo_url", Required = Required.Always)]
        public Uri Url { get; set; }

        /// <summary>
        /// Optional. Width of the photo
        /// </summary>
        [JsonProperty("photo_width", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Width { get; set; }

        /// <summary>
        /// Optional. Height of the photo
        /// </summary>
        [JsonProperty("photo_height", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Height { get; set; }

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
        public string Title { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public InputMessageContent InputMessageContent { get; set; }
    }
}

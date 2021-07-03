using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults.Abstractions;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a link to a photo. By default, this photo will be sent by the user with optional caption. Alternatively, you can use <see cref="InlineQueryResultPhoto.InputMessageContent"/> to send a message with the specified content instead of the photo.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class InlineQueryResultPhoto : InlineQueryResult,
                                          ICaptionInlineQueryResult
    {
        /// <summary>
        /// Type of the result, must be photo
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public override InlineQueryResultType Type => InlineQueryResultType.Photo;

        /// <summary>
        /// A valid URL of the photo. Photo must be in <b>jpeg</b> format. Photo size must not exceed 5MB
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string PhotoUrl { get; }

        /// <summary>
        /// URL of the thumbnail for the photo
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string ThumbUrl { get; }

        /// <summary>
        /// Optional. Width of the photo
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? PhotoWidth { get; set; }

        /// <summary>
        /// Optional. Height of the photo
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? PhotoHeight { get; set; }

        /// <summary>
        /// Optional. Title for the result
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string? Title { get; set; }

        /// <summary>
        /// Optional. Short description of the result
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string? Description { get; set; }

        /// <summary>
        /// Optional. Caption of the photo to be sent, 0-1024 characters after entities parsing
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string? Caption { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ParseMode? ParseMode { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public MessageEntity[]? CaptionEntities { get; set; }

        /// <summary>
        /// Optional. Content of the message to be sent instead of the photo
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public InputMessageContent? InputMessageContent { get; set; }

        /// <summary>
        /// Initializes a new inline query representing a link to a photo
        /// </summary>
        /// <param name="id">Unique identifier of this result</param>
        /// <param name="photoUrl">A valid URL of the photo. Photo size must not exceed 5MB.</param>
        /// <param name="thumbUrl">Optional. Url of the thumbnail for the result.</param>
        public InlineQueryResultPhoto(string id, string photoUrl, string thumbUrl)
            : base(id)
        {
            PhotoUrl = photoUrl;
            ThumbUrl = thumbUrl;
        }
    }
}

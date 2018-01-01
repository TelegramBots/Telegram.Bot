using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.InlineQueryResults.Abstractions;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a link to an animated GIF file.
    /// By default, this animated GIF file will be sent by the user with optional caption.
    /// Alternatively, you can provide message_text to send it instead of the animation.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn,
                NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class InlineQueryResultGif : InlineQueryResult,
                                        ICaptionInlineQueryResult,
                                        IThumbnailUrlInlineQueryResult,
                                        ITitleInlineQueryResult
    {
        /// <summary>
        /// Initializes a new inline query result
        /// </summary>
        public InlineQueryResultGif()
        {
            Type = InlineQueryResultType.Gif;
        }

        /// <summary>
        /// A valid URL for the GIF file. File size must not exceed 1MB
        /// </summary>
        [JsonProperty("gif_url", Required = Required.Always)]
        public string Url { get; set; }

        /// <summary>
        /// Optional. Width of the GIF
        /// </summary>
        [JsonProperty("gif_width")]
        public int Width { get; set; }

        /// <summary>
        /// Optional. Height of the GIF
        /// </summary>
        [JsonProperty("gif_height", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int Height { get; set; }

        /// <summary>
        /// Optional. Duration of the GIF
        /// </summary>
        [JsonProperty("gif_duration")]
        public int Duration { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Caption { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string ThumbUrl { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string Title { get; set; }
    }
}

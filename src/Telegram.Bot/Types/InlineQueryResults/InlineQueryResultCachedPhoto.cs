using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.InlineQueryResults.Abstractions;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a link to a photo stored on the Telegram servers. By default, this photo will be sent by the user with an optional caption. Alternatively, you can use input_message_content to send a message with the specified content instead of the photo.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn,
                NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class InlineQueryResultCachedPhoto : InlineQueryResult,
                                                ICaptionInlineQueryResult,
                                                ITitleInlineQueryResult
    {
        /// <summary>
        /// Initializes a new inline query result
        /// </summary>
        public InlineQueryResultCachedPhoto()
        {
            Type = InlineQueryResultType.CachedPhoto;
        }

        /// <summary>
        /// A valid file identifier of the photo
        /// </summary>
        [JsonProperty("photo_file_id", Required = Required.Always)]
        public string FileId { get; set; }

        /// <summary>
        /// Optional. Short description of the result
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string Description { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Caption { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string Title { get; set; }
    }
}

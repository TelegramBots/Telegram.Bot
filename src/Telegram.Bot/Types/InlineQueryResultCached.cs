using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents a link to a result stored on the Telegram servers. By default, this result will be sent by the user with an optional caption. Alternatively, you can use input_message_content to send a message with the specified content instead of the photo.
    /// </summary>
    public class InlineQueryResultCached : InlineQueryResult
    {
        /// <summary>
        /// Optional. Short description of the result
        /// </summary>
        [JsonProperty("description", Required = Required.Default)]
        public string Description { get; set; }

        /// <summary>
        /// Optional. Caption of the result to be sent, 0-200 characters
        /// </summary>
        [JsonProperty("caption", Required = Required.Default)]
        public string Caption { get; set; }

        [JsonIgnore]
        public new string MessageText { get; set; }

        [JsonIgnore]
        public new ParseMode ParseMode { get; set; }

        [JsonIgnore]
        public new string ThumbUrl { get; set; }

        [JsonIgnore]
        public new bool DisableWebPagePreview { get; set; }
    }
}

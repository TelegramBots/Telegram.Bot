using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents a link to a result stored on the Telegram servers. By default, this result will be sent by the user with an optional caption. Alternatively, you can use input_message_content to send a message with the specified content instead of the photo.
    /// </summary>
    public class InlineQueryResultCached : InlineQueryResult
    {
        /// <summary>
        /// Optional. Caption of the result to be sent, 0-200 characters
        /// </summary>
        [JsonProperty("caption", Required = Required.Default)]
        public string Caption { get; set; }
    }
}

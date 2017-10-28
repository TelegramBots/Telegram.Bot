using Newtonsoft.Json;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a link to a result stored on the Telegram servers. By default, this result will be sent by the user with an optional caption. Alternatively, you can use input_message_content to send a message with the specified content instead of the photo.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class InlineQueryResultCached : InlineQueryResult
    {
        /// <summary>
        /// Optional. Caption of the result to be sent, 0-200 characters
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string Caption { get; set; }
    }
}

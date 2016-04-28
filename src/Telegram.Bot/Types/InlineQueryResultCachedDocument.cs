using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents a link to a file stored on the Telegram servers. By default, this file will be sent by the user with an optional caption. Alternatively, you can use input_message_content to send a message with the specified content instead of the file. Currently, only pdf-files and zip archives can be sent using this method.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class InlineQueryResultCachedDocument : InlineQueryResultCached
    {
        /// <summary>
        /// A valid file identifier for the file
        /// </summary>
        [JsonProperty("document_file_id", Required = Required.Always)]
        public string FileId { get; set; }

        /// <summary>
        /// Optional. Short description of the result
        /// </summary>
        [JsonProperty("description", Required = Required.Default)]
        public string Description { get; set; }
    }
}

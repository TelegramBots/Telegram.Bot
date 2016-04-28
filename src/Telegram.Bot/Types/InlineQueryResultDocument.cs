using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents a link to a file. By default, this file will be sent by the user with an optional caption. Alternatively, you can use input_message_content to send a message with the specified content instead of the file. Currently, only .PDF and .ZIP files can be sent using this method.
    /// </summary>
    /// <remarks>
    /// This will only work in Telegram versions released after 9 April, 2016. Older clients will ignore them.
    /// </remarks>
    [JsonObject(MemberSerialization.OptIn)]
    public class InlineQueryResultDocument : InlineQueryResultNew
    {
        /// <summary>
        /// Optional. Caption of the document to be sent, 0-200 characters
        /// </summary>
        [JsonProperty("caption", Required = Required.Default)]
        public string Caption { get; set; }

        /// <summary>
        /// A valid URL for the file
        /// </summary>
        [JsonProperty("document_url", Required = Required.Always)]
        public string Url { get; set; }

        /// <summary>
        /// Mime type of the content of the file, either “application/pdf” or “application/zip”
        /// </summary>
        [JsonProperty("mime_type", Required = Required.Always)]
        public string MimeType { get; set; }

        /// <summary>
        /// Optional. Short description of the result
        /// </summary>
        [JsonProperty("description", Required = Required.Always)]
        public string Description { get; set; }
    }
}

using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents a link to a photo stored on the Telegram servers. By default, this photo will be sent by the user with an optional caption. Alternatively, you can use input_message_content to send a message with the specified content instead of the photo.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class InlineQueryResultCachedPhoto : InlineQueryResultCached
    {
        /// <summary>
        /// A valid file identifier of the photo
        /// </summary>
        [JsonProperty("photo_file_id", Required = Required.Always)]
        public string FileId { get; set; }

        /// <summary>
        /// Optional. Short description of the result
        /// </summary>
        [JsonProperty("description", Required = Required.Default)]
        public string Description { get; set; }
    }
}

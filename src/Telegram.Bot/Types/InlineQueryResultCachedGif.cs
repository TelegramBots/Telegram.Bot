using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents a link to an animated GIF file stored on the Telegram servers. By default, this animated GIF file will be sent by the user with an optional caption. Alternatively, you can use input_message_content to send a message with specified content instead of the animation.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class InlineQueryResultCachedGif : InlineQueryResultCached
    {
        /// <summary>
        /// A valid file identifier for the GIF file
        /// </summary>
        [JsonProperty("gif_file_id", Required = Required.Always)]
        public string FileId { get; set; }
    }
}

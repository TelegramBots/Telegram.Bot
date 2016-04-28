using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents a link to a video animation (H.264/MPEG-4 AVC video without sound) stored on the Telegram servers. By default, this animated MPEG-4 file will be sent by the user with an optional caption. Alternatively, you can use input_message_content to send a message with the specified content instead of the animation.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class InlineQueryResultCachedMpeg4Gif : InlineQueryResultCached
    {
        /// <summary>
        /// A valid file identifier for the MP4 file
        /// </summary>
        [JsonProperty("mpeg4_file_id", Required = Required.Always)]
        public string FileId { get; set; }
    }
}

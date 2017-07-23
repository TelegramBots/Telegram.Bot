using System.ComponentModel;

using Newtonsoft.Json;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a link to an mp3 audio file stored on the Telegram servers. By default, this audio file will be sent by the user. Alternatively, you can use input_message_content to send a message with the specified content instead of the audio.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class InlineQueryResultAudio : InlineQueryResultNew
    {
        /// <summary>
        /// A valid file identifier for the audio file
        /// </summary>
        [JsonProperty("audio_file_id", Required = Required.Always)]
        public string FileId { get; set; }

        /// <summary>
        /// A valid URL for the audio file
        /// </summary>
        [JsonProperty("audio_url", Required = Required.Always)]
        public string Url { get; set; }

        /// <summary>
        /// Optional. Performer
        /// </summary>
        [JsonProperty("performer", Required = Required.Default, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string Performer { get; set; }

        /// <summary>
        /// Optional. Audio duration in seconds
        /// </summary>
        [JsonProperty("audio_duration", Required = Required.Always, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int Duration { get; set; }

        /// <summary>
        /// Optional. Url of the thumbnail for the result
        /// </summary>
        [JsonIgnore]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new string ThumbUrl { get; set; }

        /// <summary>
        /// Optional. Thumbnail width
        /// </summary>
        [JsonIgnore]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new int ThumbWidth { get; set; }

        /// <summary>
        /// Optional. Thumbnail height
        /// </summary>
        [JsonIgnore]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new int ThumbHeight { get; set; }
    }
}

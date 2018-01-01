using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.InlineQueryResults.Abstractions;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a link to an mp3 audio file stored on the Telegram servers. By default, this audio file will be sent by the user. Alternatively, you can use input_message_content to send a message with the specified content instead of the audio.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn,
                NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class InlineQueryResultCachedAudio : InlineQueryResult,
                                                ICaptionInlineQueryResult
    {
        /// <summary>
        /// Initializes a new inline query result
        /// </summary>
        public InlineQueryResultCachedAudio()
        {
            Type = InlineQueryResultType.CachedAudio;
        }

        /// <summary>
        /// A valid file identifier for the audio file
        /// </summary>
        [JsonProperty("audio_file_id", Required = Required.Always)]
        public string FileId { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Caption { get; set; }
    }
}

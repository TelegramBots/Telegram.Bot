using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.InlineQueryResults.Abstractions;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a link to a voice recording in an .ogg container encoded with OPUS. By default, this voice recording will be sent by the user. Alternatively, you can use <see cref="InlineQueryResult.InputMessageContent"/> to send a message with the specified content instead of the voice message.
    /// </summary>
    /// <remarks>
    /// This will only work in Telegram versions released after 9 April, 2016. Older clients will ignore them.
    /// </remarks>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn,
                NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class InlineQueryResultVoice : InlineQueryResult,
                                          ICaptionInlineQueryResult
    {
        /// <summary>
        /// Initializes a new inline query result
        /// </summary>
        public InlineQueryResultVoice()
        {
            Type = InlineQueryResultType.Voice;
        }

        /// <summary>
        /// A valid URL for the voice recording
        /// </summary>
        [JsonProperty("voice_url", Required = Required.Always)]
        public string Url { get; set; }

        /// <summary>
        /// Optional. Recording duration in seconds
        /// </summary>
        [JsonProperty("voice_duration", Required = Required.Always)]
        public int Duration { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Caption { get; set; }
    }
}

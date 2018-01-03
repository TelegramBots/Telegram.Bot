using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.InlineQueryResults.Abstractions;
using Telegram.Bot.Types.InputMessageContents;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a link to a voice recording in an .ogg container encoded with OPUS. By default, this voice recording will be sent by the user. Alternatively, you can use <see cref="InputMessageContents.InputMessageContent"/> to send a message with the specified content instead of the voice message.
    /// </summary>
    /// <remarks>
    /// This will only work in Telegram versions released after 9 April, 2016. Older clients will ignore them.
    /// </remarks>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class InlineQueryResultVoice : InlineQueryResult,
                                          ICaptionInlineQueryResult,
                                          ITitleInlineQueryResult,
                                          IInputMessageContentResult
    {
        /// <summary>
        /// Initializes a new inline query result
        /// </summary>
        public InlineQueryResultVoice()
            : base(InlineQueryResultType.Voice)
        { }

        /// <summary>
        /// Initializes a new inline query result
        /// </summary>
        /// <param name="id">Unique identifier of this result</param>
        /// <param name="voiceUrl">A valid URL for the voice recording</param>
        /// <param name="title">Title of the result</param>
        public InlineQueryResultVoice(string id, Uri voiceUrl, string title)
            : this()
        {
            Id = id;
            Url = voiceUrl;
            Title = title;
        }

        /// <summary>
        /// A valid URL for the voice recording
        /// </summary>
        [JsonProperty("voice_url", Required = Required.Always)]
        public Uri Url { get; set; }

        /// <inheritdoc />
        [JsonProperty(Required = Required.Always)]
        public string Title { get; set; }

        /// <summary>
        /// Optional. Recording duration in seconds
        /// </summary>
        [JsonProperty("voice_duration", Required = Required.Always)]
        public int Duration { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Caption { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public InputMessageContent InputMessageContent { get; set; }
    }
}

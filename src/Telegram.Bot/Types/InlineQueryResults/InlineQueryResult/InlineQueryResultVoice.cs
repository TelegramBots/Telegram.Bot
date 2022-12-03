using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types.InlineQueryResults;

/// <summary>
/// Represents a link to a voice recording in an .OGG container encoded with OPUS. By default, this
/// voice recording will be sent by the user. Alternatively, you can use
/// <see cref="InlineQueryResultVoice.InputMessageContent"/> to send a message with the specified
/// content instead of the the voice message.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class InlineQueryResultVoice : InlineQueryResult
{
    /// <summary>
    /// Type of the result, must be voice
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public override InlineQueryResultType Type => InlineQueryResultType.Voice;

    /// <summary>
    /// A valid URL for the voice recording
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string VoiceUrl { get; }

    /// <summary>
    /// Recording title
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Title { get; }

    /// <inheritdoc cref="Documentation.Caption" />
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Caption { get; set; }

    /// <inheritdoc cref="Documentation.ParseMode" />
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public ParseMode? ParseMode { get; set; }

    /// <inheritdoc cref="Documentation.CaptionEntities" />
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public MessageEntity[]? CaptionEntities { get; set; }

    /// <summary>
    /// Optional. Recording duration in seconds
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? VoiceDuration { get; set; }

    /// <inheritdoc cref="Documentation.InputMessageContent" />
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public InputMessageContent? InputMessageContent { get; set; }

    /// <summary>
    /// Initializes a new inline query result
    /// </summary>
    /// <param name="id">Unique identifier of this result</param>
    /// <param name="voiceUrl">A valid URL for the voice recording</param>
    /// <param name="title">Title of the result</param>
    public InlineQueryResultVoice(string id, string voiceUrl, string title)
        : base(id)
    {
        VoiceUrl = voiceUrl;
        Title = title;
    }
}

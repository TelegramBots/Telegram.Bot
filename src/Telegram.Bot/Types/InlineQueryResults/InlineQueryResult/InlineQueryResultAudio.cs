using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types.InlineQueryResults;

/// <summary>
/// Represents a link to an MP3 audio file. By default, this audio file will be sent by the user.
/// Alternatively, you can use <see cref="InlineQueryResultAudio.InputMessageContent"/> to send
/// a message with the specified content instead of the audio.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class InlineQueryResultAudio : InlineQueryResult
{
    /// <summary>
    /// Type of the result, must be audio
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public override InlineQueryResultType Type => InlineQueryResultType.Audio;

    /// <summary>
    /// A valid URL for the audio file
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string AudioUrl { get; }

    /// <summary>
    /// Title
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
    /// Optional. Performer
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Performer { get; set; }

    /// <summary>
    /// Optional. Audio duration in seconds
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? AudioDuration { get; set; }

    /// <inheritdoc cref="Documentation.InputMessageContent" />
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public InputMessageContent? InputMessageContent { get; set; }

    /// <summary>
    /// Initializes a new inline query result
    /// </summary>
    /// <param name="id">Unique identifier of this result</param>
    /// <param name="audioUrl">A valid URL for the audio file</param>
    /// <param name="title">Title of the result</param>
    public InlineQueryResultAudio(string id, string audioUrl, string title)
        : base(id)
    {
        AudioUrl = audioUrl;
        Title = title;
    }
}

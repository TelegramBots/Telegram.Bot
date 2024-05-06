using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types.InlineQueryResults;

/// <summary>
/// Represents a link to an MP3 audio file stored on the Telegram servers. By default, this audio
/// file will be sent by the user. Alternatively, you can use
/// <see cref="InlineQueryResultCachedAudio.InputMessageContent"/> to send a message with the
/// specified content instead of the audio.
/// </summary>
public class InlineQueryResultCachedAudio : InlineQueryResult
{
    /// <summary>
    /// Type of the result, must be audio
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override InlineQueryResultType Type => InlineQueryResultType.Audio;

    /// <summary>
    /// A valid file identifier for the audio file
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string AudioFileId { get; init; }

    /// <inheritdoc cref="Documentation.Caption" />
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Caption { get; set; }

    /// <inheritdoc cref="Documentation.ParseMode" />
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ParseMode? ParseMode { get; set; }

    /// <inheritdoc cref="Documentation.CaptionEntities" />
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public MessageEntity[]? CaptionEntities { get; set; }

    /// <inheritdoc cref="Documentation.InputMessageContent" />
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public InputMessageContent? InputMessageContent { get; set; }

    /// <summary>
    /// Initializes a new inline query result
    /// </summary>
    /// <param name="id">Unique identifier of this result</param>
    /// <param name="audioFileId">A valid file identifier for the audio file</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public InlineQueryResultCachedAudio(string id, string audioFileId)
        : base(id)
    {
        AudioFileId = audioFileId;
    }

    /// <summary>
    /// Initializes a new inline query result
    /// </summary>
    public InlineQueryResultCachedAudio()
    { }
}

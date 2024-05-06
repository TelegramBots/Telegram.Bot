using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types.InlineQueryResults;

/// <summary>
/// Represents a link to a video animation (H.264/MPEG-4 AVC video without sound) stored on the
/// Telegram servers. By default, this animated MPEG-4 file will be sent by the user with an
/// optional caption. Alternatively, you can use
/// <see cref="InlineQueryResultCachedMpeg4Gif.InputMessageContent"/> to send a message with
/// the specified content instead of the animation.
/// </summary>
public class InlineQueryResultCachedMpeg4Gif : InlineQueryResult
{
    /// <summary>
    /// Type of the result, must be mpeg4_gif
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override InlineQueryResultType Type => InlineQueryResultType.Mpeg4Gif;

    /// <summary>
    /// A valid file identifier for the MP4 file
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Mpeg4FileId { get; init; }

    /// <summary>
    /// Optional. Title for the result
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Title { get; set; }

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
    /// <param name="mpeg4FileId">A valid file identifier for the MP4 file</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public InlineQueryResultCachedMpeg4Gif(string id, string mpeg4FileId)
        : base(id)
    {
        Mpeg4FileId = mpeg4FileId;
    }

    /// <summary>
    /// Initializes a new inline query result
    /// </summary>
    public InlineQueryResultCachedMpeg4Gif()
    { }
}

using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types.InlineQueryResults;

/// <summary>
/// Represents a link to a sticker stored on the Telegram servers. By default, this sticker will
/// be sent by the user. Alternatively, you can use
/// <see cref="InlineQueryResultCachedSticker.InputMessageContent"/> to send a message with
/// the specified content instead of the sticker.
/// </summary>
public class InlineQueryResultCachedSticker : InlineQueryResult
{
    /// <summary>
    /// Type of the result, must be sticker
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override InlineQueryResultType Type => InlineQueryResultType.Sticker;

    /// <summary>
    /// A valid file identifier of the sticker
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string StickerFileId { get; init; }

    /// <inheritdoc cref="Documentation.InputMessageContent" />
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public InputMessageContent? InputMessageContent { get; set; }

    /// <summary>
    /// Initializes a new inline query result
    /// </summary>
    /// <param name="id">Unique identifier of this result</param>
    /// <param name="stickerFileId">A valid file identifier of the sticker</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public InlineQueryResultCachedSticker(string id, string stickerFileId)
        : base(id)
    {
        StickerFileId = stickerFileId;
    }

    /// <summary>
    /// Initializes a new inline query result
    /// </summary>
    public InlineQueryResultCachedSticker()
    { }
}

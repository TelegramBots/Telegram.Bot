

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types.InlineQueryResults;

/// <summary>
/// Represents a link to a sticker stored on the Telegram servers. By default, this sticker will
/// be sent by the user. Alternatively, you can use
/// <see cref="InlineQueryResultCachedSticker.InputMessageContent"/> to send a message with
/// the specified content instead of the sticker.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class InlineQueryResultCachedSticker : InlineQueryResult
{
    /// <summary>
    /// Type of the result, must be sticker
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public override InlineQueryResultType Type => InlineQueryResultType.Sticker;

    /// <summary>
    /// A valid file identifier of the sticker
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string StickerFileId { get; }

    /// <inheritdoc cref="Documentation.InputMessageContent" />
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public InputMessageContent? InputMessageContent { get; set; }

    /// <summary>
    /// Initializes a new inline query result
    /// </summary>
    /// <param name="id">Unique identifier of this result</param>
    /// <param name="stickerFileId">A valid file identifier of the sticker</param>
    public InlineQueryResultCachedSticker(string id, string stickerFileId)
        : base(id)
    {
        StickerFileId = stickerFileId;
    }
}

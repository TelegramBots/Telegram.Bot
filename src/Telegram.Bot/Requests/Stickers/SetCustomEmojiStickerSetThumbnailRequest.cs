namespace Telegram.Bot.Requests;

/// <summary>Use this method to set the thumbnail of a custom emoji sticker set.<para>Returns: </para></summary>
public partial class SetCustomEmojiStickerSetThumbnailRequest : RequestBase<bool>
{
    /// <summary>Sticker set name</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Name { get; set; }

    /// <summary>Custom emoji identifier of a sticker from the sticker set; pass an empty string to drop the thumbnail and use the first sticker as the thumbnail.</summary>
    public string? CustomEmojiId { get; set; }

    /// <summary>Instantiates a new <see cref="SetCustomEmojiStickerSetThumbnailRequest"/></summary>
    public SetCustomEmojiStickerSetThumbnailRequest() : base("setCustomEmojiStickerSetThumbnail") { }
}

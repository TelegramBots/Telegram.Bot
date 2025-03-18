// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to set the thumbnail of a custom emoji sticker set.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SetCustomEmojiStickerSetThumbnailRequest() : RequestBase<bool>("setCustomEmojiStickerSetThumbnail")
{
    /// <summary>Sticker set name</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Name { get; set; }

    /// <summary>Custom emoji identifier of a sticker from the sticker set; pass an empty string to drop the thumbnail and use the first sticker as the thumbnail.</summary>
    [JsonPropertyName("custom_emoji_id")]
    public string? CustomEmojiId { get; set; }
}

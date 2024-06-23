namespace Telegram.Bot.Types;

/// <summary>This object represents a sticker.</summary>
public partial class Sticker : FileBase
{
    /// <summary>Type of the sticker, currently one of <see cref="StickerType.Regular">Regular</see>, <see cref="StickerType.Mask">Mask</see>, <see cref="StickerType.CustomEmoji">CustomEmoji</see>. The type of the sticker is independent from its format, which is determined by the fields <see cref="IsAnimated">IsAnimated</see> and <see cref="IsVideo">IsVideo</see>.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public StickerType Type { get; set; }

    /// <summary>Sticker width</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Width { get; set; }

    /// <summary>Sticker height</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Height { get; set; }

    /// <summary><see langword="true"/>, if the sticker is <a href="https://telegram.org/blog/animated-stickers">animated</a></summary>
    public bool IsAnimated { get; set; }

    /// <summary><see langword="true"/>, if the sticker is a <a href="https://telegram.org/blog/video-stickers-better-reactions">video sticker</a></summary>
    public bool IsVideo { get; set; }

    /// <summary><em>Optional</em>. Sticker thumbnail in the .WEBP or .JPG format</summary>
    public PhotoSize? Thumbnail { get; set; }

    /// <summary><em>Optional</em>. Emoji associated with the sticker</summary>
    public string? Emoji { get; set; }

    /// <summary><em>Optional</em>. Name of the sticker set to which the sticker belongs</summary>
    public string? SetName { get; set; }

    /// <summary><em>Optional</em>. For premium regular stickers, premium animation for the sticker</summary>
    public File? PremiumAnimation { get; set; }

    /// <summary><em>Optional</em>. For mask stickers, the position where the mask should be placed</summary>
    public MaskPosition? MaskPosition { get; set; }

    /// <summary><em>Optional</em>. For custom emoji stickers, unique identifier of the custom emoji</summary>
    public string? CustomEmojiId { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the sticker must be repainted to a text color in messages, the color of the Telegram Premium badge in emoji status, white color on chat photos, or another appropriate color in other places</summary>
    public bool NeedsRepainting { get; set; }
}

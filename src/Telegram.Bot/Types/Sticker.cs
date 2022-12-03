using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a sticker.
/// <a href="https://core.telegram.org/bots/api#sticker"/>
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class Sticker : FileBase
{
    /// <summary>
    /// Type of the sticker. The type of the sticker is independent from its format,
    /// which is determined by the fields <see cref="IsAnimated"/> and <see cref="IsVideo"/>.
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public StickerType Type { get; set; }

    /// <summary>
    /// Sticker width
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int Width { get; set; }

    /// <summary>
    /// Sticker height
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int Height { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the sticker is animated
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool IsAnimated { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the sticker is a video sticker
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool IsVideo { get; set; }

    /// <summary>
    /// Optional. Sticker thumbnail in the .WEBP or .JPG format
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public PhotoSize? Thumb { get; set; }

    /// <summary>
    /// Optional. Emoji associated with the sticker
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Emoji { get; set; }

    /// <summary>
    /// Optional. Name of the sticker set to which the sticker belongs
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? SetName { get; set; }

    /// <summary>
    /// Optional. Premium animation for the sticker, if the sticker is premium
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public File? PremiumAnimation { get; set; }

    /// <summary>
    /// Optional. For mask stickers, the position where the mask should be placed
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public MaskPosition? MaskPosition { get; set; }

    /// <summary>
    /// Optional. For custom emoji stickers, unique identifier of the custom emoji
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? CustomEmojiId { get; set; }
}

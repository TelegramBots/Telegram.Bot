using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a sticker set.
/// <a href="https://core.telegram.org/bots/api#stickerset"/>
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class StickerSet
{
    /// <summary>
    /// Sticker set name
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Name { get; set; } = default!;

    /// <summary>
    /// Sticker set title
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Title { get; set; } = default!;

    /// <summary>
    /// Type of stickers in the set
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public StickerType StickerType { get; set; } = default!;

    /// <summary>
    /// <see langword="true"/>, if the sticker set contains animated stickers
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool IsAnimated { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the sticker set contains video stickers
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool IsVideo { get; set; }

    /// <summary>
    /// List of all set stickers
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public Sticker[] Stickers { get; set; } = default!;

    /// <summary>
    /// Optional. Sticker set thumbnail in the .WEBP or .TGS format
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public PhotoSize? Thumb { get; set; }
}

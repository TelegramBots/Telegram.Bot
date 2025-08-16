// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object describes a unique gift that was upgraded from a regular gift.</summary>
public partial class UniqueGift
{
    /// <summary>Human-readable name of the regular gift from which this unique gift was upgraded</summary>
    [JsonPropertyName("base_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string BaseName { get; set; } = default!;

    /// <summary>Unique name of the gift. This name can be used in <c>https://t.me/nft/...</c> links and story areas</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Name { get; set; } = default!;

    /// <summary>Unique number of the upgraded gift among gifts upgraded from the same regular gift</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Number { get; set; }

    /// <summary>Model of the gift</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public UniqueGiftModel Model { get; set; } = default!;

    /// <summary>Symbol of the gift</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public UniqueGiftSymbol Symbol { get; set; } = default!;

    /// <summary>Backdrop of the gift</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public UniqueGiftBackdrop Backdrop { get; set; } = default!;

    /// <summary><em>Optional</em>. Information about the chat that published the gift</summary>
    [JsonPropertyName("publisher_chat")]
    public Chat? PublisherChat { get; set; }
}

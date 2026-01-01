// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents a gift that can be sent by the bot.</summary>
public partial class Gift
{
    /// <summary>Unique identifier of the gift</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Id { get; set; } = default!;

    /// <summary>The sticker that represents the gift</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Sticker Sticker { get; set; } = default!;

    /// <summary>The number of Telegram Stars that must be paid to send the sticker</summary>
    [JsonPropertyName("star_count")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public long StarCount { get; set; }

    /// <summary><em>Optional</em>. The number of Telegram Stars that must be paid to upgrade the gift to a unique one</summary>
    [JsonPropertyName("upgrade_star_count")]
    public long? UpgradeStarCount { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the gift can only be purchased by Telegram Premium subscribers</summary>
    [JsonPropertyName("is_premium")]
    public bool IsPremium { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the gift can be used (after being upgraded) to customize a user's appearance</summary>
    [JsonPropertyName("has_colors")]
    public bool HasColors { get; set; }

    /// <summary><em>Optional</em>. The total number of gifts of this type that can be sent by all users; for limited gifts only</summary>
    [JsonPropertyName("total_count")]
    public int? TotalCount { get; set; }

    /// <summary><em>Optional</em>. The number of remaining gifts of this type that can be sent by all users; for limited gifts only</summary>
    [JsonPropertyName("remaining_count")]
    public int? RemainingCount { get; set; }

    /// <summary><em>Optional</em>. The total number of gifts of this type that can be sent by the bot; for limited gifts only</summary>
    [JsonPropertyName("personal_total_count")]
    public int? PersonalTotalCount { get; set; }

    /// <summary><em>Optional</em>. The number of remaining gifts of this type that can be sent by the bot; for limited gifts only</summary>
    [JsonPropertyName("personal_remaining_count")]
    public int? PersonalRemainingCount { get; set; }

    /// <summary><em>Optional</em>. Background of the gift</summary>
    public GiftBackground? Background { get; set; }

    /// <summary><em>Optional</em>. The total number of different unique gifts that can be obtained by upgrading the gift</summary>
    [JsonPropertyName("unique_gift_variant_count")]
    public int? UniqueGiftVariantCount { get; set; }

    /// <summary><em>Optional</em>. Information about the chat that published the gift</summary>
    [JsonPropertyName("publisher_chat")]
    public Chat? PublisherChat { get; set; }
}

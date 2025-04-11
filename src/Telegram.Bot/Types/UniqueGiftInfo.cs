// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes a service message about a unique gift that was sent or received.</summary>
public partial class UniqueGiftInfo
{
    /// <summary>Information about the gift</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public UniqueGift Gift { get; set; } = default!;

    /// <summary>Origin of the gift. Currently, either “upgrade” or “transfer”</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Origin { get; set; } = default!;

    /// <summary><em>Optional</em>. Unique identifier of the received gift for the bot; only present for gifts received on behalf of business accounts</summary>
    [JsonPropertyName("owned_gift_id")]
    public string? OwnedGiftId { get; set; }

    /// <summary><em>Optional</em>. Number of Telegram Stars that must be paid to transfer the gift; omitted if the bot cannot transfer the gift</summary>
    [JsonPropertyName("transfer_star_count")]
    public int? TransferStarCount { get; set; }
}

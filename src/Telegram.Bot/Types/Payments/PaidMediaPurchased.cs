// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Payments;

/// <summary>This object contains information about a paid media purchase.</summary>
public partial class PaidMediaPurchased
{
    /// <summary>User who purchased the media</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public User From { get; set; } = default!;

    /// <summary>Bot-specified paid media payload</summary>
    [JsonPropertyName("paid_media_payload")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string PaidMediaPayload { get; set; } = default!;
}

// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Contains information about a suggested post.</summary>
public partial class SuggestedPostInfo
{
    /// <summary>State of the suggested post. Currently, it can be one of “pending”, “approved”, “declined”.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string State { get; set; } = default!;

    /// <summary><em>Optional</em>. Proposed price of the post. If the field is omitted, then the post is unpaid.</summary>
    public SuggestedPostPrice? Price { get; set; }

    /// <summary><em>Optional</em>. Proposed send date of the post. If the field is omitted, then the post can be published at any time within 30 days at the sole discretion of the user or administrator who approves it.</summary>
    [JsonPropertyName("send_date")]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime? SendDate { get; set; }
}

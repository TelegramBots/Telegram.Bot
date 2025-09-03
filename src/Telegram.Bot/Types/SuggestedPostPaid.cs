// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes a service message about a successful payment for a suggested post.</summary>
public partial class SuggestedPostPaid
{
    /// <summary><em>Optional</em>. Message containing the suggested post. Note that the <see cref="Message"/> object in this field will not contain the <em>ReplyToMessage</em> field even if it itself is a reply.</summary>
    [JsonPropertyName("suggested_post_message")]
    public Message? SuggestedPostMessage { get; set; }

    /// <summary>Currency in which the payment was made. Currently, one of “XTR” for Telegram Stars or “TON” for toncoins</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Currency { get; set; } = default!;

    /// <summary><em>Optional</em>. The amount of the currency that was received by the channel in nanotoncoins; for payments in toncoins only</summary>
    public long? Amount { get; set; }

    /// <summary><em>Optional</em>. The amount of Telegram Stars that was received by the channel; for payments in Telegram Stars only</summary>
    [JsonPropertyName("star_amount")]
    public StarAmount? StarAmount { get; set; }
}

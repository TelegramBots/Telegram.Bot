// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes a service message about a transfer with a TON wallet.</summary>
public partial class TonWalletTransfer
{
    /// <summary>Unique identifier of the transaction; may be empty if the transfer hasn't been sent yet</summary>
    [JsonPropertyName("transaction_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string TransactionId { get; set; } = default!;

    /// <summary>Address of the transfer peer in the TON blockchain</summary>
    [JsonPropertyName("peer_address")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string PeerAddress { get; set; } = default!;

    /// <summary>The amount of transferred cryptocurrency, in the <em>smallest units</em> of the cryptocurrency, as a decimal number; negative for outgoing transfers</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Amount { get; set; } = default!;

    /// <summary><em>Optional</em>. Comment added to the transfer; omitted if the comment is encrypted</summary>
    public string? Comment { get; set; }
}

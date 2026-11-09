// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object describes a TON wallet of the user, which is used in a query from a widget.</summary>
public partial class WidgetQueryWallet
{
    /// <summary>Address of the wallet</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Address { get; set; } = default!;

    /// <summary>Balance of the wallet, in the <em>smallest units</em> of the cryptocurrency, as a decimal number</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Amount { get; set; } = default!;

    /// <summary><em>Optional</em>. Hex-encoded public key of the wallet; omitted if unknown</summary>
    [JsonPropertyName("public_key")]
    public string? PublicKey { get; set; }

    /// <summary><em>Optional</em>. Base64-encoded serialized StateInit of the wallet contract; omitted if and only if <see cref="PublicKey">PublicKey</see> is omitted</summary>
    [JsonPropertyName("state_init")]
    public string? StateInit { get; set; }
}

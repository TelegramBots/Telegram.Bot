// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Passport;

/// <summary>This object represents the data of an identity document.</summary>
public partial class IdDocumentData : IDecryptedValue
{
    /// <summary>Document number</summary>
    [JsonPropertyName("document_no")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string DocumentNo { get; set; } = default!;

    /// <summary><em>Optional.</em> Date of expiry, in DD.MM.YYYY format</summary>
    [JsonPropertyName("expiry_date")]
    public string? ExpiryDate { get; set; }
}

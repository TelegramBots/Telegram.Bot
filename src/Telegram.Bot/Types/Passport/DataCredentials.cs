// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Passport;

/// <summary>These credentials can be used to decrypt encrypted data from the <em>data</em> field in <a href="https://core.telegram.org/bots/api#encryptedpassportelement">EncryptedPassportElement</a>.</summary>
public partial class DataCredentials
{
    /// <summary>Checksum of encrypted data</summary>
    [JsonPropertyName("data_hash")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string DataHash { get; set; } = default!;

    /// <summary>Secret of encrypted data</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Secret { get; set; } = default!;
}

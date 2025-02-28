// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Passport;

/// <summary>These credentials can be used to decrypt encrypted files from the <em>FrontSide</em>, <em>ReverseSide</em>, <em>selfie</em>, <em>files</em> and <em>translation</em> fields in <a href="https://core.telegram.org/bots/api#encryptedpassportelement">EncryptedPassportElement</a>.</summary>
public partial class FileCredentials
{
    /// <summary>Checksum of encrypted file</summary>
    [JsonPropertyName("file_hash")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string FileHash { get; set; } = default!;

    /// <summary>Secret of encrypted file</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Secret { get; set; } = default!;
}

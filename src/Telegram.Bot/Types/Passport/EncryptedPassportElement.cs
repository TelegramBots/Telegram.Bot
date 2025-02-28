// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Passport;

/// <summary>Describes documents or other Telegram Passport elements shared with the bot by the user.</summary>
public partial class EncryptedPassportElement
{
    /// <summary>Element type. One of <see cref="EncryptedPassportElementType.PersonalDetails">PersonalDetails</see>, <see cref="EncryptedPassportElementType.Passport">Passport</see>, <see cref="EncryptedPassportElementType.DriverLicense">DriverLicense</see>, <see cref="EncryptedPassportElementType.IdentityCard">IdentityCard</see>, <see cref="EncryptedPassportElementType.InternalPassport">InternalPassport</see>, <see cref="EncryptedPassportElementType.Address">Address</see>, <see cref="EncryptedPassportElementType.UtilityBill">UtilityBill</see>, <see cref="EncryptedPassportElementType.BankStatement">BankStatement</see>, <see cref="EncryptedPassportElementType.RentalAgreement">RentalAgreement</see>, <see cref="EncryptedPassportElementType.PassportRegistration">PassportRegistration</see>, <see cref="EncryptedPassportElementType.TemporaryRegistration">TemporaryRegistration</see>, <see cref="EncryptedPassportElementType.PhoneNumber">PhoneNumber</see>, <see cref="EncryptedPassportElementType.Email">Email</see>.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public EncryptedPassportElementType Type { get; set; }

    /// <summary><em>Optional</em>. Base64-encoded encrypted Telegram Passport element data provided by the user; available only for <see cref="PersonalDetails"/>, “passport”, “DriverLicense”, “IdentityCard”, “InternalPassport” and “address” types. Can be decrypted and verified using the accompanying <see cref="EncryptedCredentials"/>.</summary>
    public string? Data { get; set; }

    /// <summary><em>Optional</em>. User's verified phone number; available only for “<see cref="PhoneNumber">PhoneNumber</see>” type</summary>
    [JsonPropertyName("phone_number")]
    public string? PhoneNumber { get; set; }

    /// <summary><em>Optional</em>. User's verified email address; available only for “email” type</summary>
    public string? Email { get; set; }

    /// <summary><em>Optional</em>. Array of encrypted files with documents provided by the user; available only for “UtilityBill”, “BankStatement”, “RentalAgreement”, “PassportRegistration” and “TemporaryRegistration” types. Files can be decrypted and verified using the accompanying <see cref="EncryptedCredentials"/>.</summary>
    public PassportFile[]? Files { get; set; }

    /// <summary><em>Optional</em>. Encrypted file with the front side of the document, provided by the user; available only for “passport”, “DriverLicense”, “IdentityCard” and “InternalPassport”. The file can be decrypted and verified using the accompanying <see cref="EncryptedCredentials"/>.</summary>
    [JsonPropertyName("front_side")]
    public PassportFile? FrontSide { get; set; }

    /// <summary><em>Optional</em>. Encrypted file with the reverse side of the document, provided by the user; available only for “DriverLicense” and “IdentityCard”. The file can be decrypted and verified using the accompanying <see cref="EncryptedCredentials"/>.</summary>
    [JsonPropertyName("reverse_side")]
    public PassportFile? ReverseSide { get; set; }

    /// <summary><em>Optional</em>. Encrypted file with the selfie of the user holding a document, provided by the user; available if requested for “passport”, “DriverLicense”, “IdentityCard” and “InternalPassport”. The file can be decrypted and verified using the accompanying <see cref="EncryptedCredentials"/>.</summary>
    public PassportFile? Selfie { get; set; }

    /// <summary><em>Optional</em>. Array of encrypted files with translated versions of documents provided by the user; available if requested for “passport”, “DriverLicense”, “IdentityCard”, “InternalPassport”, “UtilityBill”, “BankStatement”, “RentalAgreement”, “PassportRegistration” and “TemporaryRegistration” types. Files can be decrypted and verified using the accompanying <see cref="EncryptedCredentials"/>.</summary>
    public PassportFile[]? Translation { get; set; }

    /// <summary>Base64-encoded element hash for using in <see cref="PassportElementErrorUnspecified"/></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Hash { get; set; } = default!;
}

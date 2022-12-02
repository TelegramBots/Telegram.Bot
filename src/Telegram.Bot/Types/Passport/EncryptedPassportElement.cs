using static Telegram.Bot.Types.Passport.EncryptedPassportElementType;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types.Passport;

/// <summary>
/// Contains information about documents or other Telegram Passport elements shared with the bot by the user.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class EncryptedPassportElement
{
    /// <summary>
    /// Element type. One of <see cref="EncryptedPassportElementType"/>
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public EncryptedPassportElementType Type { get; set; }

    /// <summary>
    /// Optional. Base64-encoded encrypted Telegram Passport element data provided by the user, available for
    /// <see cref="PersonalDetails"/>, <see cref="Passport"/>, <see cref="DriverLicence"/>,
    /// <see cref="IdentityCard"/>, <see cref="InternalPassport"/> and <see cref="Address"/>
    /// types. Can be decrypted and verified using the accompanying <see cref="EncryptedCredentials"/>.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Data { get; set; }

    /// <summary>
    /// Optional. User's verified phone number, available only for <see cref="PhoneNumber"/> type.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Optional. User's verified email address, available only for <see cref="Email"/> type.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Email { get; set; }

    /// <summary>
    /// Optional. Array of encrypted files with documents provided by the user, available for
    /// <see cref="UtilityBill"/>, <see cref="BankStatement"/>, <see cref="RentalAgreement"/>,
    /// <see cref="PassportRegistration"/> and <see cref="TemporaryRegistration"/> types.
    /// Files can be decrypted and verified using the accompanying <see cref="EncryptedCredentials"/>.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public PassportFile[]? Files { get; set; }

    /// <summary>
    /// Optional. Encrypted file with the front side of the document, provided by the user. Available for
    /// <see cref="Passport"/>, <see cref="DriverLicence"/>, <see cref="IdentityCard"/> and
    /// <see cref="InternalPassport"/>. The file can be decrypted and verified using the accompanying
    /// <see cref="EncryptedCredentials"/>.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public PassportFile? FrontSide { get; set; }

    /// <summary>
    /// Optional. Encrypted file with the reverse side of the document, provided by the user. Available for
    /// <see cref="DriverLicence"/> and <see cref="IdentityCard"/>. The file can be decrypted and verified using
    /// the accompanying <see cref="EncryptedCredentials"/>.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public PassportFile? ReverseSide { get; set; }

    /// <summary>
    /// Optional. Encrypted file with the selfie of the user holding a document, provided by the user;
    /// available for <see cref="Passport"/>, <see cref="DriverLicence"/>, <see cref="IdentityCard"/> and
    /// <see cref="InternalPassport"/>. The file can be decrypted and verified using the accompanying
    /// <see cref="EncryptedCredentials"/>.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public PassportFile? Selfie { get; set; }

    /// <summary>
    /// Optional. Array of encrypted files with translated versions of documents provided by the user.
    /// Available if requested for <see cref="Passport"/>, <see cref="DriverLicence"/>,
    /// <see cref="IdentityCard"/>, <see cref="InternalPassport"/>, <see cref="UtilityBill"/>,
    /// <see cref="BankStatement"/>, <see cref="RentalAgreement"/>, <see cref="PassportRegistration"/> and
    /// <see cref="TemporaryRegistration"/> types. Files can be decrypted and verified using the accompanying
    /// <see cref="EncryptedCredentials"/>.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public PassportFile[]? Translation { get; set; }

    /// <summary>
    /// Base64-encoded element hash for using in PassportElementErrorUnspecified
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Hash { get; set; } = default!;
}

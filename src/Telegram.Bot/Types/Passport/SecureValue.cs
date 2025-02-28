// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Passport;

/// <summary>This object represents the credentials required to decrypt encrypted values. All fields are optional and depend on the type of <a href="https://core.telegram.org/bots/api#fields">fields</a> that were requested.</summary>
public partial class SecureValue
{
    /// <summary><em>Optional.</em> Credentials for encrypted Telegram Passport data. Available for "PersonalDetails", "passport", "DriverLicense", "IdentityCard", "InternalPassport" and "address" types.</summary>
    public DataCredentials? Data { get; set; }

    /// <summary><em>Optional.</em> Credentials for an encrypted document's front side. Available for "passport", "DriverLicense", "IdentityCard" and "InternalPassport".</summary>
    [JsonPropertyName("front_side")]
    public FileCredentials? FrontSide { get; set; }

    /// <summary><em>Optional.</em> Credentials for an encrypted document's reverse side. Available for "DriverLicense" and "IdentityCard".</summary>
    [JsonPropertyName("reverse_side")]
    public FileCredentials? ReverseSide { get; set; }

    /// <summary><em>Optional.</em> Credentials for an encrypted selfie of the user with a document. Available for "passport", "DriverLicense", "IdentityCard" and "InternalPassport".</summary>
    public FileCredentials? Selfie { get; set; }

    /// <summary><em>Optional.</em> Credentials for an encrypted translation of the document. Available for "passport", "DriverLicense", "IdentityCard", "InternalPassport", "UtilityBill", "BankStatement", "RentalAgreement", "PassportRegistration" and "TemporaryRegistration".</summary>
    public FileCredentials[]? Translation { get; set; }

    /// <summary><em>Optional.</em> Credentials for encrypted files. Available for "UtilityBill", "BankStatement", "RentalAgreement", "PassportRegistration" and "TemporaryRegistration" types.</summary>
    public FileCredentials[]? Files { get; set; }
}

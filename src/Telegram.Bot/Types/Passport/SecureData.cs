// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Passport;

/// <summary>This object represents the credentials required to decrypt encrypted data. All fields are optional and depend on <a href="https://core.telegram.org/bots/api#fields">fields</a> that were requested.</summary>
public partial class SecureData
{
    /// <summary><em>Optional.</em> Credentials for encrypted personal details</summary>
    [JsonPropertyName("personal_details")]
    public SecureValue? PersonalDetails { get; set; }

    /// <summary><em>Optional.</em> Credentials for encrypted passport</summary>
    public SecureValue? Passport { get; set; }

    /// <summary><em>Optional.</em> Credentials for encrypted internal passport</summary>
    [JsonPropertyName("internal_passport")]
    public SecureValue? InternalPassport { get; set; }

    /// <summary><em>Optional.</em> Credentials for encrypted driver license</summary>
    [JsonPropertyName("driver_license")]
    public SecureValue? DriverLicense { get; set; }

    /// <summary><em>Optional.</em> Credentials for encrypted ID card</summary>
    [JsonPropertyName("identity_card")]
    public SecureValue? IdentityCard { get; set; }

    /// <summary><em>Optional.</em> Credentials for encrypted residential address</summary>
    public SecureValue? Address { get; set; }

    /// <summary><em>Optional.</em> Credentials for encrypted utility bill</summary>
    [JsonPropertyName("utility_bill")]
    public SecureValue? UtilityBill { get; set; }

    /// <summary><em>Optional.</em> Credentials for encrypted bank statement</summary>
    [JsonPropertyName("bank_statement")]
    public SecureValue? BankStatement { get; set; }

    /// <summary><em>Optional.</em> Credentials for encrypted rental agreement</summary>
    [JsonPropertyName("rental_agreement")]
    public SecureValue? RentalAgreement { get; set; }

    /// <summary><em>Optional.</em> Credentials for encrypted registration from internal passport</summary>
    [JsonPropertyName("passport_registration")]
    public SecureValue? PassportRegistration { get; set; }

    /// <summary><em>Optional.</em> Credentials for encrypted temporary registration</summary>
    [JsonPropertyName("temporary_registration")]
    public SecureValue? TemporaryRegistration { get; set; }
}

// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Passport;

/// <summary>This object represents the credentials required to decrypt encrypted data. All fields are optional and depend on <a href="https://core.telegram.org/bots/api#fields">fields</a> that were requested.</summary>
public partial class SecureData
{
    /// <summary><em>Optional.</em> Credentials for encrypted personal details</summary>
    public SecureValue? PersonalDetails { get; set; }

    /// <summary><em>Optional.</em> Credentials for encrypted passport</summary>
    public SecureValue? Passport { get; set; }

    /// <summary><em>Optional.</em> Credentials for encrypted internal passport</summary>
    public SecureValue? InternalPassport { get; set; }

    /// <summary><em>Optional.</em> Credentials for encrypted driver license</summary>
    public SecureValue? DriverLicense { get; set; }

    /// <summary><em>Optional.</em> Credentials for encrypted ID card</summary>
    public SecureValue? IdentityCard { get; set; }

    /// <summary><em>Optional.</em> Credentials for encrypted residential address</summary>
    public SecureValue? Address { get; set; }

    /// <summary><em>Optional.</em> Credentials for encrypted utility bill</summary>
    public SecureValue? UtilityBill { get; set; }

    /// <summary><em>Optional.</em> Credentials for encrypted bank statement</summary>
    public SecureValue? BankStatement { get; set; }

    /// <summary><em>Optional.</em> Credentials for encrypted rental agreement</summary>
    public SecureValue? RentalAgreement { get; set; }

    /// <summary><em>Optional.</em> Credentials for encrypted registration from internal passport</summary>
    public SecureValue? PassportRegistration { get; set; }

    /// <summary><em>Optional.</em> Credentials for encrypted temporary registration</summary>
    public SecureValue? TemporaryRegistration { get; set; }
}

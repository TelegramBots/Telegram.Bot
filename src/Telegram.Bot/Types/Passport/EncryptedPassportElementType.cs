// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Passport;

/// <summary><see cref="EncryptedPassportElement"/>: Element type. One of <see cref="PersonalDetails">PersonalDetails</see>, <see cref="Passport">Passport</see>, <see cref="DriverLicense">DriverLicense</see>, <see cref="IdentityCard">IdentityCard</see>, <see cref="InternalPassport">InternalPassport</see>, <see cref="Address">Address</see>, <see cref="UtilityBill">UtilityBill</see>, <see cref="BankStatement">BankStatement</see>, <see cref="RentalAgreement">RentalAgreement</see>, <see cref="PassportRegistration">PassportRegistration</see>, <see cref="TemporaryRegistration">TemporaryRegistration</see>, <see cref="PhoneNumber">PhoneNumber</see>, <see cref="Email">Email</see>.</summary>
[JsonConverter(typeof(EnumConverter<EncryptedPassportElementType>))]
public enum EncryptedPassportElementType
{
    /// <summary>“PersonalDetails” type</summary>
    PersonalDetails = 1,
    /// <summary>“passport” type</summary>
    Passport,
    /// <summary>“DriverLicense” type</summary>
    DriverLicense,
    /// <summary>“IdentityCard” type</summary>
    IdentityCard,
    /// <summary>“InternalPassport” type</summary>
    InternalPassport,
    /// <summary>“address” type</summary>
    Address,
    /// <summary>“UtilityBill” type</summary>
    UtilityBill,
    /// <summary>“BankStatement” type</summary>
    BankStatement,
    /// <summary>“RentalAgreement” type</summary>
    RentalAgreement,
    /// <summary>“PassportRegistration” type</summary>
    PassportRegistration,
    /// <summary>“TemporaryRegistration” type</summary>
    TemporaryRegistration,
    /// <summary>“PhoneNumber” type</summary>
    PhoneNumber,
    /// <summary>“email” type</summary>
    Email,
}

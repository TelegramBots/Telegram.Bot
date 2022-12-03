namespace Telegram.Bot.Types.Passport;

/// <summary>
///
/// </summary>
[JsonConverter(typeof(EncryptedPassportElementTypeConverter))]
public enum EncryptedPassportElementType
{
    /// <summary>
    /// Personal details
    /// </summary>
    PersonalDetails = 1,

    /// <summary>
    /// Passport
    /// </summary>
    Passport,

    /// <summary>
    /// Driver licence
    /// </summary>
    DriverLicence,

    /// <summary>
    /// Identity card
    /// </summary>
    IdentityCard,

    /// <summary>
    /// Internal passport
    /// </summary>
    InternalPassport,

    /// <summary>
    /// Address
    /// </summary>
    Address,

    /// <summary>
    /// Utility bill
    /// </summary>
    UtilityBill,

    /// <summary>
    /// Bank statement
    /// </summary>
    BankStatement,

    /// <summary>
    /// Rental agreement
    /// </summary>
    RentalAgreement,

    /// <summary>
    /// Passport registration
    /// </summary>
    PassportRegistration,

    /// <summary>
    /// Temporary registration
    /// </summary>
    TemporaryRegistration,

    /// <summary>
    /// Phone number
    /// </summary>
    PhoneNumber,

    /// <summary>
    /// Email
    /// </summary>
    Email
}

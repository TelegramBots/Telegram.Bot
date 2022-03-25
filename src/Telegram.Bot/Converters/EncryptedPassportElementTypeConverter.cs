using System.Collections.Generic;
using Telegram.Bot.Types.Passport;
using static Telegram.Bot.Types.Passport.EncryptedPassportElementType;

namespace Telegram.Bot.Converters;

internal class EncryptedPassportElementTypeConverter : EnumConverter<EncryptedPassportElementType>
{
    static readonly IReadOnlyDictionary<string, EncryptedPassportElementType> StringToEnum =
        new Dictionary<string, EncryptedPassportElementType>
        {
            {"personal_details", PersonalDetails},
            {"passport", Passport},
            {"driver_licence", DriverLicence},
            {"identity_card", IdentityCard},
            {"internal_passport", InternalPassport},
            {"address", Address},
            {"utility_bill", UtilityBill},
            {"bank_statement", BankStatement},
            {"rental_agreement", RentalAgreement},
            {"passport_registration", PassportRegistration},
            {"temporary_registration", TemporaryRegistration},
            {"phone_number", PhoneNumber},
            {"email", Email}
        };

    static readonly IReadOnlyDictionary<EncryptedPassportElementType, string> EnumToString =
        new Dictionary<EncryptedPassportElementType, string>
        {
            {PersonalDetails, "personal_details"},
            {Passport, "passport"},
            {DriverLicence, "driver_licence"},
            {IdentityCard, "identity_card"},
            {InternalPassport, "internal_passport"},
            {Address, "address"},
            {UtilityBill, "utility_bill"},
            {BankStatement, "bank_statement"},
            {RentalAgreement, "rental_agreement"},
            {PassportRegistration, "passport_registration"},
            {TemporaryRegistration, "temporary_registration"},
            {PhoneNumber, "phone_number"},
            {Email, "email"}
        };

    protected override EncryptedPassportElementType GetEnumValue(string value) =>
        StringToEnum.TryGetValue(value, out var enumValue)
            ? enumValue
            : 0;

    protected override string GetStringValue(EncryptedPassportElementType value) =>
        EnumToString.TryGetValue(value, out var stringValue)
            ? stringValue
            : "unknown";
}
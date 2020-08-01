// ReSharper disable once CheckNamespace

namespace Telegram.Bot
{
    public static partial class PassportEnums
    {
        /// <summary>
        /// Provides scope names that a bot can request for
        /// </summary>
        public static class Scope
        {
            /// <summary>
            /// Personal details
            /// </summary>
            public const string PersonalDetails = "personal_details";

            /// <summary>
            /// Passport
            /// </summary>
            public const string Passport = "passport";

            /// <summary>
            /// Driver license
            /// </summary>
            public const string DriverLicense = "driver_license";

            /// <summary>
            /// Identity card
            /// </summary>
            public const string IdentityCard = "identity_card";

            /// <summary>
            /// Internal passport
            /// </summary>
            public const string InternalPassport = "internal_passport";

            /// <summary>
            /// Residential address
            /// </summary>
            public const string Address = "address";

            /// <summary>
            /// Utility bill
            /// </summary>
            public const string UtilityBill = "utility_bill";

            /// <summary>
            /// Bank statement
            /// </summary>
            public const string BankStatement = "bank_statement";

            /// <summary>
            /// Rental agreement
            /// </summary>
            public const string RentalAgreement = "rental_agreement";

            /// <summary>
            /// Passport registration
            /// </summary>
            public const string PassportRegistration = "passport_registration";

            /// <summary>
            /// Temporary registration
            /// </summary>
            public const string TemporaryRegistration = "temporary_registration";

            /// <summary>
            /// Phone number
            /// </summary>
            public const string PhoneNumber = "phone_number";

            /// <summary>
            /// Email
            /// </summary>
            public const string Email = "email";

            /// <summary>
            /// Special type "id_document" is an alias for one of "passport", "driver_license", or "identity_card"
            /// </summary>
            public const string IdDocument = "id_document";

            /// <summary>
            /// Special type "address_document" is an alias for one of "utility_bill", "bank_statement", or
            /// "rental_agreement"
            /// </summary>
            public const string AddressDocument = "address_document";
        }
    }
}

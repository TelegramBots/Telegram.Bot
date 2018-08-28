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
            /// Residential Address
            /// </summary>
            public const string ResidentialAddress = "address";

            /// <summary>
            /// Passport
            /// </summary>
            public const string Passport = "passport";

            /// <summary>
            /// Phone Number
            /// </summary>
            public const string PhoneNumber = "phone_number";

            /// <summary>
            /// Email
            /// </summary>
            public const string Email = "email";
        }
    }
}

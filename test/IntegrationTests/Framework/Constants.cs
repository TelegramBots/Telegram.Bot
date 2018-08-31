using IntegrationTests.Framework.XunitExtensions;

namespace IntegrationTests.Framework
{
    public static class Constants
    {
        public const string AssemblyName = "IntegrationTests";

        public const string TestCaseOrderer =
            AssemblyName + "." + nameof(Framework) + "." + nameof(Framework.TestCaseOrderer);

        public const string TestCaseDiscoverer =
            AssemblyName + "." + nameof(Framework) + "." + nameof(XunitExtensions) + "." + nameof(RetryFactDiscoverer);

        public static class TestCollections
        {
            public const string PersonalDetails = "Personal details";

            public const string ResidentialAddress = "Residential address";

            public const string DriverLicense = "Driver license";

            public const string PhoneAndEmail = "Phone and Email";

            public const string RentalAgreementAndBill = "Rental agreement and utility bill";

            public const string IdentityCardErrors = "Identity card errors";

            public const string PassportRegistrationErrors = "Passport registration errors";

            public const string UnspecifiedError = "Unspecified error";
        }
    }
}

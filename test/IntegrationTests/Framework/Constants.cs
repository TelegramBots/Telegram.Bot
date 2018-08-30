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
            public const string PersonalDetails2 = "Personal details v1.1";

            public const string ResidentialAddress = "Residential address";

            public const string RentalAgreementAndBill = "Rental agreement and utility bill";

            public const string DriverLicense = "Driver license";

            public const string PhoneEmail = "Phone and Email";

            public const string Passport = "Passport";

            public const string DriverLicenseErrors = "Driver license errors";

            public const string UnspecifiedError = "Unspecified error";
        }
    }
}

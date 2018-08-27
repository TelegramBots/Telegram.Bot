using IntegrationTests.Framework.XunitExtensions;

namespace IntegrationTests.Framework
{
    public static class Constants
    {
        public const string MethodTraitName = "Method";

        public const string AssemblyName = "IntegrationTests";

        public const string TestCaseOrderer =
            AssemblyName + "." + nameof(Framework) + "." + nameof(Framework.TestCaseOrderer);

        public const string TestCaseDiscoverer =
            AssemblyName + "." + nameof(Framework) + "." + nameof(XunitExtensions) + "." + nameof(RetryFactDiscoverer);

        public static class TestCollections
        {
            public const string Passport = "Passport";
        }
    }
}

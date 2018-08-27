using IntegrationTests.Framework.XunitExtensions;

namespace IntegrationTests.Framework
{
    public static class Constants
    {
        public const string CategoryTraitName = "Category";

        public const string InteractiveCategoryValue = "Interactive";

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

        public static class FileNames
        {
            private const string FilesDir = "Files/";

            public static class Documents
            {
                private const string DocumentDir = FilesDir + "Document/";

                public const string Hamlet = DocumentDir + "hamlet.pdf";
            }
        }

        public static class TelegramBotApiMethods
        {
            public const string SendMessage = "sendMessage";
        }
    }
}

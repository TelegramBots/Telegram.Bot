namespace Telegram.Bot.Tests.Unit
{
    public static class Constants
    {
        public static class FileNames
        {
            private const string FilesDir = "Files/";

            public static class Passport
            {
                private const string PassportDir = FilesDir + "Passport/";

                public const string PrivateKey = PassportDir + "PrivateKey.json";

                public const string PassportMessage = PassportDir + "PassportMessage.json";
            }
        }
    }
}

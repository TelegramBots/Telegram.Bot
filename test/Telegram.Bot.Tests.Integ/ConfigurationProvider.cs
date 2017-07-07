using System.IO;
using Microsoft.Extensions.Configuration;

namespace Telegram.Bot.Tests.Integ
{
    public static class ConfigurationProvider
    {
        private static readonly IConfigurationRoot Configuration;

        static ConfigurationProvider()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", true)
                .AddEnvironmentVariables("TelegramBot_")
                .Build();
        }

        public static class TelegramBot
        {
            public static string ApiToken
            {
                get
                {
                    string token = Configuration[nameof(ApiToken)];
                    return token;
                }
            }

            public static string PaymentProviderToken
            {
                get
                {
                    string providerToken = Configuration[nameof(PaymentProviderToken)];
                    return providerToken;
                }
            }

        }

        public static class TestAnalyst
        {
            // todo remove if not needed
            public static int? UserId
            {
                get
                {
                    int? userid;
                    if (int.TryParse(Configuration[nameof(UserId)], out int i))
                        userid = i;
                    else
                        userid = null;
                    return userid;
                }
            }

            public static string ChatId
            {
                get
                {
                    string chatid = Configuration[nameof(ChatId)];
                    return chatid;
                }
            }

            public static string[] AllowedUserNames
            {
                get
                {
                    string names = Configuration[nameof(AllowedUserNames)];
                    string[] usernames = names.Split(',');
                    return usernames;
                }
            }
        }
    }
}

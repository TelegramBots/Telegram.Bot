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
                    return Configuration["ApiToken"];
                }
            }
        }

        public static class TestAnalyst
        {
            public static int UserId
            {
                get { return int.Parse(Configuration["UserId"]); }
            }

            public static string ChatId
            {
                get { return Configuration["ChatId"]; }
            }
        }
    }
}

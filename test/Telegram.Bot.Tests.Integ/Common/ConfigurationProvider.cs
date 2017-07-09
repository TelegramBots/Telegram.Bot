using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Telegram.Bot.Tests.Integ.Common
{
    public static class ConfigurationProvider
    {
        public static TestConfigurations TestConfigurations;

        static ConfigurationProvider()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", true)
                .AddEnvironmentVariables("TelegramBot_")
                .Build();

            TestConfigurations = new TestConfigurations
            {
                ApiToken = configuration[nameof(TestConfigurations.ApiToken)],
                PaymentProviderToken = configuration[nameof(TestConfigurations.PaymentProviderToken)],
                AllowedUserNames = configuration[nameof(TestConfigurations.AllowedUserNames)],
                PrivateChatId = configuration[nameof(TestConfigurations.PrivateChatId)],
                SuperGroupChatId = configuration[nameof(TestConfigurations.SuperGroupChatId)],
                RegularMemberUserId = configuration[nameof(TestConfigurations.RegularMemberUserId)],
                RegularMemberUserName = configuration[nameof(TestConfigurations.RegularMemberUserName)],
                RegularMemberPrivateChatId = configuration[nameof(TestConfigurations.RegularMemberPrivateChatId)],
            };

            if (string.IsNullOrWhiteSpace(TestConfigurations.ApiToken))
                throw new ArgumentNullException(nameof(TestConfigurations.ApiToken), "API token is not provided or is empty.");

            if (TestConfigurations.ApiToken?.Length < 25)
                throw new ArgumentException("API token is too short.", nameof(TestConfigurations.ApiToken));

            if (string.IsNullOrWhiteSpace(TestConfigurations.PaymentProviderToken))
                throw new ArgumentNullException(nameof(TestConfigurations.PaymentProviderToken),
                    "Payment provider token is not provided or is empty.");

            if (TestConfigurations.PaymentProviderToken?.Length < 15)
                throw new ArgumentException("Payment provider token is too short.", nameof(TestConfigurations.PaymentProviderToken));
        }
    }
}

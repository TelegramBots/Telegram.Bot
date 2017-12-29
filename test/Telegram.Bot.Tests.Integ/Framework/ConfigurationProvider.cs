using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Telegram.Bot.Tests.Integ.Framework
{
    public static class ConfigurationProvider
    {
        public static TestConfigurations TestConfigurations;

        static ConfigurationProvider()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json", true)
                .AddEnvironmentVariables("TelegramBot_")
                .Build();

            TestConfigurations = new TestConfigurations
            {
                ApiToken = configuration[nameof(TestConfigurations.ApiToken)],
                AllowedUserNames = configuration[nameof(TestConfigurations.AllowedUserNames)],

                SuperGroupChatId = configuration[nameof(TestConfigurations.SuperGroupChatId)],
                ChannelChatId = configuration[nameof(TestConfigurations.ChannelChatId)],

                PaymentProviderToken = configuration[nameof(TestConfigurations.PaymentProviderToken)],

                RegularGroupMemberId = configuration[nameof(TestConfigurations.RegularGroupMemberId)],
            };

            if (long.TryParse(configuration[nameof(TestConfigurations.TesterPrivateChatId)], out long privateChat))
            {
                TestConfigurations.TesterPrivateChatId = privateChat;
            }
            if (int.TryParse(configuration[nameof(TestConfigurations.TesterPrivateChatId)], out int userId))
            {
                TestConfigurations.StickerOwnerUserId = userId;
            }

            if (string.IsNullOrWhiteSpace(TestConfigurations.ApiToken))
                throw new ArgumentNullException(nameof(TestConfigurations.ApiToken), "API token is not provided or is empty.");

            if (TestConfigurations.ApiToken?.Length < 25)
                throw new ArgumentException("API token is too short.", nameof(TestConfigurations.ApiToken));

            if (!TestConfigurations.AllowedUserNamesArray.Any())
                throw new ArgumentException("Allowed user names is not provided", nameof(TestConfigurations.AllowedUserNames));
        }
    }
}

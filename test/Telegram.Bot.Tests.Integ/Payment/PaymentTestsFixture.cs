using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Tests.Integ.Payment
{
    public class PaymentTestsFixture
    {
        public TestsFixture TestsFixture { get; }

        public ChatId TesterPrivateChatId { get; }

        public string PaymentProviderToken { get; }

        public string Currency { get; set; }

        public int[] Prices { get; set; }

        public PaymentTestsFixture(TestsFixture testsFixture)
        {
            TestsFixture = testsFixture;

            PaymentProviderToken = ConfigurationProvider.TestConfigurations.PaymentProviderToken;
            if (string.IsNullOrWhiteSpace(PaymentProviderToken))
            {
                throw new ArgumentNullException(nameof(PaymentProviderToken),
                    "Payment provider token is not provided or is empty.");
            }

            if (PaymentProviderToken.Length < 15)
            {
                throw new ArgumentException("Payment provider token is too short.", nameof(PaymentProviderToken));
            }

            Currency = ConfigurationProvider.TestConfigurations.Currency;
            if (string.IsNullOrWhiteSpace(Currency))
            {
                throw new ArgumentNullException(nameof(Currency),
                    "Currency is not provided or is empty.");
            }

            if (Currency.Length > 3)
            {
                throw new ArgumentException("Currency is too long. It should consist of 3 letters.", nameof(Currency));
            }

            Prices = ConfigurationProvider.TestConfigurations.PricesArray;
            if (!Prices.Any())
            {
                throw new ArgumentException("There is no prices in configuration. Provide at least one price.", nameof(Prices));
            }

            if (Currency.Length > 3)
            {
                throw new ArgumentException("Currency is too long. It should consist of 3 letters.", nameof(Currency));
            }

            var privateChatId = ConfigurationProvider.TestConfigurations.TesterPrivateChatId;
            if (privateChatId is null)
            {
                TestsFixture.SendTestCollectionNotificationAsync(
                        Constants.TestCollections.Payment,
                        "A tester should send /test command in a private chat to begin")
                    .Wait();

                TestsFixture.UpdateReceiver.DiscardNewUpdatesAsync().Wait();
                TesterPrivateChatId = TestsFixture.GetChatIdFromTesterAsync(ChatType.Private).Result;
            }
            else
            {
                TesterPrivateChatId = privateChatId;

                TestsFixture.SendTestCollectionNotificationAsync(Constants.TestCollections.Payment,
                    "All messages for this collection will be sent in private chat")
                    .Wait();

                TestsFixture.SendTestCollectionNotificationAsync(Constants.TestCollections.Payment, chatid: TesterPrivateChatId)
                    .Wait();
            }
        }

        public Task<Message> SendTestCaseNotificationAsync(string testcase, string instructions = null)
        {
            return TestsFixture.SendTestCaseNotificationAsync(testcase, instructions, TesterPrivateChatId);
        }

    }
}

using System;
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

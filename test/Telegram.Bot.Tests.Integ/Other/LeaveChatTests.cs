using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Tests.Integ.Framework.Fixtures;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Other
{
    [Collection(Constants.TestCollections.LeaveChat)]
    [TestCaseOrderer(Constants.TestCaseOrderer2, Constants.AssemblyName)]
    public class LeaveChatTests : IClassFixture<LeaveChatTests.Fixture>
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly Fixture _classFixture;

        private readonly TestsFixture _fixture;

        public LeaveChatTests(TestsFixture fixture, Fixture classFixture)
        {
            _fixture = fixture;
            _classFixture = classFixture;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldLeaveChat,
            Skip = "Bot should stay in chat for other the test cases")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.LeaveChat)]
        public async Task Should_Get_Private_Chat()
        {
            // ToDo: Exception when leaving private chat
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldLeaveChat);

            await BotClient.LeaveChatAsync(
                chatId: _fixture.SupergroupChat
            );
        }

        private static class FactTitles
        {
            public const string ShouldLeaveChat = "Should leave chat";
        }

        public class Fixture : PrivateChatFixture
        {
            public Fixture(TestsFixture testsFixture)
                : base(testsFixture, Constants.TestCollections.LeaveChat)
            {
            }
        }
    }
}
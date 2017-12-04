using System;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Integ.GettingUpdates
{
    [Collection(Constants.TestCollections.GettingUpdates)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class GettingUpdatesTests
    {
        public ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public GettingUpdatesTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = FactTitles.ShouldGetBotUser)]
        [ExecutionOrder(1.1)]
        public async Task Should_Get_Bot_User()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldGetBotUser);
            // todo set botuser on fixture so other tests (inline query instructions) can use it 
            User botUser = await BotClient.GetMeAsync();

            Assert.NotNull(botUser);
            Assert.True(botUser.IsBot);
            Assert.EndsWith("bot", botUser.Username, StringComparison.OrdinalIgnoreCase);
        }

        private static class FactTitles
        {
            public const string ShouldGetBotUser = "Should get bot user info";
        }
    }
}

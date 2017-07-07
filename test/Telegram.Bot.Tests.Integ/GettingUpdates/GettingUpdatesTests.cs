using System;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Integ.GettingUpdates
{
    [Collection(CommonConstants.TestCollections.GettingUpdates)]
    [TestCaseOrderer(CommonConstants.TestCaseOrderer, CommonConstants.AssemblyName)]
    [Trait(CommonConstants.CategoryTraitName, CommonConstants.TestCategories.GettingUpdates)]
    public class GettingUpdatesTests
    {
        public ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly BotClientFixture _fixture;

        public GettingUpdatesTests(BotClientFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = FactTitles.ShouldGetBotUser)]
        [ExecutionOrder(1.1)]
        public async Task ShouldGetBotUser()
        {
            await _fixture.SendTestCaseNotification(FactTitles.ShouldGetBotUser);
            // todo set botuser on fixture so other tests (inline query instructions) can use it 
            User botUser = await BotClient.GetMeAsync();

            Assert.NotNull(botUser);
            Assert.True(botUser.Username.EndsWith("bot", StringComparison.OrdinalIgnoreCase));
        }

        private static class FactTitles
        {
            public const string ShouldGetBotUser = "Should get bot user info";
        }
    }
}

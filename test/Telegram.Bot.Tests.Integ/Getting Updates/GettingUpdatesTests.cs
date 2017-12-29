using System;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Getting_Updates
{
    [Collection(Constants.TestCollections.GettingUpdates)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class GettingUpdatesTests
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public GettingUpdatesTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = FactTitles.ShouldPassApiTokenTest)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetMe)]
        [ExecutionOrder(1.1)]
        public async Task Should_Pass_Test_Api_Token()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldPassApiTokenTest);

            bool result = await BotClient.TestApiAsync();

            Assert.True(result);
        }

        [Fact(DisplayName = FactTitles.ShouldFailApiTokenTest)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetMe)]
        [ExecutionOrder(1.2)]
        public async Task Should_Fail_Test_Api_Token()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldFailApiTokenTest);

            ITelegramBotClient botClient = new TelegramBotClient("0:1this_is_an-invalid-token_for_tests");
            bool result = await botClient.TestApiAsync();

            Assert.False(result);
        }

        [Fact(DisplayName = FactTitles.ShouldGetBotUser)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetMe)]
        [ExecutionOrder(2.1)]
        public async Task Should_Get_Bot_User()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldGetBotUser);
            User botUser = await BotClient.GetMeAsync();

            Assert.NotNull(botUser);
            Assert.True(botUser.IsBot);
            Assert.EndsWith("bot", botUser.Username, StringComparison.OrdinalIgnoreCase);
        }

        private static class FactTitles
        {
            public const string ShouldPassApiTokenTest = "Should pass API Token test with valid token";

            public const string ShouldFailApiTokenTest = "Should pass API Token test with invalid token";

            public const string ShouldGetBotUser = "Should get bot user info";
        }
    }
}

using System;
using System.Net.Http;
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

        [OrderedFact(DisplayName = FactTitles.ShouldPassApiTokenTest)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetMe)]
        public async Task Should_Pass_Test_Api_Token()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldPassApiTokenTest);

            bool result = await BotClient.TestApiAsync();

            Assert.True(result);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldThrowHttpRequestException)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetMe)]
        public async Task Should_Fail_Test_Api_Token()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowHttpRequestException);

            ITelegramBotClient botClient = new TelegramBotClient("0:1this_is_an-invalid-token_for_tests");

            HttpRequestException exception = await Assert.ThrowsAnyAsync<HttpRequestException>(() =>
                botClient.TestApiAsync()
            );

            Assert.Equal("Response status code does not indicate success: 404 (Not Found).", exception.Message);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldTestBadApiToken)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetMe)]
        public async Task Should_Test_Bad_BotToken()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldTestBadApiToken);

            ITelegramBotClient botClient = new TelegramBotClient("123456:ABC-DEF1234ghIkl-zyx57W2v1u123ew11");
            bool result = await botClient.TestApiAsync();

            Assert.False(result);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldGetBotUser)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetMe)]
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

            public const string ShouldThrowHttpRequestException =
                "Should throw HttpRequestException with \"404 (Not Found)\" error when malformed API Token is provided";

            public const string ShouldTestBadApiToken =
                "Should fail API Token test with invalid token";

            public const string ShouldGetBotUser = "Should get bot user info";
        }
    }
}

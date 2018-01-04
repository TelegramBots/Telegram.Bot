using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;
using File = System.IO.File;

namespace Telegram.Bot.Tests.Integ.Getting_Updates
{
    /// <remarks>
    /// Webhooks should be immediately disabled because test framework uses getUpdates method
    /// </remarks>
    [Collection(Constants.TestCollections.Webhook)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class WebhookTests
    {
        public ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public WebhookTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = FactTitles.ShouldSetWebhook, Skip = "setWebhook requests are rate limited")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetWebhook)]
        [ExecutionOrder(1.0)]
        public async Task Should_Set_Webhook()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSetWebhook);

            await BotClient.SetWebhookAsync("https://www.telegram.org/");

            await BotClient.DeleteWebhookAsync();
        }

        [Fact(DisplayName = FactTitles.ShouldSetWebhookWithOptions, Skip = "setWebhook requests are rate limited")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetWebhook)]
        [ExecutionOrder(1.1)]
        public async Task Should_Set_Webhook_With_Options()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSetWebhookWithOptions);

            await BotClient.SetWebhookAsync(
                url: "https://www.t.me/",
                maxConnections: 5,
                allowedUpdates: new[] { UpdateType.CallbackQuery, UpdateType.InlineQuery }
            );

            await BotClient.DeleteWebhookAsync();
        }

        [Fact(DisplayName = FactTitles.ShouldDeleteWebhookUsingEmptyUrl)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetWebhook)]
        [ExecutionOrder(1.2)]
        public async Task Should_Delete_Webhook_Using_setWebhook()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldDeleteWebhookUsingEmptyUrl);

            await BotClient.SetWebhookAsync(string.Empty);
        }

        [Fact(DisplayName = FactTitles.ShouldSetWebhookWithCertificate)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetWebhook)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetWebhookInfo)]
        [ExecutionOrder(1.3)]
        public async Task Should_Set_Webhook_With_SelfSigned_Cert()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSetWebhookWithCertificate);

            const string url = "https://www.telegram.org/";
            const int maxConnections = 5;

            using (var stream = File.OpenRead(Constants.FileNames.Certificate.PublicKey))
            {
                await BotClient.SetWebhookAsync(
                    url: url,
                    certificate: stream,
                    maxConnections: maxConnections,
                    allowedUpdates: new UpdateType[0]
                );
            }

            WebhookInfo info = await BotClient.GetWebhookInfoAsync();

            Assert.Equal(url, info.Url);
            Assert.True(info.HasCustomCertificate);
            Assert.Equal(maxConnections, info.MaxConnections);
            Assert.Null(info.AllowedUpdates);

            await BotClient.DeleteWebhookAsync();
        }

        [Fact(DisplayName = FactTitles.ShouldDeleteWebhook)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.DeleteWebhook)]
        [ExecutionOrder(1.5)]
        public async Task Should_Delete_Webhook()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldDeleteWebhook);

            await BotClient.DeleteWebhookAsync();
        }

        [Fact(DisplayName = FactTitles.ShouldGetDeletedWebhookInfo)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetWebhookInfo)]
        [ExecutionOrder(1.6)]
        public async Task Should_Get_Deleted_Webhook_Info()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldGetDeletedWebhookInfo);

            WebhookInfo info = await BotClient.GetWebhookInfoAsync();

            Assert.Empty(info.Url);
            Assert.False(info.HasCustomCertificate);
            Assert.Equal(default, info.MaxConnections);
            Assert.Equal(default, info.AllowedUpdates);
        }

        private static class FactTitles
        {
            public const string ShouldSetWebhook = "Should set webhook";

            public const string ShouldSetWebhookWithOptions = "Should set webhook with options";

            public const string ShouldDeleteWebhookUsingEmptyUrl = "Should delete webhook by setting it to an empty URL";

            public const string ShouldSetWebhookWithCertificate = "Should set webhook with self-signed certificate";

            public const string ShouldDeleteWebhook = "Should delete webhook";

            public const string ShouldGetDeletedWebhookInfo = "Should get info of deleted webhook";
        }
    }
}

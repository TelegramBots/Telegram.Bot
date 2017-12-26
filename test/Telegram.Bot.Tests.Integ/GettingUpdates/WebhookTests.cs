using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types.Enums;
using Xunit;
using File = System.IO.File;

namespace Telegram.Bot.Tests.Integ.GettingUpdates
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
                allowedUpdates: new[] { UpdateType.CallbackQueryUpdate, UpdateType.InlineQueryUpdate }
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
        [ExecutionOrder(1.3)]
        public async Task Should_Set_Webhook_With_SelfSigned_Cert()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSetWebhookWithCertificate);

            using (var stream = File.OpenRead(Constants.FileNames.Certificate.PublicKey))
            {
                await BotClient.SetWebhookAsync(
                    url: "https://www.telegram.org/",
                    certificate: stream,
                    allowedUpdates: new UpdateType[0]
                );
            }

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

        private static class FactTitles
        {
            public const string ShouldSetWebhook = "Should set webhook";

            public const string ShouldSetWebhookWithOptions = "Should set webhook with options";

            public const string ShouldDeleteWebhookUsingEmptyUrl = "Should delete webhook by setting it to an empty URL";

            public const string ShouldSetWebhookWithCertificate = "Should set webhook with self-signed certificate";

            public const string ShouldDeleteWebhook = "Should delete webhook";
        }
    }
}

using System;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;
using File = System.IO.File;

namespace Telegram.Bot.Tests.Integ.Getting_Updates;

/// <summary>Webhook tests</summary>
/// <remarks>
/// Webhooks must be immediately disabled because the test framework uses getUpdates method.
/// </remarks>
[Collection(Constants.TestCollections.Webhook)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class WebhookTests(TestsFixture fixture) : TestClass(fixture), IDisposable
{
    /// <summary>
    /// Ensures that the webhooks are immediately disabled after each test case.
    /// </summary>
    public void Dispose()
    {
        BotClient.DeleteWebhook()
            .GetAwaiter()
            .GetResult();
        GC.SuppressFinalize(this);
    }

    [OrderedFact("Should set webhook", Skip = "setWebhook requests are rate limited")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetWebhook)]
    public async Task Should_Set_Webhook()
    {
        await BotClient.SetWebhook(url: "https://www.telegram.org/");
    }

    [OrderedFact("Should set webhook with options", Skip = "setWebhook requests are rate limited")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetWebhook)]
    public async Task Should_Set_Webhook_With_Options()
    {
        await BotClient.SetWebhook(
            url: "https://www.t.me/",
            maxConnections: 5,
            allowedUpdates: [UpdateType.CallbackQuery, UpdateType.InlineQuery]
        );
    }

    [OrderedFact("Should delete the webhook by setting it to an empty URL")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetWebhook)]
    public async Task Should_Delete_Webhook_Using_setWebhook()
    {
        await BotClient.SetWebhook(url: "");
    }

    [OrderedFact("Should set webhook with self-signed certificate")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetWebhook)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetWebhookInfo)]
    public async Task Should_Set_Webhook_With_SelfSigned_Cert()
    {
        await using (Stream stream = File.OpenRead(Constants.PathToFile.Certificate.PublicKey))
        {
            await BotClient.WithStreams(stream).SetWebhook(
                url: "https://www.telegram.org/",
                certificate: InputFile.FromStream(stream),
                maxConnections: 3,
                allowedUpdates: [] // send all types of updates
            );
        }

        WebhookInfo info = await BotClient.GetWebhookInfo();

        Assert.Equal("https://www.telegram.org/", info.Url);
        Assert.True(info.HasCustomCertificate);
        Assert.Equal(3, info.MaxConnections);
        Assert.Null(info.AllowedUpdates);
    }

    [OrderedFact("Should delete webhook")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.DeleteWebhook)]
    public async Task Should_Delete_Webhook()
    {
        await BotClient.DeleteWebhook();
    }

    [OrderedFact("Should get info of the deleted webhook")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetWebhookInfo)]
    public async Task Should_Get_Deleted_Webhook_Info()
    {
        WebhookInfo info = await BotClient.GetWebhookInfo();

        Assert.Empty(info.Url);
        Assert.False(info.HasCustomCertificate);
        Assert.Null(info.MaxConnections);
        Assert.Null(info.AllowedUpdates);
    }
}

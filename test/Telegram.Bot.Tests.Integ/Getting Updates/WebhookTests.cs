using System;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot.Requests;
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
public class WebhookTests(TestsFixture fixture) : IDisposable
{
    ITelegramBotClient BotClient => _fixture.BotClient;

    readonly TestsFixture _fixture = fixture;

    /// <summary>
    /// Ensures that the webhooks are immediately disabled after each test case.
    /// </summary>
    public void Dispose()
    {
        BotClient.DeleteWebhookAsync(new DeleteWebhookRequest())
            .GetAwaiter()
            .GetResult();
    }

    [OrderedFact("Should set webhook", Skip = "setWebhook requests are rate limited")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetWebhook)]
    public async Task Should_Set_Webhook()
    {
        await BotClient.SetWebhookAsync(new SetWebhookRequest { Url = "https://www.telegram.org/"});
    }

    [OrderedFact("Should set webhook with options", Skip = "setWebhook requests are rate limited")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetWebhook)]
    public async Task Should_Set_Webhook_With_Options()
    {
        await BotClient.SetWebhookAsync(
            new SetWebhookRequest
            {
                Url = "https://www.t.me/",
                MaxConnections = 5,
                AllowedUpdates = [UpdateType.CallbackQuery, UpdateType.InlineQuery],
            }
        );
    }

    [OrderedFact("Should delete the webhook by setting it to an empty URL")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetWebhook)]
    public async Task Should_Delete_Webhook_Using_setWebhook()
    {
        await BotClient.SetWebhookAsync(new SetWebhookRequest { Url = string.Empty });
    }

    [OrderedFact("Should set webhook with self-signed certificate")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetWebhook)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetWebhookInfo)]
    public async Task Should_Set_Webhook_With_SelfSigned_Cert()
    {
        await using (Stream stream = File.OpenRead(Constants.PathToFile.Certificate.PublicKey))
        {
            await BotClient.SetWebhookAsync(
                new SetWebhookRequest
                {
                    Url = "https://www.telegram.org/",
                    Certificate = InputFile.FromStream(stream),
                    MaxConnections = 3,
                    AllowedUpdates = Array.Empty<UpdateType>(), // send all types of updates
                }
            );
        }

        WebhookInfo info = await BotClient.GetWebhookInfoAsync(new GetWebhookInfoRequest());

        Assert.Equal("https://www.telegram.org/", info.Url);
        Assert.True(info.HasCustomCertificate);
        Assert.Equal(3, info.MaxConnections);
        Assert.Null(info.AllowedUpdates);
    }

    [OrderedFact("Should delete webhook")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.DeleteWebhook)]
    public async Task Should_Delete_Webhook()
    {
        await BotClient.DeleteWebhookAsync(new DeleteWebhookRequest());
    }

    [OrderedFact("Should get info of the deleted webhook")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetWebhookInfo)]
    public async Task Should_Get_Deleted_Webhook_Info()
    {
        WebhookInfo info = await BotClient.GetWebhookInfoAsync(new GetWebhookInfoRequest());

        Assert.Empty(info.Url);
        Assert.False(info.HasCustomCertificate);
        Assert.Null(info.MaxConnections);
        Assert.Null(info.AllowedUpdates);
    }
}

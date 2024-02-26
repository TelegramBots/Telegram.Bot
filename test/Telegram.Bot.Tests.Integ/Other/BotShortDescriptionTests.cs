using System;
using System.Threading.Tasks;
using Telegram.Bot.Requests;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Other;

[Collection(Constants.TestCollections.BotShortDescription)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class BotShortDescriptionTests(TestsFixture fixture) : IAsyncLifetime
{
    string _languageCode;

    ITelegramBotClient BotClient => fixture.BotClient;

    [OrderedFact("Should set a new bot short description")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetMyShortDescription)]
    public async Task Should_Set_New_Bot__Short_Description()
    {
        const string shortDescription = "Test bot short description";

        await BotClient.SetMyShortDescriptionAsync(
            new SetMyShortDescriptionRequest { ShortDescription = shortDescription }
        );
    }

    [OrderedFact("Should get previously set bot short description")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetMyShortDescription)]
    public async Task Should_Get_Set_Bot_Short_Description()
    {
        const string shortDescription = "Test bot short description";

        await BotClient.SetMyShortDescriptionAsync(
            new SetMyShortDescriptionRequest { ShortDescription = shortDescription }
        );

        await Task.Delay(TimeSpan.FromSeconds(10));

        BotShortDescription currentShortDescription = await fixture.BotClient.GetMyShortDescriptionAsync(new GetMyShortDescriptionRequest());

        Assert.NotNull(currentShortDescription);
        Assert.Equal(shortDescription, currentShortDescription.ShortDescription);
    }

    [OrderedFact("Should delete bot short description")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetMyShortDescription)]
    public async Task Should_Delete_Bot_Short_Description()
    {
        const string shortDescription = "Test bot short description";

        await BotClient.SetMyShortDescriptionAsync(
            new SetMyShortDescriptionRequest { ShortDescription = shortDescription }
        );

        BotShortDescription setShortDescription = await fixture.BotClient.GetMyShortDescriptionAsync(new GetMyShortDescriptionRequest());

        Assert.NotNull(setShortDescription);
        Assert.Equal(shortDescription, setShortDescription.ShortDescription);

        await BotClient.SetMyShortDescriptionAsync(
            new SetMyShortDescriptionRequest { ShortDescription = "" }
        );

        // Test fails receiving old description without a delay
        await Task.Delay(TimeSpan.FromSeconds(20));

        BotShortDescription currentShortDescription = await fixture.BotClient.GetMyShortDescriptionAsync(new GetMyShortDescriptionRequest());

        Assert.NotNull(currentShortDescription.ShortDescription);
        Assert.Empty(currentShortDescription.ShortDescription);
    }

    [OrderedFact("Should set short description with language code area")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetMyShortDescription)]
    public async Task Should_Set_Short_Description_With_Language_Code_Area()
    {
        const string shortDescription = "Короткое тестовое описание бота";

        _languageCode = "ru";

        await BotClient.SetMyShortDescriptionAsync(
            new SetMyShortDescriptionRequest
            {
                ShortDescription = shortDescription,
                LanguageCode = _languageCode,
            }
        );

        await Task.Delay(TimeSpan.FromSeconds(10));

        BotShortDescription newDescription = await fixture.BotClient.GetMyShortDescriptionAsync(
            new GetMyShortDescriptionRequest {LanguageCode = _languageCode}
        );

        Assert.NotNull(newDescription);
        Assert.Equal(shortDescription, newDescription.ShortDescription);
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        await BotClient.SetMyShortDescriptionAsync(
            new SetMyShortDescriptionRequest { ShortDescription = ""
         });

        await BotClient.SetMyShortDescriptionAsync(
            new SetMyShortDescriptionRequest
            {
                ShortDescription = "",
                LanguageCode = _languageCode
            }
        );
    }
}

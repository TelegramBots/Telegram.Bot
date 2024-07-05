using System;
using System.Threading.Tasks;
using Telegram.Bot.Requests;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Other;

[Collection(Constants.TestCollections.BotShortDescription)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class BotShortDescriptionTests(TestsFixture fixture) : TestClass(fixture), IAsyncLifetime
{
    string _languageCode;

    [OrderedFact("Should set a new bot short description")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetMyShortDescription)]
    public async Task Should_Set_New_Bot__Short_Description()
    {
        const string shortDescription = "Test bot short description";

        await BotClient.SetMyShortDescriptionAsync(
            shortDescription: shortDescription
        );
    }

    [OrderedFact("Should get previously set bot short description")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetMyShortDescription)]
    public async Task Should_Get_Set_Bot_Short_Description()
    {
        const string shortDescription = "Test bot short description";

        await BotClient.SetMyShortDescriptionAsync(
            shortDescription: shortDescription
        );

        await Task.Delay(TimeSpan.FromSeconds(10));

        BotShortDescription currentShortDescription = await Fixture.BotClient.GetMyShortDescriptionAsync();

        Assert.NotNull(currentShortDescription);
        Assert.Equal(shortDescription, currentShortDescription.ShortDescription);
    }

    [OrderedFact("Should delete bot short description")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetMyShortDescription)]
    public async Task Should_Delete_Bot_Short_Description()
    {
        const string shortDescription = "Test bot short description";

        await BotClient.SetMyShortDescriptionAsync(
            shortDescription: shortDescription
        );

        BotShortDescription setShortDescription = await Fixture.BotClient.GetMyShortDescriptionAsync();

        Assert.NotNull(setShortDescription);
        Assert.Equal(shortDescription, setShortDescription.ShortDescription);

        await BotClient.SetMyShortDescriptionAsync(
            shortDescription: ""
        );

        // Test fails receiving old description without a delay
        await Task.Delay(TimeSpan.FromSeconds(20));

        BotShortDescription currentShortDescription = await Fixture.BotClient.GetMyShortDescriptionAsync();

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
            shortDescription: shortDescription,
            languageCode: _languageCode
        );

        await Task.Delay(TimeSpan.FromSeconds(10));

        BotShortDescription newDescription = await Fixture.BotClient.GetMyShortDescriptionAsync(languageCode: _languageCode);

        Assert.NotNull(newDescription);
        Assert.Equal(shortDescription, newDescription.ShortDescription);
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        await BotClient.SetMyShortDescriptionAsync(
            shortDescription: ""
        );

        await BotClient.SetMyShortDescriptionAsync(
            shortDescription: "",
            languageCode: _languageCode
        );
    }
}

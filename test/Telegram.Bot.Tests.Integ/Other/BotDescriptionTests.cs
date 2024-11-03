using System;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Other;

[Collection(Constants.TestCollections.BotDescription)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class BotDescriptionTests(TestsFixture fixture) : TestClass(fixture), IAsyncLifetime
{
    string _languageCode;

    [OrderedFact("Should set a new bot description")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetMyDescription)]
    public async Task Should_Set_New_Bot_Description()
    {
        string description = "Test bot description";

        await BotClient.SetMyDescription(
            description: description
        );
    }

    [OrderedFact("Should get previously set bot description")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetMyDescription)]
    public async Task Should_Get_Set_Bot_Description()
    {
        const string description = "Test bot description";

        await BotClient.SetMyDescription(
            description: description
        );

        await Task.Delay(TimeSpan.FromSeconds(10));

        BotDescription currentDescription = await Fixture.BotClient.GetMyDescription();

        Assert.NotNull(currentDescription);
        Assert.Equal(description, currentDescription.Description);
    }

    [OrderedFact("Should delete bot description")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetMyDescription)]
    public async Task Should_Delete_Bot_Description()
    {
        string description = "Test bot description";

        await BotClient.SetMyDescription(
            description: description
        );

        BotDescription setDescription = await Fixture.BotClient.GetMyDescription();

        Assert.NotNull(setDescription);
        Assert.Equal(description, setDescription.Description);

        await BotClient.SetMyDescription(
            description: ""
        );

        await Task.Delay(TimeSpan.FromSeconds(10));

        BotDescription currentDescription = await Fixture.BotClient.GetMyDescription();

        Assert.NotNull(currentDescription.Description);
        Assert.Empty(currentDescription.Description);
    }

    [OrderedFact("Should set description with language code area")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetMyDescription)]
    public async Task Should_Set_Description_With_Language_Code_Area()
    {
        string description = "Тестовое описание бота";

        _languageCode = "ru";

        await BotClient.SetMyDescription(
            description: description,
            languageCode: _languageCode
        );

        await Task.Delay(TimeSpan.FromSeconds(10));

        BotDescription newDescription = await Fixture.BotClient.GetMyDescription(languageCode: _languageCode);

        Assert.NotNull(newDescription);
        Assert.Equal(description, newDescription.Description);
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        await BotClient.SetMyDescription(
            description: ""
        );

        await BotClient.SetMyDescription(
            description: "",
            languageCode: _languageCode
        );
    }
}

using System;
using System.Threading.Tasks;
using Telegram.Bot.Requests;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Other;

[Collection(Constants.TestCollections.BotDescription)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class BotDescriptionTests(TestsFixture fixture) : IAsyncLifetime
{
    string _languageCode;

    ITelegramBotClient BotClient => fixture.BotClient;

    [OrderedFact("Should set a new bot description")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetMyDescription)]
    public async Task Should_Set_New_Bot_Description()
    {
        string description = "Test bot description";

        await BotClient.SetMyDescriptionAsync(
            new SetMyDescriptionRequest
            {
                Description = description,
            }
        );
    }

    [OrderedFact("Should get previously set bot description")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetMyDescription)]
    public async Task Should_Get_Set_Bot_Description()
    {
        const string description = "Test bot description";

        await BotClient.SetMyDescriptionAsync(
            new SetMyDescriptionRequest
            {
                Description = description,
            }
        );

        await Task.Delay(TimeSpan.FromSeconds(10));

        BotDescription currentDescription = await fixture.BotClient.GetMyDescriptionAsync(new GetMyDescriptionRequest());

        Assert.NotNull(currentDescription);
        Assert.Equal(description, currentDescription.Description);
    }

    [OrderedFact("Should delete bot description")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetMyDescription)]
    public async Task Should_Delete_Bot_Description()
    {
        string description = "Test bot description";

        await BotClient.SetMyDescriptionAsync(
            new SetMyDescriptionRequest
            {
                Description = description,
            }
        );

        BotDescription setDescription = await fixture.BotClient.GetMyDescriptionAsync(new GetMyDescriptionRequest());

        Assert.NotNull(setDescription);
        Assert.Equal(description, setDescription.Description);

        await BotClient.SetMyDescriptionAsync(
            new SetMyDescriptionRequest
            {
                Description = string.Empty,
            }
        );

        await Task.Delay(TimeSpan.FromSeconds(10));

        BotDescription currentDescription = await fixture.BotClient.GetMyDescriptionAsync(new GetMyDescriptionRequest());

        Assert.NotNull(currentDescription.Description);
        Assert.Empty(currentDescription.Description);
    }

    [OrderedFact("Should set description with language code area")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetMyDescription)]
    public async Task Should_Set_Description_With_Language_Code_Area()
    {
        string description = "Тестовое описание бота";

        _languageCode = "ru";

        await BotClient.SetMyDescriptionAsync(
            new SetMyDescriptionRequest
            {
                Description = description,
                LanguageCode = _languageCode,
            }
        );

        await Task.Delay(TimeSpan.FromSeconds(10));

        BotDescription newDescription = await fixture.BotClient.GetMyDescriptionAsync(
            new GetMyDescriptionRequest
            {
                LanguageCode = _languageCode
            }
        );

        Assert.NotNull(newDescription);
        Assert.Equal(description, newDescription.Description);
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        await BotClient.SetMyDescriptionAsync(
            new SetMyDescriptionRequest
            {
                Description = string.Empty,
            }
        );

        await BotClient.SetMyDescriptionAsync(
            new SetMyDescriptionRequest
            {
                Description = string.Empty,
                LanguageCode = _languageCode,
            }
        );
    }
}

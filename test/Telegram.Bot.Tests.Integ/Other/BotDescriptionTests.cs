using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Other;

[Collection(Constants.TestCollections.BotDescription)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class BotDescriptionTests: IAsyncLifetime
{
    readonly TestsFixture _fixture;
    string _languageCode;

    ITelegramBotClient BotClient => _fixture.BotClient;

    public BotDescriptionTests(TestsFixture fixture)
    {
        _fixture = fixture;
    }

    [OrderedFact("Should set a new bot description")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetMyDescription)]
    public async Task Should_Set_New_Bot_Description()
    {
        string description = "Test bot description";

        await BotClient.SetMyDescriptionAsync(
            description: description
        );
    }

    [OrderedFact("Should get previously set bot description")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetMyDescription)]
    public async Task Should_Get_Set_Bot_Description()
    {
        string description = "Test bot description";

        await BotClient.SetMyDescriptionAsync(
            description: description
        );

        BotDescription currentDescription = await _fixture.BotClient.GetMyDescriptionAsync();

        Assert.NotNull(currentDescription);
        Assert.Equal(description, currentDescription.Description);
    }

    [OrderedFact("Should delete bot description")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetMyDescription)]
    public async Task Should_Delete_Bot_Description()
    {
        string description = "Test bot description";

        await BotClient.SetMyDescriptionAsync(
            description: description
        );

        BotDescription setDescription = await _fixture.BotClient.GetMyDescriptionAsync();

        Assert.NotNull(setDescription);
        Assert.Equal(description, setDescription.Description);

        await BotClient.SetMyDescriptionAsync(
            description: string.Empty
        );

        BotDescription currentDescription = await _fixture.BotClient.GetMyDescriptionAsync();

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
            description: description,
            languageCode: _languageCode
        );

        BotDescription newDescription = await _fixture.BotClient.GetMyDescriptionAsync(languageCode: _languageCode);

        Assert.NotNull(newDescription);
        Assert.Equal(description, newDescription.Description);
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        await BotClient.SetMyDescriptionAsync(
            description: string.Empty
        );

        await BotClient.SetMyDescriptionAsync(
            description: string.Empty,
            languageCode: _languageCode
        );
    }
}

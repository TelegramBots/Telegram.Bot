using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Other;

[Collection(Constants.TestCollections.BotShortDescription)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class BotShortDescriptionTests: IAsyncLifetime
{
    readonly TestsFixture _fixture;
    string _languageCode;

    ITelegramBotClient BotClient => _fixture.BotClient;

    public BotShortDescriptionTests(TestsFixture fixture)
    {
        _fixture = fixture;
    }

    [OrderedFact("Should set a new bot short description")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetMyShortDescription)]
    public async Task Should_Set_New_Bot__Short_Description()
    {
        string shortDescription = "Test bot short description";

        await BotClient.SetMyShortDescriptionAsync(
            shortDescription: shortDescription
        );
    }

    [OrderedFact("Should get previously set bot short description")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetMyShortDescription)]
    public async Task Should_Get_Set_Bot_Short_Description()
    {
        string shortDescription = "Test bot short description";

        await BotClient.SetMyShortDescriptionAsync(
            shortDescription: shortDescription
        );

        BotShortDescription currentShortDescription = await _fixture.BotClient.GetMyShortDescriptionAsync();

        Assert.NotNull(currentShortDescription);
        Assert.Equal(shortDescription, currentShortDescription.ShortDescription);
    }

    [OrderedFact("Should delete bot short description")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetMyShortDescription)]
    public async Task Should_Delete_Bot_Short_Description()
    {
        string shortDescription = "Test bot short description";

        await BotClient.SetMyShortDescriptionAsync(
            shortDescription: shortDescription
        );

        BotShortDescription setShortDescription = await _fixture.BotClient.GetMyShortDescriptionAsync();

        Assert.NotNull(setShortDescription);
        Assert.Equal(shortDescription, setShortDescription.ShortDescription);

        await BotClient.SetMyShortDescriptionAsync(
            shortDescription: string.Empty
        );

        BotShortDescription currentShortDescription = await _fixture.BotClient.GetMyShortDescriptionAsync();

        Assert.NotNull(currentShortDescription.ShortDescription);
        Assert.Empty(currentShortDescription.ShortDescription);
    }

    [OrderedFact("Should set short description with language code area")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetMyShortDescription)]
    public async Task Should_Set_Short_Description_With_Language_Code_Area()
    {
        string shortDescription = "Короткое тестовое описание бота";

        _languageCode = "ru";

        await BotClient.SetMyShortDescriptionAsync(
            shortDescription: shortDescription,
            languageCode: _languageCode
        );

        BotShortDescription newDescription = await _fixture.BotClient.GetMyShortDescriptionAsync(languageCode: _languageCode);

        Assert.NotNull(newDescription);
        Assert.Equal(shortDescription, newDescription.ShortDescription);
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        await BotClient.SetMyShortDescriptionAsync(
            shortDescription: string.Empty
        );

        await BotClient.SetMyShortDescriptionAsync(
            shortDescription: string.Empty,
            languageCode: _languageCode
        );
    }
}

using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Tests.Integ.Framework;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Games;

[Collection(Constants.TestCollections.GameException)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class GamesExceptionTests
{
    ITelegramBotClient BotClient => _fixture.BotClient;

    readonly TestsFixture _fixture;

    public GamesExceptionTests(TestsFixture fixture)
    {
        _fixture = fixture;
    }

    [OrderedFact("Should throw InvalidGameShortNameException")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendGame)]
    public async Task Should_Throw_InvalidGameShortNameException()
    {
        ApiRequestException e = await Assert.ThrowsAsync<ApiRequestException>(() =>
            BotClient.SendGameAsync(
                chatId: _fixture.SupergroupChat.Id,
                gameShortName: "my game"
            )
        );

        Assert.Contains("Bad Request: GAME_SHORTNAME_INVALID", e.Message);
    }

    [OrderedFact("Should throw InvalidGameShortNameException for empty name")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendGame)]
    public async Task Should_Throw_InvalidGameShortNameException_2()
    {
        ApiRequestException e = await Assert.ThrowsAsync<ApiRequestException>(() =>
            BotClient.SendGameAsync(
                chatId: _fixture.SupergroupChat.Id,
                gameShortName: string.Empty
            )
        );

        Assert.Contains("game_short_name", e.Message);
    }

    [OrderedFact("Should throw InvalidGameShortNameException for non-existent game")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendGame)]
    public async Task Should_Throw_InvalidGameShortNameException_3()
    {
        ApiRequestException e = await Assert.ThrowsAsync<ApiRequestException>(() =>
            BotClient.SendGameAsync(
                chatId: _fixture.SupergroupChat.Id,
                gameShortName: "non_existing_game"
            )
        );

        Assert.Contains("Bad Request: wrong game short name specified", e.Message);
    }

    // ToDo: Send game with markup & game button NOT as 1st: BUTTON_POS_INVALID
    // ToDo: Send game with markup & w/o game button: REPLY_MARKUP_GAME_EMPTY
}
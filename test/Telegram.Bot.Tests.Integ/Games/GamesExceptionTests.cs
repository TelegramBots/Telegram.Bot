using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Tests.Integ.Framework;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Games
{
    [Collection(Constants.TestCollections.GameException)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class GamesExceptionTests
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public GamesExceptionTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        [OrderedFact("Should throw ApiRequestException")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendGame)]
        public async Task Should_Throw_ApiRequestException_When_GameShortName_Invalid()
        {
            ApiRequestException exception = await Assert.ThrowsAsync<ApiRequestException>(async () =>
                await BotClient.SendGameAsync(
                    chatId: _fixture.SupergroupChat.Id,
                    gameShortName: "my game"
                )
            );

            Assert.Equal(400, exception.ErrorCode);
            Assert.Contains("GAME_SHORTNAME_INVALID", exception.Message);
        }

        [OrderedFact("Should throw ApiRequestException for empty name")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendGame)]
        public async Task Should_Throw_ApiRequestException_When_GameShortName_Invalid_2()
        {
            ApiRequestException exception = await Assert.ThrowsAsync<ApiRequestException>(async () =>
                await BotClient.SendGameAsync(
                    chatId: _fixture.SupergroupChat.Id,
                    gameShortName: string.Empty
                )
            );

            Assert.Equal(400, exception.ErrorCode);
            Assert.Contains("game_short_name", exception.Message);
        }

        [OrderedFact("Should throw ApiRequestException for non-existent game")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendGame)]
        public async Task Should_Throw_ApiRequestException_When_GameShortName_Invalid_3()
        {
            ApiRequestException exception = await Assert.ThrowsAsync<ApiRequestException>(async () =>
                await BotClient.SendGameAsync(
                    chatId: _fixture.SupergroupChat.Id,
                    gameShortName: "non_existing_game"
                )
            );

            Assert.Equal(400, exception.ErrorCode);
            Assert.Contains("wrong game short name specified", exception.Message);
        }

        // ToDo: Send game with markup & game button NOT as 1st: BUTTON_POS_INVALID
        // ToDo: Send game with markup & w/o game button: REPLY_MARKUP_GAME_EMPTY
    }
}

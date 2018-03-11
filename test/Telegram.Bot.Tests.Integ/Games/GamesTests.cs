using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Games
{
    [Collection(Constants.TestCollections.Games)]
    [TestCaseOrderer(Constants.TestCaseOrderer2, Constants.AssemblyName)]
    public class GamesTests
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public GamesTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldSendGame)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendGame)]
        public async Task Should_Send_Game()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendGame);

            Message gameMessage = await BotClient.SendGameAsync(
                chatId: _fixture.SupergroupChat.Id,
                gameShortName: "game1"
            );

            Assert.NotEmpty(gameMessage.Game.Title);
            Assert.NotEmpty(gameMessage.Game.Description);
            Assert.NotNull(gameMessage.Game.Photo);
            Assert.NotEmpty(gameMessage.Game.Photo.Select(_ => _.FileId));
            Assert.All(gameMessage.Game.Photo, _ => Assert.True(_.FileSize > 50));
            Assert.All(gameMessage.Game.Photo, _ => Assert.True(_.Width > 25));
            Assert.All(gameMessage.Game.Photo, _ => Assert.True(_.Height > 25));
        }

        [OrderedFact(DisplayName = FactTitles.ShouldSendGameWithReplyMarkup)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendGame)]
        public async Task Should_Send_Game_With_ReplyMarkup()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendGameWithReplyMarkup);

            Message gameMessage = await BotClient.SendGameAsync(
                chatId: _fixture.SupergroupChat.Id,
                gameShortName: "game1",
                replyMarkup: new[]
                {
                    InlineKeyboardButton.WithCallBackGame("Play"),
                    InlineKeyboardButton.WithCallbackData("Second button")
                }
            );

            Assert.NotEmpty(gameMessage.Game.Title);
            Assert.NotEmpty(gameMessage.Game.Description);
            Assert.NotNull(gameMessage.Game.Photo);
            Assert.NotEmpty(gameMessage.Game.Photo.Select(_ => _.FileId));
            Assert.All(gameMessage.Game.Photo, _ => Assert.True(_.FileSize > 50));
            Assert.All(gameMessage.Game.Photo, _ => Assert.True(_.Width > 25));
            Assert.All(gameMessage.Game.Photo, _ => Assert.True(_.Height > 25));
        }

        private static class FactTitles
        {
            public const string ShouldSendGame = "Should send game";

            public const string ShouldSendGameWithReplyMarkup = "Should send game with a custom reply markup";
        }
    }
}
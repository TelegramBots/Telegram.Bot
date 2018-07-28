using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Games
{
    [Collection(Constants.TestCollections.Games2)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class GamesTests2 : IClassFixture<GamesFixture>
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        private readonly GamesFixture _classFixture;

        public GamesTests2(TestsFixture fixture, GamesFixture classFixture)
        {
            _fixture = fixture;
            _classFixture = classFixture;
        }

        #region Regular Game Message

        [OrderedFact(DisplayName = FactTitles.ShouldSendGame)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendGame)]
        public async Task Should_Send_Game()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendGame);

            Message gameMessage = await BotClient.SendGameAsync(
                chatId: _fixture.SupergroupChat.Id,
                gameShortName: _classFixture.GameShortName
            );

            Assert.Equal(MessageType.Game, gameMessage.Type);
            Assert.NotEmpty(gameMessage.Game.Title);
            Assert.NotEmpty(gameMessage.Game.Description);
            Assert.NotNull(gameMessage.Game.Photo);
            Assert.NotEmpty(gameMessage.Game.Photo.Select(_ => _.FileId));
            Assert.All(gameMessage.Game.Photo, _ => Assert.True(_.FileSize > 50));
            Assert.All(gameMessage.Game.Photo, _ => Assert.True(_.Width > 25));
            Assert.All(gameMessage.Game.Photo, _ => Assert.True(_.Height > 25));

            _classFixture.GameMessage = gameMessage;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldSendGameWithReplyMarkup)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendGame)]
        public async Task Should_Send_Game_With_ReplyMarkup()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendGameWithReplyMarkup);

            Message gameMessage = await BotClient.SendGameAsync(
                chatId: _fixture.SupergroupChat.Id,
                gameShortName: _classFixture.GameShortName,
                replyMarkup: new[]
                {
                    InlineKeyboardButton.WithCallBackGame("Play"),
                    InlineKeyboardButton.WithCallbackData("Second button")
                }
            );

            Assert.Equal(MessageType.Game, gameMessage.Type);
            Assert.NotEmpty(gameMessage.Game.Title);
            Assert.NotEmpty(gameMessage.Game.Description);
            Assert.NotNull(gameMessage.Game.Photo);
            Assert.NotEmpty(gameMessage.Game.Photo.Select(_ => _.FileId));
            Assert.All(gameMessage.Game.Photo, _ => Assert.True(_.FileSize > 50));
            Assert.All(gameMessage.Game.Photo, _ => Assert.True(_.Width > 25));
            Assert.All(gameMessage.Game.Photo, _ => Assert.True(_.Height > 25));
        }

        [OrderedFact(DisplayName = FactTitles.ShouldGetHighScores)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetGameHighScores)]
        public async Task Should_Get_High_Scores()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldGetHighScores);

            GameHighScore[] highScores = await BotClient.GetGameHighScoresAsync(
                userId: _classFixture.Player.Id,
                chatId: _fixture.SupergroupChat.Id,
                messageId: _classFixture.GameMessage.MessageId
            );

            Assert.All(highScores, _ => Assert.True(_.Position > 0));
            Assert.All(highScores, _ => Assert.True(_.Score > 0));
            Assert.All(highScores.Select(_ => _.User), Assert.NotNull);

            _classFixture.HighScores = highScores;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldSetGameScore)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetGameScore)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetGameHighScores)]
        public async Task Should_Set_Game_Score()
        {
            int playerId = _classFixture.Player.Id;

            bool playerAlreadyHasScore = _classFixture.HighScores
                .Any(highScore => highScore.User.Id == playerId);

            int oldScore = playerAlreadyHasScore
                ? _classFixture.HighScores.Single(highScore => highScore.User.Id == playerId).Score
                : 0;

            int newScore = oldScore + 1 + new Random().Next(3);

            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSetGameScore,
                $"Changing score from {oldScore} to {newScore} for {_classFixture.Player.Username.Replace("_", @"\_")}.");

            Message gameMessage = await BotClient.SetGameScoreAsync(
                userId: playerId,
                score: newScore,
                chatId: _fixture.SupergroupChat.Id,
                messageId: _classFixture.GameMessage.MessageId
            );

            Assert.True(JToken.DeepEquals(
                JToken.FromObject(_classFixture.GameMessage),
                JToken.FromObject(gameMessage)
            ));

            _classFixture.HighScores =
                await BotClient.GetGameHighScoresAsync(playerId, _fixture.SupergroupChat.Id, gameMessage.MessageId);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldDeductGameScore)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetGameScore)]
        public async Task Should_Deduct_Game_Score()
        {
            int playerId = _classFixture.Player.Id;
            int oldScore = _classFixture.HighScores.Single(highScore => highScore.User.Id == playerId).Score;
            int newScore = oldScore - 1;

            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldDeductGameScore,
                $"Changing score from {oldScore} to {newScore} for {_classFixture.Player.Username.Replace("_", @"\_")}.");

            Message gameMessage = await BotClient.SetGameScoreAsync(
                userId: playerId,
                score: newScore,
                chatId: _fixture.SupergroupChat.Id,
                messageId: _classFixture.GameMessage.MessageId,
                force: true
            );

            Assert.Equal(MessageType.Game, gameMessage.Type);
        }

        #endregion

        private static class FactTitles
        {
            public const string ShouldSendGame = "Should send game";

            public const string ShouldSendGameWithReplyMarkup = "Should send game with a custom reply markup";

            public const string ShouldGetHighScores = "Should get game high score";

            public const string ShouldSetGameScore = "Should set game score";

            public const string ShouldDeductGameScore = "Should deduct game score by setting it forcefully";
        }
    }
}

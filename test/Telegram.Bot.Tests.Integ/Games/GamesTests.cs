using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Games
{
    [Collection(Constants.TestCollections.Games)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class GamesTests : IClassFixture<GamesFixture>
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        private readonly GamesFixture _classFixture;

        public GamesTests(TestsFixture fixture, GamesFixture classFixture)
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
                $"Changing score from {oldScore} to {newScore} for @{_classFixture.Player.Username.Replace("_", @"\_")}.");

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
                $"Changing score from {oldScore} to {newScore} for @{_classFixture.Player.Username.Replace("_", @"\_")}.");

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

        #region Inline Game Message

        [OrderedFact(DisplayName = FactTitles.ShouldAnswerInlineQueryWithGame)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        public async Task Should_Answer_InlineQuery_With_Game()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAnswerInlineQueryWithGame,
                startInlineQuery: true);

            Update queryUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            const string resultId = "game";
            await BotClient.AnswerInlineQueryAsync(
                inlineQueryId: queryUpdate.InlineQuery.Id,
                results: new InlineQueryResultBase[]
                {
                    new InlineQueryResultGame(
                        id: resultId,
                        gameShortName: _classFixture.GameShortName
                    )
                },
                cacheTime: 0
            );

            (Update messageUpdate, Update chosenResultUpdate) = await _fixture.UpdateReceiver.GetInlineQueryResultUpdates(MessageType.Game);

            Assert.Equal(MessageType.Game, messageUpdate.Message.Type);
            Assert.Equal(resultId, chosenResultUpdate.ChosenInlineResult.ResultId);
            Assert.Empty(chosenResultUpdate.ChosenInlineResult.Query);

            _classFixture.InlineGameMessageId = chosenResultUpdate.ChosenInlineResult.InlineMessageId;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldGetHighScoresInline)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetGameHighScores)]
        public async Task Should_Get_High_Scores_Inline_Message()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldGetHighScoresInline);

            GameHighScore[] highScores = await BotClient.GetGameHighScoresAsync(
                userId: _classFixture.Player.Id,
                inlineMessageId: _classFixture.InlineGameMessageId
            );

            Assert.All(highScores, _ => Assert.True(_.Position > 0));
            Assert.All(highScores, _ => Assert.True(_.Score > 0));
            Assert.All(highScores.Select(_ => _.User), Assert.NotNull);

            _classFixture.HighScores = highScores;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldSetGameScoreInline)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetGameScore)]
        public async Task Should_Set_Game_Score_Inline_Message()
        {
            int playerId = _classFixture.Player.Id;
            int oldScore = _classFixture.HighScores.Single(highScore => highScore.User.Id == playerId).Score;
            int newScore = oldScore + 1 + new Random().Next(3);

            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSetGameScoreInline,
                $"Changing score from {oldScore} to {newScore} for @{_classFixture.Player.Username.Replace("_", @"\_")}.");

            await BotClient.SetGameScoreAsync(
                userId: playerId,
                score: newScore,
                inlineMessageId: _classFixture.InlineGameMessageId
            );
        }

        #endregion

        [OrderedFact(DisplayName = FactTitles.ShouldAnswerGameCallbackQuery)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerCallbackQuery)]
        public async Task Should_Answer_CallbackQuery_With_Game_Url()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAnswerGameCallbackQuery,
                "Click on any Play button on any of the game messges above 👆");

            Update cqUpdate = await _fixture.UpdateReceiver.GetCallbackQueryUpdateAsync();

            Assert.True(cqUpdate.CallbackQuery.IsGameQuery);

            await BotClient.AnswerCallbackQueryAsync(
                callbackQueryId: cqUpdate.CallbackQuery.Id,
                url: "https://tbot.xyz/lumber/"
            );
        }

        private static class FactTitles
        {
            public const string ShouldSendGame = "Should send game";

            public const string ShouldSendGameWithReplyMarkup = "Should send game with a custom reply markup";

            public const string ShouldGetHighScores = "Should get game high score";

            public const string ShouldSetGameScore = "Should set game score";

            public const string ShouldDeductGameScore = "Should deduct game score by setting it forcefully";

            public const string ShouldAnswerGameCallbackQuery = "Should answer game callback query";

            public const string ShouldAnswerInlineQueryWithGame = "Should answer inline query with a game";

            public const string ShouldGetHighScoresInline = "Should get game high score for inline message";

            public const string ShouldSetGameScoreInline = "Should set game score for inline message";
        }
    }
}

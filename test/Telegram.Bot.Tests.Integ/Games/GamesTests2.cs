using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Games;

[Collection(Constants.TestCollections.Games2)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class GamesTests2(TestsFixture fixture, GamesFixture classFixture) : IClassFixture<GamesFixture>
{
    ITelegramBotClient BotClient => fixture.BotClient;

    [OrderedFact("Should send game")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendGame)]
    public async Task Should_Send_Game()
    {
        Message gameMessage = await BotClient.SendGameAsync(
            new()
            {
                ChatId = fixture.SupergroupChat.Id,
                GameShortName = classFixture.GameShortName,
            }
        );

        Assert.Equal(MessageType.Game, gameMessage.Type);
        Assert.NotNull(gameMessage.Game);
        Assert.NotNull(gameMessage.Game?.Title);
        Assert.NotEmpty(gameMessage.Game!.Title);
        Assert.NotEmpty(gameMessage.Game!.Description);
        Assert.NotNull(gameMessage.Game.Photo);
        Assert.NotEmpty(gameMessage.Game.Photo.Select(p => p.FileId));
        Assert.All(gameMessage.Game.Photo, p => Assert.True(p.FileSize > 100));
        Assert.All(gameMessage.Game.Photo, p => Assert.True(p.Width > 80));
        Assert.All(gameMessage.Game.Photo, p => Assert.True(p.Height > 40));

        classFixture.GameMessage = gameMessage;
    }

    [OrderedFact("Should send game with a custom reply markup")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendGame)]
    public async Task Should_Send_Game_With_ReplyMarkup()
    {
        Message gameMessage = await BotClient.SendGameAsync(
            new()
            {
                ChatId = fixture.SupergroupChat.Id,
                GameShortName = classFixture.GameShortName,
                ReplyMarkup = new([
                    InlineKeyboardButton.WithCallBackGame(text: "Play"),
                    InlineKeyboardButton.WithCallbackData(textAndCallbackData: "Second button")
                ]),
            }
        );

        Assert.Equal(MessageType.Game, gameMessage.Type);
        Assert.NotNull(gameMessage.Game);
        Assert.NotEmpty(gameMessage.Game!.Title);
        Assert.NotEmpty(gameMessage.Game.Description);
        Assert.NotNull(gameMessage.Game.Photo);
        Assert.NotEmpty(gameMessage.Game.Photo.Select(p => p.FileId));
        Assert.All(gameMessage.Game.Photo, p => Assert.True(p.FileSize > 100));
        Assert.All(gameMessage.Game.Photo, p => Assert.True(p.Width > 80));
        Assert.All(gameMessage.Game.Photo, p => Assert.True(p.Height > 40));
    }

    [OrderedFact("Should get game high score")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetGameHighScores)]
    public async Task Should_Get_High_Scores()
    {
        GameHighScore[] highScores = await BotClient.GetGameHighScoresAsync(
            new()
            {
                UserId = classFixture.Player.Id,
                ChatId = fixture.SupergroupChat.Id,
                MessageId = classFixture.GameMessage.MessageId,
            }
        );

        Assert.All(highScores, hs => Assert.True(hs.Position > 0));
        Assert.All(highScores, hs => Assert.True(hs.Score > 0));
        Assert.All(highScores.Select(hs => hs.User), Assert.NotNull);

        classFixture.HighScores = highScores;
    }

    [OrderedFact("Should set game score")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetGameScore)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetGameHighScores)]
    public async Task Should_Set_Game_Score()
    {
        long playerId = classFixture.Player.Id;

        bool playerAlreadyHasScore = classFixture.HighScores
            .Any(highScore => highScore.User.Id == playerId);

        int oldScore = playerAlreadyHasScore
            ? classFixture.HighScores.Single(highScore => highScore.User.Id == playerId).Score
            : 0;

        int newScore = oldScore + 1 + new Random().Next(3);

        await fixture.SendTestInstructionsAsync(
            $"Changing score from {oldScore} to {newScore} for {classFixture.Player.Username!.Replace("_", @"\_")}."
        );

        Message gameMessage = await BotClient.SetGameScoreAsync(
            new()
            {
                UserId = playerId,
                Score = newScore,
                ChatId = fixture.SupergroupChat.Id,
                MessageId = classFixture.GameMessage.MessageId,
            }
        );

        Assert.Equal(classFixture.GameMessage.MessageId, gameMessage.MessageId);

        // update the high scores cache
        await Task.Delay(1_000);
        classFixture.HighScores = await BotClient.GetGameHighScoresAsync(
            new()
            {
                UserId = playerId,
                ChatId = fixture.SupergroupChat.Id,
                MessageId = gameMessage.MessageId,
            }
        );
    }

    [OrderedFact("Should deduct game score by setting it forcefully")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetGameScore)]
    public async Task Should_Deduct_Game_Score()
    {
        long playerId = classFixture.Player.Id;
        int oldScore = classFixture.HighScores.Single(highScore => highScore.User.Id == playerId).Score;
        int newScore = oldScore - 1;

        await fixture.SendTestInstructionsAsync(
            $"Changing score from {oldScore} to {newScore} for {classFixture.Player.Username!.Replace("_", @"\_")}."
        );

        Message gameMessage = await BotClient.SetGameScoreAsync(
            new()
            {
                UserId = playerId,
                Score = newScore,
                ChatId = fixture.SupergroupChat.Id,
                MessageId = classFixture.GameMessage.MessageId,
                Force = true,
            }
        );

        Assert.Equal(MessageType.Game, gameMessage.Type);
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Requests;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Games;

[Collection(Constants.TestCollections.Games)]
[Trait(Constants.CategoryTraitName, Constants.InteractiveCategoryValue)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class GamesTests(TestsFixture fixture, GamesFixture classFixture) : IClassFixture<GamesFixture>
{
    ITelegramBotClient BotClient => fixture.BotClient;

    [OrderedFact("Should answer inline query with a game")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
    public async Task Should_Answer_InlineQuery_With_Game()
    {
        await fixture.SendTestInstructionsAsync(
            "Staring the inline query with this message...",
            startInlineQuery: true
        );

        Update queryUpdate = await fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "game";
        await BotClient.AnswerInlineQueryAsync(
            new()
            {
                InlineQueryId = queryUpdate.InlineQuery!.Id,
                Results = [
                    new InlineQueryResultGame
                    {
                        Id = resultId,
                        GameShortName = classFixture.GameShortName,
                    }
                ],
                CacheTime = 0
            }
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: fixture.SupergroupChat.Id,
                messageType: MessageType.Game
            );

        Assert.Equal(MessageType.Game, messageUpdate?.Message?.Type);
        Assert.Equal(resultId, chosenResultUpdate?.ChosenInlineResult?.ResultId);
        Assert.NotNull(chosenResultUpdate?.ChosenInlineResult);
        Assert.NotNull(chosenResultUpdate);
        Assert.Empty(chosenResultUpdate.ChosenInlineResult.Query);

        classFixture.InlineGameMessageId = chosenResultUpdate.ChosenInlineResult.InlineMessageId;
    }

    [OrderedFact("Should get game high score for inline message")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetGameHighScores)]
    public async Task Should_Get_High_Scores_Inline_Message()
    {
        GameHighScore[] highScores = await BotClient.GetInlineGameHighScoresAsync(
            new()
            {
                UserId = classFixture.Player.Id,
                InlineMessageId = classFixture.InlineGameMessageId,
            }
        );

        Assert.All(highScores, _ => Assert.True(_.Position > 0));
        Assert.All(highScores, _ => Assert.True(_.Score > 0));
        Assert.All(highScores.Select(_ => _.User), Assert.NotNull);

        classFixture.HighScores = highScores;
    }

    [OrderedFact("Should set game score for inline message")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetGameScore)]
    public async Task Should_Set_Game_Score_Inline_Message()
    {
        long playerId = classFixture.Player.Id;
        int oldScore = classFixture.HighScores.Single(highScore => highScore.User.Id == playerId).Score;
        int newScore = oldScore + 1 + new Random().Next(3);

        await fixture.SendTestInstructionsAsync(
            $"Changing score from {oldScore} to {newScore} for {classFixture.Player.Username!.Replace("_", @"\_")}."
        );

        await BotClient.SetInlineGameScoreAsync(
            new()
            {
                UserId = playerId,
                Score = newScore,
                InlineMessageId = classFixture.InlineGameMessageId,
            }
        );
    }

    [OrderedFact("Should answer game callback query")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerCallbackQuery)]
    public async Task Should_Answer_CallbackQuery_With_Game_Url()
    {
        await fixture.SendTestInstructionsAsync(
            "Click on any Play button on any of the game messages above 👆"
        );

        Update cqUpdate = await fixture.UpdateReceiver.GetCallbackQueryUpdateAsync();

        Assert.True(cqUpdate.CallbackQuery?.IsGameQuery);

        await BotClient.AnswerCallbackQueryAsync(
            new AnswerCallbackQueryRequest
            {
                CallbackQueryId = cqUpdate.CallbackQuery!.Id,
                Url = "https://tbot.xyz/lumber/",
            }
        );
    }
}

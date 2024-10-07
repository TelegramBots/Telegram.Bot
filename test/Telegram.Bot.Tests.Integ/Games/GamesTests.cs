using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Games;

[Collection(Constants.TestCollections.Games)]
[Trait(Constants.CategoryTraitName, Constants.InteractiveCategoryValue)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class GamesTests(TestsFixture fixture, GamesFixture classFixture) : TestClass(fixture), IClassFixture<GamesFixture>
{
    [OrderedFact("Should answer inline query with a game")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
    public async Task Should_Answer_InlineQuery_With_Game()
    {
        await Fixture.SendTestInstructionsAsync(
            "Staring the inline query with this message...",
            startInlineQuery: true
        );

        Update queryUpdate = await Fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "game";
        await BotClient.AnswerInlineQuery(
            inlineQueryId: queryUpdate.InlineQuery!.Id,
            results:
            [
                new InlineQueryResultGame
                {
                    Id = resultId,
                    GameShortName = classFixture.GameShortName,
                }
            ],
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await Fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: Fixture.SupergroupChat.Id,
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
        GameHighScore[] highScores = await BotClient.GetGameHighScores(
            userId: classFixture.Player.Id,
            inlineMessageId: classFixture.InlineGameMessageId
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

        await Fixture.SendTestInstructionsAsync(
            $"Changing score from {oldScore} to {newScore} for {classFixture.Player.Username!.Replace("_", @"\_")}."
        );

        await BotClient.SetGameScore(
            userId: playerId,
            score: newScore,
            inlineMessageId: classFixture.InlineGameMessageId
        );
    }

    [OrderedFact("Should answer game callback query")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerCallbackQuery)]
    public async Task Should_Answer_CallbackQuery_With_Game_Url()
    {
        await Fixture.SendTestInstructionsAsync(
            "Click on any Play button on any of the game messages above 👆"
        );

        Update cqUpdate = await Fixture.UpdateReceiver.GetCallbackQueryUpdateAsync();

        Assert.NotNull(cqUpdate.CallbackQuery?.GameShortName);

        await BotClient.AnswerCallbackQuery(
            callbackQueryId: cqUpdate.CallbackQuery!.Id,
            url: "https://tbot.xyz/lumber/"
        );
    }
}

using System;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Polls;

[Collection(Constants.TestCollections.NativePolls)]
[Trait(Constants.CategoryTraitName, Constants.InteractiveCategoryValue)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class QuizPollTests(QuizPollTestsFixture classFixture)
    : TestClass(classFixture.TestsFixture), IClassFixture<QuizPollTestsFixture>
{
    [OrderedFact(
        "Should send public quiz poll")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendPoll)]
    public async Task Should_Send_Public_Quiz_Poll()
    {
        Message message = await Fixture.BotClient.SendPoll(
            chatId: Fixture.SupergroupChat,
            question: "How many silmarils were made in J. R. R. Tolkiens's Silmarillion?",
            options: ["One", "Ten", "Three"],
            isAnonymous: false,
            type: PollType.Quiz,
            correctOptionId: 2, // "Three",
            explanation: "Three [silmarils](https://en.wikipedia.org/wiki/Silmarils) were made",
            explanationParseMode: ParseMode.MarkdownV2
        );

        Assert.Equal(MessageType.Poll, message.Type);
        Assert.NotNull(message.Poll);
        Assert.NotEmpty(message.Poll.Id);
        Assert.False(message.Poll.IsClosed);
        Assert.False(message.Poll.IsAnonymous);
        Assert.Equal(PollType.Quiz, message.Poll.Type);
        Assert.False(message.Poll.AllowsMultipleAnswers);
        Assert.Equal(2, message.Poll.CorrectOptionId);
        Assert.Null(message.Poll.OpenPeriod);
        Assert.Null(message.Poll.CloseDate);

        Assert.Equal("How many silmarils were made in J. R. R. Tolkiens's Silmarillion?", message.Poll.Question);
        Assert.Equal(3, message.Poll.Options.Length);
        Assert.Equal("One", message.Poll.Options[0].Text);
        Assert.Equal("Ten", message.Poll.Options[1].Text);
        Assert.Equal("Three", message.Poll.Options[2].Text);
        Assert.All(message.Poll.Options, option => Assert.Equal(0, option.VoterCount));
        Assert.Equal("Three silmarils were made", message.Poll.Explanation);
        Assert.NotNull(message.Poll.ExplanationEntities);
        Assert.Single(message.Poll.ExplanationEntities);
        Assert.Contains(
            message.Poll.ExplanationEntities,
            entity => entity.Type == MessageEntityType.TextLink &&
                      entity.Url == "https://en.wikipedia.org/wiki/Silmarils"
        );

        classFixture.OriginalPollMessage = message;
    }

    [OrderedFact(
        "Should receive a poll answer update")]
    public async Task Should_Receive_Poll_Answer_Update()
    {
        await Fixture.SendTestInstructionsAsync(
            "🗳 Choose any answer in the quiz above 👆"
        );

        Poll poll = classFixture.OriginalPollMessage.Poll;

        Update pollAnswerUpdates = await Fixture.UpdateReceiver.GetUpdateAsync(
            update => update.PollAnswer?.OptionIds.Length == 1 &&
                      update.PollAnswer.PollId == poll!.Id,
            updateTypes: UpdateType.PollAnswer
        );

        PollAnswer pollAnswer = pollAnswerUpdates.PollAnswer;

        Assert.NotNull(pollAnswer);
        Assert.Equal(poll!.Id, pollAnswer.PollId);
        Assert.NotNull(pollAnswer.User);
        Assert.All(
            pollAnswer.OptionIds,
            optionId => Assert.True(optionId < poll.Options.Length)
        );

        classFixture.PollAnswer = pollAnswer;
    }

    [OrderedFact(
        "Should stop quiz poll")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.StopPoll)]
    public async Task Should_Stop_Quiz_Poll()
    {
        // don't close poll immediately, without a delay the resulting poll object
        // doesn't match up with the previously received poll answer
        await Task.Delay(TimeSpan.FromSeconds(5));

        Poll closedPoll = await BotClient.StopPoll(
            chatId: classFixture.OriginalPollMessage.Chat,
            messageId: classFixture.OriginalPollMessage.Id
        );

        Assert.Equal(classFixture.OriginalPollMessage.Poll!.Id, closedPoll.Id);
        Assert.True(closedPoll.IsClosed);

        PollAnswer pollAnswer = classFixture.PollAnswer;

        Assert.All(
            pollAnswer.OptionIds,
            optionId => Assert.True(closedPoll.Options[optionId].VoterCount > 0)
        );
    }
}

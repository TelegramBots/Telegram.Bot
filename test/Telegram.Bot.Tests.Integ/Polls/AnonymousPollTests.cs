using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Polls;

[Collection(Constants.TestCollections.NativePolls)]
[Trait(Constants.CategoryTraitName, Constants.InteractiveCategoryValue)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class AnonymousPollTests(AnonymousPollTestsFixture classFixture)
    : TestClass(classFixture.TestsFixture), IClassFixture<AnonymousPollTestsFixture>
{
    [OrderedFact(
        "Should send a poll")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendPoll)]
    public async Task Should_Send_Poll()
    {
        Message message = await BotClient.SendPoll(
            chatId: Fixture.SupergroupChat,
            question: "Who shot first?",
            options: ["Han Solo", "Greedo", "I don't care"]
        );

        Assert.Equal(MessageType.Poll, message.Type);
        Assert.NotNull(message.Poll);
        Assert.NotEmpty(message.Poll.Id);
        Assert.False(message.Poll.IsClosed);
        Assert.True(message.Poll.IsAnonymous);
        Assert.Equal(PollType.Regular, message.Poll.Type);
        Assert.False(message.Poll.AllowsMultipleAnswers);
        Assert.Null(message.Poll.CorrectOptionId);
        Assert.Null(message.Poll.OpenPeriod);
        Assert.Null(message.Poll.CloseDate);

        Assert.Equal("Who shot first?", message.Poll.Question);
        Assert.Equal(3, message.Poll.Options.Length);
        Assert.Equal("Han Solo", message.Poll.Options[0].Text);
        Assert.Equal("Greedo", message.Poll.Options[1].Text);
        Assert.Equal("I don't care", message.Poll.Options[2].Text);
        Assert.All(message.Poll.Options, option => Assert.Equal(0, option.VoterCount));

        classFixture.PollMessage = message;
    }

    [OrderedFact(
        "Should receive a poll update")]
    public async Task Should_Receive_Poll_State_Update()
    {
        string pollId = classFixture.PollMessage.Poll!.Id;

        await Fixture.SendTestInstructionsAsync("🗳 Vote for any of the options on the poll above 👆");
        Update update = (await Fixture.UpdateReceiver.GetUpdatesAsync(updateTypes: UpdateType.Poll))
            .Last();

        Assert.Equal(UpdateType.Poll, update.Type);
        Assert.NotNull(update.Poll);
        Assert.Equal(pollId, update.Poll.Id);
    }

    [OrderedFact(
        "Should stop the poll")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.StopPoll)]
    public async Task Should_Stop_Poll()
    {
        Poll poll = await BotClient.StopPoll(
            chatId: classFixture.PollMessage.Chat,
            messageId: classFixture.PollMessage.Id
        );

        Assert.Equal(classFixture.PollMessage.Poll!.Id, poll.Id);
        Assert.True(poll.IsClosed);
    }

    [OrderedFact("Should throw ApiRequestException due to not having enough poll options")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendPoll)]
    public async Task Should_Throw_Exception_Not_Enough_Options()
    {
        ApiRequestException exception = await Assert.ThrowsAsync<ApiRequestException>(() =>
            BotClient.SendPoll(
                chatId: Fixture.SupergroupChat,
                question: "You should never see this poll",
                options: ["The only poll option"]
            )
        );

        Assert.IsType<ApiRequestException>(exception);
        Assert.Equal("Bad Request: poll must have at least 2 option", exception.Message);
    }
}

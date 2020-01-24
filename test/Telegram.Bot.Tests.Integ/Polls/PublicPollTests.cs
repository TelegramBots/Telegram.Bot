using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Polls
{
    [Collection(Constants.TestCollections.NativePolls)]
    [Trait(Constants.CategoryTraitName, Constants.InteractiveCategoryValue)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class PublicPollTests : IClassFixture<PublicPollTestsFixture>
    {
        private readonly TestsFixture _fixture;
        private readonly PublicPollTestsFixture _classFixture;

        private ITelegramBotClient BotClient => _fixture.BotClient;

        public PublicPollTests(TestsFixture fixture, PublicPollTestsFixture classFixture)
        {
            _fixture = fixture;
            _classFixture = classFixture;
        }

        [OrderedFact("Should send public poll with multiple answers")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendPoll)]
        public async Task Should_Send_Non_Anonymous_Poll_With_Multiple_Answers()
        {
            Message message = await _fixture.BotClient.SendPollAsync(
                chatId: _fixture.SupergroupChat,
                question: "Pick your team",
                options: new [] { "Aragorn", "Galadriel", "Frodo" },
                isAnonymous: false,
                type: "regular",
                allowsMultipleAnswers: true
            );

            Assert.Equal(MessageType.Poll, message.Type);
            Assert.NotEmpty(message.Poll.Id);
            Assert.False(message.Poll.IsClosed);
            Assert.False(message.Poll.IsAnonymous);
            Assert.Equal("regular", message.Poll.Type);
            Assert.True(message.Poll.AllowsMultipleAnswers);
            Assert.Null(message.Poll.CorrectOptionId);

            Assert.Equal("Pick your team", message.Poll.Question);
            Assert.Equal(3, message.Poll.Options.Length);
            Assert.Equal("Aragorn", message.Poll.Options[0].Text);
            Assert.Equal("Galadriel", message.Poll.Options[1].Text);
            Assert.Equal("Frodo", message.Poll.Options[2].Text);
            Assert.All(message.Poll.Options, option => Assert.Equal(0, option.VoterCount));

            _classFixture.PollMessage = message;
        }

        [OrderedFact("Should receive a poll answer update")]
        public async Task Should_Receive_Poll_Answer_Update()
        {
            await _fixture.SendTestInstructionsAsync(
                "🗳 Vote for more than one option on the poll above 👆"
            );

            Update pollAnswerUpdates = (await _fixture.UpdateReceiver.GetUpdatesAsync(
                update => update.PollAnswer.OptionIds.Length > 1,
                updateTypes: UpdateType.PollAnswer
            )).First();

            Poll poll = _classFixture.PollMessage.Poll;
            PollAnswer pollAnswer = pollAnswerUpdates.PollAnswer;

            Assert.Equal(poll.Id, pollAnswer.PollId);
            Assert.NotNull(pollAnswer.User);
            Assert.All(
                pollAnswer.OptionIds,
                optionId => Assert.True(optionId < poll.Options.Length)
            );

            _classFixture.PollAnswer = pollAnswer;
        }

        [OrderedFact("Should stop non-anonymous the poll")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.StopPoll)]
        public async Task Should_Stop_Non_Anonymous_Poll()
        {
            Poll poll = await BotClient.StopPollAsync(
                chatId: _classFixture.PollMessage.Chat,
                messageId: _classFixture.PollMessage.MessageId
            );

            Assert.Equal(_classFixture.PollMessage.Poll.Id, poll.Id);
            Assert.True(poll.IsClosed);

            PollAnswer answer = _classFixture.PollAnswer;

            Assert.All(
                answer.OptionIds,
                optionId => Assert.True(poll.Options[optionId].VoterCount > 0)
            );
        }
    }
}

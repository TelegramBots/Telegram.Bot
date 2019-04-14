using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Requests;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Polls
{
    [Collection(Constants.TestCollections.NativePolls)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class PollMessageTests : IClassFixture<PollTestsFixture>
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly PollTestsFixture _classFixture;
        private readonly TestsFixture _fixture;

        public PollMessageTests(TestsFixture fixture, PollTestsFixture classFixture)
        {
            _fixture = fixture;
            _classFixture = classFixture;
        }

        [OrderedFact(FactTitles.ShouldSendPoll)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendPoll)]
        public async Task Should_Send_Poll()
        {
            string pollQuestion = "Poll question";
            string[] pollOptions =
            {
                "Poll option 1",
                "Poll option 2",
            };

            SendPollRequest sendPollRequest = new SendPollRequest(
                _fixture.SupergroupChat,
                pollQuestion,
                pollOptions
            );

            Message message = await BotClient.MakeRequestAsync(sendPollRequest);

            Assert.Equal(MessageType.Poll, message.Type);
            Assert.NotNull(message.Poll.Id);
            Assert.Equal(pollQuestion, message.Poll.Question);
            Assert.False(message.Poll.IsClosed);

            foreach (var pollOption in message.Poll.Options)
            {
                Assert.Contains(pollOption.Text, pollOptions, StringComparer.Ordinal);
            }

            _classFixture.PollMessage = message;
        }

        [OrderedFact(FactTitles.ShouldReceivePollStateUpdate)]
        public async Task Should_Receive_Poll_State_Update()
        {
            await _fixture.SendTestCaseNotificationAsync(
                "Any member of the test supergroup should vote in the poll"
            );

            Update update = (await _fixture.UpdateReceiver
                .GetUpdatesAsync(allowAnyone: true, updateTypes: UpdateType.Poll))
                .First();

            Poll poll = update.Poll;

            Assert.Equal(_classFixture.PollMessage.Poll.Id, poll.Id);
            Assert.False(poll.IsClosed);
        }

        [OrderedFact(FactTitles.ShouldStopPoll)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.StopPoll)]
        public async Task Should_Stop_Poll()
        {
            StopPollRequest stopPollRequest = new StopPollRequest(
                _classFixture.PollMessage.Chat,
                _classFixture.PollMessage.MessageId
            );

            Poll poll = await BotClient.MakeRequestAsync(stopPollRequest);

            Assert.Equal(_classFixture.PollMessage.Poll.Id, poll.Id);
            Assert.True(poll.IsClosed);
        }

        private static class FactTitles
        {
            public const string ShouldSendPoll = "Should send a poll";

            public const string ShouldReceivePollStateUpdate = "Should poll state update";

            public const string ShouldStopPoll = "Should stop the poll";
        }
    }
}

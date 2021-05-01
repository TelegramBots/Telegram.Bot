using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Tests.Integ.Framework.Fixtures;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Sending_Messages
{
    [Collection(Constants.TestCollections.SendCopyMessage)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class CopyMessageTests : IClassFixture<CopyMessageTests.Fixture>
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        private readonly Fixture _classFixture;

        public CopyMessageTests(TestsFixture testsFixture, Fixture classFixture)
        {
            _fixture = testsFixture;
            _classFixture = classFixture;
        }

        [OrderedFact("Should copy text message")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.CopyMessage)]
        public async Task Should_Copy_Text_Message()
        {
            Message message = await BotClient.SendTextMessageAsync(
                chatId: _fixture.SupergroupChat.Id,
                text: "hello");

            MessageId copyMessageId = await BotClient.CopyMessageAsync(
                _fixture.SupergroupChat.Id,
                _fixture.SupergroupChat.Id,
                message.MessageId);

            Assert.NotEqual(0, copyMessageId.Id);
        }


        public class Fixture : ChannelChatFixture
        {
            public Fixture(TestsFixture testsFixture)
                : base(testsFixture, Constants.TestCollections.SendCopyMessage)
            {
            }
        }
    }

}

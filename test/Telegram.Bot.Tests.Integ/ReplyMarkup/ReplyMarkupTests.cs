using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Telegram.Bot.Tests.Integ.ReplyMarkup
{
    [Collection(Constants.TestCollections.ReplyMarkup)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class ReplyMarkupTests
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public ReplyMarkupTests(TestsFixture testsFixture)
        {
            _fixture = testsFixture;
        }

        [Fact(DisplayName = FactTitles.ShouldForceReply)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
        [ExecutionOrder(2)]
        public async Task Should_Force_Reply()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldForceReply);

            await BotClient.SendTextMessageAsync(
                chatId: _fixture.SupergroupChat,
                text: "Message with force_reply",
                replyMarkup: new ForceReplyMarkup()
            );
        }

        [Fact(DisplayName = FactTitles.ShouldSendMultiRowKeyboard)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
        [ExecutionOrder(3)]
        public async Task Should_Send_MultiRow_Keyboard()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendMultiRowKeyboard);

            ReplyKeyboardMarkup replyMarkup = new[] {
                new[] {    "Top-Left",   "Top" , "Top-Right"    },
                new[] {        "Left", "Center", "Right"        },
                new[] { "Bottom-Left", "Bottom", "Bottom-Right" },
            };

            await BotClient.SendTextMessageAsync(
                chatId: _fixture.SupergroupChat,
                text: "Message with 3x3 keyboard",
                replyMarkup: replyMarkup
            );
        }

        [Fact(DisplayName = FactTitles.ShouldRemoveReplyKeyboard)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
        [ExecutionOrder(4)]
        public async Task Should_Remove_Reply_Keyboard()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldRemoveReplyKeyboard);

            await BotClient.SendTextMessageAsync(
                chatId: _fixture.SupergroupChat,
                text: "Message to remove keyboard",
                replyMarkup: new ReplyKeyboardRemove()
            );
        }

        private static class FactTitles
        {
            public const string ShouldForceReply = "Should send a message with force reply markup";

            public const string ShouldSendMultiRowKeyboard = "Should send a message multi-row keyboard reply markup";

            public const string ShouldRemoveReplyKeyboard = "Should remove reply keyboard";
        }
    }
}

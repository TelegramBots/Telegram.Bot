using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Sending_Messages;

[Collection(Constants.TestCollections.SendCopyMessage)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class CopyMessageTests(TestsFixture testsFixture)
{
    ITelegramBotClient BotClient => _fixture.BotClient;

    readonly TestsFixture _fixture = testsFixture;

    [OrderedFact("Should copy text message")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.CopyMessage)]
    public async Task Should_Copy_Text_Message()
    {
        Message message = await BotClient.SendMessageAsync(
            new()
            {
                ChatId = _fixture.SupergroupChat.Id,
                Text = "hello",
            }
        );

        MessageId copyMessageId = await BotClient.CopyMessageAsync(
            new()
            {
                ChatId = _fixture.SupergroupChat.Id,
                FromChatId = _fixture.SupergroupChat.Id,
                MessageId = message.MessageId,
            }
        );

        Assert.NotEqual(0, copyMessageId.Id);
    }

    [OrderedFact("Should copy text messages")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.CopyMessages)]
    public async Task Should_Copy_Text_Messages()
    {
        Message message1 = await BotClient.SendMessageAsync(
            new()
            {
                ChatId = _fixture.SupergroupChat.Id,
                Text = "message one.",
            }
        );

        Message message2 = await BotClient.SendMessageAsync(
            new()
            {
                ChatId = _fixture.SupergroupChat.Id,
                Text = "message two",
            }
        );

        int[] messageIds = [message1.MessageId, message2.MessageId];

        MessageId[] copyMessageIds = await BotClient.CopyMessagesAsync(
            new()
            {
                ChatId = _fixture.SupergroupChat.Id,
                FromChatId = _fixture.SupergroupChat.Id,
                MessageIds = messageIds,
            }
        );

        Assert.Equal(2, copyMessageIds.Length);
    }
}

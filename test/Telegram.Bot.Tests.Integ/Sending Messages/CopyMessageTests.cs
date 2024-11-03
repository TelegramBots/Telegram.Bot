using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Sending_Messages;

[Collection(Constants.TestCollections.SendCopyMessage)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class CopyMessageTests(TestsFixture fixture) : TestClass(fixture)
{
    [OrderedFact("Should copy text message")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.CopyMessage)]
    public async Task Should_Copy_Text_Message()
    {
        Message message = await BotClient.SendMessage(
            chatId: Fixture.SupergroupChat.Id,
            text: "hello"
        );

        MessageId copyMessageId = await BotClient.CopyMessage(
            chatId: Fixture.SupergroupChat.Id,
            fromChatId: Fixture.SupergroupChat.Id,
            messageId: message.Id
        );

        Assert.NotEqual(0, copyMessageId.Id);
    }

    [OrderedFact("Should copy text messages")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.CopyMessages)]
    public async Task Should_Copy_Text_Messages()
    {
        Message message1 = await BotClient.SendMessage(
            chatId: Fixture.SupergroupChat.Id,
            text: "message one."
        );

        Message message2 = await BotClient.SendMessage(
            chatId: Fixture.SupergroupChat.Id,
            text: "message two"
        );

        int[] messageIds = [message1.Id, message2.Id];

        MessageId[] copyMessageIds = await BotClient.CopyMessages(
            chatId: Fixture.SupergroupChat.Id,
            fromChatId: Fixture.SupergroupChat.Id,
            messageIds: messageIds
        );

        Assert.Equal(2, copyMessageIds.Length);
    }
}

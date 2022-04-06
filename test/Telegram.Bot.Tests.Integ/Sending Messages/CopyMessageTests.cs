using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Sending_Messages;

[Collection(Constants.TestCollections.SendCopyMessage)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class CopyMessageTests
{
    ITelegramBotClient BotClient => _fixture.BotClient;

    readonly TestsFixture _fixture;

    public CopyMessageTests(TestsFixture testsFixture)
    {
        _fixture = testsFixture;
    }

    [OrderedFact("Should copy text message")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.CopyMessage)]
    public async Task Should_Copy_Text_Message()
    {
        Message message = await BotClient.SendTextMessageAsync(
            chatId: _fixture.SupergroupChat.Id,
            text: "hello"
        );

        MessageId copyMessageId = await BotClient.CopyMessageAsync(
            _fixture.SupergroupChat.Id,
            _fixture.SupergroupChat.Id,
            message.MessageId
        );

        Assert.NotEqual(0, copyMessageId.Id);
    }
}
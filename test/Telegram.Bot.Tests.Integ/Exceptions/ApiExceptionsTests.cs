using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Tests.Integ.Framework;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Exceptions;

[Collection(Constants.TestCollections.Exceptions)]
[Trait(Constants.CategoryTraitName, Constants.InteractiveCategoryValue)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class ApiExceptionsTests(TestsFixture fixture) : TestClass(fixture)
{
    [OrderedFact("Should throw ChatNotFoundException while trying to send message to a user who hasn't " +
                 "started a chat with bot but bot knows about him/her.")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
    public async Task Should_Throw_Exception_ChatNotFoundException()
    {
        //ToDo add exception. forward message from another bot. Forbidden: bot can't send messages to bots
        await Fixture.SendTestInstructionsAsync(
            "Forward a message to this chat from a user that never started a chat with this bot"
        );

        //await _fixture.UpdateReceiver.DiscardNewUpdatesAsync();

        //Update forwardedMessageUpdate = await _fixture.UpdateReceiver.GetUpdateAsync(
        //    predicate: u => u.Message?.ForwardOrigin is not null,
        //    updateTypes: [UpdateType.Message]
        //);

        //MessageOriginHiddenUser hiddenUser = (MessageOriginHiddenUser)forwardedMessageUpdate.Message!.ForwardOrigin;

        ApiRequestException e = await Assert.ThrowsAsync<ApiRequestException>(async () =>
            await BotClient.SendMessage(
                int.MaxValue,
                $"Error!"
            )
        );

        Assert.Equal(400, e.ErrorCode);
    }
}

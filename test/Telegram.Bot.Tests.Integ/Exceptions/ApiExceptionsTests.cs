using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Exceptions;

[Collection(Constants.TestCollections.Exceptions)]
[Trait(Constants.CategoryTraitName, Constants.InteractiveCategoryValue)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class ApiExceptionsTests
{
    ITelegramBotClient BotClient => _fixture.BotClient;

    readonly TestsFixture _fixture;

    public ApiExceptionsTests(TestsFixture fixture)
    {
        _fixture = fixture;
    }

    [OrderedFact("Should throw ChatNotInitiatedException while trying to send message to a user who hasn't " +
                 "started a chat with bot but bot knows about him/her.")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
    public async Task Should_Throw_Exception_ChatNotInitiatedException()
    {
        //ToDo add exception. forward message from another bot. Forbidden: bot can't send messages to bots
        await _fixture.SendTestInstructionsAsync(
            "Forward a message to this chat from a user that never started a chat with this bot"
        );

        await _fixture.UpdateReceiver.DiscardNewUpdatesAsync();

        Update forwardedMessageUpdate = await _fixture.UpdateReceiver.GetUpdateAsync(
            predicate: u => u.Message?.ForwardFrom is not null,
            updateTypes: new[] { UpdateType.Message }
        );

        User forwardFromUser = forwardedMessageUpdate.Message!.ForwardFrom!;

        ApiRequestException e = await Assert.ThrowsAsync<ApiRequestException>(async () =>
            await BotClient.SendTextMessageAsync(
                forwardFromUser.Id,
                $"Error! If you see this message, talk to @{forwardFromUser.Username}"
            )
        );

        Assert.Equal(403, e.ErrorCode);
    }
}
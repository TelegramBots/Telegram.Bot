using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Exceptions;

[Collection(Constants.TestCollections.Exceptions2)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class ApiExceptionsTests2(TestsFixture fixture) : TestClass(fixture)
{
    [OrderedFact("Should throw ChatNotFoundException while trying to send message to an invalid chat")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
    public async Task Should_Throw_Exception_ChatNotFoundException()
    {
        ApiRequestException e = await Assert.ThrowsAsync<ApiRequestException>(() =>
            BotClient.SendMessage(0, "test")
        );

        Assert.Equal(400, e.ErrorCode);
    }

    [OrderedFact("Should throw UserNotFoundException while trying to promote an invalid user id")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
    public async Task Should_Throw_Exception_UserNotFoundException()
    {
        ApiRequestException e = await Assert.ThrowsAsync<ApiRequestException>(() =>
            BotClient.PromoteChatMember(Fixture.SupergroupChat.Id, 123456)
        );

        Assert.Equal(400, e.ErrorCode);
    }

    [OrderedFact("Should throw ApiRequestException while asking for user's phone number " +
                 "in non-private chat via reply keyboard markup")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
    public async Task Should_Throw_Exception_ApiRequestException()
    {
        ReplyKeyboardMarkup replyMarkup = new(new[]
        {
            KeyboardButton.WithRequestContact("Share Contact"),
        });

        ApiRequestException exception = await Assert.ThrowsAsync<ApiRequestException>(() =>
            BotClient.SendMessage(
                chatId: Fixture.SupergroupChat.Id,
                text: "You should never see this message",
                replyMarkup: replyMarkup
            )
        );

        Assert.Equal(400, exception.ErrorCode);
    }

    [OrderedFact("Should throw MessageIsNotModifiedException while editing previously " +
                 "sent message with the same text")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
    public async Task Should_Throw_Exception_MessageIsNotModifiedException()
    {
        const string messageTextToModify = "Message text to modify";
        Message message = await BotClient.SendMessage(
            chatId: Fixture.SupergroupChat.Id,
            text: messageTextToModify
        );

        ApiRequestException e = await Assert.ThrowsAsync<ApiRequestException>(() =>
            BotClient.EditMessageText(
                chatId: Fixture.SupergroupChat.Id,
                messageId: message.Id,
                text: messageTextToModify
            )
        );

        Assert.Equal(400, e.ErrorCode);
    }
}

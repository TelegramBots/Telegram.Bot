using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Exceptions
{
    [Collection(Constants.TestCollections.Exceptions2)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class ApiExceptionsTests2
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public ApiExceptionsTests2(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        [OrderedFact("Should throw ChatNotFoundException while trying to send message to an invalid chat")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
        public async Task Should_Throw_Exception_ChatNotFoundException()
        {
            BadRequestException e = await Assert.ThrowsAnyAsync<BadRequestException>(() =>
                BotClient.SendTextMessageAsync(0, "test")
            );

            Assert.IsType<ChatNotFoundException>(e);
        }

        [OrderedFact("Should throw UserNotFoundException while trying to promote an invalid user id")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
        public async Task Should_Throw_Exception_UserNotFoundException()
        {
            BadRequestException e = await Assert.ThrowsAnyAsync<BadRequestException>(() =>
                BotClient.PromoteChatMemberAsync(_fixture.SupergroupChat.Id, 123456)
            );

            Assert.IsType<UserNotFoundException>(e);
        }

        [OrderedFact("Should throw ApiRequestException while asking for user's phone number " +
                     "in non-private chat via reply keyboard markup")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
        public async Task Should_Throw_Exception_ApiRequestException()
        {
            ReplyKeyboardMarkup replyMarkup = new ReplyKeyboardMarkup(new[]
            {
                KeyboardButton.WithRequestContact("Share Contact"),
            });

            ApiRequestException exception = await Assert.ThrowsAnyAsync<ApiRequestException>(() =>
                BotClient.SendTextMessageAsync(
                    chatId: _fixture.SupergroupChat.Id,
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
            Message message = await BotClient.SendTextMessageAsync(
                chatId: _fixture.SupergroupChat.Id,
                text: messageTextToModify
            );

            BadRequestException e = await Assert.ThrowsAnyAsync<BadRequestException>(() =>
                BotClient.EditMessageTextAsync(
                    chatId: _fixture.SupergroupChat.Id,
                    messageId: message.MessageId,
                    text: messageTextToModify
                )
            );

            Assert.IsType<MessageIsNotModifiedException>(e);
        }
    }
}

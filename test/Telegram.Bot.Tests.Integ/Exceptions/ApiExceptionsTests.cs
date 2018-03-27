using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Tests.Integ.Framework.Fixtures;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;
using static Telegram.Bot.Tests.Integ.Exceptions.ApiExceptionsTests;

namespace Telegram.Bot.Tests.Integ.Exceptions
{
    [Collection(Constants.TestCollections.Exceptions)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class ApiExceptionsTests : IClassFixture<ExceptionsFixture>
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;
        private readonly ExceptionsFixture _classFixture;

        public ApiExceptionsTests(TestsFixture fixture, ExceptionsFixture classFixture)
        {
            _fixture = fixture;
            _classFixture = classFixture;
        }

        [Fact(DisplayName = FactTitles.ShouldThrowChatNotFoundException)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
        [ExecutionOrder(1)]
        public async Task Should_Throw_Exception_ChatNotFoundException()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowChatNotFoundException);

            BadRequestException e = await Assert.ThrowsAnyAsync<BadRequestException>(() =>
                BotClient.SendTextMessageAsync(0, "test"));

            Assert.IsType<ChatNotFoundException>(e);
        }

        [Fact(DisplayName = FactTitles.ShouldThrowInvalidUserIdException)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
        [ExecutionOrder(2)]
        public async Task Should_Throw_Exception_InvalidUserIdException()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowInvalidUserIdException);

            BadRequestException e = await Assert.ThrowsAnyAsync<BadRequestException>(() =>
                BotClient.PromoteChatMemberAsync(_fixture.SupergroupChat.Id, 123456));

            Assert.IsType<InvalidUserIdException>(e);
        }

        [Fact(DisplayName = FactTitles.ShouldThrowExceptionChatNotInitiatedException)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
        [ExecutionOrder(3)]
        public async Task Should_Throw_Exception_ChatNotInitiatedException()
        {
            //ToDo add exception. forward message from another bot. Forbidden: bot can't send messages to bots
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowExceptionChatNotInitiatedException,
                "Forward a message to this chat from a user that never started a chat with this bot");

            Update forwardedMessageUpdate = (await _fixture.UpdateReceiver.GetUpdatesAsync(u =>
                    u.Message.IsForwarded, updateTypes: UpdateType.Message
            )).Single();
            await _fixture.UpdateReceiver.DiscardNewUpdatesAsync();

            ForbiddenException e = await Assert.ThrowsAnyAsync<ForbiddenException>(() =>
                BotClient.SendTextMessageAsync(forwardedMessageUpdate.Message.ForwardFrom.Id,
                    $"Error! If you see this message, talk to @{forwardedMessageUpdate.Message.From.Username}"));

            Assert.IsType<ChatNotInitiatedException>(e);
        }

        [Fact(DisplayName = FactTitles.ShouldThrowExceptionContactRequestException)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
        [ExecutionOrder(4)]
        public async Task Should_Throw_Exception_ContactRequestException()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowExceptionContactRequestException);

            ReplyKeyboardMarkup replyMarkup = new ReplyKeyboardMarkup(new[]
            {
                KeyboardButton.WithRequestContact("Share Contact"),
            });

            BadRequestException e = await Assert.ThrowsAnyAsync<BadRequestException>(() =>
                BotClient.SendTextMessageAsync(_fixture.SupergroupChat.Id, "You should never see this message",
                    replyMarkup: replyMarkup));

            Assert.IsType<ContactRequestException>(e);
        }

        [Fact(DisplayName = FactTitles.ShouldThrowExceptionMessageIsNotModifiedException)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
        [ExecutionOrder(5)]
        public async Task Should_Throw_Exception_MessageIsNotModifiedException()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowExceptionMessageIsNotModifiedException);

            const string MessageTextToModify = "Message text to modify";
            Message message = await BotClient.SendTextMessageAsync(
                _fixture.SupergroupChat.Id,
                MessageTextToModify);

            BadRequestException e = await Assert.ThrowsAnyAsync<BadRequestException>(() =>
                BotClient.EditMessageTextAsync(
                    _fixture.SupergroupChat.Id,
                    message.MessageId,
                    MessageTextToModify));

            Assert.IsType<MessageIsNotModifiedException>(e);
        }

        [Fact(DisplayName = FactTitles.ShouldThrowExceptionInvalidQueryIdException)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        [ExecutionOrder(6)]
        public async Task Should_Throw_Exception_QueryIdInvalidException()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowExceptionInvalidQueryIdException);

            Update queryUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            InlineQueryResultBase[] results =
            {
                new InlineQueryResultArticle(
                    id: "article:bot-api",
                    title: "Telegram Bot API",
                    inputMessageContent: new InputTextMessageContent("https://core.telegram.org/bots/api"))
                    {
                        Description = "The Bot API is an HTTP-based interface created for developers",
                    },
            };

            await Task.Delay(10_000);

            InvalidQueryIdException e = await Assert.ThrowsAnyAsync<InvalidQueryIdException>(() =>
                BotClient.AnswerInlineQueryAsync(
                    inlineQueryId: queryUpdate.InlineQuery.Id,
                    results: results,
                    cacheTime: 0));

            Assert.Equal("inline_query_id", e.Parameter);
        }

        [Fact(DisplayName = FactTitles.ShouldThrowExceptionMissingParameterException)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
        [ExecutionOrder(7)]
        public async Task Should_Throw_Exception_MissingParameterException()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowExceptionMissingParameterException);

            InvalidParameterException exception = await Assert.ThrowsAnyAsync<InvalidParameterException>(() =>
                BotClient.SendTextMessageAsync(
                    _fixture.SupergroupChat.Id,
                    string.Empty));

            Assert.IsType<MissingParameterException>(exception);
            Assert.Equal("message text", exception.Parameter);
        }

        [Fact(DisplayName = FactTitles.ShouldThrowExceptionWrongChatTypeException_OutsideSupergroups)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatStickerSet)]
        [ExecutionOrder(8)]
        public async Task Should_Throw_Exception_WrongChatTypeException_OutsideSupergroups()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowExceptionWrongChatTypeException_OutsideSupergroups);

            const string setName = "EvilMinds";

            ApiRequestException exception = await Assert.ThrowsAnyAsync<ApiRequestException>(() =>
                _fixture.BotClient.SetChatStickerSetAsync(_classFixture.PrivateChat.Id, setName)
            );

            Assert.IsType<WrongChatTypeException>(exception);
            Assert.Equal("method is available only for supergroups", exception.Message);
        }

        [Fact(DisplayName = FactTitles.ShouldThrowExceptionWrongChatTypeException_OutsideSupergroupsOrChannels)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.UnbanChatMember)]
        [ExecutionOrder(9)]
        public async Task Should_Throw_Exception_WrongChatTypeException_OutsideSupergroupsOrChannels()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowExceptionWrongChatTypeException_OutsideSupergroupsOrChannels);

            ApiRequestException exception = await Assert.ThrowsAnyAsync<ApiRequestException>(() =>
                _fixture.BotClient.UnbanChatMemberAsync(
                    _classFixture.PrivateChat.Id,
                    (int)_classFixture.PrivateChat.Id));

            Assert.IsType<WrongChatTypeException>(exception);
            Assert.Equal("method is available for supergroup and channel chats only", exception.Message);
        }

        private static class FactTitles
        {
            public const string ShouldThrowChatNotFoundException =
                "Should throw ChatNotFoundException while trying to send message to an invalid chat";

            public const string ShouldThrowInvalidUserIdException =
                "Should throw InvalidUserIdException while trying to promote an invalid user id";

            public const string ShouldThrowExceptionChatNotInitiatedException =
                "Should throw ChatNotInitiatedException while trying to send message to a user who hasn't " +
                "started a chat with bot but bot knows about him/her.";

            public const string ShouldThrowExceptionContactRequestException =
                "Should throw ContactRequestException while asking for user's phone number in non-private " +
                "chat via reply keyboard markup";

            public const string ShouldThrowExceptionMessageIsNotModifiedException =
               "Should throw MessageIsNotModifiedException while editing previously sent message " +
                "with the same text";

            public const string ShouldThrowExceptionInvalidQueryIdException =
                   "Should throw InvalidQueryIdException when AnswerInlineQueryAsync called with" +
                    " 10 second delay";

            public const string ShouldThrowExceptionMissingParameterException =
                   "Should throw MissingParameterException when message text is empty";

            public const string ShouldThrowExceptionWrongChatTypeException_OutsideSupergroups =
                   "Should throw WrongChatTypeException when method is not called for supergroup";

            public const string ShouldThrowExceptionWrongChatTypeException_OutsideSupergroupsOrChannels =
                   "Should throw WrongChatTypeException when method is not called for supergroup or channel";
        }

        public class ExceptionsFixture : PrivateChatFixture
        {
            public ExceptionsFixture(TestsFixture testsFixture)
                : base(testsFixture, Constants.TestCollections.Exceptions)
            {
            }
        }
    }
}
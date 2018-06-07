using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Update_Messages
{
    [Collection(Constants.TestCollections.EditMessage2)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class EditMessageContentTests2
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public EditMessageContentTests2(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldEditMessageText)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditMessageText)]
        public async Task Should_Edit_Message_Text()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldEditMessageText);

            const string originalMessagePrefix = "original\n";
            (MessageEntityType Type, string Value)[] entityValueMappings = {
                (MessageEntityType.Bold, "<b>bold</b>"),
                (MessageEntityType.Italic, "<i>italic</i>"),
            };
            string messageText = originalMessagePrefix +
                    string.Join("\n", entityValueMappings.Select(tuple => tuple.Value));

            Message originalMessage = await BotClient.SendTextMessageAsync(
                chatId: _fixture.SupergroupChat.Id,
                text: messageText,
                parseMode: ParseMode.Html
            );

            DateTime timeBeforeEdition = DateTime.UtcNow;
            await Task.Delay(1_000);

            const string modifiedMessagePrefix = "modified\n";
            messageText = modifiedMessagePrefix +
                    string.Join("\n", entityValueMappings.Select(tuple => tuple.Value));
            Message editedMessage = await BotClient.EditMessageTextAsync(
                chatId: originalMessage.Chat.Id,
                messageId: originalMessage.MessageId,
                text: messageText,
                parseMode: ParseMode.Html
            );

            Assert.StartsWith(modifiedMessagePrefix, editedMessage.Text);
            Assert.Equal(originalMessage.MessageId, editedMessage.MessageId);
            Assert.True(timeBeforeEdition < editedMessage.EditDate);

            Assert.Equal(
                entityValueMappings.Select(tuple => tuple.Type),
                editedMessage.Entities.Select(e => e.Type)
            );
        }

        [OrderedFact(DisplayName = FactTitles.ShouldEditMessageMarkup)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditMessageReplyMarkup)]
        public async Task Should_Edit_Message_Markup()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldEditMessageMarkup);

            Message message = await BotClient.SendTextMessageAsync(
                chatId: _fixture.SupergroupChat.Id,
                text: "Inline keyboard will be updated shortly",
                replyMarkup: (InlineKeyboardMarkup)"Original markup"
            );

            DateTime timeBeforeEdition = DateTime.UtcNow;
            await Task.Delay(1_000);

            Message editedMessage = await BotClient.EditMessageReplyMarkupAsync(
                chatId: message.Chat.Id,
                messageId: message.MessageId,
                replyMarkup: "Edited 👍"
            );

            Assert.Equal(message.MessageId, editedMessage.MessageId);
            Assert.Equal(message.Text, editedMessage.Text);
            Assert.True(timeBeforeEdition < editedMessage.EditDate);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldEditMessageCaption)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendPhoto)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditMessageCaption)]
        public async Task Should_Edit_Message_Caption()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldEditMessageCaption);

            Message originalMessage;
            using (Stream stream = System.IO.File.OpenRead(Constants.FileNames.Photos.Bot))
            {
                originalMessage = await BotClient.SendPhotoAsync(
                    chatId: _fixture.SupergroupChat.Id,
                    photo: stream,
                    caption: "Message caption will be updated shortly"
                );
            }

            DateTime timeBeforeEdition = DateTime.UtcNow;
            await Task.Delay(1_000);

            const string captionPrefix = "Modified caption";
            (MessageEntityType Type, string Value) captionEntity = (MessageEntityType.Italic, "_with Markdown_");
            string caption = $"{captionPrefix} {captionEntity.Value}";

            Message editedMessage = await BotClient.EditMessageCaptionAsync(
                chatId: originalMessage.Chat.Id,
                messageId: originalMessage.MessageId,
                caption: caption,
                parseMode: ParseMode.Markdown
            );

            Assert.Equal(originalMessage.MessageId, editedMessage.MessageId);
            Assert.True(timeBeforeEdition < editedMessage.EditDate);
            Assert.StartsWith(captionPrefix, editedMessage.Caption);

            Assert.Equal(editedMessage.CaptionEntities.Single().Type, captionEntity.Type);
        }

        private static class FactTitles
        {
            public const string ShouldEditMessageText = "Should edit a message's text";

            public const string ShouldEditMessageMarkup = "Should edit a message's markup";

            public const string ShouldEditMessageCaption = "Should edit a message's caption";
        }
    }
}

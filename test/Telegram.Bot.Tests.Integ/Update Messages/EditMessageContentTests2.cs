using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Update_Messages;

[Collection(Constants.TestCollections.EditMessage2)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class EditMessageContentTests2(TestsFixture fixture) : TestClass(fixture)
{
    [OrderedFact("Should edit a message's text")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditMessageText)]
    public async Task Should_Edit_Message_Text()
    {
        const string originalMessagePrefix = "original\n";
        (MessageEntityType Type, string Value)[] entityValueMappings =
        [
            (MessageEntityType.Bold, "<b>bold</b>"),
            (MessageEntityType.Italic, "<i>italic</i>")
        ];
        string messageText = $"{originalMessagePrefix}{string.Join("\n", entityValueMappings.Select(tuple => tuple.Value))}";

        Message originalMessage = await BotClient.SendMessage(
            chatId: Fixture.SupergroupChat.Id,
            text: messageText,
            parseMode: ParseMode.Html
        );

        await Task.Delay(1_000);

        const string modifiedMessagePrefix = "modified\n";
        messageText = $"{modifiedMessagePrefix}{string.Join("\n", entityValueMappings.Select(tuple => tuple.Value))}";

        Message editedMessage = await BotClient.EditMessageText(
            chatId: originalMessage.Chat.Id,
            messageId: originalMessage.Id,
            text: messageText,
            parseMode: ParseMode.Html
        );

        Assert.NotNull(editedMessage.Text);
        Assert.StartsWith(modifiedMessagePrefix, editedMessage.Text);
        Assert.Equal(originalMessage.Id, editedMessage.Id);
        Assert.Equal(originalMessage.Date, editedMessage.Date);
        Assert.True(originalMessage.Date < editedMessage.EditDate);

        Assert.NotNull(editedMessage.Entities);
        Assert.Equal(
            entityValueMappings.Select(tuple => tuple.Type),
            editedMessage.Entities.Select(e => e.Type)
        );
    }

    [OrderedFact("Should edit a message's markup")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditMessageReplyMarkup)]
    public async Task Should_Edit_Message_Markup()
    {
        Message message = await BotClient.SendMessage(
            chatId: Fixture.SupergroupChat.Id,
            text: "Inline keyboard will be updated shortly",
            replyMarkup: (InlineKeyboardMarkup)"Original markup"
        );

        await Task.Delay(1_000);

        Message editedMessage = await BotClient.EditMessageReplyMarkup(
            chatId: message.Chat.Id,
            messageId: message.Id,
            replyMarkup: "Edited 👍"
        );

        Assert.Equal(message.Id, editedMessage.Id);
        Assert.Equal(message.Text, editedMessage.Text);
        Assert.True(message.Date < editedMessage.EditDate);
        Assert.Equal(message.Date, editedMessage.Date);
    }

    [OrderedFact("Should edit a message's caption")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendPhoto)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditMessageCaption)]
    public async Task Should_Edit_Message_Caption()
    {
        Message originalMessage;
        await using (Stream stream = File.OpenRead(Constants.PathToFile.Photos.Bot))
        {
            originalMessage = await BotClient.WithStreams(stream).SendPhoto(
                chatId: Fixture.SupergroupChat.Id,
                photo: InputFile.FromStream(stream),
                caption: "Message caption will be updated shortly"
            );
        }

        await Task.Delay(1_000);

        const string captionPrefix = "Modified caption";
        (MessageEntityType Type, string Value) = (MessageEntityType.Italic, "_with Markdown_");
        string caption = $"{captionPrefix} {Value}";

        Message editedMessage = await BotClient.EditMessageCaption(
            chatId: originalMessage.Chat.Id,
            messageId: originalMessage.Id,
            caption: caption,
            parseMode: ParseMode.Markdown
        );

        Assert.Equal(originalMessage.Id, editedMessage.Id);
        Assert.True(originalMessage.Date < editedMessage.EditDate);
        Assert.Equal(originalMessage.Date, editedMessage.Date);
        Assert.NotNull(editedMessage.Caption);
        Assert.StartsWith(captionPrefix, editedMessage.Caption);

        Assert.NotNull(editedMessage.CaptionEntities);
        Assert.Equal(editedMessage.CaptionEntities.Single().Type, Type);
    }
}

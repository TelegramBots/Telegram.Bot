using System;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Tests.Integ.Framework.Fixtures;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Telegram.Bot.Tests.Integ.ReplyMarkup;

[Collection(Constants.TestCollections.PrivateChatReplyMarkup)]
[Trait(Constants.CategoryTraitName, Constants.InteractiveCategoryValue)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class PrivateChatReplyMarkupTests(TestsFixture fixture, PrivateChatReplyMarkupTests.ClassFixture classFixture)
    : TestClass(fixture), IClassFixture<PrivateChatReplyMarkupTests.ClassFixture>
{
    [OrderedFact("Should request contact with keyboard reply markup")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
    public async Task Should_Request_Contact()
    {
        KeyboardButton[] keyboard = [KeyboardButton.WithRequestContact("Share Contact"),];

        ReplyKeyboardMarkup replyKeyboardMarkup = new (keyboardRow: keyboard)
        {
            ResizeKeyboard = true,
            OneTimeKeyboard = true,
        };

        await BotClient.SendMessage(
            chatId: classFixture.PrivateChat,
            text: "Share your contact info using the keyboard reply markup provided.",
            replyMarkup: replyKeyboardMarkup
        );

        Message contactMessage = await GetMessageFromChat(MessageType.Contact);

        Assert.NotNull(contactMessage.Contact);
        Assert.NotEmpty(contactMessage.Contact.FirstName);
        Assert.NotEmpty(contactMessage.Contact.PhoneNumber);
        Assert.Equal(classFixture.PrivateChat.Id, contactMessage.Contact.UserId);

        await BotClient.SendMessage(
            chatId: classFixture.PrivateChat,
            text: $"Got it: {contactMessage.Contact.FirstName}. Removing reply keyboard markup...",
            replyMarkup: new ReplyKeyboardRemove()
        );
    }

    [OrderedFact("Should request location with keyboard reply markup")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
    public async Task Should_Request_Location()
    {
        KeyboardButton[] keyboard = [KeyboardButton.WithRequestLocation("Share Location")];
        ReplyKeyboardMarkup replyKeyboardMarkup = new(keyboardRow: keyboard);

        await BotClient.SendMessage(
            chatId: classFixture.PrivateChat,
            text: "Share your location using the keyboard reply markup",
            replyMarkup: replyKeyboardMarkup
        );

        Message locationMessage = await GetMessageFromChat(MessageType.Location);

        Assert.NotNull(locationMessage.Location);

        await BotClient.SendMessage(
            chatId: classFixture.PrivateChat,
            text: "Got it. Removing reply keyboard markup...",
            replyMarkup: new ReplyKeyboardRemove()
        );
    }

    [OrderedFact("Should request users with keyboard reply markup")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
    public async Task Should_Request_Users()
    {
        KeyboardButton[] keyboard =
        [
            KeyboardButton.WithRequestUsers(text: "Share Users", requestId: 1)
        ];
        ReplyKeyboardMarkup replyKeyboardMarkup = new(keyboardRow: keyboard);

        await BotClient.SendMessage(
            chatId: classFixture.PrivateChat,
            text: "Share users using the keyboard reply markup",
            replyMarkup: replyKeyboardMarkup
        );

        Message usersMessage = await GetMessageFromChat(MessageType.UsersShared);

        Assert.NotNull(usersMessage.UsersShared);

        await BotClient.SendMessage(
            chatId: classFixture.PrivateChat,
            text: $"Got it: UserId {usersMessage.UsersShared.Users[0].UserId}. Removing reply keyboard markup...",
            replyMarkup: new ReplyKeyboardRemove()
        );
    }

    [OrderedFact("Should request chat with keyboard reply markup")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
    public async Task Should_Request_Chat()
    {
        KeyboardButton[] keyboard =
        [
            KeyboardButton.WithRequestChat(
                text: "Share Chat",
                requestId: 1,
                chatIsChannel: false)
        ];
        ReplyKeyboardMarkup replyKeyboardMarkup = new(keyboardRow: keyboard);

        await BotClient.SendMessage(
            chatId: classFixture.PrivateChat,
            text: "Share chat using the keyboard reply markup",
            replyMarkup: replyKeyboardMarkup
        );

        Message chatMessage = await GetMessageFromChat(MessageType.ChatShared);

        Assert.NotNull(chatMessage.ChatShared);

        await BotClient.SendMessage(
            chatId: classFixture.PrivateChat,
            text: $"Got it: ChatId {chatMessage.ChatShared.ChatId}. Removing reply keyboard markup...",
            replyMarkup: new ReplyKeyboardRemove()
        );
    }

    [OrderedFact("Should send inline 'copy text' button and receive it back")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
    public async Task Should_Obtain_Copied_Text()
    {
        var copiedText = "random" + Random.Shared.Next().ToString();
        var button = InlineKeyboardButton.WithCopyText("Click here to copy", copiedText);
        await BotClient.SendMessage(
            chatId: classFixture.PrivateChat,
            text: "Click button below to copy text, then paste that text in a message",
            replyMarkup: new InlineKeyboardMarkup(button)
        );

        Message chatMessage = await GetMessageFromChat(MessageType.Text);
        Assert.Equal(chatMessage.Text, copiedText);
    }

    async Task<Message> GetMessageFromChat(MessageType messageType) =>
        (await Fixture.UpdateReceiver.GetUpdateAsync(
            predicate: u => u.Message!.Type == messageType &&
                            u.Message.Chat.Id == classFixture.PrivateChat.Id,
            updateTypes: UpdateType.Message
        )).Message;

    public class ClassFixture(TestsFixture testsFixture)
        : PrivateChatFixture(testsFixture, Constants.TestCollections.ReplyMarkup);
}

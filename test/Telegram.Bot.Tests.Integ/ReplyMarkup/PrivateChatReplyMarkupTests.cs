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
public class PrivateChatReplyMarkupTests(TestsFixture testsFixture, PrivateChatReplyMarkupTests.Fixture fixture) : IClassFixture<PrivateChatReplyMarkupTests.Fixture>
{
    ITelegramBotClient BotClient => testsFixture.BotClient;

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

        await BotClient.SendMessageAsync(
            new()
            {
                ChatId = fixture.PrivateChat,
                Text = "Share your contact info using the keyboard reply markup provided.",
                ReplyMarkup = replyKeyboardMarkup,
            }
        );

        Message contactMessage = await GetMessageFromChat(MessageType.Contact);

        Assert.NotNull(contactMessage.Contact);
        Assert.NotEmpty(contactMessage.Contact.FirstName);
        Assert.NotEmpty(contactMessage.Contact.PhoneNumber);
        Assert.Equal(fixture.PrivateChat.Id, contactMessage.Contact.UserId);

        await BotClient.SendMessageAsync(
            new()
            {
                ChatId = fixture.PrivateChat,
                Text = "Got it. Removing reply keyboard markup...",
                ReplyMarkup = new ReplyKeyboardRemove(),
            }
        );
    }

    [OrderedFact("Should request location with keyboard reply markup")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
    public async Task Should_Request_Location()
    {
        KeyboardButton[] keyboard = [KeyboardButton.WithRequestLocation("Share Location")];
        ReplyKeyboardMarkup replyKeyboardMarkup = new(keyboardRow: keyboard);

        await BotClient.SendMessageAsync(
            new()
            {
                ChatId = fixture.PrivateChat,
                Text = "Share your location using the keyboard reply markup",
                ReplyMarkup = replyKeyboardMarkup,
            }
        );

        Message locationMessage = await GetMessageFromChat(MessageType.Location);

        Assert.NotNull(locationMessage.Location);

        await BotClient.SendMessageAsync(
            new()
            {
                ChatId = fixture.PrivateChat,
                Text = "Got it. Removing reply keyboard markup...",
                ReplyMarkup = new ReplyKeyboardRemove(),
            }
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

        await BotClient.SendMessageAsync(
            new()
            {
                ChatId = fixture.PrivateChat,
                Text = "Share users using the keyboard reply markup",
                ReplyMarkup = replyKeyboardMarkup,
            }
        );

        Message usersMessage = await GetMessageFromChat(MessageType.UsersShared);

        Assert.NotNull(usersMessage.UsersShared);

        await BotClient.SendMessageAsync(
            new()
            {
                ChatId = fixture.PrivateChat,
                Text = "Got it. Removing reply keyboard markup...",
                ReplyMarkup = new ReplyKeyboardRemove(),
            }
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

        await BotClient.SendMessageAsync(
            new()
            {
                ChatId = fixture.PrivateChat,
                Text = "Share chat using the keyboard reply markup",
                ReplyMarkup = replyKeyboardMarkup,
            }
        );

        Message chatMessage = await GetMessageFromChat(MessageType.ChatShared);

        Assert.NotNull(chatMessage.ChatShared);

        await BotClient.SendMessageAsync(
            new(){
                ChatId = fixture.PrivateChat,
                Text = "Got it. Removing reply keyboard markup...",
                ReplyMarkup = new ReplyKeyboardRemove(),
            }
        );
    }

    async Task<Message> GetMessageFromChat(MessageType messageType) =>
        (await testsFixture.UpdateReceiver.GetUpdateAsync(
            predicate: u => u.Message!.Type == messageType &&
                            u.Message.Chat.Id == fixture.PrivateChat.Id,
            updateTypes: UpdateType.Message
        )).Message;

    public class Fixture(TestsFixture testsFixture)
        : PrivateChatFixture(testsFixture, Constants.TestCollections.ReplyMarkup);
}

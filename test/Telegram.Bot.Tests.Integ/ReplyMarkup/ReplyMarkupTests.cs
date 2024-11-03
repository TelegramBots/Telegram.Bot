using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Telegram.Bot.Tests.Integ.ReplyMarkup;

[Collection(Constants.TestCollections.ReplyMarkup)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class ReplyMarkupTests(TestsFixture fixture) : TestClass(fixture)
{
    [OrderedFact("Should send a message with force reply markup")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
    public async Task Should_Force_Reply()
    {
        await BotClient.SendMessage(
            chatId: Fixture.SupergroupChat,
            text: "Message with force_reply",
            replyMarkup: new ForceReplyMarkup()
        );
    }

    [OrderedFact("Should send a message multi-row keyboard reply markup")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
    public async Task Should_Send_MultiRow_Keyboard()
    {
        KeyboardButton[][] keyboard =
        [
            ["Top-Left",     "Top"  , "Top-Right"],
            ["Left",        "Center", "Right"],
            ["Bottom-Left", "Bottom", "Bottom-Right"],
        ];

        ReplyKeyboardMarkup replyMarkup = new(keyboard: keyboard)
        {
            ResizeKeyboard = true,
        };

        await BotClient.SendMessage(
            chatId: Fixture.SupergroupChat,
            text: "Message with 3x3 keyboard",
            replyMarkup: replyMarkup
        );
    }

    [OrderedFact("Should send a message multi-row keyboard reply with Add* methods")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
    public async Task Should_Send_MultiRow_Keyboard_WithAdd()
    {
        var replyMarkup = new ReplyKeyboardMarkup(true);
        replyMarkup.AddButton("Top-Left");
        replyMarkup.AddButtons("Top", "Top-Right");
        replyMarkup.AddNewRow("Left", "Center", "Right");
        replyMarkup.AddNewRow().AddButtons(["Bottom-Left", "Bottom", "Bottom-Right"]);

        await BotClient.SendMessage(
            chatId: Fixture.SupergroupChat,
            text: "Message with 3x3 keyboard",
            replyMarkup: replyMarkup
        );
    }

    [OrderedFact("Should remove reply keyboard")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
    public async Task Should_Remove_Reply_Keyboard()
    {
        await BotClient.SendMessage(
            chatId: Fixture.SupergroupChat,
            text: "Message to remove keyboard",
            replyMarkup: new ReplyKeyboardRemove()
        );
    }

    [OrderedFact("Should send a message with multiple inline keyboard buttons markup")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
    public async Task Should_Send_Inline_Keyboard()
    {
        var replyMarkup = new InlineKeyboardMarkup()
            .AddButton(InlineKeyboardButton.WithUrl(
                    text: "Link to Repository",
                    url: "https://github.com/TelegramBots/Telegram.Bot"))
            .AddNewRow().AddButton("callback_data1").AddButton("callback_data: data2", "data2")
            .AddNewRow(InlineKeyboardButton.WithSwitchInlineQuery(text: "switch_inline_query"))
            .AddNewRow().AddButton(InlineKeyboardButton.WithSwitchInlineQueryCurrentChat(text: "switch_inline_query_current_chat"));

        Message sentMessage = await BotClient.SendMessage(
            chatId: Fixture.SupergroupChat,
            text: "Message with inline keyboard markup",
            replyMarkup: replyMarkup
        );

        Asserts.JsonEquals(replyMarkup, sentMessage.ReplyMarkup);
    }
}

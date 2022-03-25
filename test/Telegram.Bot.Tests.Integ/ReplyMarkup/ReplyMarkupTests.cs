using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Telegram.Bot.Tests.Integ.ReplyMarkup;

[Collection(Constants.TestCollections.ReplyMarkup)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class ReplyMarkupTests
{
    ITelegramBotClient BotClient => _fixture.BotClient;

    readonly TestsFixture _fixture;

    public ReplyMarkupTests(TestsFixture testsFixture)
    {
        _fixture = testsFixture;
    }

    [OrderedFact("Should send a message with force reply markup")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
    public async Task Should_Force_Reply()
    {
        await BotClient.SendTextMessageAsync(
            chatId: _fixture.SupergroupChat,
            text: "Message with force_reply",
            replyMarkup: new ForceReplyMarkup()
        );
    }

    [OrderedFact("Should send a message multi-row keyboard reply markup")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
    public async Task Should_Send_MultiRow_Keyboard()
    {
        ReplyKeyboardMarkup replyMarkup = new[] {
            new[] {    "Top-Left",   "Top" , "Top-Right"    },
            new[] {        "Left", "Center", "Right"        },
            new[] { "Bottom-Left", "Bottom", "Bottom-Right" },
        };

        await BotClient.SendTextMessageAsync(
            chatId: _fixture.SupergroupChat,
            text: "Message with 3x3 keyboard",
            replyMarkup: replyMarkup
        );
    }

    [OrderedFact("Should remove reply keyboard")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
    public async Task Should_Remove_Reply_Keyboard()
    {
        await BotClient.SendTextMessageAsync(
            chatId: _fixture.SupergroupChat,
            text: "Message to remove keyboard",
            replyMarkup: new ReplyKeyboardRemove()
        );
    }

    [OrderedFact("Should send a message with multiple inline keyboard buttons markup")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
    public async Task Should_Send_Inline_Keyboard()
    {
        Message sentMessage = await BotClient.SendTextMessageAsync(
            chatId: _fixture.SupergroupChat,
            text: "Message with inline keyboard markup",
            replyMarkup: new InlineKeyboardMarkup(new[]
            {
                new []
                {
                    InlineKeyboardButton.WithUrl(
                        "Link to Repository",
                        "https://github.com/TelegramBots/Telegram.Bot"
                    ),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData("callback_data1"),
                    InlineKeyboardButton.WithCallbackData("callback_data2", "data"),
                },
                new [] { InlineKeyboardButton.WithSwitchInlineQuery("switch_inline_query"), },
                new [] { InlineKeyboardButton.WithSwitchInlineQueryCurrentChat("switch_inline_query_current_chat"), },
            })
        );

        Assert.True(
            JToken.DeepEquals(
                JToken.FromObject(sentMessage.ReplyMarkup),
                JToken.FromObject(
                    new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithUrl(
                                    "Link to Repository",
                                    "https://github.com/TelegramBots/Telegram.Bot"
                                ),
                            },
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("callback_data1"),
                                InlineKeyboardButton.WithCallbackData("callback_data2", "data"),
                            },
                            new[]
                            {
                                InlineKeyboardButton.WithSwitchInlineQuery("switch_inline_query"),
                            },
                            new[]
                            {
                                InlineKeyboardButton.WithSwitchInlineQueryCurrentChat("switch_inline_query_current_chat"),
                            },
                        }
                    )
                )
            )
        );
    }
}
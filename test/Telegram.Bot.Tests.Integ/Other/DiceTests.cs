using System.Threading.Tasks;
using Telegram.Bot.Requests;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Other;

[Collection(Constants.TestCollections.Dice)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class DiceTests(TestsFixture testsFixture)
{
    [OrderedFact("Should send a die")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendDice)]
    public async Task Should_Send_A_Die()
    {
        Message message = await testsFixture.BotClient.SendDiceAsync(
            new SendDiceRequest
            {
                ChatId = testsFixture.SupergroupChat
            }
        );

        Assert.Equal(MessageType.Dice, message.Type);
        Assert.NotNull(message.Dice);
        Assert.Equal("🎲", message.Dice.Emoji);
        Assert.InRange(message.Dice.Value, 1, 6);
    }

    [OrderedFact("Should send a dart")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendDice)]
    public async Task Should_Send_A_Dart()
    {
        Message message = await testsFixture.BotClient.SendDiceAsync(
            new SendDiceRequest
            {
                ChatId = testsFixture.SupergroupChat,
                Emoji = Emoji.Darts,
            }
        );

        Assert.Equal(MessageType.Dice, message.Type);
        Assert.NotNull(message.Dice);
        Assert.Equal("🎯", message.Dice.Emoji);
        Assert.InRange(message.Dice.Value, 1, 6);
    }

    [OrderedFact("Should send a basketball")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendDice)]
    public async Task Should_Send_A_Basketball()
    {
        Message message = await testsFixture.BotClient.SendDiceAsync(
            new SendDiceRequest
            {
                ChatId = testsFixture.SupergroupChat,
                Emoji = Emoji.Basketball,
            }
        );

        Assert.Equal(MessageType.Dice, message.Type);
        Assert.NotNull(message.Dice);
        Assert.Equal("🏀", message.Dice.Emoji);
        Assert.InRange(message.Dice.Value, 1, 5);
    }

    [OrderedFact("Should send a football")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendDice)]
    public async Task Should_Send_A_Football()
    {
        Message message = await testsFixture.BotClient.SendDiceAsync(
            new SendDiceRequest
            {
                ChatId = testsFixture.SupergroupChat,
                Emoji = Emoji.Football,
            }
        );

        Assert.Equal(MessageType.Dice, message.Type);
        Assert.NotNull(message.Dice);
        Assert.Equal("⚽", message.Dice.Emoji);
        Assert.InRange(message.Dice.Value, 1, 5);
    }
    [OrderedFact("Should send a SlotMachine")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendDice)]
    public async Task Should_Send_A_SlotMachine()
    {
        Message message = await testsFixture.BotClient.SendDiceAsync(
            new SendDiceRequest
            {
                ChatId = testsFixture.SupergroupChat,
                Emoji = Emoji.SlotMachine,
            }
        );

        Assert.Equal(MessageType.Dice, message.Type);
        Assert.NotNull(message.Dice);
        Assert.Equal("🎰", message.Dice.Emoji);
        Assert.InRange(message.Dice.Value, 1, 64);
    }

    [OrderedFact("Should send a Bowling")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendDice)]
    public async Task Should_Send_A_Bowling()
    {
        Message message = await testsFixture.BotClient.SendDiceAsync(
            new SendDiceRequest
            {
                ChatId = testsFixture.SupergroupChat,
                Emoji = Emoji.Bowling
            }
        );

        Assert.Equal(MessageType.Dice, message.Type);
        Assert.NotNull(message.Dice);
        Assert.Equal("🎳", message.Dice.Emoji);
        Assert.InRange(message.Dice.Value, 1, 6);
    }
}

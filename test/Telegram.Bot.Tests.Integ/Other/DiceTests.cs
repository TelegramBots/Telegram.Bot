using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Other
{
    [Collection(Constants.TestCollections.Dice)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class DiceTests
    {
        private readonly TestsFixture _testsFixture;

        public DiceTests(TestsFixture testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [OrderedFact("Should send a die")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendDice)]
        public async Task Should_Send_A_Die()
        {
            Message message = await _testsFixture.BotClient.SendDiceAsync(_testsFixture.SupergroupChat);

            Assert.Equal(MessageType.Dice, message.Type);
            Assert.NotNull(message.Dice);
            Assert.InRange(message.Dice.Value, 1, 6);
        }
    }
}

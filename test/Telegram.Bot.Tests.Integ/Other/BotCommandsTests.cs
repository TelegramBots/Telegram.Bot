using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Other
{
    [Collection(Constants.TestCollections.BotCommands)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class BotCommandsTests : IClassFixture<BotCommandsFixture>
    {
        private readonly BotCommandsFixture _fixture;

        public BotCommandsTests(BotCommandsFixture fixture)
        {
            _fixture = fixture;
        }

        [OrderedFact("Should set a new bot command list")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetMyCommands)]
        public async Task Should_Set_New_Bot_Command_List()
        {
            _fixture.NewBotCommands = new[]
            {
                new BotCommand
                {
                    Command = "start",
                    Description = "Start command"
                },
                new BotCommand
                {
                    Command = "help",
                    Description = "Help command"
                },
            };

            await _fixture.BotClient.SetMyCommandsAsync(_fixture.NewBotCommands);
        }

        [OrderedFact("Should get previously set bot command list")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetMyCommands)]
        public async Task Should_Get_Set_Bot_Commands()
        {
            BotCommand[] currentCommands = await _fixture.BotClient.GetMyCommandsAsync();

            Assert.Equal(2, currentCommands.Length);
            Asserts.JsonEquals(_fixture.NewBotCommands, currentCommands);
        }
    }
}

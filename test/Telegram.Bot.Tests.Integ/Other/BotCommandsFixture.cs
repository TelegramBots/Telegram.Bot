using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Tests.Integ.Framework.Fixtures;
using Telegram.Bot.Types;

namespace Telegram.Bot.Tests.Integ.Other
{
    public class BotCommandsFixture : AsyncLifetimeFixture
    {
        private readonly TestsFixture _testsFixture;
        private BotCommand[] _originalCommands;

        public ITelegramBotClient BotClient => _testsFixture.BotClient;
        public BotCommand[] NewBotCommands { get; set; }

        public BotCommandsFixture(TestsFixture testsFixture)
        {
            _testsFixture = testsFixture;

            AddLifetime(
                initialize: async () => _originalCommands = await _testsFixture.BotClient.GetMyCommandsAsync(),
                dispose: async () =>
                {
                    // restore original bot commands
                    if (_originalCommands?.Length != 0)
                    {
                        await _testsFixture.BotClient.SetMyCommandsAsync(_originalCommands);
                    }
                }
            );
        }
    }
}

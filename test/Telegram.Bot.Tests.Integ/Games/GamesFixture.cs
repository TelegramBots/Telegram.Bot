using System;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Tests.Integ.Framework;

namespace Telegram.Bot.Tests.Integ.Games
{
    /// <summary>
    /// This fixture is for Games tests and it ensures test bot has a game set up before running the test methods.
    /// </summary>
    public class GamesFixture
    {
        public string GameShortName { get; }

        public GamesFixture(TestsFixture fixture)
        {
            GameShortName = "game1";

            try
            {
                fixture.BotClient.SendGameAsync(fixture.SupergroupChat.Id, GameShortName).GetAwaiter().GetResult();
            }
            catch (InvalidGameShortNameException e)
            {
                throw new ArgumentException(
                    $@"Bot doesn't have game: ""{GameShortName}"". Make sure you set up a game with @BotFather.",
                    e.Parameter, e
                );
            }
        }
    }
}
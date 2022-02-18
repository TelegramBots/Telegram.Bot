using System;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Tests.Integ.Framework.Fixtures;
using Telegram.Bot.Types;

#nullable disable

namespace Telegram.Bot.Tests.Integ.Games;

/// <summary>
/// This fixture is for Games tests and it ensures test bot has a game set up before running the test methods.
/// </summary>
public class GamesFixture : AsyncLifetimeFixture
{
    public string GameShortName { get; }

    public Message GameMessage { set; get; }

    public string InlineGameMessageId { set; get; }

    public GameHighScore[] HighScores { set; get; }

    /// <summary>
    /// A chat admin to set the game scores for.
    /// </summary>
    public User Player { get; private set; }

    public GamesFixture(TestsFixture fixture)
    {
        GameShortName = "game1";

        AddLifetime(
            initialize: async () =>
            {
                try
                {
                    await fixture.BotClient.SendGameAsync(fixture.SupergroupChat.Id, GameShortName);
                }
                catch (ApiRequestException e)
                {
                    throw new ArgumentException(
                        $@"Bot doesn't have game: ""{GameShortName}"". Make sure you set up a game with @BotFather.",
                        e
                    );
                }

                Player = await GetPlayerIdFromChatAdmins(fixture, fixture.SupergroupChat.Id);
            }
        );
    }

    static async Task<User> GetPlayerIdFromChatAdmins(TestsFixture testsFixture, long chatId)
    {
        ChatMember[] admins = await testsFixture.BotClient.GetChatAdministratorsAsync(chatId);
        ChatMember player = admins[new Random(DateTime.Now.Millisecond).Next(admins.Length)];
        return player.User;
    }
}
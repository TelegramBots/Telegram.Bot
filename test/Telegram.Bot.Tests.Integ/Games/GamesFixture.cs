using System;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;

namespace Telegram.Bot.Tests.Integ.Games
{
    /// <summary>
    /// This fixture is for Games tests and it ensures test bot has a game set up before running the test methods.
    /// </summary>
    public class GamesFixture
    {
        public string GameShortName { get; }

        public Message GameMessage { set; get; }        
       
        public string InlineGameMessageId { set; get; }

        public GameHighScore[] HighScores { set; get; }
        
        /// <summary>
        /// A chat admin to set the game scores for.
        /// </summary>
        public User Player { get; }
        
        private readonly TestsFixture _fixture;
        
        public GamesFixture(TestsFixture fixture)
        {
            _fixture = fixture;
            
            GameShortName = "game1";

            try
            {
                fixture.BotClient.SendGameAsync(fixture.SupergroupChat.Id, GameShortName).GetAwaiter().GetResult();
            }
            catch (ApiRequestException e)
            {
                throw new ArgumentException(
                    $@"Bot doesn't have game: ""{GameShortName}"". Make sure you set up a game with @BotFather.",
                    e.Message, e
                );
            }

            Player = GetPlayerIdFromChatAdmins(fixture.SupergroupChat.Id)
                .GetAwaiter().GetResult();
        }

        private async Task<User> GetPlayerIdFromChatAdmins(long chatId)
        {
            ChatMember[] admins = await _fixture.BotClient.GetChatAdministratorsAsync(chatId);
            ChatMember player = admins[new Random(DateTime.Now.Millisecond).Next(admins.Length)];
            return player.User;
        }
    }
}

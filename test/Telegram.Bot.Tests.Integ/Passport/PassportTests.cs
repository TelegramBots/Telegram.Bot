using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Passport
{
    [Collection(Constants.TestCollections.Passport)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class PassportTests
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public PassportTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }


    }
}

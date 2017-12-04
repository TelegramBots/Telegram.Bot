using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.ChatInformation
{
    [Collection(Constants.TestCollections.ChatInformation)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class ChatInfoTests
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public ChatInfoTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = FactTitles.ShouldGetSupergroupChatInfo)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetChat)]
        [ExecutionOrder(1)]
        public async Task Should_Get_Supergroup_Chat_Info()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldGetSupergroupChatInfo);

            Chat chat = await BotClient.GetChatAsync(_fixture.SuperGroupChatId);

            Assert.Equal(ChatType.Supergroup, chat.Type);
            Assert.Equal(_fixture.SuperGroupChatId.ToString(), chat.Id.ToString());
            Assert.NotNull(chat.Title);
            Assert.Null(chat.FirstName);
            Assert.Null(chat.LastName);
            Assert.False(chat.AllMembersAreAdministrators);
        }

        private static class FactTitles
        {
            public const string ShouldGetSupergroupChatInfo = "Should get supergroup chat's information";
        }
    }
}

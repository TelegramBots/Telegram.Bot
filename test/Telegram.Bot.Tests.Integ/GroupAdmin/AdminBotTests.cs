using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Common;
using Xunit;

namespace Telegram.Bot.Tests.Integ.GroupAdmin
{
    [Collection(CommonConstants.TestCollections.GroupAdmin)]
    [TestCaseOrderer(CommonConstants.TestCaseOrderer, CommonConstants.AssemblyName)]
    public class AdminBotTests
    {
        private readonly BotClientFixture _fixture;

        public AdminBotTests(BotClientFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = FactTitles.ShouldSetChatTitle)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.SetChatTitle)]
        [ExecutionOrder(1)]
        public async Task ShouldSetChatTitle()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSetChatTitle);

            bool result = await _fixture.BotClient.SetChatTitleAsync(_fixture.SuperGroupChatId, "Test Chat Title");

            Assert.True(result);
        }

        [Fact(DisplayName = FactTitles.ShouldSetChatDescription)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.SetChatDescription)]
        [ExecutionOrder(2.1)]
        public async Task ShouldSetChatDescription()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSetChatDescription);

            bool result = await _fixture.BotClient.SetChatDescriptionAsync(_fixture.SuperGroupChatId,
                "Test Chat Description");

            Assert.True(result);
        }

        [Fact(DisplayName = FactTitles.ShouldDeleteChatDescription)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.SetChatDescription)]
        [ExecutionOrder(2.2)]
        public async Task ShouldDeleteChatDescription()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldDeleteChatDescription);

            bool result = await _fixture.BotClient.SetChatDescriptionAsync(_fixture.SuperGroupChatId);

            Assert.True(result);
        }

        [Fact(DisplayName = FactTitles.ShouldExportChatInviteLink)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.ExportChatInviteLink)]
        [ExecutionOrder(3)]
        public async Task ShouldExportChatInviteLink()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldExportChatInviteLink);

            string result = await _fixture.BotClient.ExportChatInviteLinkAsync(_fixture.SuperGroupChatId);

            Assert.StartsWith("https://t.me/joinchat/", result);
        }

        private static class FactTitles
        {
            public const string ShouldSetChatTitle = "Should set chat title";

            public const string ShouldExportChatInviteLink = "Should export an invite link to the group";

            public const string ShouldSetChatDescription = "Should set chat description";

            public const string ShouldDeleteChatDescription = "Should delete chat description";
        }
    }
}

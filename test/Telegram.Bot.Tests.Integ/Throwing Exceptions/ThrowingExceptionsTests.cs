using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.ThrowingExceptions
{
    [Collection(CommonConstants.TestCollections.Exceptions)]
    [TestCaseOrderer(CommonConstants.TestCaseOrderer, CommonConstants.AssemblyName)]
    public class ThrowingExceptionsTests
    {
        public ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public ThrowingExceptionsTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        #region 1. Chat not found

        [Fact(DisplayName = FactTitles.ShouldThrowChatNotFoundException)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.SendMessage)]
        [ExecutionOrder(1)]
        public async Task Should_Throw_Chat_Not_Found_Exception()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowChatNotFoundException);

            Exception e = await Assert.ThrowsAnyAsync<ApiRequestException>(() =>
                _fixture.BotClient.SendTextMessageAsync(0, "test"));
            Assert.IsType<ChatNotFoundException>(e);
        }

        #endregion

        #region 2. User not found

        [Fact(DisplayName = FactTitles.ShouldThrowUserNotFoundException)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.PromoteChatMember)]
        [ExecutionOrder(2)]
        public async Task Should_Throw_User_Not_Found_Exception()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowUserNotFoundException);

            Exception e = await Assert.ThrowsAnyAsync<ApiRequestException>(() =>
                _fixture.BotClient.PromoteChatMemberAsync(_fixture.SuperGroupChatId, 0));
            Assert.IsType<UserNotFoundException>(e);
        }

        #endregion

        private static class FactTitles
        {
            public const string ShouldThrowChatNotFoundException = "Should throw ChatNotFoundException";

            public const string ShouldThrowUserNotFoundException = "Should throw UserNotFoundException";
        }
    }
}

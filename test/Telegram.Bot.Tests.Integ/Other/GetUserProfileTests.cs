using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Other
{
    [Collection(Constants.TestCollections.GetUserProfilePhotos)]
    [TestCaseOrderer(Constants.TestCaseOrderer2, Constants.AssemblyName)]
    public class GetUserProfileTests
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public GetUserProfileTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldLeaveChat)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.LeaveChat)]
        public async Task Should_Get_User_Profile_Photos()
        {
            // ToDo: Exception when leaving private chat
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldLeaveChat);

            UserProfilePhotos profilePhotos = await BotClient.GetUserProfilePhotosAsync(
                userId: _fixture.BotUser.Id
            );
            
            Assert.True(1 <= profilePhotos.TotalCount);
            Assert.NotNull(profilePhotos.Photos.First());
        }

        private static class FactTitles
        {
            public const string ShouldLeaveChat = "Should leave chat";
        }
    }
}
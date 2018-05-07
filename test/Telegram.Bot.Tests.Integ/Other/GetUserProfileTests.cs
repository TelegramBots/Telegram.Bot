using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Other
{
    [Collection(Constants.TestCollections.GetUserProfilePhotos)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class GetUserProfileTests
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public GetUserProfileTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldGetBotProfilePhotos)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetUserProfilePhotos)]
        public async Task Should_Get_User_Profile_Photos()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldGetBotProfilePhotos);

            UserProfilePhotos profilePhotos = await BotClient.GetUserProfilePhotosAsync(
                userId: _fixture.BotUser.Id
            );
            
            Assert.True(1 <= profilePhotos.TotalCount);
            Assert.NotNull(profilePhotos.Photos.First());
        }

        private static class FactTitles
        {
            public const string ShouldGetBotProfilePhotos = "Should get bot's profile photos";
        }
    }
}
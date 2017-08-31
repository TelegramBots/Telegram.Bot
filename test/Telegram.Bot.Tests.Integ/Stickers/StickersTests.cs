using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Stickers
{
    [Collection(CommonConstants.TestCollections.Stickers)]
    [TestCaseOrderer(CommonConstants.TestCaseOrderer, CommonConstants.AssemblyName)]
    public class StickersTests
    {
        private readonly TestsFixture _fixture;

        private ITelegramBotClient BotClient => _fixture.BotClient;

        public StickersTests(TestsFixture assemblyFixture)
        {
            _fixture = assemblyFixture;
        }

        #region 1. Get and send stickers from a set

        [Fact(DisplayName = FactTitles.ShouldGetStickerSet)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.GetStickerSet)]
        [ExecutionOrder(1.1)]
        public async Task Should_Get_Sticker_Set()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldGetStickerSet);
            const string setName = "EvilMinds";
            StickerSet stickerSet = await BotClient.GetStickerSetAsync(setName);

            Assert.Equal(setName, stickerSet.Name);
            Assert.Equal("Evil Minds", stickerSet.Title);
            Assert.False(stickerSet.ContainsMasks);
            Assert.NotEmpty(stickerSet.Stickers);
            Assert.True(20 < stickerSet.Stickers.Count);
            // ToDo: Add more asserts

            // ToDo: Assign sticker set to this class's fixture
        }

        // ToDo: Send one of the stickers retrieved to chat

        #endregion

        private static class FactTitles
        {
            public const string ShouldGetStickerSet = "Should get sticker set";
        }
    }
}

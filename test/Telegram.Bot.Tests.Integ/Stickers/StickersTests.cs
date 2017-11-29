using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Stickers
{
    [Collection(CommonConstants.TestCollections.Stickers)]
    [TestCaseOrderer(CommonConstants.TestCaseOrderer, CommonConstants.AssemblyName)]
    public class StickersTests : IClassFixture<StickersTestsFixture>
    {
        private readonly StickersTestsFixture _classFixture;

        private readonly TestsFixture _fixture;

        public StickersTests(StickersTestsFixture classFixture)
        {
            _classFixture = classFixture;
            _fixture = _classFixture.TestsFixture;
        }

        #region 1. Get and send stickers from a set

        [Fact(DisplayName = FactTitles.ShouldGetStickerSet)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.GetStickerSet)]
        [ExecutionOrder(1.1)]
        public async Task Should_Get_Sticker_Set()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldGetStickerSet);
            const string setName = "EvilMinds";
            StickerSet stickerSet = await _fixture.BotClient.GetStickerSetAsync(setName);

            Assert.Equal(setName, stickerSet.Name);
            Assert.Equal("Evil Minds", stickerSet.Title);
            Assert.False(stickerSet.ContainsMasks);
            Assert.NotEmpty(stickerSet.Stickers);
            Assert.True(20 < stickerSet.Stickers.Count);
            // ToDo: Add more asserts

            _classFixture.StickerSet = stickerSet;
        }

        [Fact(DisplayName = FactTitles.ShouldSendSticker)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.SendSticker)]
        [ExecutionOrder(1.2)]
        public async Task Should_Send_Sticker()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendSticker);
            FileToSend stickerFile = new FileToSend(_classFixture.StickerSet.Stickers[0].FileId);

            Message message = await _fixture.BotClient.SendStickerAsync(_fixture.SuperGroupChatId, stickerFile);

            Assert.Equal(MessageType.StickerMessage, message.Type);
            Assert.Equal(stickerFile.FileId, message.Sticker.FileId);
        }
        #endregion

        private static class FactTitles
        {
            public const string ShouldGetStickerSet = "Should get sticker set";
            public const string ShouldSendSticker = "Should send sticker";
        }
    }
}

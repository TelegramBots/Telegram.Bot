using System.IO;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.SendingMessages
{
    [Collection(CommonConstants.TestCollections.AlbumMessage)]
    [TestCaseOrderer(CommonConstants.TestCaseOrderer, CommonConstants.AssemblyName)]
    public class AlbumMessageTests
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public AlbumMessageTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = FactTitles.ShouldUploadPhotosInAlbum)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.SendPhoto)]
        [ExecutionOrder(1.1)]
        public async Task Should_Send_2_Photos_Album()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldUploadPhotosInAlbum);

            Message[] messages;
            using (Stream
                stream1 = new FileStream("Files/Photo/logo.png", FileMode.Open),
                stream2 = new FileStream("Files/Photo/bot.gif", FileMode.Open))
            {
                InputMediaBase[] inputMedia = {
                    new InputMediaPhoto
                    {
                        Media = new InputMediaType("logo", stream1),
                        Caption = "Logo"
                    },
                    new InputMediaPhoto
                    {
                        Media = new InputMediaType("bot", stream2),
                        Caption = "Bot"
                    },
                };

                messages = await BotClient.SendMediaGroupAsync(
                    _fixture.SuperGroupChatId,
                    inputMedia
                );
            }

            Assert.Equal(2, messages.Length);
            Assert.All(messages, msg => Assert.Equal(MessageType.PhotoMessage, msg.Type));
        }

        private static class FactTitles
        {
            public const string ShouldUploadPhotosInAlbum = "Should upload 2 photos and send them in an album";
        }
    }
}

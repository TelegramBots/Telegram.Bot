using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.SendingMessages
{
    [Collection(CommonConstants.TestCollections.AlbumMessage)]
    [TestCaseOrderer(CommonConstants.TestCaseOrderer, CommonConstants.AssemblyName)]
    public class AlbumMessageTests : IClassFixture<AlbumsTestFixture>
    {
        private readonly AlbumsTestFixture _classFixture;

        private readonly TestsFixture _fixture;

        private ITelegramBotClient BotClient => _fixture.BotClient;

        public AlbumMessageTests(AlbumsTestFixture classFixture)
        {
            _classFixture = classFixture;
            _fixture = _classFixture.TestsFixture;
        }

        [Fact(DisplayName = FactTitles.ShouldUploadPhotosInAlbum)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.SendMediaGroup)]
        [ExecutionOrder(1.1)]
        public async Task Should_Upload_2_Photos_Album()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldUploadPhotosInAlbum);

            string[] captions = { "Logo", "Bot" };

            Message[] messages;
            using (Stream
                stream1 = new FileStream("Files/Photo/logo.png", FileMode.Open),
                stream2 = new FileStream("Files/Photo/bot.gif", FileMode.Open))
            {
                InputMediaBase[] inputMedia = {
                    new InputMediaPhoto
                    {
                        Media = new InputMediaType("logo", stream1),
                        Caption = captions[0]
                    },
                    new InputMediaPhoto
                    {
                        Media = new InputMediaType("bot", stream2),
                        Caption = captions[1]
                    },
                };

                messages = await BotClient.SendMediaGroupAsync(
                    chatId: _fixture.SuperGroupChatId,
                    media: inputMedia
                );
            }

            Assert.Equal(2, messages.Length);
            Assert.All(messages, msg => Assert.Equal(MessageType.PhotoMessage, msg.Type));
            Assert.Equal(captions[0], messages[0].Caption);
            Assert.Equal(captions[1], messages[1].Caption);

            _classFixture.PhotoMessages = messages;
        }

        [Fact(DisplayName = FactTitles.ShouldSendFileIdPhotosInAlbum)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.SendMediaGroup)]
        [ExecutionOrder(1.2)]
        public async Task Should_Send_3_Photos_Album_Using_FileId()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendFileIdPhotosInAlbum);

            string[] fileIds = _classFixture.PhotoMessages
                .Select(msg => msg.Photo.First().FileId)
                .ToArray();

            Message[] messages = await BotClient.SendMediaGroupAsync(
                chatId: _fixture.SuperGroupChatId,
                media: new[]
                {
                    new InputMediaPhoto { Media = new InputMediaType(fileIds[0])},
                    new InputMediaPhoto { Media = new InputMediaType(fileIds[1])},
                    new InputMediaPhoto { Media = new InputMediaType(fileIds[0])},
                }
            );

            Assert.Equal(3, messages.Length);
            Assert.All(messages, msg => Assert.Equal(MessageType.PhotoMessage, msg.Type));
        }

        [Fact(DisplayName = FactTitles.ShouldSendUrlPhotosInAlbum)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.SendMediaGroup)]
        [ExecutionOrder(1.3)]
        public async Task Should_Send_Photo_Album_Using_Url()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendUrlPhotosInAlbum);

            const int maxCount = 5;
            const string url = "http://lorempixel.com/400/600";
            int replyToMessageId = 746;// _classFixture.PhotoMessages.First().MessageId;

            Random rnd = new Random();

            Message[] messages = await BotClient.SendMediaGroupAsync(
                chatId: _fixture.SuperGroupChatId,
                media: Enumerable.Range(0, rnd.Next(2, maxCount + 1))
                    .Select(_ => new InputMediaPhoto
                    {
                        Media = new InputMediaType($"{url}?q={rnd.Next(100_000_000)}")
                    }),
                replyToMessageId: replyToMessageId
            );

            Assert.InRange(messages.Length, 2, maxCount);
            Assert.All(messages, msg => Assert.Equal(MessageType.PhotoMessage, msg.Type));
            Assert.All(messages, msg => Assert.Equal(replyToMessageId, msg.ReplyToMessage.MessageId));
        }

        private static class FactTitles
        {
            public const string ShouldUploadPhotosInAlbum = "Should upload 2 photos with captions and send them in an album";

            public const string ShouldSendFileIdPhotosInAlbum = "Should send an album with 3 photos using their file_id";

            public const string ShouldSendUrlPhotosInAlbum = "Should send an album using http urls in reply to 1st album message";
        }
    }
}

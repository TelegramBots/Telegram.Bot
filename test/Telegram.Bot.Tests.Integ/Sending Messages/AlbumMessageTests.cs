using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Tests.Integ.Framework.Fixtures;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Sending_Messages
{
    [Collection(Constants.TestCollections.AlbumMessage)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class AlbumMessageTests : IClassFixture<EntitiesFixture<Message>>
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly EntitiesFixture<Message> _classFixture;

        private readonly TestsFixture _fixture;

        public AlbumMessageTests(TestsFixture testsFixture, EntitiesFixture<Message> classFixture)
        {
            _fixture = testsFixture;
            _classFixture = classFixture;
        }

        [Fact(DisplayName = FactTitles.ShouldUploadPhotosInAlbum)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMediaGroup)]
        [ExecutionOrder(1)]
        public async Task Should_Upload_2_Photos_Album()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldUploadPhotosInAlbum);

            string[] captions = {"Logo", "Bot"};

            Message[] messages;
            using (Stream
                stream1 = System.IO.File.OpenRead(Constants.FileNames.Photos.Logo),
                stream2 = System.IO.File.OpenRead(Constants.FileNames.Photos.Bot)
            )
            {
                InputMediaBase[] inputMedia =
                {
                    new InputMediaPhoto
                    {
                        Media = new InputMedia(stream1, "logo"),
                        Caption = captions[0]
                    },
                    new InputMediaPhoto
                    {
                        Media = new InputMedia(stream2, "bot"),
                        Caption = captions[1]
                    },
                };

                messages = await BotClient.SendMediaGroupAsync(
                    chatId: _fixture.SupergroupChat.Id,
                    media: inputMedia,
                    disableNotification: true
                );
            }

            Assert.Equal(2, messages.Length);
            Assert.All(messages, msg => Assert.Equal(MessageType.Photo, msg.Type));
            Assert.Equal(captions[0], messages[0].Caption);
            Assert.Equal(captions[1], messages[1].Caption);

            _classFixture.Entities = messages.ToList();
        }

        [Fact(DisplayName = FactTitles.ShouldSendFileIdPhotosInAlbum)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMediaGroup)]
        [ExecutionOrder(2)]
        public async Task Should_Send_3_Photos_Album_Using_FileId()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendFileIdPhotosInAlbum);

            string[] fileIds = _classFixture.Entities
                .Select(msg => msg.Photo.First().FileId)
                .ToArray();

            Message[] messages = await BotClient.SendMediaGroupAsync(
                chatId: _fixture.SupergroupChat.Id,
                media: new[]
                {
                    new InputMediaPhoto {Media = new InputMedia(fileIds[0])},
                    new InputMediaPhoto {Media = new InputMedia(fileIds[1])},
                    new InputMediaPhoto {Media = new InputMedia(fileIds[0])},
                }
            );

            Assert.Equal(3, messages.Length);
            Assert.All(messages, msg => Assert.Equal(MessageType.Photo, msg.Type));
        }

        /// <remarks>
        /// URLs have a redundant query string to make sure Telegram doesn't use cached images
        /// </remarks>
        [Fact(DisplayName = FactTitles.ShouldSendUrlPhotosInAlbum)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMediaGroup)]
        [ExecutionOrder(3)]
        public async Task Should_Send_Photo_Album_Using_Url()
        {
            // ToDo add exception: Bad Request: failed to get HTTP URL content
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendUrlPhotosInAlbum);

            const string url1 = "https://cdn.pixabay.com/photo/2017/06/20/19/22/fuchs-2424369_640.jpg";
            const string url2 = "https://cdn.pixabay.com/photo/2017/04/11/21/34/giraffe-2222908_640.jpg";
            int replyToMessageId = _classFixture.Entities.First().MessageId;

            Message[] messages = await BotClient.SendMediaGroupAsync(
                chatId: _fixture.SupergroupChat.Id,
                media: new[]
                {
                    new InputMediaPhoto {Media = url1},
                    new InputMediaPhoto {Media = url2},
                },
                replyToMessageId: replyToMessageId
            );

            Assert.Equal(2, messages.Length);
            Assert.All(messages, msg => Assert.Equal(MessageType.Photo, msg.Type));
            Assert.All(messages, msg => Assert.Equal(replyToMessageId, msg.ReplyToMessage.MessageId));
        }

        [Fact(DisplayName = FactTitles.ShouldUploadVideosInAlbum)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMediaGroup)]
        [ExecutionOrder(4)]
        public async Task Should_Upload_2_Videos_Album()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldUploadVideosInAlbum);

            string[] captions = {"Golden Ratio", "Moon Landing", "Bot"};

            const int firstMediaDuration = 28;
            const int firstMediaWidthAndHeight = 240;

            Message[] messages;
            using (Stream
                stream0 = System.IO.File.OpenRead(Constants.FileNames.Videos.GoldenRatio),
                stream1 = System.IO.File.OpenRead(Constants.FileNames.Videos.MoonLanding),
                stream2 = System.IO.File.OpenRead(Constants.FileNames.Photos.Bot)
            )
            {
                InputMediaBase[] inputMedia =
                {
                    new InputMediaVideo
                    {
                        Media = new InputMedia(stream0, captions[0]),
                        Caption = captions[0],
                        Height = firstMediaWidthAndHeight,
                        Width = firstMediaWidthAndHeight,
                        Duration = firstMediaDuration
                    },
                    new InputMediaVideo
                    {
                        Media = new InputMedia(stream1, captions[1]),
                        Caption = captions[1]
                    },
                    new InputMediaPhoto
                    {
                        Media = new InputMedia(stream2, "bot"),
                        Caption = captions[2]
                    },
                };

                messages = await BotClient.SendMediaGroupAsync(
                    chatId: _fixture.SupergroupChat.Id,
                    media: inputMedia
                );
            }

            Assert.Equal(3, messages.Length);
            Assert.All(messages.Take(2), msg => Assert.Equal(MessageType.Video, msg.Type));
            Assert.Equal(MessageType.Photo, messages.Last().Type);
            Assert.Equal(captions, messages.Select(msg => msg.Caption));
            Assert.Equal(firstMediaWidthAndHeight, messages.First().Video.Width);
            Assert.Equal(firstMediaWidthAndHeight, messages.First().Video.Height);
            Assert.InRange(messages.First().Video.Duration, firstMediaDuration - 2, firstMediaDuration + 2);
        }

        private static class FactTitles
        {
            public const string ShouldUploadPhotosInAlbum =
                "Should upload 2 photos with captions and send them in an album";

            public const string ShouldSendFileIdPhotosInAlbum =
                "Should send an album with 3 photos using their file_id";

            public const string ShouldSendUrlPhotosInAlbum =
                "Should send an album using HTTP urls in reply to 1st album message";

            public const string ShouldUploadVideosInAlbum =
                "Should upload 2 videos and a photo with captions and send them in an album";
        }
    }
}
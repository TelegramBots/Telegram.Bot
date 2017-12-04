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
    [Collection(Constants.TestCollections.AlbumMessage)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
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

        #region Photo-Only Albums

        [Fact(DisplayName = FactTitles.ShouldUploadPhotosInAlbum)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMediaGroup)]
        [ExecutionOrder(1.1)]
        public async Task Should_Upload_2_Photos_Album()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldUploadPhotosInAlbum);

            string[] captions = { "Logo", "Bot" };

            Message[] messages;
            using (Stream
                stream1 = System.IO.File.OpenRead(Constants.FileNames.Photos.Logo),
                stream2 = System.IO.File.OpenRead(Constants.FileNames.Photos.Bot)
            )
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
                    media: inputMedia,
                    disableNotification: true
                );
            }

            Assert.Equal(2, messages.Length);
            Assert.All(messages, msg => Assert.Equal(MessageType.PhotoMessage, msg.Type));
            Assert.Equal(captions[0], messages[0].Caption);
            Assert.Equal(captions[1], messages[1].Caption);

            _classFixture.PhotoMessages = messages;
        }

        [Fact(DisplayName = FactTitles.ShouldSendFileIdPhotosInAlbum)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMediaGroup)]
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

        /// <remarks>
        /// URLs have a redundant query string to make sure Telegram doesn't use cached images
        /// </remarks>
        [Fact(DisplayName = FactTitles.ShouldSendUrlPhotosInAlbum)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMediaGroup)]
        [ExecutionOrder(1.3)]
        public async Task Should_Send_Photo_Album_Using_Url()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendUrlPhotosInAlbum);

            const string url = "https://loremflickr.com/600/400/history,culture,art,nature";
            int replyToMessageId = _classFixture.PhotoMessages.First().MessageId;

            Random rnd = new Random();

            Message[] messages = await BotClient.SendMediaGroupAsync(
                chatId: _fixture.SuperGroupChatId,
                media: Enumerable.Range(0, 2)
                    .Select(_ => rnd.Next(100_000_000))
                    .Select(number => new InputMediaPhoto
                    {
                        Media = new InputMediaType($"{url}?q={number}")
                    }),
                replyToMessageId: replyToMessageId
            );

            Assert.Equal(2, messages.Length);
            Assert.All(messages, msg => Assert.Equal(MessageType.PhotoMessage, msg.Type));
            Assert.All(messages, msg => Assert.Equal(replyToMessageId, msg.ReplyToMessage.MessageId));
        }

        #endregion

        #region Video Albums

        [Fact(DisplayName = FactTitles.ShouldUploadVideosInAlbum)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMediaGroup)]
        [ExecutionOrder(2.1)]
        public async Task Should_Upload_2_Videos_Album()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldUploadVideosInAlbum);

            string[] captions = { "Golden Ratio", "Moon Landing", "Bot" };

            const int firstMediaDuration = 28;
            const int firstMediaWidthAndHeight = 240;

            Message[] messages;
            using (Stream
                stream0 = System.IO.File.OpenRead(Constants.FileNames.Videos.GoldenRatio),
                stream1 = System.IO.File.OpenRead(Constants.FileNames.Videos.MoonLanding),
                stream2 = System.IO.File.OpenRead(Constants.FileNames.Photos.Bot)
            )
            {
                InputMediaBase[] inputMedia = {
                    new InputMediaVideo
                    {
                        Media = new InputMediaType(captions[0], stream0),
                        Caption = captions[0],
                        Height = firstMediaWidthAndHeight,
                        Width = firstMediaWidthAndHeight,
                        Duration = firstMediaDuration
                    },
                    new InputMediaVideo
                    {
                        Media = new InputMediaType(captions[1], stream1),
                        Caption = captions[1]
                    },
                    new InputMediaPhoto
                    {
                        Media = new InputMediaType("bot", stream2),
                        Caption = captions[2]
                    },
                };

                messages = await BotClient.SendMediaGroupAsync(
                    chatId: _fixture.SuperGroupChatId,
                    media: inputMedia
                );
            }

            Assert.Equal(3, messages.Length);
            Assert.All(messages.Take(2), msg => Assert.Equal(MessageType.VideoMessage, msg.Type));
            Assert.Equal(MessageType.PhotoMessage, messages.Last().Type);
            Assert.Equal(captions, messages.Select(msg => msg.Caption));
            Assert.Equal(firstMediaWidthAndHeight, messages.First().Video.Width);
            Assert.Equal(firstMediaWidthAndHeight, messages.First().Video.Height);
            Assert.InRange(messages.First().Video.Duration, firstMediaDuration - 2, firstMediaDuration + 2);
        }

        #endregion

        private static class FactTitles
        {
            public const string ShouldUploadPhotosInAlbum = "Should upload 2 photos with captions and send them in an album";

            public const string ShouldSendFileIdPhotosInAlbum = "Should send an album with 3 photos using their file_id";

            public const string ShouldSendUrlPhotosInAlbum = "Should send an album using HTTP urls in reply to 1st album message";

            public const string ShouldUploadVideosInAlbum = "Should upload 2 videos with captions and send them in an album";
        }
    }
}

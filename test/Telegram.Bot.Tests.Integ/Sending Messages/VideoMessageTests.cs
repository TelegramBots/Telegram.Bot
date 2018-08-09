using System.IO;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Sending_Messages
{
    [Collection(Constants.TestCollections.SendVideoMessage)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class SendingVideoMessageTests
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public SendingVideoMessageTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldSendVideo)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendVideo)]
        public async Task Should_Send_Video()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendVideo);

            const int duration = 104;
            const int width = 320;
            const int height = 240;
            const string caption = "Moon Landing";
            const string mimeType = "video/mp4";

            Message message;
            using (Stream stream = System.IO.File.OpenRead(Constants.FileNames.Videos.MoonLanding))
            {
                message = await BotClient.SendVideoAsync(
                    _fixture.SupergroupChat.Id,
                    stream,
                    duration,
                    width,
                    height,
                    caption
                );
            }

            Assert.Equal(MessageType.Video, message.Type);
            Assert.Equal(caption, message.Caption);
            Assert.Equal(duration, message.Video.Duration);
            Assert.Equal(width, message.Video.Width);
            Assert.Equal(height, message.Video.Height);
            Assert.Equal(mimeType, message.Video.MimeType);
            Assert.NotEmpty(message.Video.Thumb.FileId);
            Assert.True(message.Video.Thumb.FileSize > 200);
            Assert.True(message.Video.Thumb.Width > 50);
            Assert.True(message.Video.Thumb.Height > 50);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldSendVideoNote)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendVideoNote)]
        public async Task Should_Send_Video_Note()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendVideoNote);

            const int duration = 28;
            const int widthAndHeight = 240;

            Message message;
            using (Stream stream = System.IO.File.OpenRead(Constants.FileNames.Videos.GoldenRatio))
            {
                message = await BotClient.SendVideoNoteAsync(
                    chatId: _fixture.SupergroupChat.Id,
                    videoNote: stream,
                    duration: duration,
                    length: widthAndHeight
                );
            }

            Assert.Equal(MessageType.VideoNote, message.Type);
            Assert.Equal(duration, message.VideoNote.Duration);
            Assert.Equal(widthAndHeight, message.VideoNote.Length);
            Assert.NotEmpty(message.VideoNote.Thumb.FileId);
            Assert.True(message.VideoNote.Thumb.FileSize > 200);
            Assert.True(message.VideoNote.Thumb.Width > 50);
            Assert.True(message.VideoNote.Thumb.Height > 50);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldSendVideoWithThumb)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendDocument)]
        public async Task Should_Send_Video_With_Thumb()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendVideoWithThumb);

            Message message;
            using (Stream
                stream1 = System.IO.File.OpenRead(Constants.FileNames.Videos.MoonLanding),
                stream2 = System.IO.File.OpenRead(Constants.FileNames.Thumbnail.TheAbilityToBreak)
            )
            {
                message = await BotClient.SendVideoAsync(
                    /* chatId: */ _fixture.SupergroupChat,
                    /* video: */ stream1,
                    thumb: new InputMedia(stream2, "thumb.jpg")
                );
            }

            Assert.NotNull(message.Video.Thumb);
            Assert.NotEmpty(message.Video.Thumb.FileId);
            Assert.Equal(90, message.Video.Thumb.Height);
            Assert.Equal(90, message.Video.Thumb.Width);
            Assert.True(message.Video.Thumb.FileSize > 10_000);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldSendVideoNoteWithThumb)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendVideoNote)]
        public async Task Should_Send_Video_Note_With_Thumb()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendVideoNoteWithThumb);

            Message message;
            using (Stream
                stream1 = System.IO.File.OpenRead(Constants.FileNames.Videos.GoldenRatio),
                stream2 = System.IO.File.OpenRead(Constants.FileNames.Thumbnail.Video)
            )
            {
                message = await BotClient.SendVideoNoteAsync(
                    /* chatId: */ _fixture.SupergroupChat.Id,
                    /* videoNote: */ stream1,
                    thumb: new InputMedia(stream2, "thumbnail.jpg")
                );
            }

            Assert.NotNull(message.VideoNote.Thumb);
            Assert.NotEmpty(message.VideoNote.Thumb.FileId);
            Assert.Equal(90, message.VideoNote.Thumb.Height);
            Assert.Equal(90, message.VideoNote.Thumb.Width);
            Assert.True(message.VideoNote.Thumb.FileSize > 10_000);
        }

        private static class FactTitles
        {
            public const string ShouldSendVideo = "Should send a video with caption";

            public const string ShouldSendVideoNote = "Should send a video note";

            public const string ShouldSendVideoWithThumb = "Should send a video with thumbail";

            public const string ShouldSendVideoNoteWithThumb = "Should send a video note with thumbnail";
        }
    }
}

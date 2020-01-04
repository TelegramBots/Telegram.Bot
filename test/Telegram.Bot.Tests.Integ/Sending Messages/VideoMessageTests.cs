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

        [OrderedFact("Should send a video with caption")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendVideo)]
        public async Task Should_Send_Video()
        {
            Message message;
            using (Stream stream = System.IO.File.OpenRead(Constants.PathToFile.Videos.MoonLanding))
            {
                message = await BotClient.SendVideoAsync(
                    /* chatId: */ _fixture.SupergroupChat.Id,
                    /* video: */ stream,
                    /* duration: */ 104,
                    /* width */ 320,
                    /* height: */ 240,
                    /* caption: */ "Moon Landing"
                );
            }

            Assert.Equal(MessageType.Video, message.Type);
            Assert.Equal("Moon Landing", message.Caption);
            Assert.NotEmpty(message.Video.FileId);
            Assert.NotEmpty(message.Video.FileUniqueId);
            Assert.Equal(104, message.Video.Duration);
            Assert.Equal(320, message.Video.Width);
            Assert.Equal(240, message.Video.Height);
            Assert.Equal("video/mp4", message.Video.MimeType);
            Assert.NotEmpty(message.Video.Thumb.FileId);
            Assert.NotEmpty(message.Video.Thumb.FileUniqueId);
            Assert.True(message.Video.Thumb.FileSize > 200);
            Assert.Equal(320, message.Video.Thumb.Width);
            Assert.Equal(240, message.Video.Thumb.Height);
            Assert.InRange(message.Video.Thumb.FileSize, 600, 900);
        }

        [OrderedFact("Should send a video note")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendVideoNote)]
        public async Task Should_Send_Video_Note()
        {
            Message message;
            using (Stream stream = System.IO.File.OpenRead(Constants.PathToFile.Videos.GoldenRatio))
            {
                message = await BotClient.SendVideoNoteAsync(
                    /* chatId: */ _fixture.SupergroupChat.Id,
                    /* videoNote: */ stream,
                    /* duration: */ 28,
                    /* length */ 240
                );
            }

            Assert.Equal(MessageType.VideoNote, message.Type);
            Assert.NotEmpty(message.VideoNote.FileId);
            Assert.NotEmpty(message.VideoNote.FileUniqueId);
            Assert.Equal(28, message.VideoNote.Duration);
            Assert.Equal(240, message.VideoNote.Length);
            Assert.NotEmpty(message.VideoNote.Thumb.FileId);
            Assert.NotEmpty(message.VideoNote.Thumb.FileUniqueId);
            Assert.Equal(240, message.VideoNote.Thumb.Width);
            Assert.Equal(240, message.VideoNote.Thumb.Height);
            Assert.InRange(message.VideoNote.Thumb.FileSize, 1_000, 1_500);
        }

        [OrderedFact("Should send a video with thumbnail")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendDocument)]
        public async Task Should_Send_Video_With_Thumb()
        {
            Message message;
            using (Stream
                stream1 = System.IO.File.OpenRead(Constants.PathToFile.Videos.MoonLanding),
                stream2 = System.IO.File.OpenRead(Constants.PathToFile.Thumbnail.TheAbilityToBreak)
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
            Assert.NotEmpty(message.Video.Thumb.FileUniqueId);
            Assert.Equal(320, message.Video.Thumb.Width);
            Assert.Equal(240, message.Video.Thumb.Height);
            Assert.InRange(message.Video.Thumb.FileSize, 600, 900);
        }

        [OrderedFact("Should send a video note with thumbnail")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendVideoNote)]
        public async Task Should_Send_Video_Note_With_Thumb()
        {
            Message message;
            using (Stream
                stream1 = System.IO.File.OpenRead(Constants.PathToFile.Videos.GoldenRatio),
                stream2 = System.IO.File.OpenRead(Constants.PathToFile.Thumbnail.Video)
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
            Assert.NotEmpty(message.VideoNote.Thumb.FileUniqueId);
            Assert.Equal(240, message.VideoNote.Thumb.Height);
            Assert.Equal(240, message.VideoNote.Thumb.Width);
            Assert.InRange(message.VideoNote.Thumb.FileSize, 1_000, 1_500);
        }
    }
}

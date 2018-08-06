using System.IO;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Sending_Messages
{
    [Collection(Constants.TestCollections.SendAudioMessage)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class AudioMessageTests
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public AudioMessageTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldSendAudio)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendAudio)]
        public async Task Should_Send_Audio()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendAudio);

            const string performer = "Jackson F. Smith";
            const string title = "Cantina Rag";
            const int duration = 201;
            const string caption = "Audio File in .mp3 format";

            Message message;
            using (Stream stream = System.IO.File.OpenRead(Constants.FileNames.Audio.CantinaRagMp3))
            {
                message = await BotClient.SendAudioAsync(
                    chatId: _fixture.SupergroupChat,
                    audio: stream,
                    title: title,
                    performer: performer,
                    caption: caption,
                    duration: duration
                );
            }

            Assert.Equal(MessageType.Audio, message.Type);
            Assert.Equal(caption, message.Caption);
            Assert.Equal(performer, message.Audio.Performer);
            Assert.Equal(title, message.Audio.Title);
            Assert.Equal(duration, message.Audio.Duration);
            Assert.Equal("audio/mpeg", message.Audio.MimeType);
            Assert.NotEmpty(message.Audio.FileId);
            Assert.True(message.Audio.FileSize > 200);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldSendAudioWithThumbnail)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendAudio)]
        public async Task Should_Send_Audio_With_Thumb()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendAudioWithThumbnail);

            // Both audio file and its thumbnail should be uploaded
            Message message;
            using (Stream
                stream1 = System.IO.File.OpenRead(Constants.FileNames.Audio.AStateOfDespairMp3),
                stream2 = System.IO.File.OpenRead(Constants.FileNames.Thumbnail.TheAbilityToBreak)
            )
            {
                message = await BotClient.SendAudioAsync(
                    /* chatId: */ _fixture.SupergroupChat,
                    /* audio: */ new InputMedia(stream1, "Ask Again - A State of Despair.mp3"),
                    thumb: new InputMedia(stream2, "thumb.jpg")
                );
            }

            Assert.NotNull(message.Audio.Thumb);
            Assert.NotEmpty(message.Audio.Thumb.FileId);
            Assert.Equal(90, message.Audio.Thumb.Height);
            Assert.Equal(90, message.Audio.Thumb.Width);
            Assert.True(message.Audio.Thumb.FileSize > 10_000);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldSendVoice)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendVoice)]
        public async Task Should_Send_Voice()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendVoice);

            const int duration = 24;
            const string caption = "Test Voice in .ogg format";

            Message message;
            using (var stream = System.IO.File.OpenRead(Constants.FileNames.Audio.TestOgg))
            {
                message = await BotClient.SendVoiceAsync(
                    chatId: _fixture.SupergroupChat,
                    voice: stream,
                    caption: caption,
                    duration: duration
                );
            }

            Assert.Equal(MessageType.Voice, message.Type);
            Assert.Equal(caption, message.Caption);
            Assert.Equal(duration, message.Voice.Duration);
            Assert.Equal("audio/ogg", message.Voice.MimeType);
            Assert.NotEmpty(message.Voice.FileId);
            Assert.True(message.Voice.FileSize > 200);
        }

        private static class FactTitles
        {
            public const string ShouldSendAudio = "Should send an audio with caption";

            public const string ShouldSendAudioWithThumbnail = "Should send an audio with its thumbnail";

            public const string ShouldSendVoice = "Should send a voice with caption";
        }
    }
}

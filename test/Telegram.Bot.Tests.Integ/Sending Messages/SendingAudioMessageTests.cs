using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Sending_Messages
{
    [Collection(Constants.TestCollections.SendAudioMessage)]
    [TestCaseOrderer(Constants.TestCaseOrderer2, Constants.AssemblyName)]
    public class SendingAudioMessageTests
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public SendingAudioMessageTests(TestsFixture fixture)
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
            using (var stream = System.IO.File.OpenRead(Constants.FileNames.Audio.CantinaRagMp3))
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

        private static class FactTitles
        {
            public const string ShouldSendAudio = "Should send an audio with caption";
        }
    }
}

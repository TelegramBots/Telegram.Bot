using System.IO;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Sending_Messages;

[Collection(Constants.TestCollections.SendAudioMessage)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class AudioMessageTests(TestsFixture fixture)
{
    ITelegramBotClient BotClient => fixture.BotClient;

    [OrderedFact("Should send an audio with caption")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendAudio)]
    public async Task Should_Send_Audio()
    {
        const string performer = "Jackson F. Smith";
        const string title = "Cantina Rag";
        const int duration = 201;
        const string caption = "Audio File in .mp3 format";

        Message message;
        await using (Stream stream = System.IO.File.OpenRead(Constants.PathToFile.Audio.CantinaRagMp3))
        {
            message = await BotClient.SendAudioAsync(
                new()
                {
                    ChatId = fixture.SupergroupChat,
                    Audio = InputFile.FromStream(stream, "Jackson F Smith - Cantina Rag.mp3"),
                    Title = title,
                    Performer = performer,
                    Caption = caption,
                    Duration = duration,
                }
            );
        }

        Assert.Equal(MessageType.Audio, message.Type);
        Assert.Equal(caption, message.Caption);
        Assert.NotNull(message.Audio);
        Assert.Equal(performer, message.Audio.Performer);
        Assert.Equal(title, message.Audio.Title);
        Assert.Equal(duration, message.Audio.Duration);
        Assert.Equal("audio/mpeg", message.Audio.MimeType);
        Assert.Equal("Jackson F Smith - Cantina Rag.mp3", message.Audio.FileName);
        Assert.NotEmpty(message.Audio.FileId);
        Assert.NotEmpty(message.Audio.FileUniqueId);
        Assert.True(message.Audio.FileSize > 200);
    }

    [OrderedFact("Should send an audio with its thumbnail")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendAudio)]
    public async Task Should_Send_Audio_With_Thumb()
    {
        // Both audio file and its thumbnail should be uploaded
        Message message;
        await using (Stream
                     stream1 = System.IO.File.OpenRead(Constants.PathToFile.Audio.AStateOfDespairMp3),
                     stream2 = System.IO.File.OpenRead(Constants.PathToFile.Thumbnail.TheAbilityToBreak)
                    )
        {
            message = await BotClient.SendAudioAsync(
                new()
                {
                    ChatId = fixture.SupergroupChat,
                    Audio = InputFile.FromStream(stream1, "Ask Again - A State of Despair.mp3"),
                    Thumbnail = InputFile.FromStream(stream2, "thumb.jpg"),
                }
            );
        }

        Assert.NotNull(message.Audio);
        Assert.NotNull(message.Audio.Thumbnail);
        Assert.NotEmpty(message.Audio.Thumbnail.FileId);
        Assert.NotEmpty(message.Audio.Thumbnail.FileUniqueId);
        Assert.Equal(90, message.Audio.Thumbnail.Height);
        Assert.Equal(90, message.Audio.Thumbnail.Width);
        Assert.True(message.Audio.Thumbnail.FileSize > 10_000);
    }

    [OrderedFact("Should send a voice with caption")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendVoice)]
    public async Task Should_Send_Voice()
    {
        const int duration = 24;
        const string caption = "Test Voice in .ogg format";

        Message message;
        await using (Stream stream = System.IO.File.OpenRead(Constants.PathToFile.Audio.TestOgg))
        {
            message = await BotClient.SendVoiceAsync(
                new()
                {
                    ChatId = fixture.SupergroupChat,
                    Voice = InputFile.FromStream(stream),
                    Caption = caption,
                    Duration = duration,
                }
            );
        }

        Assert.Equal(MessageType.Voice, message.Type);
        Assert.Equal(caption, message.Caption);
        Assert.NotNull(message.Voice);
        Assert.Equal(duration, message.Voice.Duration);
        Assert.Equal("audio/ogg", message.Voice.MimeType);
        Assert.NotEmpty(message.Voice.FileId);
        Assert.NotEmpty(message.Voice.FileUniqueId);
        Assert.True(message.Voice.FileSize > 200);
    }
}

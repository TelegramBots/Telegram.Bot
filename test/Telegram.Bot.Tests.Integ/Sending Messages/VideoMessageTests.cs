using System.IO;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Sending_Messages;

[Collection(Constants.TestCollections.SendVideoMessage)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class SendingVideoMessageTests(TestsFixture fixture)
{
    ITelegramBotClient BotClient => fixture.BotClient;

    [OrderedFact("Should send a video with caption")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendVideo)]
    public async Task Should_Send_Video()
    {
        Message message;
        await using (Stream stream = System.IO.File.OpenRead(Constants.PathToFile.Videos.MoonLanding))
        {
            message = await BotClient.SendVideoAsync(
                new()
                {
                    ChatId = fixture.SupergroupChat.Id,
                    Video = InputFile.FromStream(stream, "moon-landing.mp4"),
                    Duration = 104,
                    Width = 320,
                    Height = 240,
                    Caption = "Moon Landing",
                }
            );
        }

        Assert.Equal(MessageType.Video, message.Type);
        Assert.Equal("Moon Landing", message.Caption);
        Assert.NotNull(message.Video);
        Assert.NotEmpty(message.Video.FileId);
        Assert.NotEmpty(message.Video.FileUniqueId);
        Assert.True(message.Video.Duration >= 104);
        Assert.Equal(320, message.Video.Width);
        Assert.Equal(240, message.Video.Height);
        Assert.Equal("video/mp4", message.Video.MimeType);
        Assert.Equal("moon-landing.mp4", message.Video.FileName);
        Assert.NotNull(message.Video.Thumbnail);
        Assert.NotEmpty(message.Video.Thumbnail.FileId);
        Assert.NotEmpty(message.Video.Thumbnail.FileUniqueId);
        Assert.True(message.Video.Thumbnail.FileSize > 200);
        Assert.Equal(320, message.Video.Thumbnail.Width);
        Assert.Equal(240, message.Video.Thumbnail.Height);
        Assert.NotNull(message.Video.Thumbnail.FileSize);
        Assert.InRange((int)message.Video.Thumbnail.FileSize, 600, 900);
    }

    [OrderedFact("Should send a video note")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendVideoNote)]
    public async Task Should_Send_Video_Note()
    {
        Message message;
        await using (Stream stream = System.IO.File.OpenRead(Constants.PathToFile.Videos.GoldenRatio))
        {
            message = await BotClient.SendVideoNoteAsync(
                new()
                {
                    ChatId = fixture.SupergroupChat.Id,
                    VideoNote = InputFile.FromStream(stream),
                    Duration = 28,
                    Length = 240,
                }
            );
        }

        Assert.Equal(MessageType.VideoNote, message.Type);
        Assert.NotNull(message.VideoNote);
        Assert.NotEmpty(message.VideoNote.FileId);
        Assert.NotEmpty(message.VideoNote.FileUniqueId);
        Assert.True(message.VideoNote.Duration >= 28);
        Assert.Equal(240, message.VideoNote.Length);
        Assert.NotNull(message.VideoNote.Thumbnail);
        Assert.NotEmpty(message.VideoNote.Thumbnail.FileId);
        Assert.NotEmpty(message.VideoNote.Thumbnail.FileUniqueId);
        Assert.Equal(240, message.VideoNote.Thumbnail.Width);
        Assert.Equal(240, message.VideoNote.Thumbnail.Height);
        Assert.NotNull(message.VideoNote.Thumbnail.FileSize);
        Assert.InRange((int)message.VideoNote.Thumbnail.FileSize, 1_000, 1_500);
    }

    [OrderedFact("Should send a video with thumbnail")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendDocument)]
    public async Task Should_Send_Video_With_Thumb()
    {
        Message message;
        await using (Stream
                     stream1 = System.IO.File.OpenRead(Constants.PathToFile.Videos.MoonLanding),
                     stream2 = System.IO.File.OpenRead(Constants.PathToFile.Thumbnail.TheAbilityToBreak)
                    )
        {
            message = await BotClient.SendVideoAsync(
                new()
                {
                    ChatId = fixture.SupergroupChat,
                    Video = InputFile.FromStream(stream1),
                    Thumbnail = InputFile.FromStream(stream2, "thumb.jpg"),
                }
            );
        }

        Assert.NotNull(message.Video);
        Assert.NotNull(message.Video.Thumbnail);
        Assert.NotEmpty(message.Video.Thumbnail.FileId);
        Assert.NotEmpty(message.Video.Thumbnail.FileUniqueId);
        Assert.Equal(320, message.Video.Thumbnail.Width);
        Assert.Equal(240, message.Video.Thumbnail.Height);
        Assert.NotNull(message.Video.Thumbnail.FileSize);
        Assert.InRange((long)message.Video.Thumbnail.FileSize, 600, 900);
    }

    [OrderedFact("Should send a video note with thumbnail")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendVideoNote)]
    public async Task Should_Send_Video_Note_With_Thumb()
    {
        Message message;
        await using (Stream
                     stream1 = System.IO.File.OpenRead(Constants.PathToFile.Videos.GoldenRatio),
                     stream2 = System.IO.File.OpenRead(Constants.PathToFile.Thumbnail.Video)
                    )
        {
            message = await BotClient.SendVideoNoteAsync(
                new()
                {
                    ChatId = fixture.SupergroupChat.Id,
                    VideoNote = InputFile.FromStream(stream1),
                    Thumbnail = InputFile.FromStream(stream2, "thumbnail.jpg"),
                }
            );
        }

        Assert.NotNull(message.VideoNote);
        Assert.NotNull(message.VideoNote.Thumbnail);
        Assert.NotEmpty(message.VideoNote.Thumbnail.FileId);
        Assert.NotEmpty(message.VideoNote.Thumbnail.FileUniqueId);
        Assert.Equal(240, message.VideoNote.Thumbnail.Height);
        Assert.Equal(240, message.VideoNote.Thumbnail.Width);
        Assert.NotNull(message.VideoNote.Thumbnail.FileSize);
        Assert.InRange((int)message.VideoNote.Thumbnail.FileSize, 1_000, 1_500);
    }
}

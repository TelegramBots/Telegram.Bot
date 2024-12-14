using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Sending_Messages;

[Collection(Constants.TestCollections.SendAnimationMessage)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class AnimationMessageTests(TestsFixture fixture) : TestClass(fixture)
{
    [OrderedFact("Should send an animation with caption")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendAnimation)]
    public async Task Should_Send_Animation()
    {
        Message message;
        await using (Stream stream = File.OpenRead(Constants.PathToFile.Animation.Earth))
        {
            message = await BotClient.WithStreams(stream).SendAnimation(
                chatId: Fixture.SupergroupChat.Id,
                animation: InputFile.FromStream(stream),
                caption: "<b>Rotating</b> <i>Earth</i>",
                parseMode: ParseMode.Html,
                duration: 4,
                width: 400,
                height: 400,
                thumbnail: null
            );
        }

        // For backwards compatibility, message type is set to Document
        Assert.Equal(MessageType.Animation, message.Type);
        Assert.NotNull(message.Document);
        Assert.NotNull(message.Animation);

        Assert.Equal("Rotating Earth", message.Caption);
        Assert.NotNull(message.CaptionEntities);
        Assert.NotNull(message.CaptionEntityValues);
        Assert.Equal(2, message.CaptionEntities.Length);
        Assert.Equal(2, message.CaptionEntityValues.Count());

        Assert.Equal(4, message.Animation.Duration);

        // Apparently Telegram converts gif to an mp4 with a lower resolution
        Assert.Equal(320, message.Animation.Width);
        Assert.Equal(320, message.Animation.Height);

        Assert.Equal("video/mp4", message.Animation.MimeType);
        Assert.NotEmpty(message.Animation.FileId);
        Assert.NotEmpty(message.Animation.FileUniqueId);
        Assert.NotNull(message.Animation.FileName);
        Assert.NotEmpty(message.Animation.FileName);
        Assert.True(message.Animation.FileSize > 80_000);
    }

    [OrderedFact("Should send an animation with thumbnail")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendAnimation)]
    public async Task Should_Send_Animation_With_Thumb()
    {
        Message message;
        await using (Stream
                     stream1 = File.OpenRead(Constants.PathToFile.Animation.Earth),
                     stream2 = File.OpenRead(Constants.PathToFile.Thumbnail.TheAbilityToBreak)
                    )
        {
            message = await BotClient.WithStreams(stream1, stream2).SendAnimation(
                chatId: Fixture.SupergroupChat,
                animation: InputFile.FromStream(stream1, "earth.gif"),
                thumbnail: InputFile.FromStream(stream2, "thumb.jpg")
            );
        }

        Assert.NotNull(message.Animation);
        Assert.NotNull(message.Animation.Thumbnail);
        Assert.NotEmpty(message.Animation.Thumbnail.FileId);
        Assert.NotEmpty(message.Animation.Thumbnail.FileUniqueId);
        Assert.Equal(320, message.Animation.Thumbnail.Height);
        Assert.Equal(320, message.Animation.Thumbnail.Width);
        Assert.True(message.Animation.Thumbnail.FileSize > 10_000);
    }
}

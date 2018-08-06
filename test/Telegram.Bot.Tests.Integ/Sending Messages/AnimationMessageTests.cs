using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Sending_Messages
{
    [Collection(Constants.TestCollections.SendAnimationMessage)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class AnimationMessageTests
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public AnimationMessageTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldSendAnimation)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendAnimation)]
        public async Task Should_Send_Animation()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendAnimation);

            Message message;
            using (Stream stream = System.IO.File.OpenRead(Constants.FileNames.Animation.Earth))
            {
                message = await BotClient.SendAnimationAsync(
                    /* chatId: */ _fixture.SupergroupChat.Id,
                    /* animation: */ stream,
                    /* duration: */ 4,
                    /* width: */ 400,
                    /* height: */ 400,
                    /* thumb: */ null,
                    /* caption: */ "<b>Rotating</b> <i>Earth</i>",
                    /* parseMode: */ ParseMode.Html
                );
            }

            // For backwards compatiblity, message type is set to Document
            Assert.Equal(MessageType.Document, message.Type);
            Assert.NotNull(message.Document);
            Assert.NotNull(message.Animation);

            Assert.Equal("Rotating Earth", message.Caption);
            Assert.Equal(2, message.CaptionEntities.Length);
            Assert.Equal(2, message.CaptionEntityValues.Count());

            Assert.Equal(4, message.Animation.Duration);
            Assert.Equal(400, message.Animation.Width);
            Assert.Equal(400, message.Animation.Height);
            Assert.Equal("video/mp4", message.Animation.MimeType);
            Assert.NotEmpty(message.Animation.FileId);
            Assert.NotEmpty(message.Animation.FileName);
            Assert.True(message.Animation.FileSize > 80_000);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldSendAnimationWithThumb)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendAnimation)]
        public async Task Should_Send_Animation_With_Thumb()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendAnimationWithThumb);

            Message message;
            using (Stream
                stream1 = System.IO.File.OpenRead(Constants.FileNames.Animation.Earth),
                stream2 = System.IO.File.OpenRead(Constants.FileNames.Thumbnail.TheAbilityToBreak)
            )
            {
                message = await BotClient.SendAnimationAsync(
                    /* chatId: */ _fixture.SupergroupChat,
                    /* animation: */ new InputMedia(stream1, "earth.gif"),
                    thumb: new InputMedia(stream2, "thumb.jpg")
                );
            }

            Assert.NotNull(message.Animation);
            Assert.NotNull(message.Animation.Thumb);
            Assert.NotEmpty(message.Animation.Thumb.FileId);
            Assert.Equal(90, message.Animation.Thumb.Height);
            Assert.Equal(90, message.Animation.Thumb.Width);
            Assert.True(message.Animation.Thumb.FileSize > 10_000);
        }

        private static class FactTitles
        {
            public const string ShouldSendAnimation = "Should send an animation with caption";

            public const string ShouldSendAnimationWithThumb = "Should send an animation with thumbail";
        }
    }
}

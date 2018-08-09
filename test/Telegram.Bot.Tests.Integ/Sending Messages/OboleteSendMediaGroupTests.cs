using System.Threading.Tasks;
using Telegram.Bot.Requests;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Sending_Messages
{
    [Collection(Constants.TestCollections.ObsoleteSendMediaGroup)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class OboleteSendMediaGroupTests
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public OboleteSendMediaGroupTests(TestsFixture testsFixture)
        {
            _fixture = testsFixture;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldSendAlbumUsingArray)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMediaGroup)]
        public async Task Should_Send_Album_Using_Array()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendAlbumUsingArray);

            Message[] messages = await BotClient.SendMediaGroupAsync(
                /* chatId: */ _fixture.SupergroupChat.Id,
                /* media: */ new InputMediaBase[]
                {
                    new InputMediaPhoto
                    {
                        Media = "https://cdn.pixabay.com/photo/2017/06/20/19/22/fuchs-2424369_640.jpg",
                    },
                    new InputMediaPhoto
                    {
                        Media = "https://cdn.pixabay.com/photo/2017/04/11/21/34/giraffe-2222908_640.jpg",
                    },
                    new InputMediaVideo
                    {
                        Media = "https://pixabay.com/en/videos/download/video-7122_medium.mp4",
                    },
                }
            );

            Assert.Equal(3, messages.Length);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldIgnoreInvalidMediaTypes)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMediaGroup)]
        public async Task Should_Ignore_Invalid_Input_Media_Types()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldIgnoreInvalidMediaTypes);

            InputMediaBase[] inputMedia =
            {
                new InputMediaPhoto
                {
                    Media = "https://cdn.pixabay.com/photo/2017/06/20/19/22/fuchs-2424369_640.jpg",
                },
                new InputMediaPhoto
                {
                    Media = "https://cdn.pixabay.com/photo/2017/04/11/21/34/giraffe-2222908_640.jpg",
                },
                new InputMediaDocument("https://www.iana.org/about/presentations/20130512-knight-rrl.pdf"),
            };

            Message[] messages = await BotClient.SendMediaGroupAsync(
                /* chatId: */ _fixture.SupergroupChat.Id,
                /* media: */ inputMedia
            );

            Assert.Equal(2, messages.Length);
            Assert.All(messages, msg => Assert.Equal(MessageType.Photo, msg.Type));
        }

        [OrderedFact(DisplayName = FactTitles.ShouldIgnoreInvalidMediaTypes)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMediaGroup)]
        public async Task Should_Ignore_Invalid_Input_Media_Types2()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldIgnoreInvalidMediaTypes);

            InputMediaBase[] inputMedia =
            {
                new InputMediaPhoto
                {
                    Media = "https://cdn.pixabay.com/photo/2017/06/20/19/22/fuchs-2424369_640.jpg",
                },
                new InputMediaPhoto
                {
                    Media = "https://cdn.pixabay.com/photo/2017/04/11/21/34/giraffe-2222908_640.jpg",
                },
                new InputMediaDocument("https://www.iana.org/about/presentations/20130512-knight-rrl.pdf"),
            };

            SendMediaGroupRequest request = new SendMediaGroupRequest(_fixture.SupergroupChat.Id, inputMedia);

            Message[] messages = await BotClient.MakeRequestAsync(request);

            Assert.Equal(2, messages.Length);
            Assert.All(messages, msg => Assert.Equal(MessageType.Photo, msg.Type));
        }


        private static class FactTitles
        {
            public const string ShouldSendAlbumUsingArray =
                "Should send album using an array on InputMediaBase";

            public const string ShouldIgnoreInvalidMediaTypes =
                "Should ignore input media types other than photo and video";
        }
    }
}

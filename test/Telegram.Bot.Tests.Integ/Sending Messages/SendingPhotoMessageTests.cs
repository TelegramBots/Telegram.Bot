using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Tests.Integ.Framework.Fixtures;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Sending_Messages
{
    [Collection(Constants.TestCollections.SendPhotoMessage)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class SendingPhotoMessageTests : IClassFixture<EntityFixture<Message>>
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        private readonly EntityFixture<Message> _classFixture;

        public SendingPhotoMessageTests(TestsFixture fixture, EntityFixture<Message> classFixture)
        {
            _fixture = fixture;
            _classFixture = classFixture;
        }

        [Fact(DisplayName = FactTitles.ShouldSendPhotoFile)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendPhoto)]
        [ExecutionOrder(1)]
        public async Task Should_Send_Photo_File()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendPhotoFile);

            Message message;
            using (Stream stream = System.IO.File.OpenRead(Constants.FileNames.Photos.Bot))
            {
                message = await BotClient.SendPhotoAsync(
                    chatId: _fixture.SupergroupChat.Id,
                    photo: stream,
                    caption: "👆 This is a\n" +
                             "Telegram Bot"
                );
            }

            Assert.Equal(MessageType.Photo, message.Type);
            Assert.NotEmpty(message.Photo);
            Assert.All(message.Photo.Select(ps => ps.FileId), Assert.NotEmpty);
            Assert.All(message.Photo.Select(ps => ps.Width), w => Assert.NotEqual(default, w));
            Assert.All(message.Photo.Select(ps => ps.Height), h => Assert.NotEqual(default, h));
            Assert.NotNull(message.From);

            _classFixture.Entity = message;
        }

        [Fact(DisplayName = FactTitles.ShouldSendPhotoUsingFileId)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendPhoto)]
        [ExecutionOrder(2)]
        public async Task Should_Send_Photo_FileId()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendPhotoUsingFileId);

            PhotoSize[] uploadedPhoto = _classFixture.Entity.Photo;
            string fileId = uploadedPhoto.First().FileId;

            Message message = await BotClient.SendPhotoAsync(
                chatId: _fixture.SupergroupChat.Id,
                photo: fileId
            );

            Assert.Single(message.Photo, photoSize => photoSize.FileId == fileId);
            Assert.True(JToken.DeepEquals(
                JToken.FromObject(uploadedPhoto), JToken.FromObject(message.Photo)
            ));
        }

        [Fact(DisplayName = FactTitles.ShouldParseMessageCaptionEntitiesIntoValues)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendPhoto)]
        [ExecutionOrder(3)]
        public async Task Should_Parse_Message_Caption_Entities_Into_Values()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldParseMessageCaptionEntitiesIntoValues);

            var entityValueMappings = new(MessageEntityType Type, string Value)[]
            {
                ( MessageEntityType.Hashtag, "#TelegramBots" ),
                ( MessageEntityType.Mention, "@BotFather" ),
                ( MessageEntityType.Url, "http://github.com/TelegramBots" ),
                ( MessageEntityType.Email, "security@telegram.org" ),
                ( MessageEntityType.BotCommand, "/test" ),
                ( MessageEntityType.BotCommand, $"/test@{_fixture.BotUser.Username}" ),
            };

            Message message;
            using (Stream stream = System.IO.File.OpenRead(Constants.FileNames.Photos.Logo))
            {
                message = await BotClient.SendPhotoAsync(
                    chatId: _fixture.SupergroupChat.Id,
                    photo: stream,
                    caption: string.Join("\n", entityValueMappings.Select(tuple => tuple.Value))
                );
            }

            Assert.Equal(
                entityValueMappings.Select(t => t.Type),
                message.CaptionEntities.Select(e => e.Type)
            );
            Assert.Equal(entityValueMappings.Select(t => t.Value), message.CaptionEntityValues);
        }

        private static class FactTitles
        {
            public const string ShouldSendPhotoFile = "Should Send photo using a file";

            public const string ShouldSendPhotoUsingFileId = "Should Send previous photo using its file_id";

            public const string ShouldParseMessageCaptionEntitiesIntoValues = "Should send photo message and parse its caption entity values";
        }
    }
}

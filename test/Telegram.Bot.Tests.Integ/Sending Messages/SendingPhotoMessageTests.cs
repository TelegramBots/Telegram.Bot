using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Requests;
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

        [OrderedFact("Should Send photo using a file")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendPhoto)]
        public async Task Should_Send_Photo_File()
        {
            Message message;
            using (Stream stream = System.IO.File.OpenRead(Constants.PathToFile.Photos.Bot))
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
            Assert.All(message.Photo.Select(ps => ps.FileUniqueId), Assert.NotEmpty);
            Assert.All(message.Photo.Select(ps => ps.Width), w => Assert.NotEqual(default, w));
            Assert.All(message.Photo.Select(ps => ps.Height), h => Assert.NotEqual(default, h));
            Assert.NotNull(message.From);

            _classFixture.Entity = message;
        }

        [OrderedFact("Should Send previous photo using its file_id")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendPhoto)]
        public async Task Should_Send_Photo_FileId()
        {
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

        [OrderedFact("Should send photo message and parse its caption entity values")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendPhoto)]
        public async Task Should_Parse_Message_Caption_Entities_Into_Values()
        {
            (MessageEntityType Type, string Value)[] entityValueMappings =
            {
                (MessageEntityType.PhoneNumber, "+386 12 345 678"),
                (MessageEntityType.Cashtag, "$EUR"),
                (MessageEntityType.Hashtag, "#TelegramBots"),
                (MessageEntityType.Mention, "@BotFather"),
                (MessageEntityType.Url, "http://github.com/TelegramBots"),
                (MessageEntityType.Email, "security@telegram.org"),
                (MessageEntityType.BotCommand, "/test"),
                (MessageEntityType.BotCommand, $"/test@{_fixture.BotUser.Username}"),
            };

            Message message;
            using (Stream stream = System.IO.File.OpenRead(Constants.PathToFile.Photos.Logo))
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

        [OrderedFact("Should send photo with markdown encoded caption")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendPhoto)]
        public async Task Should_Send_Photo_With_Markdown_Encoded_Caption()
        {
            (MessageEntityType Type, string EntityBody, string EncodedEntity)[] entityValueMappings =
            {
                (MessageEntityType.Bold, "bold", "*bold*"),
                (MessageEntityType.Italic, "italic", "_italic_"),
                (MessageEntityType.TextLink, "Text Link", "[Text Link](https://github.com/TelegramBots)"),
            };

            Message message;
            using (Stream stream = System.IO.File.OpenRead(Constants.PathToFile.Photos.Logo))
            {
                message = await BotClient.SendPhotoAsync(
                    chatId: _fixture.SupergroupChat.Id,
                    photo: stream,
                    caption: string.Join("\n", entityValueMappings.Select(tuple => tuple.EncodedEntity)),
                    parseMode: ParseMode.Markdown
                );
            }

            Assert.Equal(
                entityValueMappings.Select(t => t.Type),
                message.CaptionEntities.Select(e => e.Type)
            );
            Assert.Equal(entityValueMappings.Select(t => t.EntityBody), message.CaptionEntityValues);
        }

        [OrderedFact("Should deserialize a sendPhoto request from JSON and send it")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendPhoto)]
        public async Task Should_Send_Deserialized_Photo_Request()
        {
            string json = $@"{{
                chat_id: ""{_fixture.SupergroupChat.Id}"",
                photo: ""https://cdn.pixabay.com/photo/2017/04/11/21/34/giraffe-2222908_640.jpg"",
                caption: ""Photo request deserialized from JSON"",
            }}";

            SendPhotoRequest request = JsonConvert.DeserializeObject<SendPhotoRequest>(json);

            Message message = await BotClient.MakeRequestAsync(request);

            Assert.Equal(MessageType.Photo, message.Type);
        }
    }
}

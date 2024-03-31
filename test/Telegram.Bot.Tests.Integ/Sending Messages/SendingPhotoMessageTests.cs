using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Telegram.Bot.Requests;
using Telegram.Bot.Serialization;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Tests.Integ.Framework.Fixtures;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Sending_Messages;

[Collection(Constants.TestCollections.SendPhotoMessage)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class SendingPhotoMessageTests(TestsFixture fixture, EntityFixture<Message> classFixture)
    : IClassFixture<EntityFixture<Message>>
{
    ITelegramBotClient BotClient => fixture.BotClient;

    [OrderedFact("Should Send photo using a file")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendPhoto)]
    public async Task Should_Send_Photo_File()
    {
        await using Stream stream = System.IO.File.OpenRead(Constants.PathToFile.Photos.Bot);
        Message message = await BotClient.SendPhotoAsync(
            new()
            {
                ChatId = fixture.SupergroupChat.Id,
                Photo = InputFile.FromStream(stream),
                Caption = "👆 This is a\nTelegram Bot",
            }
        );

        Assert.Equal(MessageType.Photo, message.Type);
        Assert.NotNull(message.Photo);
        Assert.NotEmpty(message.Photo);
        Assert.All(message.Photo.Select(ps => ps.FileId), Assert.NotEmpty);
        Assert.All(message.Photo.Select(ps => ps.FileUniqueId), Assert.NotEmpty);
        Assert.All(message.Photo.Select(ps => ps.Width), w => Assert.NotEqual(default, w));
        Assert.All(message.Photo.Select(ps => ps.Height), h => Assert.NotEqual(default, h));
        Assert.NotNull(message.From);

        classFixture.Entity = message;
    }

    [OrderedFact("Should Send previous photo using its file_id")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendPhoto)]
    public async Task Should_Send_Photo_FileId()
    {
        string fileId = classFixture.Entity.Photo!.First().FileId;

        Message message = await BotClient.SendPhotoAsync(
            new()
            {
                ChatId = fixture.SupergroupChat.Id,
                Photo = InputFile.FromFileId(fileId),
            }
        );

        // Apparently file ids of photos no longer remain the same when sending them
        // using file ids
        // Assert.Single(message.Photo, photoSize => photoSize.FileId == fileId);
        Assert.NotNull(message.Photo);
        Assert.NotEmpty(message.Photo);
    }

    [OrderedFact("Should send photo message and parse its caption entity values")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendPhoto)]
    public async Task Should_Parse_Message_Caption_Entities_Into_Values()
    {
        (MessageEntityType Type, string Value)[] entityValueMappings =
        [
            (MessageEntityType.PhoneNumber, "+38612345678"),
            (MessageEntityType.Cashtag, "$EUR"),
            (MessageEntityType.Hashtag, "#TelegramBots"),
            (MessageEntityType.Mention, "@BotFather"),
            (MessageEntityType.Url, "https://github.com/TelegramBots"),
            (MessageEntityType.Email, "security@telegram.org"),
            (MessageEntityType.BotCommand, "/test"),
            (MessageEntityType.BotCommand, $"/test@{fixture.BotUser.Username}")
        ];

        await using Stream stream = System.IO.File.OpenRead(Constants.PathToFile.Photos.Logo);
        Message message = await BotClient.SendPhotoAsync(
            new()
            {
                ChatId = fixture.SupergroupChat.Id,
                Photo = InputFile.FromStream(stream),
                Caption = string.Join("\n", entityValueMappings.Select(tuple => tuple.Value)),
            }
        );

        Assert.NotNull(message.CaptionEntities);
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
        [
            (MessageEntityType.Bold, "bold", "*bold*"),
            (MessageEntityType.Italic, "italic", "_italic_"),
            (MessageEntityType.TextLink, "Text Link", "[Text Link](https://github.com/TelegramBots)")
        ];

        await using Stream stream = System.IO.File.OpenRead(Constants.PathToFile.Photos.Logo);
        Message message = await BotClient.SendPhotoAsync(
            new()
            {
                ChatId = fixture.SupergroupChat.Id,
                Photo = InputFile.FromStream(stream),
                Caption = string.Join("\n", entityValueMappings.Select(tuple => tuple.EncodedEntity)),
                ParseMode = ParseMode.Markdown,
            }
        );

        Assert.NotNull(message.CaptionEntities);
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
        string json =
            $$"""
            {
                "chat_id": "{{fixture.SupergroupChat.Id}}",
                "photo": "https://cdn.pixabay.com/photo/2017/04/11/21/34/giraffe-2222908_640.jpg",
                "caption": "Photo request deserialized from JSON"
            }
            """;

        SendPhotoRequest request = JsonSerializer.Deserialize<SendPhotoRequest>(json, JsonSerializerOptionsProvider.Options);

        Message message = await BotClient.MakeRequestAsync(request);

        Assert.Equal(MessageType.Photo, message.Type);
    }
}

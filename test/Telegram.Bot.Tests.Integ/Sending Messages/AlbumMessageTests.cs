using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Tests.Integ.Framework.Fixtures;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Sending_Messages;

[Collection(Constants.TestCollections.AlbumMessage)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class AlbumMessageTests(TestsFixture testsFixture, EntitiesFixture<Message> classFixture)
    : IClassFixture<EntitiesFixture<Message>>
{
    ITelegramBotClient BotClient => testsFixture.BotClient;

    [OrderedFact("Should upload 2 photos with captions and send them in an album")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMediaGroup)]
    public async Task Should_Upload_2_Photos_Album()
    {
        Message[] messages;
        await using (Stream
                     stream1 = System.IO.File.OpenRead(Constants.PathToFile.Photos.Logo),
                     stream2 = System.IO.File.OpenRead(Constants.PathToFile.Photos.Bot)
                    )
        {
            messages = await BotClient.SendMediaGroupAsync(
                new()
                {
                    ChatId = testsFixture.SupergroupChat.Id,
                    Media = [
                        new InputMediaPhoto
                        {
                            Media = InputFile.FromStream(stream1, "logo.png"),
                            Caption = "Logo",
                        },
                        new InputMediaPhoto
                        {
                            Media = InputFile.FromStream(stream2, "bot.gif"),
                            Caption = "Bot",
                        },
                    ],
                    DisableNotification = true,
                }
            );
        }

        Assert.Equal(2, messages.Length);
        Assert.All(messages, msg => Assert.Equal(MessageType.Photo, msg.Type));

        // All media messages have the same mediaGroupId
        Assert.NotEmpty(messages.Select(m => m.MediaGroupId));
        Assert.Single(messages.Select(msg => msg.MediaGroupId).Distinct());

        Assert.Equal("Logo", messages[0].Caption);
        Assert.Equal("Bot", messages[1].Caption);

        classFixture.Entities = messages.ToList();
    }

    [OrderedFact("Should send an album with 3 photos using their file_id")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMediaGroup)]
    public async Task Should_Send_3_Photos_Album_Using_FileId()
    {
        // Take file_id of photos uploaded in previous test case
        string[] fileIds = classFixture.Entities
            .Select(msg => msg.Photo!.First().FileId)
            .ToArray();

        Message[] messages = await BotClient.SendMediaGroupAsync(
            new()
            {
                ChatId = testsFixture.SupergroupChat.Id,
                Media = [
                    new InputMediaPhoto { Media = InputFile.FromFileId(fileIds[0])},
                    new InputMediaPhoto { Media = InputFile.FromFileId(fileIds[1])},
                    new InputMediaPhoto { Media = InputFile.FromFileId(fileIds[0])},
                ]
            }
        );

        Assert.Equal(3, messages.Length);
        Assert.All(messages, msg => Assert.Equal(MessageType.Photo, msg.Type));
    }

    [OrderedFact("Should send an album using HTTP urls in reply to 1st album message")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMediaGroup)]
    public async Task Should_Send_Photo_Album_Using_Url()
    {
        // ToDo add exception: Bad Request: failed to get HTTP URL content
        int replyToMessageId = classFixture.Entities.First().MessageId;

        Message[] messages = await BotClient.SendMediaGroupAsync(
            new()
            {
                ChatId = testsFixture.SupergroupChat.Id,
                Media = [
                    new InputMediaPhoto { Media = InputFile.FromUri("https://cdn.pixabay.com/photo/2017/06/20/19/22/fuchs-2424369_640.jpg")},
                    new InputMediaPhoto { Media = InputFile.FromUri("https://cdn.pixabay.com/photo/2017/04/11/21/34/giraffe-2222908_640.jpg")},
                ],
                ReplyParameters = new() { MessageId = replyToMessageId }
            }
        );

        Assert.Equal(2, messages.Length);
        Assert.All(messages, message => Assert.Equal(MessageType.Photo, message.Type));
        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        Assert.All(messages, message =>
        {
            Assert.NotNull(message.ReplyToMessage);
            Assert.Equal(replyToMessageId, message.ReplyToMessage.MessageId);
        });
    }

    [OrderedFact("Should upload 2 videos and a photo with captions and send them in an album")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMediaGroup)]
    public async Task Should_Upload_2_Videos_Album()
    {
        Message[] messages;
        await using (Stream
                     stream0 = System.IO.File.OpenRead(Constants.PathToFile.Videos.GoldenRatio),
                     stream1 = System.IO.File.OpenRead(Constants.PathToFile.Videos.MoonLanding),
                     stream2 = System.IO.File.OpenRead(Constants.PathToFile.Photos.Bot)
                    )
        {
            messages = await BotClient.SendMediaGroupAsync(
                new()
                {
                    ChatId = testsFixture.SupergroupChat.Id,
                    Media = [
                        new InputMediaVideo
                        {
                            Media = InputFile.FromStream(stream0, "GoldenRatio.mp4"),
                            Caption = "Golden Ratio",
                            Height = 240,
                            Width = 240,
                            Duration = 28,
                        },
                        new InputMediaVideo
                        {
                            Media = InputFile.FromStream(stream1, "MoonLanding.mp4"),
                            Caption = "Moon Landing"
                        },
                        new InputMediaPhoto
                        {
                            Media = InputFile.FromStream(stream2, "bot.gif"),
                            Caption = "Bot"
                        },
                    ],
                }
            );
        }

        Assert.Equal(3, messages.Length);

        Message videoMessage1 = messages[0];
        Message videoMessage2 = messages[1];
        Message photoMessage = messages[2];

        Assert.NotNull(videoMessage1.Video);
        Assert.NotNull(videoMessage2.Video);
        Assert.NotNull(photoMessage.Photo);

        Assert.Equal(MessageType.Video, messages[0].Type);
        Assert.Equal("Golden Ratio", messages[0].Caption);
        Assert.Equal(240, videoMessage1.Video.Width);
        Assert.Equal(240, videoMessage1.Video.Height);
        Assert.InRange(videoMessage1.Video.Duration, 28 - 2, 28 + 2);

        Assert.Equal(MessageType.Video, videoMessage2.Type);
        Assert.Equal("Moon Landing", videoMessage2.Caption);

        Assert.Equal(MessageType.Photo, photoMessage.Type);
        Assert.Equal("Bot", photoMessage.Caption);
    }

    [OrderedFact("Should upload 2 photos with markdown encoded captions and send them in an album")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMediaGroup)]
    public async Task Should_Upload_2_Photos_Album_With_Markdown_Encoded_Captions()
    {
        await using Stream
            stream1 = System.IO.File.OpenRead(Constants.PathToFile.Photos.Logo),
            stream2 = System.IO.File.OpenRead(Constants.PathToFile.Photos.Bot);

        Message[] messages = await BotClient.SendMediaGroupAsync(
            new()
            {
                ChatId = testsFixture.SupergroupChat.Id,
                Media = [
                    new InputMediaPhoto
                    {
                        Media = InputFile.FromStream(stream1, "logo.png"),
                        Caption = "*Logo*",
                        ParseMode = ParseMode.Markdown
                    },
                    new InputMediaPhoto
                    {
                        Media = InputFile.FromStream(stream2, "bot.gif"),
                        Caption = "_Bot_",
                        ParseMode = ParseMode.Markdown
                    },
                ],
            }
        );

        Message message1 = messages[0];
        Message message2 = messages[1];

        Assert.NotNull(message1.CaptionEntities);
        Assert.NotNull(message1.CaptionEntityValues);
        Assert.NotNull(message2.CaptionEntities);
        Assert.NotNull(message2.CaptionEntityValues);


        Assert.Equal("Logo", message1.CaptionEntityValues.Single());
        Assert.Equal(MessageEntityType.Bold, message1.CaptionEntities.Single().Type);

        Assert.Equal("Bot", message2.CaptionEntityValues.Single());
        Assert.Equal(MessageEntityType.Italic, message2.CaptionEntities.Single().Type);
    }

    [OrderedFact("Should send a video with thumbnail in an album")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMediaGroup)]
    public async Task Should_Video_With_Thumbnail_In_Album()
    {
        await using Stream
            stream1 = System.IO.File.OpenRead(Constants.PathToFile.Videos.GoldenRatio),
            stream2 = System.IO.File.OpenRead(Constants.PathToFile.Thumbnail.Video);

        Message[] messages = await BotClient.SendMediaGroupAsync(
            new()
            {
                ChatId = testsFixture.SupergroupChat.Id,
                Media = [
                    new InputMediaVideo
                    {
                        Media = InputFile.FromStream(stream1, "GoldenRatio.mp4"),
                        Thumbnail = InputFile.FromStream(stream2, "thumbnail.jpg"),
                        SupportsStreaming = true,
                    },
                    new InputMediaPhoto
                    {
                        Media = InputFile.FromUri("https://cdn.pixabay.com/photo/2017/04/11/21/34/giraffe-2222908_640.jpg"),
                    },
                ],
            }
        );

        Assert.Equal(MessageType.Video, messages[0].Type);
        Assert.NotNull(messages[0].Video);
        Assert.NotNull(messages[0].Video.Thumbnail);
    }
}

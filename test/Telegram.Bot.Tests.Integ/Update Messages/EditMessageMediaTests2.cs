using System;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Update_Messages;

[Collection(Constants.TestCollections.EditMessageMedia2)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class EditMessageMediaTests2(TestsFixture fixture) : TestClass(fixture)
{
    [OrderedFact("Should change a message's video to a document file with thumbnail")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendVideo)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditMessageMedia)]
    public async Task Should_Edit_Message_Video()
    {
        // Send a video to chat. This media will be changed later in test.
        Message originalMessage;
        await using (Stream stream = File.OpenRead(Constants.PathToFile.Animation.Earth))
        {
            originalMessage = await BotClient.WithStreams(stream).SendVideo(
                chatId: Fixture.SupergroupChat,
                video: InputFile.FromStream(stream),
                caption: "This message will be edited shortly"
            );
        }

        await Task.Delay(500);

        // Replace video with a document+thumb by uploading the new file
        Message editedMessage;
        await using Stream
            documentStream = File.OpenRead(Constants.PathToFile.Certificate.PublicKey),
            thumbStream = File.OpenRead(Constants.PathToFile.Thumbnail.Video);
        {
            editedMessage = await BotClient.WithStreams(documentStream, thumbStream).EditMessageMedia(
                chatId: originalMessage.Chat,
                messageId: originalMessage.Id,
                media: new InputMediaDocument
                {
                    Media = InputFile.FromStream(documentStream, "public-key.pem.txt"),
                    Thumbnail = InputFile.FromStream(thumbStream, "thumb.jpg"),
                    Caption = "**Public** key in `.pem` format",
                    ParseMode = ParseMode.Markdown,
                }
            );
        }

        Assert.Equal(originalMessage.Id, editedMessage.Id);
        Assert.Equal(MessageType.Document, editedMessage.Type);
        Assert.NotNull(editedMessage.Document);
        Assert.Null(editedMessage.Video);
        Assert.Equal("public-key.pem.txt", editedMessage.Document.FileName);
    }

    [OrderedFact("Should change a message's photo to an animation having thumbnail")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendDocument)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendPhoto)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditMessageMedia)]
    public async Task Should_Edit_Message_Photo()
    {
        // Upload a GIF file to Telegram servers and obtain its file_id. This file_id will be used later in test.
        Message gifMessage = await BotClient.SendDocument(
            chatId: Fixture.SupergroupChat,
            document: InputFile.FromUri(new Uri("https://upload.wikimedia.org/wikipedia/commons/2/2c/Rotating_earth_%28large%29.gif")),
            caption: "`file_id` of this GIF will be used"
        );

        Assert.NotNull(gifMessage.Document);

        // Send a photo to chat. This media will be changed later in test.
        Message originalMessage = await BotClient.SendPhoto(
            chatId: Fixture.SupergroupChat,
            photo: InputFile.FromUri(new Uri("https://cdn.pixabay.com/photo/2017/08/30/12/45/girl-2696947_640.jpg")),
            caption: "This message will be edited shortly"
        );

        await Task.Delay(500);

        // Replace audio with another audio by uploading the new file. A thumbnail image is also uploaded.
        await using Stream thumbStream = File.OpenRead(Constants.PathToFile.Thumbnail.Video);
        Message editedMessage = await BotClient.WithStreams(thumbStream).EditMessageMedia(
            chatId: originalMessage.Chat,
            messageId: originalMessage.Id,
            media: new InputMediaAnimation
            {
                Media = InputFile.FromFileId(gifMessage.Document.FileId),
                Thumbnail = InputFile.FromStream(thumbStream, "thumb.jpg"),
                Duration = 4,
                Height = 320,
                Width = 320,
            }
        );

        Assert.Equal(originalMessage.Id, editedMessage.Id);

        // For backward compatibility, when this field is set, the document field will also be set.
        // In that case, message type is still considered as Document.
        Assert.Equal(MessageType.Animation, editedMessage.Type);
        Assert.NotNull(editedMessage.Document);
        Assert.NotNull(editedMessage.Animation);

        Assert.NotEqual(0, editedMessage.Animation.Duration);
        Assert.NotEqual(0, editedMessage.Animation.Width);
        Assert.NotEqual(0, editedMessage.Animation.Height);
        Assert.NotEqual(0, editedMessage.Animation.FileSize);
        Assert.NotEmpty(editedMessage.Animation.FileId);
        Assert.NotNull(editedMessage.Animation.FileName);
        Assert.NotEmpty(editedMessage.Animation.FileName);
        Assert.NotNull(editedMessage.Animation.MimeType);
        Assert.NotEmpty(editedMessage.Animation.MimeType);

        Assert.NotNull(editedMessage.Animation.Thumbnail);
        Assert.NotEmpty(editedMessage.Animation.Thumbnail.FileId);
        Asserts.JsonEquals(editedMessage.Animation.Thumbnail, editedMessage.Document.Thumbnail);
    }
}

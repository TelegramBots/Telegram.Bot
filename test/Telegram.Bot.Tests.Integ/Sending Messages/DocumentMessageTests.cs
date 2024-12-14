using System.IO;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Sending_Messages;

[Collection(Constants.TestCollections.SendDocumentMessage)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class SendingDocumentMessageTests(TestsFixture fixture) : TestClass(fixture)
{
    [OrderedFact("Should send a pdf document with caption")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendDocument)]
    public async Task Should_Send_Pdf_Document()
    {
        await using Stream stream = File.OpenRead(Constants.PathToFile.Documents.Hamlet);
        Message message = await BotClient.WithStreams(stream).SendDocument(
            chatId: Fixture.SupergroupChat.Id,
            document: InputFile.FromStream(stream, "HAMLET.pdf"),
            caption: "The Tragedy of Hamlet,\nPrince of Denmark"
        );

        Assert.Equal(MessageType.Document, message.Type);
        Assert.NotNull(message.Document);
        Assert.Equal("HAMLET.pdf", message.Document.FileName);
        Assert.Equal("application/pdf", message.Document.MimeType);
        Assert.NotNull(message.Document.FileSize);

        Assert.InRange((int)message.Document.FileSize, 253_000, 257_000);
        Assert.NotEmpty(message.Document.FileId);
        Assert.NotEmpty(message.Document.FileUniqueId);
        Assert.Equal("The Tragedy of Hamlet,\nPrince of Denmark", message.Caption);
    }

    // ReSharper disable StringLiteralTypo
    [OrderedFact("Should send a pdf document having a Farsi(non-ASCII) file name")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendDocument)]
    public async Task Should_Send_Document_With_Farsi_Name()
    {
        await using Stream stream = File.OpenRead(Constants.PathToFile.Documents.Hamlet);
        Message message = await BotClient.WithStreams(stream).SendDocument(
            chatId: Fixture.SupergroupChat.Id,
            document: InputFile.FromStream(stream, "هملت.pdf"),
            caption: "تراژدی هملت\nشاهزاده دانمارک"
        );

        Assert.Equal(MessageType.Document, message.Type);
        Assert.NotNull(message.Document);
        Assert.Equal("هملت.pdf", message.Document.FileName);
        Assert.Equal("application/pdf", message.Document.MimeType);
        Assert.NotNull(message.Document.FileSize);
        Assert.InRange((int)message.Document.FileSize, 253_000, 257_000);
        Assert.NotEmpty(message.Document.FileId);
        Assert.NotEmpty(message.Document.FileUniqueId);
        Assert.Equal("تراژدی هملت\nشاهزاده دانمارک", message.Caption);
    }

    [OrderedFact("Should send a pdf document with thumbnail")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendDocument)]
    public async Task Should_Send_Document_With_Thumb()
    {
        await using Stream
            documentStream = File.OpenRead(Constants.PathToFile.Documents.Hamlet),
            thumbStream = File.OpenRead(Constants.PathToFile.Thumbnail.TheAbilityToBreak);

        Message message = await BotClient.WithStreams(documentStream, thumbStream).SendDocument(
            chatId: Fixture.SupergroupChat,
            document: InputFile.FromStream(documentStream, "Hamlet.pdf"),
            thumbnail: InputFile.FromStream(thumbStream, "thumb.jpg")
        );

        Assert.NotNull(message.Document);
        Assert.NotNull(message.Document.Thumbnail);
        Assert.NotEmpty(message.Document.Thumbnail.FileId);
        Assert.NotEmpty(message.Document.Thumbnail.FileUniqueId);
        Assert.Equal(90, message.Document.Thumbnail.Height);
        Assert.Equal(90, message.Document.Thumbnail.Width);
        Assert.NotNull(message.Document.Thumbnail.FileSize);
        Assert.InRange((int)message.Document.Thumbnail.FileSize, 11_000, 12_000);

        Assert.Equal(MessageType.Document, message.Type);
        Assert.Equal("Hamlet.pdf", message.Document.FileName);
        Assert.Equal("application/pdf", message.Document.MimeType);
        Assert.NotNull(message.Document.FileSize);
        Assert.InRange(message.Document.FileSize.Value, 253_000, 257_000);
        Assert.NotEmpty(message.Document.FileId);
        Assert.NotEmpty(message.Document.FileUniqueId);
        Assert.Null(message.Caption);
    }
}

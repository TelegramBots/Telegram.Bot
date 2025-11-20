using System.IO;
using System.Threading.Tasks;
using Telegram.Bot.Extensions;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Tests.Integ.Framework.Fixtures;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Sending_Messages;

[Collection(Constants.TestCollections.SendHtmlMessage)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class HtmlMessageTests(TestsFixture fixture, EntityFixture<Message> classFixture)
    : TestClass(fixture), IClassFixture<EntityFixture<Message>>
{
    [OrderedFact("Send a text message with inline keyboard")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendHtml)]
    public async Task Send_Text()
    {
        var msgs = await BotClient.SendHtml(Fixture.SupergroupChat.Id, """
                Try the new <code>SendHtml</code> method...
                It is <tg-spoiler>awesome!!</tg-spoiler>

                <keyboard>
                <button text="URL button" url="https://github.com/TelegramBots/Telegram.Bot/blob/master/test/Telegram.Bot.Tests.Integ/Sending%20Messages/HtmlMessageTests.cs">
                <button text="Callback btn" callback="data">
                <row>
                <button text="Copy btn" copy="Some Text">
                <button text="Switch inline" switch_inline="query" target="user,bot">
                </row>
                </keyboard>
                """);
        Assert.Single(msgs);
        Assert.Equal(MessageType.Text, msgs[0].Type);
    }

    [OrderedFact("Send a photo via URL with caption")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendHtml)]
    public async Task Send_Photo()
    {
        var msgs = await BotClient.SendHtml(Fixture.SupergroupChat.Id, """
                <img src="https://cdn.pixabay.com/photo/2017/04/11/21/34/giraffe-2222908_640.jpg">
                <u>Giraffe</u>
                """);
        Assert.Single(msgs);
        Assert.Equal(MessageType.Photo, msgs[0].Type);
        classFixture.Entity = msgs[0];
    }

    [OrderedFact("Send a photo via FileID with caption above")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendHtml)]
    public async Task Send_Photo_FileId_CaptionAbove()
    {
        var msgs = await BotClient.SendHtml(Fixture.SupergroupChat.Id, $"""
                <b><i>Giraffe</i></b>
                <img src="{classFixture.Entity.Photo[^1].FileId}">
                """);
        Assert.Single(msgs);
        Assert.Equal(MessageType.Photo, msgs[0].Type);
    }

    [OrderedFact("Send a video via URL with spoiler, no caption")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendHtml)]
    public async Task Send_Video_Spoiler()
    {
        var msgs = await BotClient.SendHtml(Fixture.SupergroupChat.Id, """
                <video src="https://cdn.pixabay.com/video/2017/07/19/10737-226624883_medium.mp4" spoiler>
                """);
        Assert.Single(msgs);
        Assert.Equal(MessageType.Video, msgs[0].Type);
        Assert.Null(msgs[0].Caption);
        Assert.True(msgs[0].HasMediaSpoiler);
    }

    [OrderedFact("Send an album via URL and FileId in reply to 1st message")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendHtml)]
    public async Task Send_Photo_Album()
    {
        int replyToMessageId = classFixture.Entity.Id;
        var msgs = await BotClient.SendHtml(Fixture.SupergroupChat.Id, $"""
                <img src="https://cdn.pixabay.com/photo/2017/06/20/19/22/fuchs-2424369_640.jpg">
                Single caption attached to first media
                <img src="{classFixture.Entity.Photo[^1].FileId}">
                """,
            replyParameters: new() { MessageId = replyToMessageId }
        );
        Assert.Equal(2, msgs.Length);
        Assert.Equal(MessageType.Photo, msgs[0].Type);
        Assert.NotNull(msgs[0].ReplyToMessage);
        Assert.Equal(replyToMessageId, msgs[0].ReplyToMessage.Id);
    }

    [OrderedFact("Send an album with caption above photo & video")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendHtml)]
    public async Task Send_Album_CaptionAbove()
    {
        var msgs = await BotClient.SendHtml(Fixture.SupergroupChat.Id, """
                Single caption above medias
                <img src="https://cdn.pixabay.com/photo/2017/06/20/19/22/fuchs-2424369_640.jpg" spoiler>
                <video src="https://pixabay.com/en/videos/download/video-10737_medium.mp4">
                """);
        Assert.Equal(2, msgs.Length);
        Assert.Equal(MessageType.Photo, msgs[0].Type);
        Assert.True(msgs[0].ShowCaptionAboveMedia);
    }

    [OrderedFact("Send an album with multiple captions")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendHtml)]
    public async Task Send_Album_MultiCaption()
    {
        var msgs = await BotClient.SendHtml(Fixture.SupergroupChat.Id, """
                <img src="https://cdn.pixabay.com/photo/2017/06/20/19/22/fuchs-2424369_640.jpg">
                <b><i>Fox</i></b>
                <img src="https://cdn.pixabay.com/photo/2017/04/11/21/34/giraffe-2222908_640.jpg">
                <u>Giraffe</u>
                """);
        Assert.Equal(2, msgs.Length);
        Assert.Equal(MessageType.Photo, msgs[0].Type);
    }

    [OrderedFact("Send an album with audio files")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendHtml)]
    public async Task Send_Audio_Files()
    {
        var msgs = await BotClient.SendHtml(Fixture.SupergroupChat.Id, """
                <file src="https://upload.wikimedia.org/wikipedia/commons/transcoded/b/bb/Test_ogg_mp3_48kbps.wav/Test_ogg_mp3_48kbps.wav.mp3">
                Caption attached to 1st file
                <file src="https://upload.wikimedia.org/wikipedia/commons/transcoded/b/bb/Test_ogg_mp3_48kbps.wav/Test_ogg_mp3_48kbps.wav.mp3">
                Caption attached to 2nd file
                """);
        Assert.Equal(2, msgs.Length);
        Assert.Equal(MessageType.Audio, msgs[0].Type);
    }

    [OrderedFact("Send an album with files")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendHtml)]
    public async Task Send_Multi_Files()
    {
        var msgs = await BotClient.SendHtml(Fixture.SupergroupChat.Id, """
                <file src="https://www.w3.org/WAI/ER/tests/xhtml/testfiles/resources/pdf/dummy.pdf">
                <file src="https://www.w3.org/WAI/ER/tests/xhtml/testfiles/resources/pdf/dummy.pdf">
                <file src="https://www.w3.org/WAI/ER/tests/xhtml/testfiles/resources/pdf/dummy.pdf">
                Caption attached to last file
                """);
        Assert.Equal(3, msgs.Length);
        Assert.Equal(MessageType.Document, msgs[0].Type);
    }

    [OrderedFact("Send a photo album via 2 streams")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendHtml)]
    public async Task Send_Uploaded_Photo_Album()
    {
        await using var stream0 = File.OpenRead(Constants.PathToFile.Photos.Logo);
        await using var stream1 = File.OpenRead(Constants.PathToFile.Photos.Bot);
        var msgs = await BotClient.SendHtml(Fixture.SupergroupChat.Id, """
                <img src="bot.gif">
                <img src="stream://0">
                """, streams: [stream0, stream1]);
        Assert.Equal(2, msgs.Length);
        Assert.Equal(MessageType.Photo, msgs[0].Type);
    }

    [OrderedFact("Send a text message with a custom preview")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendHtml)]
    public async Task Send_Custom_Preview()
    {
        var msgs = await BotClient.SendHtml(Fixture.SupergroupChat.Id, """
                Hello, world
                <preview url="https://github.com/TelegramBots/book" small above>
                """);
        Assert.Single(msgs);
        Assert.Equal(MessageType.Text, msgs[0].Type);
        Asserts.JsonEquals(new LinkPreviewOptions() { Url = "https://github.com/TelegramBots/book", PreferSmallMedia = true, ShowAboveText = true }, msgs[0].LinkPreviewOptions);
    }
}

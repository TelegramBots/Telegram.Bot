using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Inline_Mode;

[Collection(Constants.TestCollections.InlineQuery)]
[Trait(Constants.CategoryTraitName, Constants.InteractiveCategoryValue)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class InlineQueryTests(TestsFixture fixture) : TestClass(fixture)
{
    [OrderedFact("Should answer inline query with an article")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
    public async Task Should_Answer_Inline_Query_With_Article()
    {
        await Fixture.SendTestInstructionsAsync(
            "1. Start an inline query\n" +
            "2. Wait for bot to answer it\n" +
            "3. Choose the answer",
            startInlineQuery: true
        );

        // Wait for tester to start an inline query
        // This looks for an Update having a value on "inline_query" field
        Update iqUpdate = await Fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        // Prepare results of the query
        InlineQueryResult[] results =
        [
            new InlineQueryResultArticle
            {
                Id = "article:bot-api",
                Title = "Telegram Bot API",
                InputMessageContent = new InputTextMessageContent { MessageText = "https://core.telegram.org/bots/api" },
                Description = "The Bot API is an HTTP-based interface created for developers",
            }
        ];

        // Answer the query
        await BotClient.AnswerInlineQuery(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        // Wait for tester to choose a result and send it(as a message) to the chat
        (
            Update messageUpdate,
            Update chosenResultUpdate
        ) = await Fixture.UpdateReceiver.GetInlineQueryResultUpdates(
            chatId: Fixture.SupergroupChat.Id,
            messageType: MessageType.Text
        );

        Assert.Equal(MessageType.Text, messageUpdate.Message!.Type);
        Assert.Equal("article:bot-api", chosenResultUpdate.ChosenInlineResult!.ResultId);
        Assert.Equal(iqUpdate.InlineQuery.Query, chosenResultUpdate.ChosenInlineResult.Query);
    }

    [OrderedFact("Should get message from an inline query with ViaBot property set")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
    public async Task Should_Get_Message_From_Inline_Query_With_ViaBot()
    {
        await Fixture.SendTestInstructionsAsync(
            "1. Start an inline query\n" +
            "2. Wait for bot to answer it\n" +
            "3. Choose the answer",
            startInlineQuery: true
        );

        Update iqUpdate = await Fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        InlineQueryResult[] results =
        [
            new InlineQueryResultArticle
            {
                Id = "article:bot-api",
                Title = "Telegram Bot API",
                InputMessageContent = new InputTextMessageContent { MessageText = "https://core.telegram.org/bots/api" },
                Description = "The Bot API is an HTTP-based interface created for developers",
            }
        ];

        await BotClient.AnswerInlineQuery(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        Update messageUpdate = await Fixture.UpdateReceiver.GetUpdateAsync(
            predicate: update => update.Message!.ViaBot is not null,
            updateTypes: [UpdateType.Message]
        );

        Assert.NotNull(messageUpdate.Message);
        Assert.Equal(MessageType.Text, messageUpdate.Message.Type);
        Assert.NotNull(messageUpdate.Message.ViaBot);
        Assert.Equal(Fixture.BotUser.Id, messageUpdate.Message.ViaBot.Id);
    }

    [OrderedFact("Should answer inline query with a contact")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
    public async Task Should_Answer_Inline_Query_With_Contact()
    {
        await Fixture.SendTestInstructionsAsync(
            "Staring the inline query with this message...",
            startInlineQuery: true
        );

        Update iqUpdate = await Fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "contact:john-doe";
        InlineQueryResult[] results =
        [
            new InlineQueryResultContact
            {
                Id = resultId,
                PhoneNumber = "+1234567",
                FirstName = "John",
                LastName = "Doe"
            }
        ];

        await BotClient.AnswerInlineQuery(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await Fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: Fixture.SupergroupChat.Id,
                messageType: MessageType.Contact
            );
        Update resultUpdate = chosenResultUpdate;

        Assert.Equal(MessageType.Contact, messageUpdate.Message!.Type);
        Assert.Equal(resultId, resultUpdate.ChosenInlineResult!.ResultId);
        Assert.Equal(iqUpdate.InlineQuery.Query, resultUpdate.ChosenInlineResult.Query);
    }

    [OrderedFact("Should answer inline query with a location")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
    public async Task Should_Answer_Inline_Query_With_Location()
    {
        await Fixture.SendTestInstructionsAsync(
            "Staring the inline query with this message...",
            startInlineQuery: true
        );

        Update iqUpdate = await Fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "location:hobitton";
        InlineQueryResult[] results =
        [
            new InlineQueryResultLocation
            {
                Id = resultId,
                Latitude = -37.8721897f,
                Longitude = 175.6810213f,
                Title = "Hobbiton Movie Set"
            }
        ];

        await BotClient.AnswerInlineQuery(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await Fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: Fixture.SupergroupChat.Id,
                messageType: MessageType.Location
            );
        Update resultUpdate = chosenResultUpdate;

        Assert.Equal(MessageType.Location, messageUpdate.Message!.Type);
        Assert.Equal(resultId, resultUpdate.ChosenInlineResult!.ResultId);
        Assert.Equal(iqUpdate.InlineQuery.Query, resultUpdate.ChosenInlineResult.Query);
    }

    [OrderedFact("Should answer inline query with a venue")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
    public async Task Should_Answer_Inline_Query_With_Venue()
    {
        await Fixture.SendTestInstructionsAsync(
            "Staring the inline query with this message...",
            startInlineQuery: true
        );

        Update iqUpdate = await Fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "venue:hobbiton";
        InlineQueryResult[] results =
        [
            new InlineQueryResultVenue
            {
                Id = resultId,
                Latitude = -37.8721897f,
                Longitude = 175.6810213f,
                Title = "Hobbiton Movie Set",
                Address = "501 Buckland Rd, Hinuera, Matamata 3472, New Zealand",
            }
        ];

        await BotClient.AnswerInlineQuery(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await Fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: Fixture.SupergroupChat.Id,
                messageType: MessageType.Venue
            );
        Update resultUpdate = chosenResultUpdate;

        Assert.Equal(MessageType.Venue, messageUpdate.Message!.Type);
        Assert.Equal(resultId, resultUpdate.ChosenInlineResult!.ResultId);
        Assert.Equal(iqUpdate.InlineQuery.Query, resultUpdate.ChosenInlineResult.Query);
    }

    [OrderedFact("Should answer inline query with a photo")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
    public async Task Should_Answer_Inline_Query_With_Photo()
    {
        await Fixture.SendTestInstructionsAsync(
            "Staring the inline query with this message...",
            startInlineQuery: true
        );

        Update iqUpdate = await Fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "photo:rainbow-girl";
        const string url = "https://cdn.pixabay.com/photo/2017/08/30/12/45/girl-2696947_640.jpg";
        const string caption = "Rainbow Girl";
        InlineQueryResult[] results =
        [
            new InlineQueryResultPhoto
            {
                Id = resultId,
                PhotoUrl = url,
                ThumbnailUrl = url,
                Caption = caption
            }
        ];

        await BotClient.AnswerInlineQuery(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await Fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: Fixture.SupergroupChat.Id,
                messageType: MessageType.Photo
            );
        Update resultUpdate = chosenResultUpdate;

        Assert.Equal(MessageType.Photo, messageUpdate.Message!.Type);
        Assert.Equal(resultId, resultUpdate.ChosenInlineResult!.ResultId);
        Assert.Equal(iqUpdate.InlineQuery.Query, resultUpdate.ChosenInlineResult.Query);
        Assert.Equal(caption, messageUpdate.Message.Caption);
    }

    [OrderedFact("Should send a photo and answer inline query with a cached photo using its file_id")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
    public async Task Should_Answer_Inline_Query_With_Cached_Photo()
    {
        Message photoMessage;
        await using (FileStream stream = File.OpenRead(Constants.PathToFile.Photos.Apes))
        {
            photoMessage = await BotClient.WithStreams(stream).SendPhoto(
                chatId: Fixture.SupergroupChat,
                photo: InputFile.FromStream(stream),
                replyMarkup: (InlineKeyboardMarkup)InlineKeyboardButton
                    .WithSwitchInlineQueryCurrentChat("Start inline query")
            );
        }

        Update iqUpdate = await Fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "photo:apes";
        const string caption = "Apes smoking shisha";
        InlineQueryResult[] results =
        [
            new InlineQueryResultCachedPhoto
            {
                Id = resultId,
                PhotoFileId = photoMessage.Photo!.First().FileId,
                Caption = caption
            }
        ];

        await BotClient.AnswerInlineQuery(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await Fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: Fixture.SupergroupChat.Id,
                messageType: MessageType.Photo
            );
        Update resultUpdate = chosenResultUpdate;

        Assert.Equal(MessageType.Photo, messageUpdate.Message!.Type);
        Assert.Equal(resultId, resultUpdate.ChosenInlineResult!.ResultId);
        Assert.Equal(iqUpdate.InlineQuery.Query, resultUpdate.ChosenInlineResult.Query);
        Assert.Equal(caption, messageUpdate.Message.Caption);
    }

    [OrderedFact("Should answer inline query with a video")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
    public async Task Should_Answer_Inline_Query_With_Video()
    {
        await Fixture.SendTestInstructionsAsync(
            "Staring the inline query with this message...",
            startInlineQuery: true
        );

        Update iqUpdate = await Fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "sunset_video";
        InlineQueryResult[] results =
        [
            new InlineQueryResultVideo
            {
                Id = resultId,
                VideoUrl = "https://pixabay.com/en/videos/download/video-10737_medium.mp4",
                ThumbnailUrl = "https://i.vimeocdn.com/video/646283246_640x360.jpg",
                MimeType = "video/mp4",
                Title = "Sunset Landscape",
                Description = "A beautiful scene"
            }
        ];

        await BotClient.AnswerInlineQuery(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await Fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: Fixture.SupergroupChat.Id,
                messageType: MessageType.Video
            );
        Update resultUpdate = chosenResultUpdate;

        Assert.Equal(MessageType.Video, messageUpdate.Message!.Type);
        Assert.Equal(resultId, resultUpdate.ChosenInlineResult!.ResultId);
        Assert.Equal(iqUpdate.InlineQuery.Query, resultUpdate.ChosenInlineResult.Query);
    }

    [OrderedFact("Should answer inline query with a YouTube video (HTML page)")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
    public async Task Should_Answer_Inline_Query_With_HTML_Video()
    {
        // ToDo exception when input_message_content not specified. Bad Request: SEND_MESSAGE_MEDIA_INVALID

        await Fixture.SendTestInstructionsAsync(
            "Staring the inline query with this message...",
            startInlineQuery: true
        );

        Update iqUpdate = await Fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "youtube_video";
        InlineQueryResult[] results =
        [
            new InlineQueryResultVideo
            {
                Id = resultId,
                VideoUrl = "https://www.youtube.com/watch?v=1S0CTtY8Qa0",
                ThumbnailUrl = "https://www.youtube.com/watch?v=1S0CTtY8Qa0",
                MimeType = "video/mp4",
                Title = "Rocket Launch",
                InputMessageContent = new InputTextMessageContent
                {
                    MessageText = "[Rocket Launch](https://www.youtube.com/watch?v=1S0CTtY8Qa0)",
                    ParseMode = ParseMode.Markdown
                }
            }
        ];

        await BotClient.AnswerInlineQuery(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await Fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: Fixture.SupergroupChat.Id,
                messageType: MessageType.Text
            );
        Update resultUpdate = chosenResultUpdate;

        Assert.Equal(MessageType.Text, messageUpdate.Message!.Type);
        Assert.Equal(resultId, resultUpdate.ChosenInlineResult!.ResultId);
        Assert.Equal(iqUpdate.InlineQuery.Query, resultUpdate.ChosenInlineResult.Query);
    }

    [OrderedFact("Should send a video and answer inline query with a cached video using its file_id")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
    public async Task Should_Answer_Inline_Query_With_Cached_Video()
    {
        // Video from https://pixabay.com/en/videos/fireworks-rocket-new-year-s-eve-7122/
        Message videoMessage = await BotClient.SendVideo(
            chatId: Fixture.SupergroupChat,
            video: InputFile.FromUri("https://pixabay.com/en/videos/download/video-7122_medium.mp4"),
            replyMarkup: (InlineKeyboardMarkup)InlineKeyboardButton
                .WithSwitchInlineQueryCurrentChat("Start inline query")
        );

        Update iqUpdate = await Fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "fireworks_video";
        InlineQueryResult[] results =
        [
            new InlineQueryResultCachedVideo
            {
                Id = resultId,
                VideoFileId = videoMessage.Video!.FileId,
                Title = "New Year's Eve Fireworks",
                Description = "2017 Fireworks in Germany"
            }
        ];

        await BotClient.AnswerInlineQuery(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await Fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: Fixture.SupergroupChat.Id,
                messageType: MessageType.Video
            );
        Update resultUpdate = chosenResultUpdate;

        Assert.Equal(MessageType.Video, messageUpdate.Message!.Type);
        Assert.Equal(resultId, resultUpdate.ChosenInlineResult!.ResultId);
        Assert.Equal(iqUpdate.InlineQuery.Query, resultUpdate.ChosenInlineResult.Query);
    }

    [OrderedFact("Should answer inline query with an audio")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
    public async Task Should_Answer_Inline_Query_With_Audio()
    {
        await Fixture.SendTestInstructionsAsync(
            "Staring the inline query with this message...",
            startInlineQuery: true
        );

        Update iqUpdate = await Fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "audio_result";
        InlineQueryResult[] results =
        [
            new InlineQueryResultAudio
            {
                Id = resultId,
                AudioUrl = "https://upload.wikimedia.org/wikipedia/commons/transcoded/b/bb/Test_ogg_mp3_48kbps.wav/Test_ogg_mp3_48kbps.wav.mp3",
                Title = "Test ogg mp3",
                Performer = "Shishirdasika",
                AudioDuration = 25
            }
        ];

        await BotClient.AnswerInlineQuery(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await Fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: Fixture.SupergroupChat.Id,
                messageType: MessageType.Audio
            );
        Update resultUpdate = chosenResultUpdate;

        Assert.Equal(MessageType.Audio, messageUpdate.Message!.Type);
        Assert.Equal(resultId, resultUpdate.ChosenInlineResult!.ResultId);
        Assert.Equal(iqUpdate.InlineQuery.Query, resultUpdate.ChosenInlineResult.Query);
    }

    [OrderedFact("Should send an audio and answer inline query with a cached audio using its file_id")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendAudio)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
    public async Task Should_Answer_Inline_Query_With_Cached_Audio()
    {
        Message audioMessage;
        await using (FileStream stream = File.OpenRead(Constants.PathToFile.Audio.CantinaRagMp3))
        {
            audioMessage = await BotClient.WithStreams(stream).SendAudio(
                chatId: Fixture.SupergroupChat,
                audio: InputFile.FromStream(stream),
                performer: "Jackson F. Smith",
                duration: 201,
                replyMarkup: (InlineKeyboardMarkup)InlineKeyboardButton
                    .WithSwitchInlineQueryCurrentChat("Start inline query")
            );
        }

        Update iqUpdate = await Fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "audio_result";
        InlineQueryResult[] results =
        [
            new InlineQueryResultCachedAudio
            {
                Id = resultId,
                AudioFileId = audioMessage.Audio!.FileId,
                Caption = "Jackson F. Smith - Cantina Rag"
            }
        ];

        await BotClient.AnswerInlineQuery(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await Fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: Fixture.SupergroupChat.Id,
                messageType: MessageType.Audio
            );
        Update resultUpdate = chosenResultUpdate;

        Assert.Equal(MessageType.Audio, messageUpdate.Message!.Type);
        Assert.Equal(resultId, resultUpdate.ChosenInlineResult!.ResultId);
        Assert.Equal(iqUpdate.InlineQuery.Query, resultUpdate.ChosenInlineResult.Query);
    }

    [OrderedFact("Should answer inline query with an audio")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
    public async Task Should_Answer_Inline_Query_With_Voice()
    {
        await Fixture.SendTestInstructionsAsync(
            "Staring the inline query with this message...",
            startInlineQuery: true
        );

        Update iqUpdate = await Fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "voice_result";
        InlineQueryResult[] results =
        [
            new InlineQueryResultVoice
            {
                Id = resultId,
                VoiceUrl = "http://www.vorbis.com/music/Hydrate-Kenny_Beltrey.ogg",
                Title = "Hydrate - Kenny Beltrey",
                Caption = "Hydrate - Kenny Beltrey",
                VoiceDuration = 265
            }
        ];

        await BotClient.AnswerInlineQuery(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await Fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: Fixture.SupergroupChat.Id,
                messageType: MessageType.Voice
            );
        Update resultUpdate = chosenResultUpdate;

        Assert.Equal(MessageType.Voice, messageUpdate.Message!.Type);
        Assert.Equal(resultId, resultUpdate.ChosenInlineResult!.ResultId);
        Assert.Equal(iqUpdate.InlineQuery.Query, resultUpdate.ChosenInlineResult.Query);
    }

    [OrderedFact("Should send an audio and answer inline query with a cached audio using its file_id")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
    public async Task Should_Answer_Inline_Query_With_Cached_Voice()
    {
        Message voiceMessage;
        await using (FileStream stream = File.OpenRead(Constants.PathToFile.Audio.TestOgg))
        {
            voiceMessage = await BotClient.WithStreams(stream).SendVoice(
                chatId: Fixture.SupergroupChat,
                voice: InputFile.FromStream(stream),
                replyMarkup: (InlineKeyboardMarkup)InlineKeyboardButton
                    .WithSwitchInlineQueryCurrentChat("Start inline query"),
                duration: 24
            );
        }

        Update iqUpdate = await Fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "voice_result";
        InlineQueryResult[] results =
        [
            new InlineQueryResultCachedVoice
            {
                Id = resultId,
                VoiceFileId = voiceMessage.Voice!.FileId,
                Title = "Test Voice",
            }
        ];

        await BotClient.AnswerInlineQuery(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await Fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: Fixture.SupergroupChat.Id,
                messageType: MessageType.Voice
            );
        Update resultUpdate = chosenResultUpdate;

        Assert.Equal(MessageType.Voice, messageUpdate.Message!.Type);
        Assert.Equal(resultId, resultUpdate.ChosenInlineResult!.ResultId);
        Assert.Equal(iqUpdate.InlineQuery.Query, resultUpdate.ChosenInlineResult.Query);
    }

    [OrderedFact("Should answer inline query with a document")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
    public async Task Should_Answer_Inline_Query_With_Document()
    {
        await Fixture.SendTestInstructionsAsync(
            "Staring the inline query with this message...",
            startInlineQuery: true
        );

        Update iqUpdate = await Fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "document_result";
        InlineQueryResult[] results =
        [
            new InlineQueryResultDocument
            {
                Id = resultId,
                DocumentUrl = "https://www.w3.org/WAI/ER/tests/xhtml/testfiles/resources/pdf/dummy.pdf",
                Title = "Dummy PDF File",
                MimeType = "application/pdf",
                Caption = "Dummy PDF File",
                Description = "Dummy PDF File for testing",
            }
        ];

        await BotClient.AnswerInlineQuery(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await Fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: Fixture.SupergroupChat.Id,
                messageType: MessageType.Document
            );
        Update resultUpdate = chosenResultUpdate;

        Assert.Equal(MessageType.Document, messageUpdate.Message!.Type);
        Assert.Equal(resultId, resultUpdate.ChosenInlineResult!.ResultId);
        Assert.Equal(iqUpdate.InlineQuery.Query, resultUpdate.ChosenInlineResult.Query);
    }

    [OrderedFact("Should send a document and answer inline query with a cached document using its file_id")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
    public async Task Should_Answer_Inline_Query_With_Cached_Document()
    {
        Message documentMessage;
        await using (FileStream stream = File.OpenRead(Constants.PathToFile.Documents.Hamlet))
        {
            documentMessage = await BotClient.WithStreams(stream).SendDocument(
                chatId: Fixture.SupergroupChat,
                document: InputFile.FromStream(stream),
                replyMarkup: (InlineKeyboardMarkup)InlineKeyboardButton
                    .WithSwitchInlineQueryCurrentChat("Start inline query")
            );
        }

        Update iqUpdate = await Fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "document_result";
        InlineQueryResult[] results =
        [
            new InlineQueryResultCachedDocument
            {
                Id = resultId,
                DocumentFileId = documentMessage.Document!.FileId,
                Title = "Test Document",
                Caption = "The Tragedy of Hamlet, Prince of Denmark",
                Description = "Sample PDF Document",
            }
        ];

        await BotClient.AnswerInlineQuery(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await Fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: Fixture.SupergroupChat.Id,
                messageType: MessageType.Document
            );
        Update resultUpdate = chosenResultUpdate;

        Assert.Equal(MessageType.Document, messageUpdate.Message!.Type);
        Assert.Equal(resultId, resultUpdate.ChosenInlineResult!.ResultId);
        Assert.Equal(iqUpdate.InlineQuery.Query, resultUpdate.ChosenInlineResult.Query);
    }

    [OrderedFact("Should answer inline query with a gif")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
    public async Task Should_Answer_Inline_Query_With_Gif()
    {
        await Fixture.SendTestInstructionsAsync(
            "Staring the inline query with this message...",
            startInlineQuery: true
        );

        Update iqUpdate = await Fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "gif_result";
        InlineQueryResult[] results =
        [
            new InlineQueryResultGif
            {
                Id = resultId,
                GifUrl = "https://upload.wikimedia.org/wikipedia/commons/2/2c/Rotating_earth_%28large%29.gif",
                ThumbnailUrl = "https://upload.wikimedia.org/wikipedia/commons/2/2c/Rotating_earth_%28large%29.gif",
                Caption = "Rotating Earth",
                GifDuration = 4,
                GifHeight = 400,
                GifWidth = 400,
                Title = "Rotating Earth",
                ThumbnailMimeType = "image/gif",
            }
        ];

        await BotClient.AnswerInlineQuery(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await Fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: Fixture.SupergroupChat.Id,
                messageType: MessageType.Animation
            );
        Update resultUpdate = chosenResultUpdate;

        Assert.Equal(MessageType.Animation, messageUpdate.Message!.Type);
        Assert.Equal(resultId, resultUpdate.ChosenInlineResult!.ResultId);
        Assert.Equal(iqUpdate.InlineQuery.Query, resultUpdate.ChosenInlineResult.Query);
    }

    [OrderedFact("Should send a gif and answer inline query with a cached gif using its file_id")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
    public async Task Should_Answer_Inline_Query_With_Cached_Gif()
    {
        Message gifMessage = await BotClient.SendDocument(
            chatId: Fixture.SupergroupChat,
            document: InputFile.FromUri("https://upload.wikimedia.org/wikipedia/commons/2/2c/Rotating_earth_%28large%29.gif"),
            replyMarkup: (InlineKeyboardMarkup)InlineKeyboardButton
                .WithSwitchInlineQueryCurrentChat("Start inline query"));

        Update iqUpdate = await Fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "gif_result";
        InlineQueryResult[] results =
        [
            new InlineQueryResultCachedGif
            {
                Id = resultId,
                GifFileId = gifMessage.Document!.FileId,
                Caption = "Rotating Earth",
            }
        ];

        await BotClient.AnswerInlineQuery(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await Fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: Fixture.SupergroupChat.Id,
                messageType: MessageType.Animation
            );
        Update resultUpdate = chosenResultUpdate;

        Assert.Equal(MessageType.Animation, messageUpdate.Message!.Type);
        Assert.Equal(resultId, resultUpdate.ChosenInlineResult!.ResultId);
        Assert.Equal(iqUpdate.InlineQuery.Query, resultUpdate.ChosenInlineResult.Query);
    }

    [OrderedFact("Should answer inline query with an mpeg4 gif")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
    public async Task Should_Answer_Inline_Query_With_Mpeg4Gif()
    {
        await Fixture.SendTestInstructionsAsync(
            "Staring the inline query with this message...",
            startInlineQuery: true
        );

        Update iqUpdate = await Fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "mpeg4_gif_result";
        InlineQueryResult[] results =
        [
            new InlineQueryResultMpeg4Gif
            {
                Id = resultId,
                Mpeg4Url = "https://pixabay.com/en/videos/download/video-10737_medium.mp4",
                ThumbnailUrl = "https://i.vimeocdn.com/video/646283246_640x360.jpg",
                Caption = "A beautiful scene",
            }
        ];

        await BotClient.AnswerInlineQuery(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await Fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: Fixture.SupergroupChat.Id,
                messageType: MessageType.Video
            );
        Update resultUpdate = chosenResultUpdate;

        Assert.Equal(MessageType.Video, messageUpdate.Message!.Type);
        Assert.Equal(resultId, resultUpdate.ChosenInlineResult!.ResultId);
        Assert.Equal(iqUpdate.InlineQuery.Query, resultUpdate.ChosenInlineResult.Query);
    }

    [OrderedFact("Should send an mpeg4 gif and answer inline query with a cached mpeg4 gif using its file_id")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
    public async Task Should_Answer_Inline_Query_With_Cached_Mpeg4Gif()
    {
        Message gifMessage = await BotClient.SendDocument(
            chatId: Fixture.SupergroupChat,
            document: InputFile.FromUri("https://upload.wikimedia.org/wikipedia/commons/2/2c/Rotating_earth_%28large%29.gif"),
            replyMarkup: (InlineKeyboardMarkup)InlineKeyboardButton
                .WithSwitchInlineQueryCurrentChat("Start inline query"));

        Update iqUpdate = await Fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "mpeg4_gif_result";
        InlineQueryResult[] results =
        [
            new InlineQueryResultCachedMpeg4Gif
            {
                Id = resultId,
                Mpeg4FileId = gifMessage.Document!.FileId,
                Caption = "Rotating Earth",
            }
        ];

        await BotClient.AnswerInlineQuery(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await Fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: Fixture.SupergroupChat.Id,
                messageType: MessageType.Animation
            );
        Update resultUpdate = chosenResultUpdate;

        Assert.Equal(MessageType.Animation, messageUpdate.Message!.Type);
        Assert.Equal(resultId, resultUpdate.ChosenInlineResult!.ResultId);
        Assert.Equal(iqUpdate.InlineQuery.Query, resultUpdate.ChosenInlineResult.Query);
    }

    [OrderedFact("Should answer inline query with a cached sticker using its file_id")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetStickerSet)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
    public async Task Should_Answer_Inline_Query_With_Cached_Sticker()
    {
        await Fixture.SendTestInstructionsAsync(
            "Staring the inline query with this message...",
            startInlineQuery: true
        );

        Update iqUpdate = await Fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        StickerSet stickerSet = await BotClient.GetStickerSet("EvilMinds");

        const string resultId = "sticker_result";
        InlineQueryResult[] results =
        [
            new InlineQueryResultCachedSticker
            {
                Id = resultId,
                StickerFileId = stickerSet.Stickers[0].FileId,
            }
        ];

        await BotClient.AnswerInlineQuery(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await Fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: Fixture.SupergroupChat.Id,
                messageType: MessageType.Sticker
            );
        Update resultUpdate = chosenResultUpdate;

        Assert.Equal(MessageType.Sticker, messageUpdate.Message!.Type);
        Assert.Equal(resultId, resultUpdate.ChosenInlineResult!.ResultId);
        Assert.Equal(iqUpdate.InlineQuery.Query, resultUpdate.ChosenInlineResult.Query);
    }

    [OrderedFact("Should answer inline query with a photo with markdown encoded caption")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
    public async Task Should_Answer_Inline_Query_With_Photo_With_Markdown_Encoded_Caption()
    {
        await Fixture.SendTestInstructionsAsync(
            "Staring the inline query with this message...",
            startInlineQuery: true
        );

        Update iqUpdate = await Fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "photo:rainbow-girl-caption";
        const string url = "https://cdn.pixabay.com/photo/2017/08/30/12/45/girl-2696947_640.jpg";
        const string photoCaption = "Rainbow Girl";
        InlineQueryResult[] results =
        [
            new InlineQueryResultPhoto
            {
                Id = resultId,
                PhotoUrl = url,
                ThumbnailUrl = url,
                Caption = $"_{photoCaption}_",
                ParseMode = ParseMode.Markdown
            }
        ];

        await BotClient.AnswerInlineQuery(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await Fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: Fixture.SupergroupChat.Id,
                messageType: MessageType.Photo
            );

        Assert.Equal(MessageType.Photo, messageUpdate.Message!.Type);
        Assert.Equal(photoCaption, messageUpdate.Message.Caption);
        Assert.Equal(MessageEntityType.Italic, messageUpdate.Message.CaptionEntities!.Single().Type);

        Assert.Equal(UpdateType.ChosenInlineResult, chosenResultUpdate.Type);
        Assert.Equal(resultId, chosenResultUpdate.ChosenInlineResult!.ResultId);
        Assert.Equal(iqUpdate.InlineQuery.Query, chosenResultUpdate.ChosenInlineResult.Query);
    }

    [OrderedFact("Should throw exception when answering an inline query after 10 seconds")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
    public async Task Should_Throw_Exception_When_Answering_Late()
    {
        await Fixture.SendTestInstructionsAsync(
            "Write an inline query that I'll never answer!",
            startInlineQuery: true
        );

        Update queryUpdate = await Fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        InlineQueryResult[] results =
        [
            new InlineQueryResultArticle
            {
                Id = "article:bot-api",
                Title = "Telegram Bot API",
                InputMessageContent = new InputTextMessageContent { MessageText = "https://core.telegram.org/bots/api"},
                Description = "The Bot API is an HTTP-based interface created for developers",
            }
        ];

        await Task.Delay(10_000);

        ApiRequestException exception = await Assert.ThrowsAsync<ApiRequestException>(() =>
            BotClient.AnswerInlineQuery(
                inlineQueryId: queryUpdate.InlineQuery!.Id,
                results: results,
                cacheTime: 0
            )
        );

        Assert.Equal("Bad Request: query is too old and response timeout expired or query ID is invalid", exception.Message);
        Assert.Equal(400, exception.ErrorCode);
    }
}

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
public class InlineQueryTests
{
    ITelegramBotClient BotClient => _fixture.BotClient;

    readonly TestsFixture _fixture;

    public InlineQueryTests(TestsFixture fixture)
    {
        _fixture = fixture;
    }

    [OrderedFact("Should answer inline query with an article")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
    public async Task Should_Answer_Inline_Query_With_Article()
    {
        await _fixture.SendTestInstructionsAsync(
            "1. Start an inline query\n" +
            "2. Wait for bot to answer it\n" +
            "3. Choose the answer",
            startInlineQuery: true
        );

        // Wait for tester to start an inline query
        // This looks for an Update having a value on "inline_query" field
        Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        // Prepare results of the query
        InlineQueryResult[] results =
        {
            new InlineQueryResultArticle(
                id: "article:bot-api",
                title: "Telegram Bot API",
                inputMessageContent: new InputTextMessageContent("https://core.telegram.org/bots/api"))
            {
                Description = "The Bot API is an HTTP-based interface created for developers",
            },
        };

        // Answer the query
        await BotClient.AnswerInlineQueryAsync(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        // Wait for tester to choose a result and send it(as a message) to the chat
        (
            Update messageUpdate,
            Update chosenResultUpdate
        ) = await _fixture.UpdateReceiver.GetInlineQueryResultUpdates(
            chatId: _fixture.SupergroupChat.Id,
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
        await _fixture.SendTestInstructionsAsync(
            "1. Start an inline query\n" +
            "2. Wait for bot to answer it\n" +
            "3. Choose the answer",
            startInlineQuery: true
        );

        Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        InlineQueryResult[] results =
        {
            new InlineQueryResultArticle(
                id: "article:bot-api",
                title: "Telegram Bot API",
                inputMessageContent: new InputTextMessageContent("https://core.telegram.org/bots/api"))
            {
                Description = "The Bot API is an HTTP-based interface created for developers",
            },
        };

        await BotClient.AnswerInlineQueryAsync(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        Update messageUpdate = await _fixture.UpdateReceiver.GetUpdateAsync(
            predicate: update => update.Message!.ViaBot is not null,
            updateTypes: new[] { UpdateType.Message }
        );

        Assert.NotNull(messageUpdate.Message);
        Assert.Equal(MessageType.Text, messageUpdate.Message.Type);
        Assert.NotNull(messageUpdate.Message.ViaBot);
        Assert.Equal(_fixture.BotUser.Id, messageUpdate.Message.ViaBot.Id);
    }

    [OrderedFact("Should answer inline query with a contact")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
    public async Task Should_Answer_Inline_Query_With_Contact()
    {
        await _fixture.SendTestInstructionsAsync(
            "Staring the inline query with this message...",
            startInlineQuery: true
        );

        Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "contact:john-doe";
        InlineQueryResult[] results =
        {
            new InlineQueryResultContact(id: resultId, phoneNumber: "+1234567", firstName: "John")
            {
                LastName = "Doe"
            }
        };

        await BotClient.AnswerInlineQueryAsync(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await _fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: _fixture.SupergroupChat.Id,
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
        await _fixture.SendTestInstructionsAsync(
            "Staring the inline query with this message...",
            startInlineQuery: true
        );

        Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "location:hobitton";
        InlineQueryResult[] results =
        {
            new InlineQueryResultLocation(
                id: resultId,
                latitude: -37.8721897f,
                longitude: 175.6810213f,
                title: "Hobbiton Movie Set")
        };

        await BotClient.AnswerInlineQueryAsync(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await _fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: _fixture.SupergroupChat.Id,
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
        await _fixture.SendTestInstructionsAsync(
            "Staring the inline query with this message...",
            startInlineQuery: true
        );

        Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "venue:hobbiton";
        InlineQueryResult[] results =
        {
            new InlineQueryResultVenue(
                id: resultId,
                latitude: -37.8721897f,
                longitude: 175.6810213f,
                title: "Hobbiton Movie Set",
                address: "501 Buckland Rd, Hinuera, Matamata 3472, New Zealand")
        };

        await BotClient.AnswerInlineQueryAsync(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await _fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: _fixture.SupergroupChat.Id,
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
        await _fixture.SendTestInstructionsAsync(
            "Staring the inline query with this message...",
            startInlineQuery: true
        );

        Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "photo:rainbow-girl";
        const string url = "https://cdn.pixabay.com/photo/2017/08/30/12/45/girl-2696947_640.jpg";
        const string caption = "Rainbow Girl";
        InlineQueryResult[] results =
        {
            new InlineQueryResultPhoto(id: resultId, photoUrl: url, thumbUrl: url)
            {
                Caption = caption
            }
        };

        await BotClient.AnswerInlineQueryAsync(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await _fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: _fixture.SupergroupChat.Id,
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
        await using (FileStream stream = System.IO.File.OpenRead(Constants.PathToFile.Photos.Apes))
        {
            photoMessage = await BotClient.SendPhotoAsync(
                chatId: _fixture.SupergroupChat,
                photo: new InputFile(stream),
                replyMarkup: (InlineKeyboardMarkup)InlineKeyboardButton
                    .WithSwitchInlineQueryCurrentChat("Start inline query")
            );
        }

        Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "photo:apes";
        const string caption = "Apes smoking shisha";
        InlineQueryResult[] results =
        {
            new InlineQueryResultCachedPhoto(
                id: resultId,
                photoFileId: photoMessage.Photo!.First().FileId)
            {
                Caption = caption
            }
        };

        await BotClient.AnswerInlineQueryAsync(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await _fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: _fixture.SupergroupChat.Id,
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
        await _fixture.SendTestInstructionsAsync(
            "Staring the inline query with this message...",
            startInlineQuery: true
        );

        Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "sunset_video";
        InlineQueryResult[] results =
        {
            new InlineQueryResultVideo(
                id: resultId,
                videoUrl: "https://pixabay.com/en/videos/download/video-10737_medium.mp4",
                thumbUrl: "https://i.vimeocdn.com/video/646283246_640x360.jpg",
                title: "Sunset Landscape")
            {
                Description = "A beautiful scene"
            }
        };

        await BotClient.AnswerInlineQueryAsync(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await _fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: _fixture.SupergroupChat.Id,
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

        await _fixture.SendTestInstructionsAsync(
            "Staring the inline query with this message...",
            startInlineQuery: true
        );

        Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "youtube_video";
        InlineQueryResult[] results =
        {
            new InlineQueryResultVideo(
                id: resultId,
                videoUrl: "https://www.youtube.com/watch?v=1S0CTtY8Qa0",
                thumbUrl: "https://www.youtube.com/watch?v=1S0CTtY8Qa0",
                title: "Rocket Launch",
                inputMessageContent:
                new InputTextMessageContent("[Rocket Launch](https://www.youtube.com/watch?v=1S0CTtY8Qa0)")
                {
                    ParseMode = ParseMode.Markdown
                }
            )
        };

        await BotClient.AnswerInlineQueryAsync(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await _fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: _fixture.SupergroupChat.Id,
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
        Message videoMessage = await BotClient.SendVideoAsync(
            chatId: _fixture.SupergroupChat,
            video: new InputFileUrl("https://pixabay.com/en/videos/download/video-7122_medium.mp4"),
            replyMarkup: (InlineKeyboardMarkup)InlineKeyboardButton
                .WithSwitchInlineQueryCurrentChat("Start inline query")
        );

        Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "fireworks_video";
        InlineQueryResult[] results =
        {
            new InlineQueryResultCachedVideo(
                id: resultId,
                videoFileId: videoMessage.Video!.FileId,
                title: "New Year's Eve Fireworks")
            {
                Description = "2017 Fireworks in Germany"
            }
        };

        await BotClient.AnswerInlineQueryAsync(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await _fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: _fixture.SupergroupChat.Id,
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
        await _fixture.SendTestInstructionsAsync(
            "Staring the inline query with this message...",
            startInlineQuery: true
        );

        Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "audio_result";
        InlineQueryResult[] results =
        {
            new InlineQueryResultAudio(
                id: resultId,
                audioUrl:
                "https://upload.wikimedia.org/wikipedia/commons/transcoded/b/bb/Test_ogg_mp3_48kbps.wav/Test_ogg_mp3_48kbps.wav.mp3",
                title: "Test ogg mp3")
            {
                Performer = "Shishirdasika",
                AudioDuration = 25
            }
        };

        await BotClient.AnswerInlineQueryAsync(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await _fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: _fixture.SupergroupChat.Id,
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
        await using (FileStream stream = System.IO.File.OpenRead(Constants.PathToFile.Audio.CantinaRagMp3))
        {
            audioMessage = await BotClient.SendAudioAsync(
                chatId: _fixture.SupergroupChat,
                audio: new InputFile(stream),
                performer: "Jackson F. Smith",
                duration: 201,
                replyMarkup: (InlineKeyboardMarkup)InlineKeyboardButton
                    .WithSwitchInlineQueryCurrentChat("Start inline query")
            );
        }

        Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "audio_result";
        InlineQueryResult[] results =
        {
            new InlineQueryResultCachedAudio(
                id: resultId,
                audioFileId: audioMessage.Audio!.FileId)
            {
                Caption = "Jackson F. Smith - Cantina Rag"
            }
        };

        await BotClient.AnswerInlineQueryAsync(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await _fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: _fixture.SupergroupChat.Id,
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
        await _fixture.SendTestInstructionsAsync(
            "Staring the inline query with this message...",
            startInlineQuery: true
        );

        Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "voice_result";
        InlineQueryResult[] results =
        {
            new InlineQueryResultVoice(
                id: resultId,
                voiceUrl: "http://www.vorbis.com/music/Hydrate-Kenny_Beltrey.ogg",
                title: "Hydrate - Kenny Beltrey")
            {
                Caption = "Hydrate - Kenny Beltrey",
                VoiceDuration = 265
            }
        };

        await BotClient.AnswerInlineQueryAsync(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await _fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: _fixture.SupergroupChat.Id,
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
        await using (FileStream stream = System.IO.File.OpenRead(Constants.PathToFile.Audio.TestOgg))
        {
            voiceMessage = await BotClient.SendVoiceAsync(
                chatId: _fixture.SupergroupChat,
                voice: new InputFile(stream),
                duration: 24,
                replyMarkup: (InlineKeyboardMarkup)InlineKeyboardButton
                    .WithSwitchInlineQueryCurrentChat("Start inline query")
            );
        }

        Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "voice_result";
        InlineQueryResult[] results =
        {
            new InlineQueryResultCachedVoice(
                id: resultId,
                fileId: voiceMessage.Voice!.FileId,
                title: "Test Voice"
            )
        };

        await BotClient.AnswerInlineQueryAsync(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await _fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: _fixture.SupergroupChat.Id,
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
        await _fixture.SendTestInstructionsAsync(
            "Staring the inline query with this message...",
            startInlineQuery: true
        );

        Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "document_result";
        InlineQueryResult[] results =
        {
            new InlineQueryResultDocument(
                id: resultId,
                documentUrl: "https://www.w3.org/WAI/ER/tests/xhtml/testfiles/resources/pdf/dummy.pdf",
                title: "Dummy PDF File",
                mimeType: "application/pdf")
            {
                Caption = "Dummy PDF File",
                Description = "Dummy PDF File for testing",
            }
        };

        await BotClient.AnswerInlineQueryAsync(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await _fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: _fixture.SupergroupChat.Id,
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
        await using (FileStream stream = System.IO.File.OpenRead(Constants.PathToFile.Documents.Hamlet))
        {
            documentMessage = await BotClient.SendDocumentAsync(
                chatId: _fixture.SupergroupChat,
                document: new InputFile(stream),
                replyMarkup: (InlineKeyboardMarkup)InlineKeyboardButton
                    .WithSwitchInlineQueryCurrentChat("Start inline query")
            );
        }

        Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "document_result";
        InlineQueryResult[] results =
        {
            new InlineQueryResultCachedDocument(
                id: resultId,
                documentFileId: documentMessage.Document!.FileId,
                title: "Test Document")
            {
                Caption = "The Tragedy of Hamlet, Prince of Denmark",
                Description = "Sample PDF Document",
            }
        };

        await BotClient.AnswerInlineQueryAsync(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await _fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: _fixture.SupergroupChat.Id,
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
        await _fixture.SendTestInstructionsAsync(
            "Staring the inline query with this message...",
            startInlineQuery: true
        );

        Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "gif_result";
        InlineQueryResult[] results =
        {
            new InlineQueryResultGif(
                id: resultId,
                gifUrl: "https://upload.wikimedia.org/wikipedia/commons/2/2c/Rotating_earth_%28large%29.gif",
                thumbUrl: "https://upload.wikimedia.org/wikipedia/commons/2/2c/Rotating_earth_%28large%29.gif")
            {
                Caption = "Rotating Earth",
                GifDuration = 4,
                GifHeight = 400,
                GifWidth = 400,
                Title = "Rotating Earth",
                ThumbMimeType = "image/gif",
            }
        };

        await BotClient.AnswerInlineQueryAsync(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await _fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: _fixture.SupergroupChat.Id,
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
        Message gifMessage = await BotClient.SendDocumentAsync(
            chatId: _fixture.SupergroupChat,
            document: new InputFileUrl("https://upload.wikimedia.org/wikipedia/commons/2/2c/Rotating_earth_%28large%29.gif"),
            replyMarkup: (InlineKeyboardMarkup)InlineKeyboardButton
                .WithSwitchInlineQueryCurrentChat("Start inline query"));

        Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "gif_result";
        InlineQueryResult[] results =
        {
            new InlineQueryResultCachedGif(
                id: resultId,
                gifFileId: gifMessage.Document!.FileId)
            {
                Caption = "Rotating Earth",
            }
        };

        await BotClient.AnswerInlineQueryAsync(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await _fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: _fixture.SupergroupChat.Id,
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
        await _fixture.SendTestInstructionsAsync(
            "Staring the inline query with this message...",
            startInlineQuery: true
        );

        Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "mpeg4_gif_result";
        InlineQueryResult[] results =
        {
            new InlineQueryResultMpeg4Gif(
                id: resultId,
                mpeg4Url: "https://pixabay.com/en/videos/download/video-10737_medium.mp4",
                thumbUrl: "https://i.vimeocdn.com/video/646283246_640x360.jpg")
            {
                Caption = "A beautiful scene",
            },
        };

        await BotClient.AnswerInlineQueryAsync(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await _fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: _fixture.SupergroupChat.Id,
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
        Message gifMessage = await BotClient.SendDocumentAsync(
            chatId: _fixture.SupergroupChat,
            document: new InputFileUrl("https://upload.wikimedia.org/wikipedia/commons/2/2c/Rotating_earth_%28large%29.gif"),
            replyMarkup: (InlineKeyboardMarkup)InlineKeyboardButton
                .WithSwitchInlineQueryCurrentChat("Start inline query"));

        Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "mpeg4_gif_result";
        InlineQueryResult[] results =
        {
            new InlineQueryResultCachedMpeg4Gif(
                id: resultId,
                mpeg4FileId: gifMessage.Document!.FileId)
            {
                Caption = "Rotating Earth",
            }
        };

        await BotClient.AnswerInlineQueryAsync(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await _fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: _fixture.SupergroupChat.Id,
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
        await _fixture.SendTestInstructionsAsync(
            "Staring the inline query with this message...",
            startInlineQuery: true
        );

        Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        StickerSet stickerSet = await BotClient.GetStickerSetAsync("EvilMinds");

        const string resultId = "sticker_result";
        InlineQueryResult[] results =
        {
            new InlineQueryResultCachedSticker(id: resultId, stickerFileId: stickerSet.Stickers[0].FileId)
        };

        await BotClient.AnswerInlineQueryAsync(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await _fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: _fixture.SupergroupChat.Id,
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
        await _fixture.SendTestInstructionsAsync(
            "Staring the inline query with this message...",
            startInlineQuery: true
        );

        Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        const string resultId = "photo:rainbow-girl-caption";
        const string url = "https://cdn.pixabay.com/photo/2017/08/30/12/45/girl-2696947_640.jpg";
        const string photoCaption = "Rainbow Girl";
        InlineQueryResult[] results =
        {
            new InlineQueryResultPhoto(id: resultId, photoUrl: url, thumbUrl: url)
            {
                Caption = $"*{photoCaption}*",
                ParseMode = ParseMode.Markdown
            }
        };

        await BotClient.AnswerInlineQueryAsync(
            inlineQueryId: iqUpdate.InlineQuery!.Id,
            results: results,
            cacheTime: 0
        );

        (Update messageUpdate, Update chosenResultUpdate) =
            await _fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: _fixture.SupergroupChat.Id,
                messageType: MessageType.Photo
            );

        Assert.Equal(MessageType.Photo, messageUpdate.Message!.Type);
        Assert.Equal(photoCaption, messageUpdate.Message.Caption);
        Assert.Equal(MessageEntityType.Bold, messageUpdate.Message.CaptionEntities!.Single().Type);

        Assert.Equal(UpdateType.ChosenInlineResult, chosenResultUpdate.Type);
        Assert.Equal(resultId, chosenResultUpdate.ChosenInlineResult!.ResultId);
        Assert.Equal(iqUpdate.InlineQuery.Query, chosenResultUpdate.ChosenInlineResult.Query);
    }

    [OrderedFact("Should throw exception when answering an inline query after 10 seconds")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
    public async Task Should_Throw_Exception_When_Answering_Late()
    {
        await _fixture.SendTestInstructionsAsync(
            "Write an inline query that I'll never answer!",
            startInlineQuery: true
        );

        Update queryUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        InlineQueryResult[] results =
        {
            new InlineQueryResultArticle(
                id: "article:bot-api",
                title: "Telegram Bot API",
                inputMessageContent: new InputTextMessageContent("https://core.telegram.org/bots/api"))
            {
                Description = "The Bot API is an HTTP-based interface created for developers",
            },
        };

        await Task.Delay(10_000);

        ApiRequestException exception = await Assert.ThrowsAsync<ApiRequestException>(() =>
            BotClient.AnswerInlineQueryAsync(
                inlineQueryId: queryUpdate.InlineQuery!.Id,
                results: results,
                cacheTime: 0
            )
        );

        Assert.Equal("Bad Request: query is too old and response timeout expired or query ID is invalid", exception.Message);
        Assert.Equal(400, exception.ErrorCode);
    }
}

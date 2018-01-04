using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Inline_Mode
{
    [Collection(Constants.TestCollections.InlineQuery)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class InlineQueryTests
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public InlineQueryTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = FactTitles.ShouldAnswerInlineQueryWithArticle)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        [ExecutionOrder(1)]
        public async Task Should_Answer_Inline_Query_With_Article()
        {
            // ToDo: add exception: Bad Request: QUERY_ID_INVALID
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAnswerInlineQueryWithArticle,
                startInlineQuery: true);

            Update update = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            InputMessageContent inputMessageContent = new InputTextMessageContent
            {
                MessageText = "https://core.telegram.org/bots/api",
            };

            InlineQueryResultBase[] results =
            {
                new InlineQueryResultArticle(
                    id: "bot-api",
                    title: "Telegram Bot API",
                    inputMessageContent: inputMessageContent)
                {
                    Description = "The Bot API is an HTTP-based interface created for developers",
                },
            };

            await BotClient.AnswerInlineQueryAsync(
                inlineQueryId: update.InlineQuery.Id,
                results: results,
                cacheTime: 0
            );
        }

        [Fact(DisplayName = FactTitles.ShouldAnswerInlineQueryWithContact)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        [ExecutionOrder(2)]
        public async Task Should_Answer_Inline_Query_With_Contact()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAnswerInlineQueryWithContact,
                startInlineQuery: true);

            Update update = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            InlineQueryResultBase[] results =
            {
                new InlineQueryResultContact(
                    id: "bot-api",
                    phoneNumber: "+1234567",
                    firstName: "John")
                {
                    LastName = "Doe"
                }
            };

            await BotClient.AnswerInlineQueryAsync(
                inlineQueryId: update.InlineQuery.Id,
                results: results,
                cacheTime: 0
            );
        }

        [Fact(DisplayName = FactTitles.ShouldAnswerInlineQueryWithLocation)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        [ExecutionOrder(3)]
        public async Task Should_Answer_Inline_Query_With_Location()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAnswerInlineQueryWithLocation,
                startInlineQuery: true);

            Update update = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            InlineQueryResultBase[] results =
            {
                new InlineQueryResultLocation(
                    id: "bot-api",
                    latitude: -37.8721897f,
                    longitude: 175.6810213f,
                    title: "Hobbiton Movie Set")
            };

            await BotClient.AnswerInlineQueryAsync(
                inlineQueryId: update.InlineQuery.Id,
                results: results,
                cacheTime: 0
            );
        }

        [Fact(DisplayName = FactTitles.ShouldAnswerInlineQueryWithVenue)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        [ExecutionOrder(4)]
        public async Task Should_Answer_Inline_Query_With_Venue()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAnswerInlineQueryWithVenue,
                startInlineQuery: true);

            Update update = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            InlineQueryResultBase[] results =
            {
                new InlineQueryResultVenue(
                    id: "bot-api",
                    latitude: -37.8721897f,
                    longitude: 175.6810213f,
                    title: "Hobbiton Movie Set",
                    address: "501 Buckland Rd, Hinuera, Matamata 3472, New Zealand")
            };

            await BotClient.AnswerInlineQueryAsync(
                inlineQueryId: update.InlineQuery.Id,
                results: results,
                cacheTime: 0
            );
        }

        [Fact(DisplayName = FactTitles.ShouldAnswerInlineQueryWithPhoto)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        [ExecutionOrder(5)]
        public async Task Should_Answer_Inline_Query_With_Photo()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAnswerInlineQueryWithPhoto,
                startInlineQuery: true);

            Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            const string url = "https://cdn.pixabay.com/photo/2017/08/30/12/45/girl-2696947_640.jpg";

            InlineQueryResultBase[] results =
            {
                new InlineQueryResultPhoto(
                    id: "photo1",
                    photoUrl: url,
                    thumbUrl: url
                )
                {
                    Caption = "Rainbow Girl"
                }
            };

            await BotClient.AnswerInlineQueryAsync(
                inlineQueryId: iqUpdate.InlineQuery.Id,
                results: results,
                cacheTime: 0
            );
        }

        [Fact(DisplayName = FactTitles.ShouldAnswerInlineQueryWithCachedPhoto)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        [ExecutionOrder(9)]
        public async Task Should_Answer_Inline_Query_With_Cached_Photo()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAnswerInlineQueryWithCachedPhoto);

            Message photoMessage;
            using (var stream = System.IO.File.OpenRead(Constants.FileNames.Photos.Apes))
            {
                photoMessage = await BotClient.SendPhotoAsync(
                    chatId: _fixture.SupergroupChat,
                    photo: stream,
                    replyMarkup: (InlineKeyboardMarkup) InlineKeyboardButton
                        .WithSwitchInlineQueryCurrentChat("Start inline query")
                );
            }

            Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            InlineQueryResultBase[] results =
            {
                new InlineQueryResultCachedPhoto(
                    id: "photo1",
                    photoFileId: photoMessage.Photo.First().FileId
                )
                {
                    Caption = "Apes smoking shisha"
                }
            };

            await BotClient.AnswerInlineQueryAsync(
                inlineQueryId: iqUpdate.InlineQuery.Id,
                results: results,
                cacheTime: 0
            );
        }

        [Fact(DisplayName = FactTitles.ShouldAnswerInlineQueryWithVideo)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        [ExecutionOrder(6)]
        public async Task Should_Answer_Inline_Query_With_Video()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAnswerInlineQueryWithVideo,
                startInlineQuery: true);

            Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            // Video from https://pixabay.com/en/videos/sunset-landscape-idyll-cows-10737/
            InlineQueryResultBase[] results =
            {
                new InlineQueryResultVideo(
                    id: "sunset_video",
                    videoUrl: "https://pixabay.com/en/videos/download/video-10737_medium.mp4",
                    mimeType: "video/mp4",
                    thumbUrl: "https://i.vimeocdn.com/video/646283246_640x360.jpg",
                    title: "Sunset Landscape"
                )
                {
                    Description = "A beautiful scene"
                }
            };

            await BotClient.AnswerInlineQueryAsync(
                inlineQueryId: iqUpdate.InlineQuery.Id,
                results: results,
                cacheTime: 0
            );
        }

        [Fact(DisplayName = FactTitles.ShouldAnswerInlineQueryWithHtmlVideo)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        [ExecutionOrder(8)]
        public async Task Should_Answer_Inline_Query_With_HTML_Video()
        {
            // ToDo exception when input_message_content not specified. Bad Request: SEND_MESSAGE_MEDIA_INVALID

            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAnswerInlineQueryWithHtmlVideo,
                startInlineQuery: true);

            Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            InlineQueryResultBase[] results =
            {
                new InlineQueryResultVideo(
                    id: "fireworks_video",
                    videoUrl: "https://www.youtube.com/watch?v=56MDJ9tD6MY",
                    mimeType: "text/html",
                    thumbUrl: "https://www.youtube.com/watch?v=56MDJ9tD6MY",
                    title: "30 Rare Goals We See in Football"
                )
                {
                    InputMessageContent = new InputTextMessageContent
                    {
                        MessageText = "[30 Rare Goals We See in Football](https://www.youtube.com/watch?v=56MDJ9tD6MY)",
                        ParseMode = ParseMode.Markdown
                    }
                }
            };

            await BotClient.AnswerInlineQueryAsync(
                inlineQueryId: iqUpdate.InlineQuery.Id,
                results: results,
                cacheTime: 0
            );
        }

        [Fact(DisplayName = FactTitles.ShouldAnswerInlineQueryWithCachedVideo)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        [ExecutionOrder(7)]
        public async Task Should_Answer_Inline_Query_With_Cached_Video()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAnswerInlineQueryWithCachedVideo);

            // Video from https://pixabay.com/en/videos/fireworks-rocket-new-year-s-eve-7122/
            Message videoMessage = await BotClient.SendVideoAsync(
                chatId: _fixture.SupergroupChat,
                video: "https://pixabay.com/en/videos/download/video-7122_medium.mp4",
                replyMarkup: (InlineKeyboardMarkup) InlineKeyboardButton
                    .WithSwitchInlineQueryCurrentChat("Start inline query")
            );

            Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            InlineQueryResultBase[] results =
            {
                new InlineQueryResultCachedVideo(
                    id: "fireworks_video",
                    videoFileId: videoMessage.Video.FileId,
                    title: "New Year's Eve Fireworks"
                )
                {
                    Description = "2017 Fireworks in Germany"
                }
            };

            await BotClient.AnswerInlineQueryAsync(
                inlineQueryId: iqUpdate.InlineQuery.Id,
                results: results,
                cacheTime: 0
            );
        }

        [Fact(DisplayName = FactTitles.ShouldAnswerInlineQueryWithAudio)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        [ExecutionOrder(10)]
        public async Task Should_Answer_Inline_Query_With_Audio()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAnswerInlineQueryWithAudio,
                startInlineQuery: true);

            Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            InlineQueryResultBase[] results =
            {
                new InlineQueryResultAudio(
                    id: "audio_result",
                    audioUrl:
                    "https://upload.wikimedia.org/wikipedia/commons/transcoded/b/bb/Test_ogg_mp3_48kbps.wav/Test_ogg_mp3_48kbps.wav.mp3",
                    title: "Test ogg mp3"
                )
                {
                    Performer = "Shishirdasika",
                    AudioDuration = 25
                }
            };

            await BotClient.AnswerInlineQueryAsync(
                inlineQueryId: iqUpdate.InlineQuery.Id,
                results: results,
                cacheTime: 0
            );
        }

        [Fact(DisplayName = FactTitles.ShouldAnswerInlineQueryWithCachedAudio)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        [ExecutionOrder(11)]
        public async Task Should_Answer_Inline_Query_With_Cached_Audio()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAnswerInlineQueryWithCachedAudio);

            Message audioMessage;
            using (var stream = System.IO.File.OpenRead(Constants.FileNames.Audio.CantinaRagMp3))
            {
                audioMessage = await BotClient.SendAudioAsync(
                    chatId: _fixture.SupergroupChat,
                    audio: stream,
                    performer: "Jackson F. Smith",
                    duration: 201,
                    replyMarkup: (InlineKeyboardMarkup) InlineKeyboardButton
                        .WithSwitchInlineQueryCurrentChat("Start inline query")
                );
            }

            Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            InlineQueryResultBase[] results =
            {
                new InlineQueryResultCachedAudio(
                    id: "audio_result",
                    audioFileId: audioMessage.Audio.FileId
                )
                {
                    Caption = "Jackson F. Smith - Cantina Rag"
                }
            };

            await BotClient.AnswerInlineQueryAsync(
                inlineQueryId: iqUpdate.InlineQuery.Id,
                results: results,
                cacheTime: 0
            );
        }

        [Fact(DisplayName = FactTitles.ShouldAnswerInlineQueryWithAudio)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        [ExecutionOrder(12)]
        public async Task Should_Answer_Inline_Query_With_Voice()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAnswerInlineQueryWithAudio,
                startInlineQuery: true);

            Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            InlineQueryResultBase[] results =
            {
                new InlineQueryResultVoice(
                    id: "voice_result",
                    voiceUrl: "http://www.vorbis.com/music/Hydrate-Kenny_Beltrey.ogg",
                    title: "Hydrate - Kenny Beltrey"
                )
                {
                    Caption = "Hydrate - Kenny Beltrey",
                    VoiceDuration = 265
                }
            };

            await BotClient.AnswerInlineQueryAsync(
                inlineQueryId: iqUpdate.InlineQuery.Id,
                results: results,
                cacheTime: 0
            );
        }

        [Fact(DisplayName = FactTitles.ShouldAnswerInlineQueryWithCachedAudio)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        [ExecutionOrder(13)]
        public async Task Should_Answer_Inline_Query_With_Cached_Voice()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAnswerInlineQueryWithCachedAudio);

            Message voiceMessage;
            using (var stream = System.IO.File.OpenRead(Constants.FileNames.Audio.TestOgg))
            {
                voiceMessage = await BotClient.SendVoiceAsync(
                    chatId: _fixture.SupergroupChat,
                    voice: stream,
                    duration: 24,
                    replyMarkup: (InlineKeyboardMarkup) InlineKeyboardButton
                        .WithSwitchInlineQueryCurrentChat("Start inline query")
                );
            }

            Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            InlineQueryResultBase[] results =
            {
                new InlineQueryResultCachedVoice(
                    id: "voice_result",
                    fileId: voiceMessage.Voice.FileId,
                    title: "Test Voice"
                )
            };

            await BotClient.AnswerInlineQueryAsync(
                inlineQueryId: iqUpdate.InlineQuery.Id,
                results: results,
                cacheTime: 0
            );
        }

        [Fact(DisplayName = FactTitles.ShouldAnswerInlineQueryWithDocument)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        [ExecutionOrder(14)]
        public async Task Should_Answer_Inline_Query_With_Document()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAnswerInlineQueryWithDocument,
                startInlineQuery: true);

            Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            InlineQueryResultBase[] results =
            {
                new InlineQueryResultDocument(
                    id: "document_result",
                    documentUrl: "http://www.adobe.com/content/dam/acom/en/devnet/acrobat/pdfs/pdf_open_parameters.pdf",
                    title: "Parameters for Opening PDF Files",
                    mimeType: "application/pdf"
                )
                {
                    Caption = "Parameters for Opening PDF Files",
                    Description = "Sample PDF file",
                }
            };

            await BotClient.AnswerInlineQueryAsync(
                inlineQueryId: iqUpdate.InlineQuery.Id,
                results: results,
                cacheTime: 0
            );
        }

        [Fact(DisplayName = FactTitles.ShouldAnswerInlineQueryWithCachedDocument)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        [ExecutionOrder(15)]
        public async Task Should_Answer_Inline_Query_With_Cached_Document()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAnswerInlineQueryWithCachedDocument);

            Message documentMessage;
            using (var stream = System.IO.File.OpenRead(Constants.FileNames.Documents.Hamlet))
            {
                documentMessage = await BotClient.SendDocumentAsync(
                    chatId: _fixture.SupergroupChat,
                    document: stream,
                    replyMarkup: (InlineKeyboardMarkup) InlineKeyboardButton
                        .WithSwitchInlineQueryCurrentChat("Start inline query")
                );
            }

            Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            InlineQueryResultBase[] results =
            {
                new InlineQueryResultCachedDocument(
                    id: "document_result",
                    documentFileId: documentMessage.Document.FileId,
                    title: "Test Document"
                )
                {
                    Caption = "The Tragedy of Hamlet, Prince of Denmark",
                    Description = "Sample PDF Document",
                }
            };

            await BotClient.AnswerInlineQueryAsync(
                inlineQueryId: iqUpdate.InlineQuery.Id,
                results: results,
                cacheTime: 0
            );
        }

        [Fact(DisplayName = FactTitles.ShouldAnswerInlineQueryWithGif)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        [ExecutionOrder(16)]
        public async Task Should_Answer_Inline_Query_With_Gif()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAnswerInlineQueryWithGif,
                startInlineQuery: true);

            Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            InlineQueryResultBase[] results =
            {
                new InlineQueryResultGif(
                    id: "gif_result",
                    gifUrl: "https://upload.wikimedia.org/wikipedia/commons/2/2c/Rotating_earth_%28large%29.gif",
                    thumbUrl: "https://upload.wikimedia.org/wikipedia/commons/2/2c/Rotating_earth_%28large%29.gif"
                )
                {
                    Caption = "Rotating Earth",
                    GifDuration = 4,
                    GifHeight = 400,
                    GifWidth = 400,
                    Title = "Rotating Earth",
                }
            };

            await BotClient.AnswerInlineQueryAsync(
                inlineQueryId: iqUpdate.InlineQuery.Id,
                results: results,
                cacheTime: 0
            );
        }

        [Fact(DisplayName = FactTitles.ShouldAnswerInlineQueryWithCachedGif)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        [ExecutionOrder(17)]
        public async Task Should_Answer_Inline_Query_With_Cached_Gif()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAnswerInlineQueryWithCachedGif);

            Message gifMessage = await BotClient.SendDocumentAsync(
                chatId: _fixture.SupergroupChat,
                document: "https://upload.wikimedia.org/wikipedia/commons/2/2c/Rotating_earth_%28large%29.gif",
                replyMarkup: (InlineKeyboardMarkup) InlineKeyboardButton
                    .WithSwitchInlineQueryCurrentChat("Start inline query"));

            Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            InlineQueryResultBase[] results =
            {
                new InlineQueryResultCachedGif(
                    id: "gif_result",
                    gifFileId: gifMessage.Document.FileId
                )
                {
                    Caption = "Rotating Earth",
                }
            };

            await BotClient.AnswerInlineQueryAsync(
                inlineQueryId: iqUpdate.InlineQuery.Id,
                results: results,
                cacheTime: 0
            );
        }

        [Fact(DisplayName = FactTitles.ShouldAnswerInlineQueryWithMpeg4Gif)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        [ExecutionOrder(18)]
        public async Task Should_Answer_Inline_Query_With_Mpeg4Gif()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAnswerInlineQueryWithMpeg4Gif,
                startInlineQuery: true);

            Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            InlineQueryResultBase[] results =
            {
                new InlineQueryResultMpeg4Gif(
                    id: "mpeg4_gif_result",
                    mpeg4Url: "https://pixabay.com/en/videos/download/video-10737_medium.mp4",
                    thumbUrl: "https://i.vimeocdn.com/video/646283246_640x360.jpg"
                )
                {
                    Caption = "A beautiful scene",
                },
            };

            await BotClient.AnswerInlineQueryAsync(
                inlineQueryId: iqUpdate.InlineQuery.Id,
                results: results,
                cacheTime: 0
            );
        }

        [Fact(DisplayName = FactTitles.ShouldAnswerInlineQueryWithCachedMpeg4Gif)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        [ExecutionOrder(19)]
        public async Task Should_Answer_Inline_Query_With_Cached_Mpeg4Gif()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAnswerInlineQueryWithCachedMpeg4Gif);

            Message gifMessage = await BotClient.SendDocumentAsync(
                chatId: _fixture.SupergroupChat,
                document: "https://upload.wikimedia.org/wikipedia/commons/2/2c/Rotating_earth_%28large%29.gif",
                replyMarkup: (InlineKeyboardMarkup) InlineKeyboardButton
                    .WithSwitchInlineQueryCurrentChat("Start inline query"));

            Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            InlineQueryResultBase[] results =
            {
                new InlineQueryResultCachedMpeg4Gif(
                    id: "mpeg4_gif_result",
                    mpeg4FileId: gifMessage.Document.FileId
                )
                {
                    Caption = "A beautiful scene",
                }
            };

            await BotClient.AnswerInlineQueryAsync(
                inlineQueryId: iqUpdate.InlineQuery.Id,
                results: results,
                cacheTime: 0
            );
        }

        [Fact(DisplayName = FactTitles.ShouldAnswerInlineQueryWithCachedSticker)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetStickerSet)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        [ExecutionOrder(20)]
        public async Task Should_Answer_Inline_Query_With_Cached_Sticker()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAnswerInlineQueryWithCachedSticker,
                startInlineQuery: true);

            Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            StickerSet stickerSet = await BotClient.GetStickerSetAsync("EvilMinds");
            
            InlineQueryResultBase[] results =
            {
                new InlineQueryResultCachedSticker(
                    id: "sticker_result",
                    stickerFileId: stickerSet.Stickers[0].FileId
                )
            };

            await BotClient.AnswerInlineQueryAsync(
                inlineQueryId: iqUpdate.InlineQuery.Id,
                results: results,
                cacheTime: 0
            );
        }
        
        private static class FactTitles
        {
            public const string ShouldAnswerInlineQueryWithArticle = "Should answer inline query with an article";

            public const string ShouldAnswerInlineQueryWithContact = "Should answer inline query with a contact";

            public const string ShouldAnswerInlineQueryWithLocation = "Should answer inline query with a location";

            public const string ShouldAnswerInlineQueryWithVenue = "Should answer inline query with a venue";

            public const string ShouldAnswerInlineQueryWithPhoto = "Should answer inline query with a photo";

            public const string ShouldAnswerInlineQueryWithCachedPhoto =
                "Should send a photo and answer inline query with a cached photo using its file_id";

            public const string ShouldAnswerInlineQueryWithVideo = "Should answer inline query with a video";

            public const string ShouldAnswerInlineQueryWithHtmlVideo =
                "Should answer inline query with a YouTube video (HTML page)";

            public const string ShouldAnswerInlineQueryWithCachedVideo =
                "Should send a video and answer inline query with a cached video using its file_id";

            public const string ShouldAnswerInlineQueryWithAudio = "Should answer inline query with an audio";

            public const string ShouldAnswerInlineQueryWithCachedAudio =
                "Should send a audio and answer inline query with a cached audio using its file_id";

            public const string ShouldAnswerInlineQueryWithDocument = "Should answer inline query with a document";

            public const string ShouldAnswerInlineQueryWithCachedDocument =
                "Should send a document and answer inline query with a cached document using its file_id";

            public const string ShouldAnswerInlineQueryWithGif = "Should answer inline query with a gif";

            public const string ShouldAnswerInlineQueryWithCachedGif =
                "Should send a gif and answer inline query with a cached gif using its file_id";

            public const string ShouldAnswerInlineQueryWithMpeg4Gif = "Should answer inline query with an mpeg4 gif";

            public const string ShouldAnswerInlineQueryWithCachedMpeg4Gif =
                "Should send an mpeg4 gif and answer inline query with a cached mpeg4 gif using its file_id";
            
            public const string ShouldAnswerInlineQueryWithCachedSticker =
                "Should answer inline query with a cached sticker using its file_id";
        }
    }
}
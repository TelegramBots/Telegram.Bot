using System;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputMessageContents;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.ReplyMarkups.Buttons;
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

            const string url = "https://loremflickr.com/600/400/history,culture,art,nature";
            string photoUrl = $"{url}?q={new Random().Next(100_000_000)}";

            InlineQueryResultBase[] results =
            {
                new InlineQueryResultPhoto(
                    id: "photo1",
                    photoUrl: photoUrl,
                    thumbUrl: photoUrl
                )
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

        private static class FactTitles
        {
            public const string ShouldAnswerInlineQueryWithArticle = "Should answer inline query with an article";

            public const string ShouldAnswerInlineQueryWithContact = "Should answer inline query with a contact";

            public const string ShouldAnswerInlineQueryWithLocation = "Should answer inline query with a location";

            public const string ShouldAnswerInlineQueryWithVenue = "Should answer inline query with a venue";

            public const string ShouldAnswerInlineQueryWithPhoto = "Should answer inline query with a photo";

            public const string ShouldAnswerInlineQueryWithVideo = "Should answer inline query with a video";

            public const string ShouldAnswerInlineQueryWithCachedVideo =
                "Should send a video and answer inline query with a cached video using its file_id";
        }
    }
}
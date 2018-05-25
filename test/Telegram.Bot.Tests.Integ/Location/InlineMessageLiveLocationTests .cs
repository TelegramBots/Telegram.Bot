using System;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Tests.Integ.Locations
{
    [Collection(Constants.TestCollections.InlineMessageLiveLocation)]
    [Trait(Constants.CategoryTraitName, Constants.InteractiveCategoryValue)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class InlineMessageLiveLocationTests : IClassFixture<InlineMessageLiveLocationTests.Fixture>
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly Fixture _classFixture;

        private readonly TestsFixture _fixture;

        public InlineMessageLiveLocationTests(TestsFixture fixture, Fixture classFixture)
        {
            _fixture = fixture;
            _classFixture = classFixture;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldAnswerInlineQueryWithLocation)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditMessageLiveLocation)]
        public async Task Should_Answer_Inline_Query_With_Location()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAnswerInlineQueryWithLocation,
                startInlineQuery: true);

            Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            string callbackQueryData = "edit-location" + new Random().Next(1_000);
            Location newYork = new Location {Latitude = 40.7128f, Longitude = -74.0060f};

            await BotClient.AnswerInlineQueryAsync(
                inlineQueryId: iqUpdate.InlineQuery.Id,
                cacheTime: 0,
                results: new[]
                {
                    new InlineQueryResultLocation(
                        id: "live-location",
                        latitude: newYork.Latitude,
                        longitude: newYork.Longitude,
                        title: "Live Locations Test")
                    {
                        LivePeriod = 60,
                        ReplyMarkup = InlineKeyboardButton.WithCallbackData(
                            "Start live locations", callbackQueryData
                        )
                    }
                }
            );

            _classFixture.CallbackQueryData = callbackQueryData;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldEditInlineMessageLiveLocation)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditMessageLiveLocation)]
        public async Task Should_Edit_Inline_Message_Live_Location()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldEditInlineMessageLiveLocation,
                "Click on location message's button to edit the location");

            Update cqUpdate = await _fixture.UpdateReceiver
                .GetCallbackQueryUpdateAsync(data: _classFixture.CallbackQueryData);

            Location beijing = new Location {Latitude = 39.9042f, Longitude = 116.4074f};

            await BotClient.EditMessageLiveLocationAsync(
                inlineMessageId: cqUpdate.CallbackQuery.InlineMessageId,
                latitude: beijing.Latitude,
                longitude: beijing.Longitude,
                replyMarkup: InlineKeyboardMarkup.Empty()
            );

            _classFixture.InlineMessageId = cqUpdate.CallbackQuery.InlineMessageId;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldStopInlineMessageLiveLocation)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.StopMessageLiveLocation)]
        public async Task Should_Stop_Inline_Message_Live_Location()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldStopInlineMessageLiveLocation);

            await BotClient.StopMessageLiveLocationAsync(
                inlineMessageId: _classFixture.InlineMessageId
            );
        }

        private static class FactTitles
        {
            public const string ShouldAnswerInlineQueryWithLocation =
                "Should answer inline query with a location result";

            public const string ShouldEditInlineMessageLiveLocation =
                "Should edit live location of previously sent inline message";

            public const string ShouldStopInlineMessageLiveLocation =
                "Should stop live locations of previously sent inline message";
        }

        public class Fixture
        {
            public string InlineMessageId { get; set; }

            public string CallbackQueryData { get; set; }
        }
    }
}

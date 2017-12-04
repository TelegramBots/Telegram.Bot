using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Telegram.Bot.Tests.Integ.LiveLocation
{
    [Collection(Constants.TestCollections.LiveLocations)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class LiveLocationsTests
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public LiveLocationsTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = FactTitles.ShouldSendLiveLocations)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendLocation)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditMessageLiveLocation)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.StopMessageLiveLocation)]
        [ExecutionOrder(1.1)]
        public async Task Should_Send_Live_Location()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendLiveLocations);

            const int livePeriod = 60;
            Location[] locations =
            {
                new Location {Latitude = 52.5200f, Longitude = 13.4050f}, // Berlin
                new Location {Latitude = 43.6532f, Longitude = -79.3832f}, // Toronto
                new Location {Latitude = 59.9343f, Longitude = 30.3351f}, // Saint Petersburg
                new Location {Latitude = 35.6892f, Longitude = 51.3890f}, // Tehran
            };

            Message message = await BotClient.SendLocationAsync(
                _fixture.SuperGroupChatId,
                locations[0].Latitude,
                locations[0].Longitude,
                livePeriod
            );

            foreach (Location newLocation in locations.Skip(1))
            {
                await Task.Delay(1_500);

                Message editedMessage = await BotClient.EditMessageLiveLocationAsync(
                    message.Chat.Id,
                    message.MessageId,
                    newLocation.Latitude,
                    newLocation.Longitude
                );

                Assert.Equal(message.MessageId, editedMessage.MessageId);
                Assert.Equal(newLocation.Latitude, editedMessage.Location.Latitude, 3);
                Assert.Equal(newLocation.Longitude, editedMessage.Location.Longitude, 3);
            }

            Message stopedLocationMessage = await BotClient.StopMessageLiveLocationAsync(
                message.Chat.Id,
                message.MessageId
            );

            Assert.Equal(message.MessageId, stopedLocationMessage.MessageId);
            Assert.Equal(locations.Last().Latitude, stopedLocationMessage.Location.Latitude, 3);
            Assert.Equal(locations.Last().Longitude, stopedLocationMessage.Location.Longitude, 3);
        }

        [Fact(DisplayName = FactTitles.ShouldAnswerInlineQueryWithLiveLocation)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditMessageLiveLocation)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.StopMessageLiveLocation)]
        [ExecutionOrder(1.2)]
        public async Task Should_Answer_Inline_Query_With_Live_Locations()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAnswerInlineQueryWithLiveLocation,
                "Start an inline query, post its result to chat, and click on the inline button");

            const int livePeriod = 60;

            Location[] locations =
            {
                new Location {Latitude = 40.7128f, Longitude = -74.0060f}, // New York
                new Location {Latitude = 39.9042f, Longitude = 116.4074f}, // Beijing
            };

            InlineKeyboardMarkup initialMarkup = new InlineKeyboardMarkup(new InlineKeyboardButton[]
            {
                new InlineKeyboardCallbackButton("Start live locations", "start-live-location"),
            });

            InlineQueryResult[] inlineQueryResults =
            {
                new InlineQueryResultLocation
                {
                    Id = "live-location",
                    Title = "Live Locations Test",
                    Latitude = locations[0].Latitude,
                    Longitude = locations[0].Longitude,
                    LivePeriod = livePeriod,
                    ReplyMarkup = initialMarkup
                },
            };

            string inlineQueryId = (await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync()).InlineQuery.Id;

            await BotClient.AnswerInlineQueryAsync(
                inlineQueryId,
                inlineQueryResults,
                cacheTime: 0
            );

            string inlineMessageId = (await _fixture.UpdateReceiver.GetCallbackQueryUpdateAsync())
                .CallbackQuery.InlineMessageId;

            foreach (Location newLocation in locations.Skip(1))
            {
                await Task.Delay(15_00);

                bool result = await BotClient.EditMessageLiveLocationAsync(
                    inlineMessageId,
                    newLocation.Latitude,
                    newLocation.Longitude,
                    new InlineKeyboardMarkup(new InlineKeyboardButton[0])
                );

                Assert.True(result);
            }

            bool stopResult = await BotClient.StopMessageLiveLocationAsync(inlineMessageId);

            Assert.True(stopResult);
        }

        private static class FactTitles
        {
            public const string ShouldSendLiveLocations =
                "Should send a live location, update it 3 times, and stop live locations";

            public const string ShouldAnswerInlineQueryWithLiveLocation =
                "Should answer inline query with live locations, update it, and stop live locations";
        }
    }
}
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Telegram.Bot.Tests.Integ.UpdatingMessages
{
    [Collection(CommonConstants.TestCollections.LiveLocation)]
    [TestCaseOrderer(CommonConstants.TestCaseOrderer, CommonConstants.AssemblyName)]
    public class LiveLocationTests
    {
        public ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public LiveLocationTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = FactTitles.ShouldSendLiveLocation)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.SendLocation)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.EditMessageLiveLocation)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.StopMessageLiveLocation)]
        [ExecutionOrder(1.1)]
        public async Task Should_Send_Live_Location()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendLiveLocation);

            var livePeriod = 60;
            var initialLocation = new Location { Latitude = 60, Longitude = 30 };

            var subsequentLocations = new[]
            {
                new Location {Latitude = 60, Longitude = 31},
                new Location {Latitude = 60, Longitude = 32},
                new Location {Latitude = 60, Longitude = 33}
            };

            var message = await BotClient.SendLocationAsync(_fixture.SuperGroupChatId, initialLocation.Latitude,
                initialLocation.Longitude, livePeriod);

            await Task.Delay(500);

            foreach (var subsequentLocation in subsequentLocations)
            {
                var editedMessage =
                    await BotClient.EditMessageLiveLocationAsync(message.Chat.Id, message.MessageId,
                        subsequentLocation.Latitude, subsequentLocation.Longitude);

                Assert.Equal(message.MessageId, editedMessage.MessageId);

                await Task.Delay(500);
            }

            var stopedLocationMessage = await BotClient.StopMessageLiveLocationAsync(message.Chat.Id, message.MessageId);

            Assert.Equal(message.MessageId, stopedLocationMessage.MessageId);
        }

        [Fact(DisplayName = FactTitles.ShouldAnswerInlineQueryWithLiveLocation)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.AnswerInlineQuery)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.EditMessageLiveLocation)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.StopMessageLiveLocation)]
        [ExecutionOrder(1.2)]
        public async Task Should_Answer_Inline_Query_With_Live_Location()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAnswerInlineQueryWithLiveLocation,
                "Start an inline query, post its result to chat, click on the inline button and wait");

            var livePeriod = 60;
            var initialLocation = new Location { Latitude = 60, Longitude = 30 };

            var initialMarkup = new InlineKeyboardMarkup(new InlineKeyboardButton[]
            {
                new InlineKeyboardCallbackButton("Click me", "start-live-location"),
            });

            var editedMarkup = new InlineKeyboardMarkup(new InlineKeyboardButton[0]);

            var subsequentLocations = new[]
            {
                new Location {Latitude = 60, Longitude = 31},
                new Location {Latitude = 60, Longitude = 32},
                new Location {Latitude = 60, Longitude = 33}
            };

            var inlineQueryResults = new InlineQueryResult[]
            {
                new InlineQueryResultLocation
                {
                    Id = "live-location",
                    Title = "Live location test",
                    Latitude = initialLocation.Latitude,
                    Longitude = initialLocation.Longitude,
                    LivePeriod = livePeriod,
                    ReplyMarkup = initialMarkup
                },
            };

            var iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            await BotClient.AnswerInlineQueryAsync(iqUpdate.InlineQuery.Id, inlineQueryResults, 0);
            var callbackUpdate = await _fixture.UpdateReceiver.GetCallbackQueryUpdateAsync();

            foreach (var subsequentLocation in subsequentLocations)
            {
                await BotClient.EditMessageLiveLocationAsync(callbackUpdate.CallbackQuery.InlineMessageId,
                    subsequentLocation.Latitude, subsequentLocation.Longitude, editedMarkup);

                await Task.Delay(1000);
            }

            var stopSuccess = await BotClient.StopMessageLiveLocationAsync(callbackUpdate.CallbackQuery.InlineMessageId);

            Assert.True(stopSuccess);
        }

        private static class FactTitles
        {
            public const string ShouldSendLiveLocation = "Should send a live location with a few location updates and then stop live location";

            public const string ShouldAnswerInlineQueryWithLiveLocation = "Should answer inline query with a live location with a few location updates and then stop live location";
        }
    }
}

using System;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Tests.Integ.Locations;

[Collection(Constants.TestCollections.InlineMessageLiveLocation)]
[Trait(Constants.CategoryTraitName, Constants.InteractiveCategoryValue)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class InlineMessageLiveLocationTests(TestsFixture fixture, InlineMessageLiveLocationTests.Fixture classFixture)
    : IClassFixture<InlineMessageLiveLocationTests.Fixture>
{
    ITelegramBotClient BotClient => fixture.BotClient;

    [OrderedFact("Should answer inline query with a location result")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditMessageLiveLocation)]
    public async Task Should_Answer_Inline_Query_With_Location()
    {
        await fixture.SendTestInstructionsAsync(
            "Staring the inline query with this message...",
            startInlineQuery: true
        );

        Update iqUpdate = await fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        string callbackQueryData = $"edit-location{new Random().Next(1_000)}";
        Location newYork = new() { Latitude = 40.7128f, Longitude = -74.0060f };

        await BotClient.AnswerInlineQueryAsync(
            new()
            {
                InlineQueryId = iqUpdate.InlineQuery!.Id,
                CacheTime = 0,
                Results = [
                    new InlineQueryResultLocation
                    {
                        Id = "live-location",
                        Latitude = newYork.Latitude,
                        Longitude = newYork.Longitude,
                        Title = "Live Locations Test",
                        LivePeriod = 60,
                        ReplyMarkup = InlineKeyboardButton.WithCallbackData("Start live locations", callbackQueryData)
                    }
                ],
            }
        );

        classFixture.CallbackQueryData = callbackQueryData;
    }

    [OrderedFact("Should edit live location of previously sent inline message")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditMessageLiveLocation)]
    public async Task Should_Edit_Inline_Message_Live_Location()
    {
        await fixture.SendTestInstructionsAsync("Click on location message's button to edit the location");

        Update cqUpdate = await fixture.UpdateReceiver
            .GetCallbackQueryUpdateAsync(data: classFixture.CallbackQueryData);

        Location beijing = new() { Latitude = 39.9042f, Longitude = 116.4074f };

        await BotClient.EditInlineMessageLiveLocationAsync(
            new()
            {
                InlineMessageId = cqUpdate.CallbackQuery!.InlineMessageId!,
                Latitude = beijing.Latitude,
                Longitude = beijing.Longitude,
                ReplyMarkup = InlineKeyboardMarkup.Empty(),
            }
        );

        classFixture.InlineMessageId = cqUpdate.CallbackQuery.InlineMessageId;
    }

    [OrderedFact("Should stop live locations of previously sent inline message")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.StopMessageLiveLocation)]
    public async Task Should_Stop_Inline_Message_Live_Location()
    {
        await BotClient.StopInlineMessageLiveLocationAsync(
            new()
            {
                InlineMessageId = classFixture.InlineMessageId,
            }
        );
    }

    public class Fixture
    {
        public string InlineMessageId { get; set; }

        public string CallbackQueryData { get; set; }
    }
}

using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Tests.Integ.Framework.Fixtures;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Tests.Integ.Locations;

[Collection(Constants.TestCollections.LiveLocation)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class LiveLocationTests(TestsFixture fixture, EntityFixture<Message> classFixture)
    : TestClass(fixture), IClassFixture<EntityFixture<Message>>
{
    Message LocationMessage
    {
        get => classFixture.Entity;
        set => classFixture.Entity = value;
    }

    [OrderedFact("Should send a location with live period to update")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendLocation)]
    public async Task Should_Send_Live_Location()
    {
        const float latBerlin = 52.5200f;
        const float lonBerlin = 13.4050f;

        Message message = await BotClient.SendLocation(
            chatId: Fixture.SupergroupChat.Id,
            latitude: latBerlin,
            longitude: lonBerlin,
            livePeriod: 60
        );

        Assert.Equal(MessageType.Location, message.Type);
        Assert.Equal(latBerlin, message.Location!.Latitude, 3);
        Assert.Equal(lonBerlin, message.Location.Longitude, 3);

        LocationMessage = message;
    }

    [OrderedFact("Should update live location 3 times")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditMessageLiveLocation)]
    public async Task Should_Update_Live_Location()
    {
        Location[] locations = [
            new() { Latitude = 43.6532f, Longitude = -79.3832f }, // Toronto
            new() { Latitude = 59.9343f, Longitude = 30.3351f },  // Saint Petersburg
            new() { Latitude = 35.6892f, Longitude = 51.3890f } // Tehran
        ];

        Message editedMessage = default;
        foreach (Location newLocation in locations)
        {
            await Task.Delay(1_500);

            editedMessage = await BotClient.EditMessageLiveLocation(
                chatId: LocationMessage.Chat.Id,
                messageId: LocationMessage.Id,
                latitude: newLocation.Latitude,
                longitude: newLocation.Longitude
            );

            Assert.Equal(MessageType.Location, editedMessage.Type);
            Assert.Equal(LocationMessage.Id, editedMessage.Id);
            Assert.Equal(newLocation.Latitude, editedMessage.Location!.Latitude, 3);
            Assert.Equal(newLocation.Longitude, editedMessage.Location!.Longitude, 3);
        }

        LocationMessage = editedMessage!;
    }

    [OrderedFact("Should stop live locations")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.StopMessageLiveLocation)]
    public async Task Should_Stop_Live_Location()
    {
        Message message = await BotClient.StopMessageLiveLocation(
            chatId: LocationMessage.Chat,
            messageId: LocationMessage.Id
        );

        Assert.Equal(LocationMessage.Id, message.Id);
        Assert.Equal(LocationMessage.Chat.Id, message.Chat.Id);
        Assert.Equal(LocationMessage.From!.Id, message.From!.Id);
        Assert.Equal(LocationMessage.Location!.Longitude, message.Location!.Longitude);
        Assert.Equal(LocationMessage.Location.Latitude, message.Location.Latitude);
        Assert.Equal(LocationMessage.Location.Longitude, message.Location.Longitude);
    }
}

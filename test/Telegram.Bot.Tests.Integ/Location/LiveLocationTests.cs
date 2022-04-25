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
public class LiveLocationTests : IClassFixture<EntityFixture<Message>>
{
    ITelegramBotClient BotClient => _fixture.BotClient;

    Message LocationMessage
    {
        get => _classFixture.Entity;
        set => _classFixture.Entity = value;
    }

    readonly TestsFixture _fixture;

    readonly EntityFixture<Message> _classFixture;

    public LiveLocationTests(TestsFixture fixture, EntityFixture<Message> classFixture)
    {
        _fixture = fixture;
        _classFixture = classFixture;
    }

    [OrderedFact("Should send a location with live period to update")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendLocation)]
    public async Task Should_Send_Live_Location()
    {
        const float latBerlin = 52.5200f;
        const float lonBerlin = 13.4050f;

        Message message = await BotClient.SendLocationAsync(
            chatId: _fixture.SupergroupChat.Id,
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
        Location[] locations = {
            new Location { Latitude = 43.6532f, Longitude = -79.3832f }, // Toronto
            new Location { Latitude = 59.9343f, Longitude = 30.3351f },  // Saint Petersburg
            new Location { Latitude = 35.6892f, Longitude = 51.3890f },  // Tehran
        };

        Message editedMessage = default;
        foreach (Location newLocation in locations)
        {
            await Task.Delay(1_500);

            editedMessage = await BotClient.EditMessageLiveLocationAsync(
                chatId: LocationMessage.Chat.Id,
                messageId: LocationMessage.MessageId,
                latitude: newLocation.Latitude,
                longitude: newLocation.Longitude
            );

            Assert.Equal(MessageType.Location, editedMessage.Type);
            Assert.Equal(LocationMessage.MessageId, editedMessage.MessageId);
            Assert.Equal(newLocation.Latitude, editedMessage.Location!.Latitude, 3);
            Assert.Equal(newLocation.Longitude, editedMessage.Location!.Longitude, 3);
        }

        LocationMessage = editedMessage!;
    }

    [OrderedFact("Should stop live locations")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.StopMessageLiveLocation)]
    public async Task Should_Stop_Live_Location()
    {
        Message message = await BotClient.StopMessageLiveLocationAsync(
            chatId: LocationMessage.Chat,
            messageId: LocationMessage.MessageId
        );

        Assert.Equal(LocationMessage.MessageId, message.MessageId);
        Assert.Equal(LocationMessage.Chat.Id, message.Chat.Id);
        Assert.Equal(LocationMessage.From!.Id, message.From!.Id);
        Assert.Equal(LocationMessage.Location!.Longitude, message.Location!.Longitude);
        Assert.Equal(LocationMessage.Location.Latitude, message.Location.Latitude);
        Assert.Equal(LocationMessage.Location.Longitude, message.Location.Longitude);
    }
}
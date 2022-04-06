using System.Threading.Tasks;
using Newtonsoft.Json;
using Telegram.Bot.Requests;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Sending_Messages;

[Collection(Constants.TestCollections.SendVenueMessage)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class SendingVenueMessageTests
{
    ITelegramBotClient BotClient => _fixture.BotClient;

    readonly TestsFixture _fixture;

    public SendingVenueMessageTests(TestsFixture fixture)
    {
        _fixture = fixture;
    }

    [OrderedFact("Should send a venue")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendVenue)]
    public async Task Should_Send_Venue()
    {
        const string title = "Rubirosa Ristorante";
        const string address = "235 Mulberry St";
        const float lat = 40.722728f;
        const float lon = -73.996006f;
        const string foursquareId = "4cc6222106c25481d7a4a047";

        Message message = await BotClient.SendVenueAsync(
            chatId: _fixture.SupergroupChat,
            latitude: lat,
            longitude: lon,
            title: title,
            address: address,
            foursquareId: foursquareId
        );

        Assert.Equal(MessageType.Venue, message.Type);
        Assert.NotNull(message.Venue);
        Assert.Equal(title, message.Venue.Title);
        Assert.Equal(address, message.Venue.Address);
        Assert.Equal(foursquareId, message.Venue.FoursquareId);
        Assert.InRange(message.Venue.Location.Latitude, lat - 0.001f, lat + 0.001f);
        Assert.InRange(message.Venue.Location.Longitude, lon - 0.001f, lon + 0.001f);
    }

    [OrderedFact("Should deserialize a sendVenue request and send it")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendVenue)]
    public async Task Should_Deserialize_Send_Venue()
    {
        string json = $@"{{
                chat_id: ""{_fixture.SupergroupChat.Id}"",
                latitude: 48.204296,
                longitude: 16.365514,
                title: ""Burggarten"",
                address: ""Opernring"",
                foursquare_id: ""4b7ff7c3f964a5208d4730e3"",
                foursquare_type: ""parks_outdoors/park""
            }}";

        SendVenueRequest request = JsonConvert.DeserializeObject<SendVenueRequest>(json);

        Message message = await BotClient.MakeRequestAsync(request);

        Assert.Equal(MessageType.Venue, message.Type);
        Assert.NotNull(message.Venue);
        Assert.Equal("Burggarten", message.Venue.Title);
        Assert.Equal("Opernring", message.Venue.Address);
        Assert.Equal("4b7ff7c3f964a5208d4730e3", message.Venue.FoursquareId);
        Assert.Equal("parks_outdoors/park", message.Venue.FoursquareType);
        Assert.InRange(message.Venue.Location.Latitude, 48.204296 - 0.001f, 48.204296 + 0.001f);
        Assert.InRange(message.Venue.Location.Longitude, 16.365514 - 0.001f, 16.365514 + 0.001f);
    }
}
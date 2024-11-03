using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Sending_Messages;

[Collection(Constants.TestCollections.SendVenueMessage)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class SendingVenueMessageTests(TestsFixture fixture) : TestClass(fixture)
{
    [OrderedFact("Should send a venue")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendVenue)]
    public async Task Should_Send_Venue()
    {
        const string title = "Rubirosa Ristorante";
        const string address = "235 Mulberry St";
        const float lat = 40.722728f;
        const float lon = -73.996006f;
        const string foursquareId = "4cc6222106c25481d7a4a047";

        Message message = await BotClient.SendVenue(
            chatId: Fixture.SupergroupChat,
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
}

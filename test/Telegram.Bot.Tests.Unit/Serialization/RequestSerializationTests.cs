using System.Net.Http;
using System.Threading.Tasks;
using Telegram.Bot.Requests;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class RequestSerializationTests
{
    [Fact]
    public async Task Should_Serialize_DeleteWebhookRequest_Content()
    {
        DeleteWebhookRequest deleteWebhookRequest = new() { DropPendingUpdates = true };
        HttpContent deleteWebhookContent = deleteWebhookRequest.ToHttpContent()!;

        string stringContent = await deleteWebhookContent.ReadAsStringAsync();
        JsonNode? root = JsonNode.Parse(stringContent);
        Assert.NotNull(root);
        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);
        Assert.Single(j);
        Assert.Equal(true, (bool?)j["drop_pending_updates"]);
    }

    [Fact(DisplayName = "Should serialize request")]
    public void Should_Serialize_Request()
    {
        GetUpdatesRequest request = new() { Offset = 12345 };

        string serializeRequest = JsonSerializer.Serialize(request, JsonBotAPI.Options);
        JsonObject j = Assert.IsAssignableFrom<JsonObject>(JsonNode.Parse(serializeRequest));

        Assert.Single(j);
        Assert.Equal(12345, (long?)j["offset"]);
    }

    [Fact(DisplayName = "Should serialize createChatInviteLink request")]
    public async Task Should_Serialize_CreateChatInviteLink_Request()
    {
        DateTime expireDate = new(2022, 1, 8, 10, 33, 45, DateTimeKind.Utc);
        CreateChatInviteLinkRequest createChatInviteLinkRequest = new()
        {
            ChatId = 1_000_000,
            ExpireDate = expireDate,
            CreatesJoinRequest = true,
            MemberLimit = 123,
            Name = "Test link name"
        };

        HttpContent createChatInviteLinkContent = createChatInviteLinkRequest.ToHttpContent()!;

        string stringContent = await createChatInviteLinkContent.ReadAsStringAsync();
        JsonObject j = Assert.IsAssignableFrom<JsonObject>(JsonNode.Parse(stringContent));

        Assert.Equal(5, j.Count);
        Assert.Equal(1641638025, (long?)j["expire_date"]);
        Assert.Equal(1000000, (long?)j["chat_id"]);
        Assert.Equal("Test link name", (string?)j["name"]);
        Assert.Equal(123, (long?)j["member_limit"]);
        Assert.Equal(true, (bool?)j["creates_join_request"]);
    }

    [Fact(DisplayName = "Should build and compare sendPhotoRequest request")]
    public async Task Should_Build_Compare_SendPhotoRequest()
    {
        string json =
            $$"""
            {
                "chat_id": "1234567",
                "photo": "https://cdn.pixabay.com/photo/2017/04/11/21/34/giraffe-2222908_640.jpg",
                "caption": "Photo request deserialized from JSON"
            }
            """;
        SendPhotoRequest? request = JsonSerializer.Deserialize<SendPhotoRequest>(json, JsonBotAPI.Options);

        var bot = new Polling.MockTelegramBotClient();
        var ex = await Assert.ThrowsAsync<NotSupportedException>(
            () => bot.SendPhoto(
                1234567,
                "https://cdn.pixabay.com/photo/2017/04/11/21/34/giraffe-2222908_640.jpg",
                caption: "Photo request deserialized from JSON"
            ));
        var sentRequest = Assert.IsType<SendPhotoRequest>(ex.Data["request"]);

        //TODO Assert.Equivalent(request, sentRequest, true); // needs xunit 2.8.2
        Assert.Equal(JsonSerializer.Serialize(request), JsonSerializer.Serialize(sentRequest));
    }

    [Fact(DisplayName = "Should build and compare sendVenueRequest request")]
    public async Task Should_Build_Compare_SendVenueRequest()
    {
        string json =
            $$"""
            {
                "chat_id": "2345678",
                "latitude": 48.204296,
                "longitude": 16.365514,
                "title": "Burggarten",
                "address": "Opernring",
                "foursquare_id": "4b7ff7c3f964a5208d4730e3",
                "foursquare_type": "parks_outdoors/park"
            }
            """;
        SendVenueRequest request = JsonSerializer.Deserialize<SendVenueRequest>(json, JsonBotAPI.Options)!;
        Assert.Equal("sendVenue", request.MethodName);
        Assert.Equal("Burggarten", request.Title);
        Assert.Equal("Opernring", request.Address);
        Assert.Equal("4b7ff7c3f964a5208d4730e3", request.FoursquareId);
        Assert.Equal("parks_outdoors/park", request.FoursquareType);
        Assert.InRange(request.Latitude, 48.204296 - 0.001f, 48.204296 + 0.001f);
        Assert.InRange(request.Longitude, 16.365514 - 0.001f, 16.365514 + 0.001f);

        var bot = new Polling.MockTelegramBotClient();
        var ex = await Assert.ThrowsAsync<NotSupportedException>(
            () => bot.SendVenue(
                chatId: 2345678,
                latitude: 48.204296,
                longitude: 16.365514,
                title: "Burggarten",
                address: "Opernring",
                foursquareId: "4b7ff7c3f964a5208d4730e3",
                foursquareType: "parks_outdoors/park"
            ));
        var sentRequest = Assert.IsType<SendVenueRequest>(ex.Data["request"]);

        //TODO Assert.Equivalent(request, sentRequest, true); // needs xunit 2.8.2
        Assert.Equal(JsonSerializer.Serialize(request), JsonSerializer.Serialize(sentRequest));
    }
}

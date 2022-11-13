using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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

        Assert.NotNull(stringContent);
        Assert.Contains(@"""drop_pending_updates"":true", stringContent);
    }

    [Fact(DisplayName = "Should serialize request")]
    public void Should_Serialize_Request()
    {
        GetUpdatesRequest request = new() { Offset = 12345 };

        string serializeRequest = JsonConvert.SerializeObject(request);

        Assert.DoesNotContain(@"""MethodName""", serializeRequest);
        Assert.DoesNotContain(@"""IsWebhookResponse""", serializeRequest);
    }

    [Fact(DisplayName = "Should properly serialize request with custom json settings")]
    public void Should_Properly_Serialize_Request_With_Custom_Json_Settings()
    {
        GetUpdatesRequest request = new() { Offset = 12345 };

        JsonSerializerSettings settings = new()
        {
            NullValueHandling = NullValueHandling.Include,
            ContractResolver = new CamelCasePropertyNamesContractResolver
            {
                IgnoreSerializableAttribute = true,
                IgnoreShouldSerializeMembers = true
            },
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateTimeZoneHandling = DateTimeZoneHandling.Unspecified
        };

        string serializeRequest = JsonConvert.SerializeObject(request, settings);

        Assert.DoesNotContain(@"""MethodName""", serializeRequest);
        Assert.DoesNotContain(@"""method_name""", serializeRequest);
        Assert.DoesNotContain(@"""IsWebhookResponse""", serializeRequest);
        Assert.DoesNotContain(@"""is_webhook_response""", serializeRequest);
        Assert.Contains(@"""offset"":12345", serializeRequest);
        Assert.DoesNotContain(@"""allowed_updates""", serializeRequest);
    }

    [Fact(DisplayName = "Should serialize createChatInviteLink request")]
    public async Task Should_Serialize_CreateChatInviteLink_Request()
    {
        DateTime expireDate = new(2022, 1, 8, 10, 33, 45, DateTimeKind.Utc);
        CreateChatInviteLinkRequest createChatInviteLinkRequest = new(chatId: 1_000_000)
        {
            ExpireDate = expireDate,
            CreatesJoinRequest = true,
            MemberLimit = 123,
            Name = "Test link name"
        };

        HttpContent createChatInviteLinkContent = createChatInviteLinkRequest.ToHttpContent()!;

        string stringContent = await createChatInviteLinkContent.ReadAsStringAsync();

        Assert.Contains(@"""expire_date"":1641638025", stringContent);
        Assert.Contains(@"""chat_id"":1000000", stringContent);
        Assert.Contains(@"""name"":""Test link name""", stringContent);
        Assert.Contains(@"""member_limit"":123", stringContent);
        Assert.Contains(@"""creates_join_request"":true", stringContent);
    }
}

using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
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
        JObject j = JObject.Parse(stringContent);


        Assert.Single(j);
        Assert.Equal(true, j["drop_pending_updates"]);
    }

    [Fact(DisplayName = "Should serialize request")]
    public void Should_Serialize_Request()
    {
        GetUpdatesRequest request = new() { Offset = 12345 };

        string serializeRequest = JsonConvert.SerializeObject(request);
        JObject j = JObject.Parse(serializeRequest);

        Assert.Single(j);
        Assert.Equal(12345, j["offset"]);
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
        JObject j = JObject.Parse(serializeRequest);

        Assert.Single(j);
        Assert.Equal(12345, j["offset"]);
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
        JObject j = JObject.Parse(stringContent);

        Assert.Equal(5, j.Children().Count());
        Assert.Equal(1641638025, j["expire_date"]);
        Assert.Equal(1000000, j["chat_id"]);
        Assert.Equal("Test link name", j["name"]);
        Assert.Equal(123, j["member_limit"]);
        Assert.Equal(true, j["creates_join_request"]);
    }

    [Fact]
    public async Task Should_Ignore_Obsolete_ReplyToMessageId_Property()
    {
        SendMessageRequest request = new()
        {
            ChatId = 123,
            Text = "Test",
            ReplyToMessageId = 1004,
        };

        HttpContent httpContent = request.ToHttpContent()!;

        string stringContent = await httpContent.ReadAsStringAsync();
        JObject j = JObject.Parse(stringContent);

        Assert.Equal(1004, request.ReplyToMessageId);
        Assert.NotNull(request.ReplyParameters);
        Assert.Equal(1004, request.ReplyParameters.MessageId);
        Assert.Null(request.ReplyParameters.ChatId);
        Assert.Null(request.ReplyParameters.QuoteEntities);
        Assert.Null(request.ReplyParameters.AllowSendingWithoutReply);
        Assert.Null(request.ReplyParameters.QuotePosition);
        Assert.Null(request.ReplyParameters.QuoteParseMode);
        Assert.False(j.ContainsKey("reply_to_message_id"));

        JToken? jrp = j["reply_parameters"];
        Assert.NotNull(jrp);
        Assert.Equal(1004, jrp["message_id"]);
    }

    [Fact]
    public async Task Should_Ignore_Obsolete_DisableWebPagePreview_Property()
    {
        SendMessageRequest request = new()
        {
            ChatId = 123,
            Text = "Test",
            DisableWebPagePreview = true,
        };

        HttpContent httpContent = request.ToHttpContent()!;

        string stringContent = await httpContent.ReadAsStringAsync();
        JObject j = JObject.Parse(stringContent);

        Assert.True(request.DisableWebPagePreview);
        Assert.NotNull(request.LinkPreviewOptions);
        Assert.True(request.LinkPreviewOptions.IsDisabled);
        Assert.Null(request.LinkPreviewOptions.Url);
        Assert.Null(request.LinkPreviewOptions.PreferLargeMedia);
        Assert.Null(request.LinkPreviewOptions.PreferSmallMedia);
        Assert.Null(request.LinkPreviewOptions.ShowAboveText);
        Assert.False(j.ContainsKey("disable_web_page_preview"));

        JToken? jlpo = j["link_preview_options"];
        Assert.NotNull(jlpo);
        Assert.Equal(true, jlpo["is_disabled"]);
    }
}

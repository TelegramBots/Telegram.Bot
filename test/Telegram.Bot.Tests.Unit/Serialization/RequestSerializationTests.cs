using System.Net.Http;
using System.Threading.Tasks;
using Telegram.Bot.Requests;
using Xunit;
using JsonSerializerOptionsProvider = Telegram.Bot.Serialization.JsonSerializerOptionsProvider;

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

        string serializeRequest = JsonSerializer.Serialize(request, JsonSerializerOptionsProvider.Options);
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
        JsonObject j = Assert.IsAssignableFrom<JsonObject>(JsonNode.Parse(stringContent));

        Assert.Equal(1004, request.ReplyToMessageId);
        Assert.NotNull(request.ReplyParameters);
        Assert.Equal(1004, request.ReplyParameters.MessageId);
        Assert.Null(request.ReplyParameters.ChatId);
        Assert.Null(request.ReplyParameters.QuoteEntities);
        Assert.Null(request.ReplyParameters.AllowSendingWithoutReply);
        Assert.Null(request.ReplyParameters.QuotePosition);
        Assert.Null(request.ReplyParameters.QuoteParseMode);
        Assert.False(j.ContainsKey("reply_to_message_id"));

        JsonNode? node = j["reply_parameters"];
        Assert.NotNull(node);
        JsonObject jrp = Assert.IsAssignableFrom<JsonObject>(node);
        Assert.NotNull(jrp);
        Assert.Equal(1004, (long?)jrp["message_id"]);
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
        JsonObject j = Assert.IsAssignableFrom<JsonObject>(JsonNode.Parse(stringContent));

        Assert.True(request.DisableWebPagePreview);
        Assert.NotNull(request.LinkPreviewOptions);
        Assert.True(request.LinkPreviewOptions.IsDisabled);
        Assert.Null(request.LinkPreviewOptions.Url);
        Assert.Null(request.LinkPreviewOptions.PreferLargeMedia);
        Assert.Null(request.LinkPreviewOptions.PreferSmallMedia);
        Assert.Null(request.LinkPreviewOptions.ShowAboveText);
        Assert.False(j.ContainsKey("disable_web_page_preview"));

        JsonNode? node = j["link_preview_options"];
        Assert.NotNull(node);
        JsonObject jlpo = Assert.IsAssignableFrom<JsonObject>(node);
        Assert.NotNull(jlpo);
        Assert.Equal(true, (bool?)jlpo["is_disabled"]);
    }
}

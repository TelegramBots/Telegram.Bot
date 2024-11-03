using System.Net.Http;
using System.Threading.Tasks;
using Telegram.Bot.Requests;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class MethodNameTests
{
    [Fact(DisplayName = "Should serialize method name in webhook responses")]
    public void Should_Serialize_MethodName_In_Webhook_Responses()
    {
        SendMessageRequest sendMessageRequest = new()
        {
            ChatId = 1,
            Text = "text",
            IsWebhookResponse = true
        };

        string request = JsonSerializer.Serialize(sendMessageRequest, JsonBotAPI.Options);
        JsonNode? root = JsonNode.Parse(request);
        Assert.NotNull(root);
        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);

        Assert.Equal(3, j.Count);
        Assert.Equal(1, (long?)j["chat_id"]);
        Assert.Equal("text", (string?)j["text"]);
        Assert.Equal("sendMessage", (string?)j["method"]);
    }

    [Fact(DisplayName = "Should not serialize method name when not a webhook responses")]
    public void Should_Not_Serialize_MethodName_When_Not_In_Webhook_Responses()
    {
        SendMessageRequest sendMessageRequest = new()
        {
            ChatId = 1,
            Text = "text",
            IsWebhookResponse = false
        };

        string request = JsonSerializer.Serialize(sendMessageRequest, JsonBotAPI.Options);
        JsonNode? root = JsonNode.Parse(request);
        Assert.NotNull(root);
        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);

        Assert.Equal(2, j.Count);
        Assert.Equal(1, (long?)j["chat_id"]);
        Assert.Equal("text", (string?)j["text"]);
        Assert.False(j.ContainsKey("method"));
    }

    [Fact(DisplayName = "Should serialize only the method name in parameterless webhook responses")]
    public void Should_Serialize_MethodName_In_Parameterless_Webhook_Responses()
    {
        DeleteWebhookRequest deleteWebhookRequest = new() { IsWebhookResponse = true };

        string request = JsonSerializer.Serialize(deleteWebhookRequest, JsonBotAPI.Options);
        JsonNode? root = JsonNode.Parse(request);
        Assert.NotNull(root);
        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);

        Assert.Single(j);
        Assert.Equal("deleteWebhook", (string?)j["method"]);
    }

    [Fact(DisplayName = "Should serialize an empty object when not a parameterless webhook response")]
    public void Should_Serialize_Empty_Object_When_Not_Parameterless_Webhook_Response()
    {
        DeleteWebhookRequest deleteWebhookRequest = new() { IsWebhookResponse = false };

        string request = JsonSerializer.Serialize(deleteWebhookRequest, JsonBotAPI.Options);
        JsonNode? root = JsonNode.Parse(request);
        Assert.NotNull(root);
        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);

        Assert.Empty(j);
    }

    [Fact(DisplayName = "Should build a HttpContent in parameterless webhook responses")]
    public void Should_Build_HttpContent_In_Parameterless_Webhook_Response()
    {
        DeleteWebhookRequest deleteWebhookRequest = new() { IsWebhookResponse = true };

        HttpContent? content = deleteWebhookRequest.ToHttpContent();
        Assert.NotNull(content);
    }

    [Fact(DisplayName = "Should build a json with method name in parameterless webhook responses")]
    public async Task Should_Build_Json_With_MethodName_In_Parameterless_Webhook_ResponseAsync()
    {
        CloseRequest closeRequest = new() { IsWebhookResponse = true };

        HttpContent? content = closeRequest.ToHttpContent();

        //StringContent stringContent = Assert.IsAssignableFrom<StringContent>(content);
        Assert.NotNull(content);

        string body = await content.ReadAsStringAsync();
        JsonNode? root = JsonNode.Parse(body);
        Assert.NotNull(root);
        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);

        Assert.Single(j);
        Assert.Equal("close", (string?)j["method"]);
    }

    [Fact(DisplayName = "Should not build an HttpContent when not a parameterless webhook responses")]
    public void Should_Not_Serialize_MethodName_When_Not_Parameterless_Webhook_Responses()
    {
        CloseRequest closeRequest = new() { IsWebhookResponse = false };

        HttpContent? content = closeRequest.ToHttpContent();
        Assert.Null(content);
    }
}

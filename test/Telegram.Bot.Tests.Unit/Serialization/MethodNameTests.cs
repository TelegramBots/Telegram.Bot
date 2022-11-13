using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telegram.Bot.Requests;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class MethodNameTests
{
    [Fact(DisplayName = "Should serialize method name in webhook responses")]
    public void Should_Serialize_MethodName_In_Webhook_Responses()
    {
        SendMessageRequest sendMessageRequest = new(1, "text") { IsWebhookResponse = true };

        string request = JsonConvert.SerializeObject(sendMessageRequest);
        Assert.Contains(@"""method"":""sendMessage""", request);
    }

    [Fact(DisplayName = "Should not serialize method name when not a webhook responses")]
    public void Should_Not_Serialize_MethodName_When_Not_In_Webhook_Responses()
    {
        SendMessageRequest sendMessageRequest = new(1, "text") { IsWebhookResponse = false };

        string request = JsonConvert.SerializeObject(sendMessageRequest);
        Assert.DoesNotContain(@"""method"":""sendMessage""", request);
    }

    [Fact(DisplayName = "Should serialize only the method name in parameterless webhook responses")]
    public void Should_Serialize_MethodName_In_Parameterless_Webhook_Responses()
    {
        DeleteWebhookRequest deleteWebhookRequest = new() { IsWebhookResponse = true };

        string request = JsonConvert.SerializeObject(deleteWebhookRequest);
        Assert.Equal(@"{""method"":""deleteWebhook""}", request);
    }

    [Fact(DisplayName = "Should serialize an empty object when not a parameterless webhook response")]
    public void Should_Serialize_Empty_Object_When_Not_Parameterless_Webhook_Response()
    {
        DeleteWebhookRequest deleteWebhookRequest = new() { IsWebhookResponse = false };

        string request = JsonConvert.SerializeObject(deleteWebhookRequest);
        Assert.Equal("{}", request);
    }

    [Fact(DisplayName = "Should build a HttpContent in parameterless webhook responses")]
    public void Should_Build_HttpContent_In_Parameterless_Webhook_Response()
    {
        DeleteWebhookRequest deleteWebhookRequest = new() { IsWebhookResponse = true };

        HttpContent? content = deleteWebhookRequest.ToHttpContent();
        Assert.NotNull(content);
    }

    [Fact(DisplayName = "Should build a StringContent with method name in parameterless webhook responses")]
    public async Task Should_Build_StringContent_With_MethodName_In_Parameterless_Webhook_ResponseAsync()
    {
        CloseRequest closeRequest = new() { IsWebhookResponse = true };

        HttpContent? content = closeRequest.ToHttpContent();

        StringContent stringContent = Assert.IsType<StringContent>(content);
        Assert.NotNull(content);

        string body = await stringContent.ReadAsStringAsync();
        Assert.Equal(@"{""method"":""close""}", body);
    }

    [Fact(DisplayName = "Should not build an HttpContent when not a parameterless webhook responses")]
    public void Should_Not_Serialize_MethodName_When_Not_Parameterless_Webhook_Responses()
    {
        CloseRequest closeRequest = new() { IsWebhookResponse = false };

        HttpContent? content = closeRequest.ToHttpContent();
        Assert.Null(content);
    }
}

using Newtonsoft.Json;
using Telegram.Bot.Requests;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Serialization
{
    public class MethodNameTests
    {
        [Fact(DisplayName = "Should serialize method name in webhook responses")]
        public void Should_Serialize_MethodName_In_Webhook_Responses()
        {
            SendMessageRequest sendMessageRequest = new SendMessageRequest(1, "text")
            {
                IsWebhookResponse = true
            };

            string request = JsonConvert.SerializeObject(sendMessageRequest);
            Assert.Contains(@"""method"":""sendMessage""", request);
        }

        [Fact(DisplayName = "Should not serialize method name when not a webhook responses")]
        public void Should_Not_Serialize_MethodName_When_Not_In_Webhook_Responses()
        {
            SendMessageRequest sendMessageRequest = new SendMessageRequest(1, "text")
            {
                IsWebhookResponse = false
            };

            string request = JsonConvert.SerializeObject(sendMessageRequest);
            Assert.DoesNotContain(@"""method"":""sendMessage""", request);
        }
    }
}

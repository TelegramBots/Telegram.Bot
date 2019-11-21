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
            var sendMessageRequest = new SendMessageRequest(1, "text")
            {
                IsWebhookResponse = true
            };

            var request = JsonConvert.SerializeObject(sendMessageRequest);
            Assert.Contains(@"""method"":""sendMessage""", request);
        }

        [Fact(DisplayName = "Should not serialize method name in webhook responses")]
        public void Should_Not_Serialize_MethodName_In_Webhook_Responses()
        {
            var sendMessageRequest = new SendMessageRequest(1, "text")
            {
                IsWebhookResponse = false
            };

            var request = JsonConvert.SerializeObject(sendMessageRequest);
            Assert.DoesNotContain(@"""method"":""sendMessage""", request);
        }
    }
}

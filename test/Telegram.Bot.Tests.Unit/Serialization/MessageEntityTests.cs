using Newtonsoft.Json;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Serialization
{
    public class MessageEntityTests
    {
        [Fact(DisplayName = "Should deserialize message entity with phone number type")]
        public void Should_Deserialize_Message_Entity_With_Phone_Number_Type()
        {
            const string json = @"{
                ""offset"": 10,
                ""length"": 10,
                ""type"": ""phone_number""
            }";

            var message = JsonConvert.DeserializeObject<MessageEntity>(json);

            Assert.Equal(MessageEntityType.PhoneNumber, message.Type);
        }

        [Fact(DisplayName = "Should serialize message entity with phone number type")]
        public void Should_Serialize_Message_Entity_With_Phone_Number_Type()
        {
            var messageEntity = new MessageEntity
            {
                Length = 10,
                Offset = 10,
                Type = MessageEntityType.PhoneNumber
            };

            var json = JsonConvert.SerializeObject(messageEntity);

            Assert.NotNull(json);
            Assert.True(json.Length > 10);
            Assert.Contains(@"""type"":""phone_number""", json);
        }

        [Fact(DisplayName = "Should deserialize message entity with unknown type")]
        public void Should_Deserialize_Message_Entity_With_Unknown_Type()
        {
            const string json = @"{
                ""offset"": 10,
                ""length"": 10,
                ""type"": ""totally_unknown_type""
            }";

            var message = JsonConvert.DeserializeObject<MessageEntity>(json);

            Assert.Equal(MessageEntityType.Unknown, message.Type);
        }

        [Fact(DisplayName = "Should serialize message entity with unknown type")]
        public void Should_Serialize_Message_Entity_With_Unknown_Type()
        {
            var messageEntity = new MessageEntity
            {
                Length = 10,
                Offset = 10,
                Type = MessageEntityType.Unknown
            };

            var json = JsonConvert.SerializeObject(messageEntity);

            Assert.NotNull(json);
            Assert.True(json.Length > 10);
            Assert.Contains(@"""type"":""unknown""", json);
        }
    }
}

using Newtonsoft.Json;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Serialization
{
    public class ChatMemberSerializationTests
    {
        [Fact]
        public void Should_Deserialize_Chat_Member_Member()
        {
            var creator = new
            {
                status = ChatMemberStatus.Creator,
                user = new
                {
                    id = 12345,
                    is_bot = true,
                    first_name = "First Name",
                    last_name = "Last Name",
                    username = "test_bot",
                    language_code = "en_US",
                },
                is_anonymous = true,
                custom_title = "custom test title"
            };

            var chatMemberJson = JsonConvert.SerializeObject(creator, Formatting.Indented);
            var chatMember = JsonConvert.DeserializeObject<ChatMember>(chatMemberJson);

            Assert.IsType<ChatMemberOwner>(chatMember);
            Assert.Equal(ChatMemberStatus.Creator, chatMember.Status);
            Assert.True(((ChatMemberOwner)chatMember).IsAnonymous);
            Assert.Equal("custom test title", ((ChatMemberOwner)chatMember).CustomTitle);
            Assert.NotNull(chatMember.User);
            Assert.Equal(12345, chatMember.User.Id);
            Assert.True(chatMember.User.IsBot);
            Assert.Equal("First Name", chatMember.User.FirstName);
            Assert.Equal("Last Name", chatMember.User.LastName);
            Assert.Equal("test_bot", chatMember.User.Username);
            Assert.Equal("en_US", chatMember.User.LanguageCode);
        }

        [Fact]
        public void Should_Serialize_Chat_Member_Member()
        {
            var creator = new ChatMemberOwner
            {
                User = new User
                {
                    Id = 12345,
                    IsBot = true,
                    FirstName = "First Name",
                    LastName = "Last Name",
                    Username = "test_bot",
                    LanguageCode = "en_US",
                },
                IsAnonymous = true,
                CustomTitle = "Custom test title"
            };

            var chatMemberJson = JsonConvert.SerializeObject(creator);
            Assert.Contains(@"""status"":""creator""", chatMemberJson);
            Assert.Contains(@"""is_anonymous"":true", chatMemberJson);
            Assert.Contains(@"""custom_title"":""Custom test title""", chatMemberJson);
            Assert.Contains(@"""user"":{", chatMemberJson);
            Assert.Contains(@"""id"":12345", chatMemberJson);
            Assert.Contains(@"""is_bot"":true", chatMemberJson);
            Assert.Contains(@"""first_name"":""First Name""", chatMemberJson);
            Assert.Contains(@"""last_name"":""Last Name""", chatMemberJson);
            Assert.Contains(@"""username"":""test_bot""", chatMemberJson);
            Assert.Contains(@"""language_code"":""en_US""", chatMemberJson);
        }
    }
}

using System.Collections.Generic;
using Newtonsoft.Json;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Unit
{
    public class UserTests
    {
        [Fact]
        public void Should_ToString()
        {
            Assert.Equal("@alicebot (12345)", new User
            {
                Id = 12345,
                IsBot = true,
                FirstName = "Alice Bot",
                Username = "alicebot"
            }.ToString());

            Assert.Equal("BoBBy (67890)", new User
            {
                Id = 67890,
                IsBot = false,
                FirstName = "BoBBy"
            }.ToString());

            Assert.Equal("Chris Dale (54321)", new User
            {
                Id = 54321,
                IsBot = false,
                FirstName = "Chris",
                LastName = "Dale"
            }.ToString());
        }

        [Fact]
        public void Should_Add_To_Dict()
        {
            int id1 = 12345;
            int id2 = 67890;
            string fname1 = "alicebot";
            string fname2 = "bob";

            string json1 = $@"{{
                ""id"": {id1},
                ""is_bot"": true,
                ""first_name"": ""{fname1}""
            }}";
            string json2 = $@"{{
                ""id"": {id2},
                ""is_bot"": false,
                ""first_name"": ""{fname2}""
            }}";

            User user1 = JsonConvert.DeserializeObject<User>(json1);
            User user2 = JsonConvert.DeserializeObject<User>(json2);

            Dictionary<User, string> dict = new Dictionary<User, string>
            {
                {user1, nameof(user1)},
                {user2, nameof(user2)}
            };

            string user1Value = dict[user1];

            Assert.Equal(nameof(user1), user1Value);
        }
    }
}
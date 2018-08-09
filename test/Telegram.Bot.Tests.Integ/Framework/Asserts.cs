using Newtonsoft.Json.Linq;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Framework
{
    public static class Asserts
    {
        public static void JsonEquals(object obj1, object obj2) =>
            Assert.True(JToken.DeepEquals(
                JToken.FromObject(obj1),
                JToken.FromObject(obj2)
            ));
    }
}

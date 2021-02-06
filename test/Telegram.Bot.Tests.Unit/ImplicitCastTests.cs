using Telegram.Bot.Types;
using Xunit;

#nullable enable
namespace Telegram.Bot.Tests.Unit
{
    public class ImplicitCastTests
    {
        [Fact]
        public void Shoud_Cast()
        {
            Chat? chat = null;
            Assert.Null(ChatIdMethod(chat));

            ChatId? ChatIdMethod(ChatId? chat)
            {
                return chat;
            }
        }
    }
}

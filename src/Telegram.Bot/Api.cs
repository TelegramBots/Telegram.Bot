using System;

namespace Telegram.Bot
{
    [Obsolete("Telegram.Bot.Api is Deprecated, please use Telegram.Bot.Client")]
    public class Api : Client
    {
        [Obsolete("Telegram.Bot.Api is Deprecated, please use Telegram.Bot.Client")]
        public Api(string token) : base(token) { }
    }
}

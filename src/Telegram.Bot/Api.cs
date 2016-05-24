using System;

namespace Telegram.Bot
{
    [Obsolete("Telegram.Bot.Api is deprecated, please use Telegram.Bot.TelegramBotClient")]
    public class Api : TelegramBotClient
    {
        [Obsolete("Telegram.Bot.Api is deprecated, please use Telegram.Bot.TelegramBotClient")]
        public Api(string token) : base(token) { }
    }
}

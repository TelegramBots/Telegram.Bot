using System;

namespace Telegram.Bot
{
#pragma warning disable CS1591

    [Obsolete("Telegram.Bot.Api is deprecated, please use Telegram.Bot.TelegramBotClient")]
    public class Api : TelegramBotClient
    {
        [Obsolete("Telegram.Bot.Api is deprecated, please use Telegram.Bot.TelegramBotClient")]
        public Api(string token) : base(token) { }
    }

#pragma warning restore CS1591
}

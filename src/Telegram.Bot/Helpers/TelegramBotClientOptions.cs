using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Telegram.Bot.Helpers
{
    /// <summary>
    /// Used for configuring the bot 
    /// </summary>
    public class TelegramBotClientOptions
    {
        /// <summary>
        /// The telegram bot token. Ask @botfather about it. 
        /// </summary>
        public string Token { get; set; } = "";
        /// <summary>
        /// An HttpBotClient instance that will be used for the bot. If not provided, a new one
        /// will be created. 
        /// </summary>
        public HttpClient HttpClient { get; set; } = null;
    }
}

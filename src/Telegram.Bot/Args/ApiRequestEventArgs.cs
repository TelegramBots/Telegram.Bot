using System;
using System.Net.Http;

namespace Telegram.Bot.Args
{
    public class ApiRequestEventArgs : EventArgs
    {
        public string Uri { get; internal set; }

        public HttpContent HttpContent { get; internal set; }
    }
}
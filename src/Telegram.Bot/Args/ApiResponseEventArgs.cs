using System.Net.Http;

namespace Telegram.Bot.Args
{
    public class ApiResponseEventArgs
    {
        public string Payload { get; internal set; }

        public HttpResponseMessage ResponseMessage { get; internal set; }

        public ApiRequestEventArgs ApiRequestEventArgs { get; internal set; }
    }
}
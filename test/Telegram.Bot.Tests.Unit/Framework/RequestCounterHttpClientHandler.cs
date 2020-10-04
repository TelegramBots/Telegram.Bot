using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Telegram.Bot.Tests.Unit.Framework
{
    internal class RequestCounterHttpClientHandler : HttpClientHandler
    {
        public int RequestCounter { get; private set; }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            RequestCounter++;

            var httpResponseMessage = new HttpResponseMessage()
            {
                Content = new StringContent("{ \"ok\": true, \"result\": []}"),
                StatusCode = HttpStatusCode.OK
            };

            return Task.FromResult(httpResponseMessage);
        }
    }
}

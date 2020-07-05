using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Telegram.Bot.Tests.Unit
{
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpStatusCode _httpStatusCode;
        private readonly HttpContent _responseContent;

        public MockHttpMessageHandler(
            HttpStatusCode httpStatusCode,
            HttpContent responseContent = default)
        {
            _httpStatusCode = httpStatusCode;
            _responseContent = responseContent;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var httpResponse = new HttpResponseMessage(_httpStatusCode)
            {
                Content = _responseContent
            };

            return Task.FromResult(httpResponse);
        }
    }
}

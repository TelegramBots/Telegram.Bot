using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Benchmarks
{
    class MockHttpMessageHandler : HttpMessageHandler
    {
        public const string ResponseGetMe = @"{""ok"":true,""result"":{""id"":123000321,""is_bot"":true,""first_name"":""testbot"",""username"":""testbot""}}";
        public const string ResponseGetUpdates = @"{""ok"":true,""result"":[{""update_id"":123456789,""message"":{""message_id"":1234,""from"":{""id"":123456789,""is_bot"":false,""first_name"":""Foo"",""username"":""foobar"",""language_code"":""en""},""chat"":{""id"":123456789,""first_name"":""foo"",""username"":""foobar"",""type"":""private""},""date"":1569600000,""text"":""test""}}]}";

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string responseText = request.RequestUri.Segments.Last() switch
            {
                "getMe" => ResponseGetMe,
                "getUpdates" => ResponseGetUpdates,
                _ => throw new ArgumentException($"Unexpected request method name.")
            };
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseText, Encoding.UTF8, "application/json"),
            };
            return Task.FromResult(response);
        }
    }
}

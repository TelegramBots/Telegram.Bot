using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Xunit;

namespace Telegram.Bot.Tests.Unit;

public class HttpContentTests
{
    static readonly byte[] _gzABC = [31, 139, 8, 0, 0, 0, 0, 0, 0, 10, 115, 116, 114, 6, 0, 72, 3, 131, 163, 3, 0, 0, 0]; // gzipped "ABC"

    [Fact]
    public async Task RetryWithSeekableStream()
    {
        var httpClient = new HttpClient(new TestHandler());
        var bot = new TelegramBotClient(new TelegramBotClientOptions("666:DEVIL") { RetryThreshold = 60, RetryCount = 3 }, httpClient);
        using var stream = new MemoryStream([65, 66, 67]); // "ABC"
        var ex = await Assert.ThrowsAsync<ApiRequestException>(async () => await bot.SendDocument(123, stream));
        Assert.Equal("Too Many Requests: 3", ex.Message);
    }

    [Fact]
    public async Task RetryWithNonSeekableStream()
    {
        var httpClient = new HttpClient(new TestHandler());
        var bot = new TelegramBotClient(new TelegramBotClientOptions("666:DEVIL") { RetryThreshold = 60, RetryCount = 3 }, httpClient);
        using var stream = new GZipStream(new MemoryStream(_gzABC), CompressionMode.Decompress);
        var ex = await Assert.ThrowsAsync<ApiRequestException>(async () => await bot.SendDocument(123, stream));
        Assert.Equal("Too Many Requests: 3", ex.Message);
    }

    public class TestHandler : DelegatingHandler
    {
        int _counter = 0;
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var ms = new MemoryStream();
            await request.Content!.CopyToAsync(ms);
            ms.Position = 0;
            var s = await new StreamReader(ms).ReadToEndAsync();
            Assert.Contains("ABC", s);
            return new HttpResponseMessage(HttpStatusCode.TooManyRequests) { Content = new StringContent($$"""
                { "error_code": 429, "description": "Too Many Requests: {{++_counter}}", "parameters": {"retry_after": 1} }
                """) };
        }
    }
}

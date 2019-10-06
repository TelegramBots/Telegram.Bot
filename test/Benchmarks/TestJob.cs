using BenchmarkDotNet.Attributes;
using System.Net.Http;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Benchmarks
{
    [MemoryDiagnoser]
    public class TestJob
    {
        private readonly ITelegramBotClient _botClient;

        private readonly HttpClient _httpClient = new HttpClient(new MockHttpMessageHandler(), true);

        public TestJob()
        {
            _botClient = new TelegramBotClient("123:foo", _httpClient);
        }

        [Benchmark]
        public async Task<User> GetBotUserAsync()
        {
            var botUser = await _botClient.GetMeAsync();
            //var updates = await _botClient.GetUpdatesAsync();

            return botUser;
        }
    }
}

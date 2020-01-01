using System.Text.Json;

namespace Telegram.Bot.Json.Helpers
{
    internal sealed class JsonSnakeCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            return name;
        }
    }
}

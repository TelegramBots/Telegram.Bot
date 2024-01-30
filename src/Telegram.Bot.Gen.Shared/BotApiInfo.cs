using System.Text.Json.Serialization;

namespace Telegram.Bot.Gen.Shared;

public class BotApiInfo
{
    [JsonPropertyName("apiVersion")]
    public string ApiVersion { get; set; }
}

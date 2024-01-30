using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Telegram.Bot.Gen.Shared;

public class BotApiData
{
    [JsonPropertyName("info")]
    public BotApiInfo Info { get; set; }
    [JsonPropertyName("methods")]
    public IReadOnlyCollection<BotApiMethod> Methods { get; set; }
    [JsonPropertyName("types")]
    public IReadOnlyCollection<BotApiType> Types { get; set; }
}

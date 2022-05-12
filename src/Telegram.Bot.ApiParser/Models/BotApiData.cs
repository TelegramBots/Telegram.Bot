using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Telegram.Bot.ApiParser.Models;

public sealed record BotApiData(
    [property: JsonPropertyName("info")]
    BotApiInfo Info,
    [property: JsonPropertyName("methods")]
    List<BotApiMethod> Methods,
    [property: JsonPropertyName("types")]
    List<BotApiType> Types);

using System.Text.Json.Serialization;

namespace Telegram.Bot.ApiParser.Models;

public sealed record BotApiInfo(
    [property: JsonPropertyName("apiVersion")]
    string ApiVersion);

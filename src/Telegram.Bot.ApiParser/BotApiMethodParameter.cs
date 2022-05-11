using System.Text.Json.Serialization;

namespace Telegram.Bot.ApiParser;

public sealed record BotApiMethodParameter(
    string ParameterName,
    string ParameterDescription,
    string ParameterTypeName,
    bool IsEnum,
    bool IsRequired,
    [property: JsonIgnore]
    BotApiMethod Parent);

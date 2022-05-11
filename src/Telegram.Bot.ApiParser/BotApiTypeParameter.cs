using System.Text.Json.Serialization;

namespace Telegram.Bot.ApiParser;

internal sealed record BotApiTypeParameter(
    string ParameterName,
    string ParameterDescription,
    string ParameterTypeName,
    bool IsEnum,
    [property: JsonIgnore]
    BotApiType Parent);

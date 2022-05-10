using System.Text.Json.Serialization;

namespace Telegram.Bot.ApiTypesGenerator;

public sealed record BotApiMethodParameter(
    string ParameterName,
    string ParameterDescription,
    string ParameterTypeName,
    bool IsEnum,
    bool IsRequired,
    [property: JsonIgnore]
    BotApiMethod Parent);

using System.Text.Json.Serialization;

namespace Telegram.Bot.ApiParser.Models;

public sealed record BotApiParameter(
    [property: JsonPropertyName("parameterName")]
    string ParameterName,
    [property: JsonPropertyName("parameterTypeName")]
    string ParameterTypeName,
    [property: JsonPropertyName("parameterDescription")]
    BotApiDescription ParameterDescription,
    [property: JsonPropertyName("isRequired")]
    bool IsRequired);

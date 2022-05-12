using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Telegram.Bot.ApiParser.Models;

public sealed record BotApiMethod(
    [property: JsonPropertyName("methodName")]
    string MethodName,
    [property: JsonPropertyName("methodDescription")]
    BotApiDescription MethodDescription,
    [property: JsonPropertyName("methodGroup")]
    string MethodGroup,
    [property: JsonPropertyName("parameters")]
    List<BotApiParameter> Parameters);

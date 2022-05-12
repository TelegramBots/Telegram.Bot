using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Telegram.Bot.ApiParser.Models;

public sealed record BotApiType(
    [property: JsonPropertyName("typeName")]
    string TypeName,
    [property: JsonPropertyName("typeDescription")]
    BotApiDescription TypeDescription,
    [property: JsonPropertyName("typeGroup")]
    string TypeGroup,
    [property: JsonPropertyName("parameters")]
    List<BotApiParameter> Parameters);

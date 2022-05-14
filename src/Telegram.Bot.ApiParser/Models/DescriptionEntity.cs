using System;
using System.Text.Json.Serialization;
using Telegram.Bot.ApiParser.Models.Enums;

namespace Telegram.Bot.ApiParser.Models;

public sealed record DescriptionEntity(
    [property: JsonPropertyName("entityText")]
    string EntityText,
    [property: JsonPropertyName("entityValue")]
    string EntityValue,
    [property: JsonPropertyName("entityKind")]
    DescriptionEntityKind? EntityKind,
    [property: JsonIgnore]
    Func<TelegramBotDocParser, DescriptionEntityKind>? EntityKindFactory);

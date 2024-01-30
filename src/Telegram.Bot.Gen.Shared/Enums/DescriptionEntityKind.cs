using System.Text.Json.Serialization;

namespace Telegram.Bot.Gen.Shared.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DescriptionEntityKind
{
    Method,
    Type,
    Url
}

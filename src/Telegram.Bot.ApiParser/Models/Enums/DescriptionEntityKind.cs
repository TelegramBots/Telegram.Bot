using System.Text.Json.Serialization;

namespace Telegram.Bot.ApiParser.Models.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DescriptionEntityKind
{
    Method,
    Type,
    Url
}

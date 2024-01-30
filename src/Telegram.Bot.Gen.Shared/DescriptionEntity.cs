using System.Text.Json.Serialization;
using Telegram.Bot.Gen.Shared.Enums;

namespace Telegram.Bot.Gen.Shared;

public class DescriptionEntity
{
    [JsonPropertyName("entityText")]
    public string EntityText { get; set; }
    [JsonPropertyName("entityValue")]
    public string EntityValue { get; set; }
    [JsonPropertyName("entityKind")]
    public DescriptionEntityKind EntityKind { get; set; }
}

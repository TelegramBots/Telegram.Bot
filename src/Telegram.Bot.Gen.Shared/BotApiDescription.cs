using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Telegram.Bot.Gen.Shared;

public class BotApiDescription
{
    [JsonPropertyName("descriptionText")]
    public string DescriptionText { get; set; }
    [JsonPropertyName("entities")]
    public List<DescriptionEntity> Entities { get; set; }
}

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Telegram.Bot.Gen.Shared;

public class BotApiType
{
    [JsonPropertyName("typeName")]
    public string TypeName { get; set; }
    [JsonPropertyName("typeDescription")]
    public BotApiDescription TypeDescription { get; set; }
    [JsonPropertyName("typeGroup")]
    public string TypeGroup { get; set; }
    [JsonPropertyName("parameters")]
    public List<BotApiParameter> Parameters { get; set; }
    [JsonPropertyName("isCompositeType"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool IsCompositeType { get; set; }
}

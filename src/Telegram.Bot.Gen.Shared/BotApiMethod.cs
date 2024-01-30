using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Telegram.Bot.Gen.Shared;

public class BotApiMethod
{
    [JsonPropertyName("methodName")]
    public string MethodName { get; set; }
    [JsonPropertyName("methodDescription")]
    public BotApiDescription MethodDescription { get; set; }
    [JsonPropertyName("methodGroup")]
    public string MethodGroup { get; set; }
    [JsonPropertyName("parameters")]
    public List<BotApiParameter> Parameters { get; set; }
    [JsonPropertyName("methodReturnType")]
    public string MethodReturnType { get; set; }
}

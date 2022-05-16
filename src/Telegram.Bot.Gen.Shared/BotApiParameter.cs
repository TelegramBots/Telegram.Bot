using System.Text.Json.Serialization;

namespace Telegram.Bot.Gen.Shared;

public class BotApiParameter
{
    [JsonPropertyName("parameterName")]
    public string ParameterName { get; set; }
    [JsonPropertyName("parameterTypeName")]
    public string ParameterTypeName { get; set; }
    [JsonPropertyName("parameterDescription")]
    public BotApiDescription ParameterDescription { get; set; }
    [JsonPropertyName("isRequired")]
    public bool IsRequired { get; set; }
}

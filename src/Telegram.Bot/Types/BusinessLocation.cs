namespace Telegram.Bot.Types;

/// <summary>
///
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class BusinessLocation
{
    /// <summary>
    /// Address of the business
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Address { get; set; } = default!;

    /// <summary>
    /// Optional. Location of the business
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public Location? Location { get; set; }
}

namespace Telegram.Bot.Types;

/// <summary>
///
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class Birthday
{
    /// <summary>
    /// Day of the user's birth; 1-31
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int Day { get; set; }

    /// <summary>
    /// Month of the user's birth; 1-12
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int Month { get; set; }

    /// <summary>
    /// Optional. Year of the user's birth
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? Year { get; set; }
}

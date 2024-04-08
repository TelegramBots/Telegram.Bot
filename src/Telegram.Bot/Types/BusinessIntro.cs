namespace Telegram.Bot.Types;

/// <summary>
///
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class BusinessIntro
{
    /// <summary>
    /// Optional. Title text of the business intro
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Title { get; set; }

    /// <summary>
    /// Optional. Message text of the business intro
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Message { get; set; }

    /// <summary>
    /// Optional. Sticker of the business intro
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public Sticker? Sticker { get; set; }
}

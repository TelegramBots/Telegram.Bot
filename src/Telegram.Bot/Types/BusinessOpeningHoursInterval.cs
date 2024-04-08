namespace Telegram.Bot.Types;

/// <summary>
///
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class BusinessOpeningHoursInterval
{
    /// <summary>
    /// The minute's sequence number in a week, starting on Monday, marking the start of the time interval during which the business is open; 0 - 7 24 60
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int OpeningMinute { get; set; }

    /// <summary>
    /// The minute's sequence number in a week, starting on Monday, marking the end of the time interval during which the business is open; 0 - 8 24 60
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int ClosingMinute { get; set; }
}

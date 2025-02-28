// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes an interval of time during which a business is open.</summary>
public partial class BusinessOpeningHoursInterval
{
    /// <summary>The minute's sequence number in a week, starting on Monday, marking the start of the time interval during which the business is open; 0 - 7 * 24 * 60</summary>
    [JsonPropertyName("opening_minute")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int OpeningMinute { get; set; }

    /// <summary>The minute's sequence number in a week, starting on Monday, marking the end of the time interval during which the business is open; 0 - 8 * 24 * 60</summary>
    [JsonPropertyName("closing_minute")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int ClosingMinute { get; set; }
}

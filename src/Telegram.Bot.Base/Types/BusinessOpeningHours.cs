namespace Telegram.Bot.Types;

/// <summary>
///
/// </summary>
public class BusinessOpeningHours
{
    /// <summary>
    /// Unique name of the time zone for which the opening hours are defined
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string TimeZoneName { get; set; } = default!;

    /// <summary>
    /// Array of List of time intervals describing business opening hours
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public BusinessOpeningHoursInterval[] OpeningHours { get; set; } = default!;
}

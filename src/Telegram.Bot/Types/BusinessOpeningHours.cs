namespace Telegram.Bot.Types;

/// <summary>
///
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class BusinessOpeningHours
{
    /// <summary>
    /// Unique name of the time zone for which the opening hours are defined
    /// </summary>
    public string TimeZoneName { get; set; } = default!;

    /// <summary>
    /// Array of List of time intervals describing business opening hours
    /// </summary>
    public BusinessOpeningHoursInterval[] OpeningHours { get; set; } = default!;
}

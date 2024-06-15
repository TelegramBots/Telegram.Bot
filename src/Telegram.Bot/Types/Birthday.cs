namespace Telegram.Bot.Types;

/// <summary>
///
/// </summary>
public class Birthday
{
    /// <summary>
    /// Day of the user's birth; 1-31
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Day { get; set; }

    /// <summary>
    /// Month of the user's birth; 1-12
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Month { get; set; }

    /// <summary>
    /// Optional. Year of the user's birth
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Year { get; set; }
}

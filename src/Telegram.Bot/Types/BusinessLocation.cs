namespace Telegram.Bot.Types;

/// <summary>
///
/// </summary>
public class BusinessLocation
{
    /// <summary>
    /// Address of the business
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Address { get; set; } = default!;

    /// <summary>
    /// Optional. Location of the business
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Location? Location { get; set; }
}

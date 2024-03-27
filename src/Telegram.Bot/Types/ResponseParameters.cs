namespace Telegram.Bot.Types;

/// <summary>
/// Contains information about why a request was unsuccessful.
/// </summary>
public class ResponseParameters
{
    /// <summary>
    /// The group has been migrated to a supergroup with the specified identifier.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? MigrateToChatId { get; set; }

    /// <summary>
    /// In case of exceeding flood control, the number of seconds left to wait before the request can be repeated.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? RetryAfter { get; set; }
}

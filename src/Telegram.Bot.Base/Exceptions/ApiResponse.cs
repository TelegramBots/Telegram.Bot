namespace Telegram.Bot.Exceptions;

/// <summary>
/// Represents failed API response
/// </summary>
public class ApiResponse
{
    /// <summary>
    /// Gets the error message.
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Description { get; init; }

    /// <summary>
    /// Gets the error code.
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int ErrorCode { get; init; }

    /// <summary>
    /// Contains information about why a request was unsuccessful.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ResponseParameters? Parameters { get; init; }
}

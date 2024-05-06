namespace Telegram.Bot.Types;

/// <summary>
/// Represents bot API response
/// </summary>
/// <typeparam name="TResult">Expected type of operation result</typeparam>
public class ApiResponse<TResult>
{
    /// <summary>
    /// Gets a value indicating whether the request was successful.
    /// </summary>
    [JsonRequired]
    public required bool Ok { get; init; }

    /// <summary>
    /// Gets the error message.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; init; }

    /// <summary>
    /// Gets the error code.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? ErrorCode { get; init; }

    /// <summary>
    /// Contains information about why a request was unsuccessful.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ResponseParameters? Parameters { get; init; }

    /// <summary>
    /// Gets the result object.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public TResult? Result { get; init; }
}

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
    public string Description { get; private set; }

    /// <summary>
    /// Gets the error code.
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int ErrorCode { get; private set; }

    /// <summary>
    /// Contains information about why a request was unsuccessful.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ResponseParameters? Parameters { get; private set; }

    /// <summary>
    /// Initializes an instance of <see cref="ApiResponse"/>
    /// </summary>
    /// <param name="errorCode">Error code</param>
    /// <param name="description">Error message</param>
    /// <param name="parameters">Information about why a request was unsuccessful</param>
    public ApiResponse(
        int errorCode,
        string description,
        ResponseParameters? parameters)
    {
        ErrorCode = errorCode;
        Description = description;
        Parameters = parameters;
    }
}

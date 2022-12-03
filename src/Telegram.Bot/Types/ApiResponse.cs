using System.Diagnostics.CodeAnalysis;

namespace Telegram.Bot.Types;

/// <summary>
/// Represents bot API response
/// </summary>
/// <typeparam name="TResult">Expected type of operation result</typeparam>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class ApiResponse<TResult>
{
    /// <summary>
    /// Gets a value indicating whether the request was successful.
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool Ok { get; private set; }

    /// <summary>
    /// Gets the error message.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Description { get; private set; }

    /// <summary>
    /// Gets the error code.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? ErrorCode { get; private set; }

    /// <summary>
    /// Contains information about why a request was unsuccessful.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public ResponseParameters? Parameters { get; private set; }

    /// <summary>
    /// Gets the result object.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    [MaybeNull]
    [AllowNull]
    public TResult Result { get; private set; }

    /// <summary>
    /// Initializes an instance of <see cref="ApiResponse{TResult}"/>
    /// </summary>
    /// <param name="ok">Indicating whether the request was successful</param>
    /// <param name="result">Result object</param>
    /// <param name="errorCode">Error code</param>
    /// <param name="description">Error message</param>
    /// <param name="parameters">Information about why a request was unsuccessful</param>
    public ApiResponse(
        bool ok,
        TResult result,
        int errorCode,
        string description,
        ResponseParameters? parameters = default)
    {
        Ok = ok;
        ErrorCode = errorCode;
        Description = description;
        Parameters = parameters;
        Result = result;
    }

    [JsonConstructor]
    private ApiResponse()
    { }
}

using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization.Metadata;
using Telegram.Bot.Requests.Abstractions;

namespace Telegram.Bot.Requests;

/// <summary>
/// Represents an API request
/// </summary>
/// <typeparam name="TResponse">Type of result expected in result</typeparam>

public abstract class RequestBase<TResponse> : IRequest<TResponse>
{
    /// <inheritdoc />
    [JsonIgnore]
    public HttpMethod Method { get; }

    /// <inheritdoc />
    [JsonIgnore]
    public string MethodName { get; }

    [JsonIgnore]
    private JsonTypeInfo JsonTypeInfo { get; }

    /// <summary>
    /// Initializes an instance of request
    /// </summary>
    /// <param name="methodName">Bot API method</param>
    /// <param name="jsonTypeInfo"></param>
    protected RequestBase(string methodName, JsonTypeInfo jsonTypeInfo)
        : this(methodName, HttpMethod.Post, jsonTypeInfo)
    { }

    /// <summary>
    /// Initializes an instance of request
    /// </summary>
    /// <param name="methodName">Bot API method</param>
    /// <param name="method">HTTP method to use</param>
    /// <param name="jsonTypeInfo"></param>
    protected RequestBase(string methodName, HttpMethod method, JsonTypeInfo jsonTypeInfo) =>
        (MethodName, Method, JsonTypeInfo) = (methodName, method, jsonTypeInfo);

    /// <summary>
    /// Generate content of HTTP message
    /// </summary>
    /// <returns>Content of HTTP request</returns>
    public virtual HttpContent? ToHttpContent()
    {
        return new StringContent(
            content: JsonSerializer.Serialize(this, JsonTypeInfo),
            encoding: Encoding.UTF8,
            mediaType: "application/json"
        );
    }

    /// <inheritdoc />
    [JsonIgnore]
    public bool IsWebhookResponse { get; set; }

    /// <summary>
    /// If <see cref="IsWebhookResponse"/> is set to <see langword="true"/> is set to the method
    /// name, otherwise it won't be serialized
    /// </summary>
    [JsonPropertyName("method")]
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    internal string? WebHookMethodName => IsWebhookResponse ? MethodName : default;
}

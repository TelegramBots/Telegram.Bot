namespace Telegram.Bot.Requests;

/// <summary>Represents an API request</summary>
/// <typeparam name="TResponse">Type of result expected in result</typeparam>
/// <param name="methodName">Bot API method</param>
public abstract class RequestBase<TResponse>(string methodName) : IRequest<TResponse>
{
    /// <inheritdoc/>
    [JsonIgnore]
    public HttpMethod HttpMethod { get; set; } = HttpMethod.Post;

    /// <inheritdoc/>
    [JsonIgnore]
    public string MethodName { get; } = methodName;

    /// <inheritdoc/>
    [JsonIgnore]
    public bool IsWebhookResponse { get; set; }

    /// <summary><see href="https://core.telegram.org/bots/api#making-requests-when-getting-updates"/></summary>
    [JsonInclude]
    internal string? Method => IsWebhookResponse ? MethodName : default;

    /// <summary>Generate content of HTTP message</summary>
    /// <returns>Content of HTTP request</returns>
    public virtual HttpContent? ToHttpContent() =>
#if NET6_0_OR_GREATER
        System.Net.Http.Json.JsonContent.Create(this, GetType(), options: JsonBotAPI.Options);
#else
        new StringContent(JsonSerializer.Serialize(this, GetType(), JsonBotAPI.Options), System.Text.Encoding.UTF8, "application/json");
#endif
}

/// <summary>Represents a request that doesn't require any parameters</summary>
/// <param name="methodName">Name of request method</param>
public abstract class ParameterlessRequest<TResult>(string methodName) : RequestBase<TResult>(methodName)
{
    /// <inheritdoc/>
    public override HttpContent? ToHttpContent() => IsWebhookResponse ? base.ToHttpContent() : default;
}

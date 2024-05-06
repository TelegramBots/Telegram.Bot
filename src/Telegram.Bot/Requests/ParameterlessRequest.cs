using System.Net.Http;
using System.Text.Json.Serialization.Metadata;

namespace Telegram.Bot.Requests;

/// <summary>
/// Represents a request that doesn't require any parameters
/// </summary>
/// <typeparam name="TResult"></typeparam>

public abstract class ParameterlessRequest<TResult> : RequestBase<TResult>
{
    /// <summary>
    /// Initializes an instance of <see cref="ParameterlessRequest{TResult}"/>
    /// </summary>
    /// <param name="methodName">Name of request method</param>
    /// <param name="jsonTypeInfo"></param>
    protected ParameterlessRequest(string methodName, JsonTypeInfo jsonTypeInfo)
        : base(methodName, jsonTypeInfo)
    { }

    /// <summary>
    /// Initializes an instance of <see cref="ParameterlessRequest{TResult}"/>
    /// </summary>
    /// <param name="methodName">Name of request method</param>
    /// <param name="method">HTTP request method</param>
    /// <param name="jsonTypeInfo"></param>
    protected ParameterlessRequest(
        string methodName,
        HttpMethod method, JsonTypeInfo jsonTypeInfo)
        : base(methodName, method, jsonTypeInfo)
    { }

    /// <inheritdoc cref="RequestBase{TResponse}.ToHttpContent"/>
    public override HttpContent? ToHttpContent() =>
        IsWebhookResponse
            ? base.ToHttpContent()
            : default;
}

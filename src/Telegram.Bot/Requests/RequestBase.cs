using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Requests.Abstractions;

namespace Telegram.Bot.Requests;

/// <summary>
/// Represents an API request
/// </summary>
/// <typeparam name="TResponse">Type of result expected in result</typeparam>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public abstract class RequestBase<TResponse> : IRequest<TResponse>
{
    /// <inheritdoc />
    [JsonIgnore]
    public HttpMethod Method { get; }

    /// <inheritdoc />
    [JsonIgnore]
    public string MethodName { get; }

    /// <summary>
    /// Initializes an instance of request
    /// </summary>
    /// <param name="methodName">Bot API method</param>
    protected RequestBase(string methodName)
        : this(methodName, HttpMethod.Post)
    { }

    /// <summary>
    /// Initializes an instance of request
    /// </summary>
    /// <param name="methodName">Bot API method</param>
    /// <param name="method">HTTP method to use</param>
    protected RequestBase(string methodName, HttpMethod method)
    {
        (MethodName, Method) = (methodName, method);

        cachedStreamContent = new(async () =>
        {
            HttpContent? content = this.ToHttpContent();
            if (content is null)
                return null;

            var stream = await content.ReadAsStreamAsync().ConfigureAwait(false);
            var cachedContent = new StreamContent(stream);
            foreach (var header in content.Headers)
            {
                cachedContent.Headers.Add(header.Key, header.Value);
            }
            return cachedContent;
        });
    }

    private readonly Lazy<Task<StreamContent?>> cachedStreamContent;

    /// <inheritdoc/>
    public virtual Task<StreamContent?> CachedContent() => cachedStreamContent.Value;

    /// <inheritdoc/>
    public virtual HttpContent? ToHttpContent() =>
        new StringContent(
            content: JsonConvert.SerializeObject(this),
            encoding: Encoding.UTF8,
            mediaType: "application/json");

    /// <inheritdoc />
    [JsonIgnore]
    public bool IsWebhookResponse { get; set; }

    /// <summary>
    /// If <see cref="IsWebhookResponse"/> is set to <see langword="true"/> is set to the method
    /// name, otherwise it won't be serialized
    /// </summary>
    [JsonProperty("method", DefaultValueHandling = DefaultValueHandling.Ignore)]
    internal string? WebHookMethodName => IsWebhookResponse ? MethodName : default;
}

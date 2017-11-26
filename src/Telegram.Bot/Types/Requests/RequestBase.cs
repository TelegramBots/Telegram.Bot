using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents a API request
    /// </summary>
    /// <typeparam name="TResponse">Type of result expected in result</typeparam>
    public abstract class RequestBase<TResponse> : IRequest<TResponse>
        where TResponse : IResponse
    {
        /// <inheritdoc />
        [JsonIgnore]
        public HttpMethod Method { get; }

        /// <inheritdoc />
        [JsonIgnore]
        public string MethodName { get; protected set; }

        /// <summary>
        /// Initializes an instance of request
        /// </summary>
        /// <param name="methodName">Bot API method</param>
        protected RequestBase(string methodName)
            : this(methodName, HttpMethod.Post)
        {
        }

        /// <summary>
        /// Initializes an instance of request
        /// </summary>
        /// <param name="methodName">Bot API method</param>
        /// <param name="method">HTTP method to use</param>
        protected RequestBase(string methodName, HttpMethod method)
        {
            MethodName = methodName;
            Method = method;
        }

        /// <summary>
        /// Generate content of HTTP message
        /// </summary>
        /// <param name="serializerSettings">JSON serialization setting</param>
        /// <returns>Content of HTTP request</returns>
        public virtual HttpContent ToHttpContent(JsonSerializerSettings serializerSettings)
        {
            string payload = JsonConvert.SerializeObject(this, serializerSettings);
            return new StringContent(payload, Encoding.UTF8, "application/json");
        }
    }
}

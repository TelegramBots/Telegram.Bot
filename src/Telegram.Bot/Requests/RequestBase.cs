using System.Net.Http;
using System.Text;
using Telegram.Bot.Requests.Abstractions;

#if NETSTANDARD2_0
using System.Text.Json;
#else
using Newtonsoft.Json;
#endif

namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Represents a API request
    /// </summary>
    /// <typeparam name="TResponse">Type of result expected in result</typeparam>
    public abstract class RequestBase<TResponse> : IRequest<TResponse>
    {
        /// <inheritdoc />
        public HttpMethod Method { get; }

        /// <inheritdoc />
        public string MethodName { get; protected set; }

#if NETSTANDARD2_0
        private static readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
#endif

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
            MethodName = methodName;
            Method = method;
        }

        /// <summary>
        /// Generate content of HTTP message
        /// </summary>
        /// <returns>Content of HTTP request</returns>
        public virtual HttpContent ToHttpContent()
        {
            string payload;
#if NETSTANDARD2_0
            payload = JsonSerializer.Serialize(this, _serializerOptions);
#else
            payload = JsonConvert.SerializeObject(this);
#endif

            return new StringContent(payload, Encoding.UTF8, "application/json");
        }
    }
}

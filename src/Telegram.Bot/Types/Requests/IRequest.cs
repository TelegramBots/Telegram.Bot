using System.Net.Http;
using Newtonsoft.Json;

// ReSharper disable once CheckNamespace
// ReSharper disable once UnusedTypeParameter
namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents a request to Bot API
    /// </summary>
    /// <typeparam name="TResponse">Type of result expected in result</typeparam>
    public interface IRequest<TResponse>
        where TResponse : IResponse
    {
        /// <summary>
        /// HTTP method of request
        /// </summary>
        HttpMethod Method { get; }

        /// <summary>
        /// API method name
        /// </summary>
        string MethodName { get; }

        /// <summary>
        /// Generate content of HTTP message
        /// </summary>
        /// <param name="serializerSettings">JSON serialization setting</param>
        /// <returns>Content of HTTP request</returns>
        HttpContent ToHttpContent(JsonSerializerSettings serializerSettings);
    }
}

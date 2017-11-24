using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types
{
    public abstract class RequestBase<TResponse> : IRequest<TResponse>
        where TResponse : IResponse
    {
        [JsonIgnore]
        public HttpMethod Method { get; }

        [JsonIgnore]
        public string MethodName { get; protected set; }

        protected RequestBase(string methodName)
            : this(methodName, HttpMethod.Post)
        {
        }

        protected RequestBase(string methodName, HttpMethod method)
        {
            MethodName = methodName;
            Method = method;
        }

        public virtual HttpContent ToHttpContent(JsonSerializerSettings serializerSettings)
        {
            string payload = JsonConvert.SerializeObject(this, serializerSettings);
            return new StringContent(payload, Encoding.UTF8, "application/json");
        }
    }
}

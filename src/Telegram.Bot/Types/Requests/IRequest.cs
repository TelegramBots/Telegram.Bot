using System.Net.Http;
using Newtonsoft.Json;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types
{
    public interface IRequest<TResponse>
        where TResponse : IResponse
    {
        HttpMethod Method { get; }

        string MethodName { get; }

        HttpContent ToHttpContent(JsonSerializerSettings serializerSettings);
    }
}

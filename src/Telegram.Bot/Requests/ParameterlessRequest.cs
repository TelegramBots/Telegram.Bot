using System.Net.Http;

namespace Telegram.Bot.Requests
{
    public class ParameterlessRequest<TResult> : RequestBase<TResult>
    {
        public ParameterlessRequest(string methodName)
            : base(methodName)
        { }

        public ParameterlessRequest(string methodName, HttpMethod method)
            : base(methodName, method)
        { }

        /// <inheritdoc cref="RequestBase{TResponse}.ToHttpContent"/>
        public override HttpContent ToHttpContent() => default;
    }
}

using Telegram.Bot.Requests.Abstractions;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.MakeRequestAsync" /> method.
    /// </summary>
    public class MakeRequestParameters<TResponse> : ParametersBase
    {
        /// <summary>
        ///     API request object
        /// </summary>
        public IRequest<TResponse> Request { get; set; }

        /// <summary>
        ///     Sets <see cref="Request" /> property.
        /// </summary>
        /// <param name="request">API request object</param>
        public MakeRequestParameters<TResponse> WithRequest(IRequest<TResponse> request)
        {
            Request = request;
            return this;
        }
    }
}

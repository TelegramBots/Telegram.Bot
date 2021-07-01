using System.Net.Http;

namespace Telegram.Bot.Args
{
    /// <summary>
    /// Provides data for ApiResponseReceived event
    /// </summary>
    public class ApiResponseEventArgs
    {
        /// <summary>
        /// HTTP response received from API
        /// </summary>
        public HttpResponseMessage ResponseMessage { get; }

        /// <summary>
        /// Event arguments of this request
        /// </summary>
        public ApiRequestEventArgs ApiRequestEventArgs { get;  }

        public ApiResponseEventArgs(
            HttpResponseMessage responseMessage,
            ApiRequestEventArgs apiRequestEventArgs)
        {
            ResponseMessage = responseMessage;
            ApiRequestEventArgs = apiRequestEventArgs;
        }
    }
}

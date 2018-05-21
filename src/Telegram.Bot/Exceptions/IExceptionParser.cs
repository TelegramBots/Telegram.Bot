using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Telegram.Bot.Exceptions
{
    /// <summary>
    /// Parses unsuccessful HTTP resposnes from Telegram Bot API to make specific exceptions
    /// </summary>
    public interface IExceptionParser
    {
        /// <summary>
        /// Parses HTTP response and constructs a specific exception out of it
        /// </summary>
        /// <param name="httpResponse">HTTP response from Bot API</param>
        /// <returns>Exception to indicate the error</returns>
        Task<Exception> ParseResponseAsync(HttpResponseMessage httpResponse);
    }
}

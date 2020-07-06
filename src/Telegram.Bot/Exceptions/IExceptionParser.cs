using Telegram.Bot.Types;

namespace Telegram.Bot.Exceptions
{
    /// <summary>
    /// Parses unsuccessful responses from Telegram Bot API to make specific exceptions
    /// </summary>
    public interface IExceptionParser
    {
        /// <summary>
        /// Parses HTTP response and constructs a specific exception out of it
        /// </summary>
        /// <param name="errorCode">Error core of the response</param>
        /// <param name="description">Description of the error</param>
        /// <param name="responseParameters">
        /// Contains additional information about why a request was unsuccessful
        /// </param>
        /// <returns></returns>
        ApiRequestException Parse(
            int errorCode,
            string description,
            ResponseParameters? responseParameters);
    }
}

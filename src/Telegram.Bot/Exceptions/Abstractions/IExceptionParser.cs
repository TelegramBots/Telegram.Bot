using Telegram.Bot.Types;

namespace Telegram.Bot.Exceptions.Abstractions
{
    /// <summary>
    /// Telegram API error response parser.
    /// </summary>
    public interface IExceptionParser
    {
        /// <summary>
        /// Parse JSON-formatted Telegram API response and throw error-specific exception.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiResponse">JSON-formatted Telegram API response.</param>
        /// <returns></returns>
        ApiRequestException Parse<T>(ApiResponse<T> apiResponse);
    }
}

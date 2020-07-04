using Telegram.Bot.Types;

namespace Telegram.Bot.Exceptions
{
    /// <summary>
    /// An interface for parsing exceptions from Bot API
    /// </summary>
    public interface IExceptionParser
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="description"></param>
        /// <param name="responseParameters"></param>
        /// <returns></returns>
        ApiRequestException Parse(
            int errorCode,
            string description,
            ResponseParameters? responseParameters);
    }
}

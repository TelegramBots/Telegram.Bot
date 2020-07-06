using Telegram.Bot.Types;

namespace Telegram.Bot.Exceptions
{
    internal class ExceptionParser : IExceptionParser
    {
        public ApiRequestException Parse(
            int errorCode,
            string description,
            ResponseParameters? responseParameters) =>
            new ApiRequestException(description, errorCode, responseParameters);
    }
}

using System;
using Telegram.Bot.Types;

namespace Telegram.Bot.Exceptions
{
    public abstract class BadRequestException : ApiRequestException
    {
        public override int ErrorCode => BadRequestErrorCode;

        public const int BadRequestErrorCode = 400;

        public const string BadRequestErrorDescription = "Bad Request: ";

        public BadRequestException(string message)
            : base(message, BadRequestErrorCode)
        {
        }

        public BadRequestException(string message, Exception innerException)
            : base(message, BadRequestErrorCode, innerException)
        {
        }

        public BadRequestException(string message, ResponseParameters parameters)
            : base(message, BadRequestErrorCode, parameters)
        {
        }

        public BadRequestException(string message, ResponseParameters parameters, Exception innerException)
            : base(message, BadRequestErrorCode, parameters, innerException)
        {
        }
    }
}

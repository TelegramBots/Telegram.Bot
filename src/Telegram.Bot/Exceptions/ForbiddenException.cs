using System;
using Telegram.Bot.Types;

namespace Telegram.Bot.Exceptions
{
    public abstract class ForbiddenException : ApiRequestException
    {
        public override int ErrorCode => ForbiddenErrorCode;

        public const int ForbiddenErrorCode = 403;

        public const string ForbiddenErrorDescription = "Forbidden: ";

        public ForbiddenException(string message)
            : base(message, ForbiddenErrorCode)
        {
        }

        public ForbiddenException(string message, Exception innerException)
            : base(message, ForbiddenErrorCode, innerException)
        {
        }

        public ForbiddenException(string message, ResponseParameters parameters)
            : base(message, ForbiddenErrorCode, parameters)
        {
        }

        public ForbiddenException(string message, ResponseParameters parameters, Exception innerException)
            : base(message, ForbiddenErrorCode, parameters, innerException)
        {
        }
    }
}

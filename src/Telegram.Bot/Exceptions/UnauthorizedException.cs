using System;
using System.Net;
using Telegram.Bot.Types;

namespace Telegram.Bot.Exceptions
{
    /// <summary>
    /// Represents an error from Bot API with 401 Unauthorized HTTP status
    /// </summary>
    public class UnauthorizedException : ApiRequestException
    {
        /// <inheritdoc />
        public override int ErrorCode => UnauthorizedErrorCode;

        /// <summary>
        /// Represent error code number
        /// </summary>
        public const int UnauthorizedErrorCode = (int)HttpStatusCode.Unauthorized;

        /// <summary>
        /// Represent error description
        /// </summary>
        public const string BadRequestErrorDescription = "Unauthorized";

        /// <summary>
        /// Initializes a new instance of the <see cref="UnauthorizedException"/> class
        /// </summary>
        /// <param name="message">The message</param>
        public UnauthorizedException(string message)
            : base(message, UnauthorizedErrorCode)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnauthorizedException"/> class
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="innerException">The inner exception</param>
        public UnauthorizedException(string message, Exception innerException)
            : base(message, UnauthorizedErrorCode, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnauthorizedException"/> class
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="parameters">Response parameters</param>
        public UnauthorizedException(string message, ResponseParameters parameters)
            : base(message, UnauthorizedErrorCode, parameters)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnauthorizedException"/> class
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="parameters">Response parameters</param>
        /// <param name="innerException">The inner exception</param>
        public UnauthorizedException(string message, ResponseParameters parameters, Exception innerException)
            : base(message, UnauthorizedErrorCode, parameters, innerException)
        {
        }
    }
}

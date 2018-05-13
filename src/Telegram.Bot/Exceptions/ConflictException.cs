using System;
using System.Net;
using Telegram.Bot.Types;

namespace Telegram.Bot.Exceptions
{
    /// <summary>
    /// Represents an error from Bot API with 401 Unauthorized HTTP status
    /// </summary>
    public class ConflictException : ApiRequestException
    {
        /// <inheritdoc />
        public override int ErrorCode => ConflictErrorCode;

        /// <summary>
        /// Represent error code number
        /// </summary>
        public const int ConflictErrorCode = (int)HttpStatusCode.Conflict;

        /// <summary>
        /// Represent error description
        /// </summary>
        public const string BadRequestErrorDescription = "Conflict: ";

        /// <summary>
        /// Initializes a new instance of the <see cref="ConflictException"/> class
        /// </summary>
        /// <param name="message">The message</param>
        public ConflictException(string message)
            : base(message, ConflictErrorCode)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConflictException"/> class
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="innerException">The inner exception</param>
        public ConflictException(string message, Exception innerException)
            : base(message, ConflictErrorCode, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConflictException"/> class
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="parameters">Response parameters</param>
        public ConflictException(string message, ResponseParameters parameters)
            : base(message, ConflictErrorCode, parameters)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConflictException"/> class
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="parameters">Response parameters</param>
        /// <param name="innerException">The inner exception</param>
        public ConflictException(string message, ResponseParameters parameters, Exception innerException)
            : base(message, ConflictErrorCode, parameters, innerException)
        {
        }
    }
}

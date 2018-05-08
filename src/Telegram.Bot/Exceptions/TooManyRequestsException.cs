using System;
using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Exceptions
{
    /// <summary>
    /// Represents an error from Bot API with 429 Too Many Requests HTTP status
    /// </summary>
    public class TooManyRequestsException : ApiRequestException
    {
        /// <inheritdoc />
        public override int ErrorCode => TooManyRequestsErrorCode;

        /// <summary>
        /// Represent error code number
        /// </summary>
        public const int TooManyRequestsErrorCode = 429;

        /// <summary>
        /// Represent error description
        /// </summary>
        public const string TooManyRequestsErrorDescription = "Too Many Requests: ";

        /// <summary>
        /// Initializes a new instance of the <see cref="TooManyRequestsException"/> class
        /// </summary>
        /// <param name="message">The message</param>
        public TooManyRequestsException(string message)
            : base(message, TooManyRequestsErrorCode)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TooManyRequestsException"/> class
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="innerException">The inner exception</param>
        public TooManyRequestsException(string message, Exception innerException)
            : base(message, TooManyRequestsErrorCode, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TooManyRequestsException"/> class
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="parameters">Response parameters</param>
        public TooManyRequestsException(string message, ResponseParameters parameters)
            : base(message, TooManyRequestsErrorCode, parameters)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TooManyRequestsException"/> class
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="parameters">Response parameters</param>
        /// <param name="innerException">The inner exception</param>
        public TooManyRequestsException(string message, ResponseParameters parameters, Exception innerException)
            : base(message, TooManyRequestsErrorCode, parameters, innerException)
        {
        }
    }
}

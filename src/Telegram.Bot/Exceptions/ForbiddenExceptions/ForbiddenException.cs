using System;
using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Exceptions
{
    /// <summary>
<<<<<<< HEAD:src/Telegram.Bot/Exceptions/ForbiddenExceptions/ForbiddenException.cs
    /// A base class for "Error 403: Forbidden" API responses
    /// </summary>
    public class ForbiddenException : ApiRequestException
=======
    /// Represents an error from Bot API with 403 Forbidden HTTP status
    /// </summary>
    public abstract class ForbiddenException : ApiRequestException
>>>>>>> upstream/develop:src/Telegram.Bot/Exceptions/ForbiddenException.cs
    {
        /// <inheritdoc />
        public override int ErrorCode => ForbiddenErrorCode;

        /// <summary>
        /// Represent error code number
        /// </summary>
        public const int ForbiddenErrorCode = 403;

        /// <summary>
        /// Represent error description
        /// </summary>
        public const string ForbiddenErrorDescription = "Forbidden: ";

        /// <summary>
        /// Initializes a new instance of the <see cref="ForbiddenException"/> class
        /// </summary>
        /// <param name="message">The message</param>
        public ForbiddenException(string message)
            : base(message, ForbiddenErrorCode)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ForbiddenException"/> class
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="innerException">The inner exception</param>
        public ForbiddenException(string message, Exception innerException)
            : base(message, ForbiddenErrorCode, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ForbiddenException"/> class
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="parameters">Response parameters</param>
        public ForbiddenException(string message, ResponseParameters parameters)
            : base(message, ForbiddenErrorCode, parameters)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ForbiddenException"/> class
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="parameters">Response parameters</param>
        /// <param name="innerException">The inner exception</param>
        public ForbiddenException(string message, ResponseParameters parameters, Exception innerException)
            : base(message, ForbiddenErrorCode, parameters, innerException)
        {
        }
    }
}

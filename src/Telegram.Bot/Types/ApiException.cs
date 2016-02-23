using System;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents an api error
    /// </summary>
    public class ApiRequestException : Exception
    {
        /// <summary>
        /// Gets the error code.
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        public int ErrorCode { get; internal set; }

        public ApiRequestException(string message) : base(message)
        {
        }

        public ApiRequestException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
        public ApiRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ApiRequestException(string message, int errorCode, Exception innerException) : base(message, innerException)
        {
            ErrorCode = errorCode;
        }
    }
}

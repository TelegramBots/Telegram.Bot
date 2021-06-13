// ReSharper disable once CheckNamespace

using System;

namespace Telegram.Bot.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a parameter is invalid
    /// </summary>
    [Obsolete("Custom exceptions will be removed in the next major update")]
    public class InvalidParameterException : BadRequestException
    {
        internal const string ParamGroupName = "param";

        /// <summary>
        /// Name of invalid parameter
        /// </summary>
        public string Parameter { get; }

        /// <summary>
        /// Initializes a new object of the <see cref="InvalidParameterException"/> class
        /// </summary>
        /// <param name="paramName">Name of parameter</param>
        /// <param name="message">Message of the exception</param>
        public InvalidParameterException(string paramName, string message)
            : base(message)
        {
            Parameter = paramName;
        }

        /// <summary>
        /// Initializes a new object of the <see cref="InvalidParameterException"/> class
        /// </summary>
        /// <param name="paramName">Name of parameter</param>
        public InvalidParameterException(string paramName)
            : base(paramName)
        {
            Parameter = paramName;
        }
    }
}

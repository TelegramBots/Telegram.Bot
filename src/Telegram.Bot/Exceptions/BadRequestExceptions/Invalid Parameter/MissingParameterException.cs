// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Exceptions
{
    /// <summary>
    /// The exception is thrown when parameter is missing
    /// </summary>
    public class MissingParameterException : InvalidParameterException
    {
        /// <summary>
        /// The missing parameter.
        /// </summary>
        public string MissingParameter { get; }

        /// <summary>
        /// Initializes a new object of the <see cref="MissingParameterException"/> class
        /// </summary>
        /// <param name="paramName">The name of the missing parameter.</param>
        /// <param name="message">The error message of this exception.</param>
        public MissingParameterException(string paramName, string message)
            : base(paramName, message)
        {
            MissingParameter = paramName;
        }
    }
}

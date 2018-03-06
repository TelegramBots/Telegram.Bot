// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Exceptions
{
    /// <summary>
    /// The exception that is thrown when Query Id is invalid or AnswerInlineQueryAsync 
    /// called with 10 second delay
    /// </summary>
    public class InvalidQueryIdException : InvalidParameterException
    {
        /// <summary>
        /// Initializes a new object of the <see cref="InvalidQueryIdException"/> class
        /// </summary>
        /// <param name="message">The error message of this exception.</param>
        public InvalidQueryIdException(string message)
            : base("inline_query_id", message)
        {
        }
    }
}
// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Exceptions
{
    /// <summary>
    /// The exception that is thrown when Query Id is invalid or AnswerInlineQueryAsync 
    /// called with 10 second delay
    /// </summary>
    public class QueryIdInvalidException : BadRequestException
    {
        /// <summary>
        /// Initializes a new object of the <see cref="QueryIdInvalidException"/> class
        /// </summary>
        /// <param name="message">The error message of this exception.</param>
        public QueryIdInvalidException(string message)
            : base(message)
        {
        }
    }
}
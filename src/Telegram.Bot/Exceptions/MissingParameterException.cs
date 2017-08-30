using System;
using System.Collections.Generic;
using System.Text;

namespace Telegram.Bot.Exceptions
{
    /// <summary>
    /// Reperesents an api exception for a missing parameter.
    /// </summary>
    public class MissingParameterException : ApiRequestException
    {
        /// <summary>
        /// The missing parameter.
        /// </summary>
        public string MissingParameter { get; set; }
        /// <summary>
        /// Initializes a new object of the <see cref="MissingParameterException"/> class
        /// </summary>
        /// <param name="message">The error message of this exception.</param>
        /// <param name="paramName">The name of the missing parameter.</param>
        public MissingParameterException(string message, string paramName) : base(message, 400)
        {
            MissingParameter = paramName;
        }
    }
}

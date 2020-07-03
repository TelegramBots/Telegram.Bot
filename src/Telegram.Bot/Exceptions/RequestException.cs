using System;
using System.Net;

namespace Telegram.Bot.Exceptions
{
    public class RequestException : Exception
    {
        public HttpStatusCode? HttpStatusCode { get; }
        public string? Body { get; set; }

        public RequestException(string message)
            : base(message)
        {

        }

        public RequestException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public RequestException(string message, HttpStatusCode httpStatusCode, string body)
            : base(message)
        {
            HttpStatusCode = httpStatusCode;
            Body = body;
        }

        public RequestException(
            string message,
            HttpStatusCode httpStatusCode,
            string body,
            Exception innerException)
            : base(message, innerException)
        {
            HttpStatusCode = httpStatusCode;
            Body = body;
        }
    }
}

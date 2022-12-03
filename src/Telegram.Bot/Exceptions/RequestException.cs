using System.Net;

namespace Telegram.Bot.Exceptions;

/// <summary>
/// Represents a request error
/// </summary>
#pragma warning disable CA1032
public class RequestException : Exception
#pragma warning restore CA1032
{
    /// <summary>
    /// <see cref="HttpStatusCode"/> of the received response
    /// </summary>
    public HttpStatusCode? HttpStatusCode { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RequestException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public RequestException(string message)
        : base(message)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="RequestException"/> class.
    /// </summary>
    /// <param name="message">
    /// The error message that explains the reason for the exception.
    /// </param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception, or a null reference
    /// (Nothing in Visual Basic) if no inner exception is specified.
    /// </param>
    public RequestException(string message, Exception innerException)
        : base(message, innerException)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="RequestException"/> class.
    /// </summary>
    /// <param name="message">
    /// The error message that explains the reason for the exception.
    /// </param>
    /// <param name="httpStatusCode">
    /// <see cref="HttpStatusCode"/> of the received response
    /// </param>
    public RequestException(string message, HttpStatusCode httpStatusCode)
        : base(message) =>
        HttpStatusCode = httpStatusCode;

    /// <summary>
    /// Initializes a new instance of the <see cref="RequestException"/> class.
    /// </summary>
    /// <param name="message">
    /// The error message that explains the reason for the exception.
    /// </param>
    /// <param name="httpStatusCode">
    /// <see cref="HttpStatusCode"/> of the received response
    /// </param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception, or a null reference
    /// (Nothing in Visual Basic) if no inner exception is specified.
    /// </param>
    public RequestException(string message, HttpStatusCode httpStatusCode, Exception innerException)
        : base(message, innerException) =>
        HttpStatusCode = httpStatusCode;
}
using Telegram.Bot.Types;

namespace Telegram.Bot.Exceptions;

/// <summary>Represents an exception that occured while handling an <see cref="Update"/></summary>
#pragma warning disable CA1032
public class UpdateHandlingException : Exception
#pragma warning restore CA1032
{
    /// <summary>The <see cref="Update"/> that was being handled when the exception occured</summary>
    public Update Update { get; }

    /// <summary>Initializes a new instance of the <see cref="UpdateHandlingException"/> class.</summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="update">The <see cref="Update"/> that was being handled when the exception occured.</param>
    public UpdateHandlingException(string message, Update update) : base(message) => Update = update;

    /// <summary>Initializes a new instance of the <see cref="UpdateHandlingException"/> class.</summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="update">The <see cref="Update"/> that was being handled when the exception occured.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public UpdateHandlingException(string message, Update update, Exception innerException) : base(message, innerException) => Update = update;
}

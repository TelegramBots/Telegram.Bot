using JetBrains.Annotations;

namespace Telegram.Bot.Polling;

/// <summary>
/// 
/// </summary>
[PublicAPI]
public class ErrorContext
{
    /// <summary>
    /// 
    /// </summary>
    public bool ErrorHandled { get; set; }
    
    /// <summary>
    /// The <see cref="Exception"/> to handle
    /// </summary>
    public Exception Exception { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="exception">The <see cref="Exception"/> to handle</param>
    public ErrorContext(Exception exception)
    {
        Exception = exception;
    }
}

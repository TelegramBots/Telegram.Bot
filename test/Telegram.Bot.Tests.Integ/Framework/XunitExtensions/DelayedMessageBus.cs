using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Telegram.Bot.Tests.Integ.Framework.XunitExtensions;

/// <summary>
/// Used to capture messages to potentially be forwarded later. Messages are forwarded by
/// disposing of the message bus.
/// </summary>
public class DelayedMessageBus : IMessageBus
{
    readonly IMessageBus _innerBus;
    readonly List<IMessageSinkMessage> _messages = new();

    public DelayedMessageBus(IMessageBus innerBus) => _innerBus = innerBus;

    /// <inheritdoc />
    public bool QueueMessage(IMessageSinkMessage message)
    {
        lock (_messages)
        {
            _messages.Add(message);
        }

        // No way to ask the inner bus if they want to cancel without sending them the message, so
        // we just go ahead and continue always.
        return true;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        foreach (var message in _messages)
        {
            _innerBus.QueueMessage(message);
        }
    }

    /// <summary>
    /// Retrieve TestFailed message from IMessageSinkMessage list.
    /// </summary>
    public TestFailed FailedMessages
    {
        get
        {
            lock (_messages)
            {
                return _messages.Find(m => m.GetType() == typeof(TestFailed)) as TestFailed;
            }
        }
    }

    public bool ContainsException(string exceptionTypeFullName) =>
        FailedMessages?.ExceptionTypes?.Contains(exceptionTypeFullName) ?? false;
}
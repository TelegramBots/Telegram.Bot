using System.Collections.Generic;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Telegram.Bot.Tests.Integ.Framework.XunitExtensions;

public class RetryFactDiscoverer : IXunitTestCaseDiscoverer
{
    readonly IMessageSink _diagnosticMessageSink;

    public RetryFactDiscoverer(IMessageSink diagnosticMessageSink)
    {
        _diagnosticMessageSink = diagnosticMessageSink;
    }

    /// <inheritdoc />
    public IEnumerable<IXunitTestCase> Discover(
        ITestFrameworkDiscoveryOptions discoveryOptions,
        ITestMethod testMethod,
        IAttributeInfo factAttribute)
    {
        int maxRetries = factAttribute
            .GetNamedArgument<int>(nameof(OrderedFactAttribute.MaxRetries));

        int delaySeconds = factAttribute
            .GetNamedArgument<int>(nameof(OrderedFactAttribute.DelaySeconds));

        string exceptionTypeFullName = factAttribute
            .GetNamedArgument<string>(nameof(OrderedFactAttribute.ExceptionTypeFullName));

        var retryTestCase = new RetryTestCase(
            _diagnosticMessageSink,
            discoveryOptions.MethodDisplayOrDefault(),
            testMethod,
            maxRetries,
            delaySeconds,
            exceptionTypeFullName
        );

        yield return retryTestCase;
    }
}
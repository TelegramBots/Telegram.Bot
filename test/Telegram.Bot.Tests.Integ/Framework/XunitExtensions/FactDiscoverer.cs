using System.Collections.Generic;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Telegram.Bot.Tests.Integ.Framework.XunitExtensions
{
    public class FactDiscoverer : IXunitTestCaseDiscoverer
    {
        private readonly IMessageSink _diagnosticMessageSink;

        public FactDiscoverer(IMessageSink diagnosticMessageSink)
        {
            _diagnosticMessageSink = diagnosticMessageSink;
        }

        public IEnumerable<IXunitTestCase> Discover(
            ITestFrameworkDiscoveryOptions discoveryOptions,
            ITestMethod testMethod,
            IAttributeInfo factAttribute
        )
        {
            bool shouldRetry = factAttribute.GetNamedArgument<bool>(nameof(OrderedFactAttribute.Retry));

            XunitTestCase testCase = shouldRetry
                ? new RetryTestCase(
                    _diagnosticMessageSink,
                    discoveryOptions.MethodDisplayOrDefault(),
                    testMethod
                )
                : new XunitTestCase(
                    _diagnosticMessageSink,
                    discoveryOptions.MethodDisplayOrDefault(),
                    testMethod
                );

            yield return testCase;
        }
    }
}

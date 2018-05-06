using System.Collections.Generic;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Telegram.Bot.Tests.Integ.Framework.XunitExtensions
{
    public class RetryFactDiscoverer : IXunitTestCaseDiscoverer
    {
        private readonly IMessageSink _diagnosticMessageSink;

        public RetryFactDiscoverer(IMessageSink diagnosticMessageSink)
        {
            _diagnosticMessageSink = diagnosticMessageSink;
        }

        /// <inheritdoc />
        public IEnumerable<IXunitTestCase> Discover
            (ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
        {
            var maxRetries = factAttribute.GetNamedArgument<int>("MaxRetries");
            if (maxRetries < 1)
                maxRetries = 2;

            var exceptionTypeFullName = factAttribute.GetNamedArgument<string>("ExceptionTypeFullName");

            yield return new RetryTestCase
                (_diagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), testMethod, maxRetries, exceptionTypeFullName);
        }
    }
}

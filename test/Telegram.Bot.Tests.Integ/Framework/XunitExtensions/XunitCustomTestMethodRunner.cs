using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Telegram.Bot.Tests.Integ.Framework.XunitExtensions
{
    public class XunitCustomTestMethodRunner : XunitTestMethodRunner
    {
        private readonly IMessageSink _diagnosticMessageSink;
        private readonly object[] _constructorArguments;

        public XunitCustomTestMethodRunner(ITestMethod testMethod,
                                           IReflectionTypeInfo @class,
                                           IReflectionMethodInfo method,
                                           IEnumerable<IXunitTestCase> testCases,
                                           IMessageSink diagnosticMessageSink,
                                           IMessageBus messageBus,
                                           ExceptionAggregator aggregator,
                                           CancellationTokenSource cancellationTokenSource,
                                           object[] constructorArguments)
            : base(testMethod, @class, method, testCases, diagnosticMessageSink, messageBus, aggregator, cancellationTokenSource, constructorArguments)
        {
            _diagnosticMessageSink = diagnosticMessageSink;
            _constructorArguments = constructorArguments;
        }

        protected override Task<RunSummary> RunTestCaseAsync(IXunitTestCase testCase)
        {
            // We've done everything we need, so let the built-in types do the rest of the heavy lifting
            return testCase.RunAsync(_diagnosticMessageSink, MessageBus, _constructorArguments, new ExceptionAggregator(Aggregator), CancellationTokenSource);
        }
    }
}

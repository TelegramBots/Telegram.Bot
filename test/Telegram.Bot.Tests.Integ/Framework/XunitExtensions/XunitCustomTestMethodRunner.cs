using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Polly;
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

        protected override Task<RunSummary> RunTestCaseAsync(IXunitTestCase testCase) =>
            Policy
                .HandleResult<RunSummary>(r => r.Failed != 0)
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(30),
                    TimeSpan.FromSeconds(45),
                    TimeSpan.FromSeconds(60),
                })
                .ExecuteAsync(() =>
                    // We've done everything we need, so let the built-in types do the rest of the heavy lifting
                    testCase.RunAsync
                        (_diagnosticMessageSink, MessageBus, _constructorArguments, new ExceptionAggregator(Aggregator), CancellationTokenSource)
                );
    }
}

using System;
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

        public XunitCustomTestMethodRunner(
            ITestMethod testMethod,
            IReflectionTypeInfo @class,
            IReflectionMethodInfo method,
            IEnumerable<IXunitTestCase> testCases,
            IMessageSink diagnosticMessageSink,
            IMessageBus messageBus,
            ExceptionAggregator aggregator,
            CancellationTokenSource cancellationTokenSource,
            object[] constructorArguments
        )
            : base(testMethod, @class, method, testCases, diagnosticMessageSink, messageBus, aggregator,
                cancellationTokenSource, constructorArguments)
        {
            _diagnosticMessageSink = diagnosticMessageSink;
            _constructorArguments = constructorArguments;
        }

        protected override async Task<RunSummary> RunTestCaseAsync(IXunitTestCase testCase)
        {
            const int maxRetry = 1;
            const int delaySeconds = 30;

            RunSummary summary = null;
            int retryCount = 0;
            while (retryCount < maxRetry)
            {
                summary = await testCase.RunAsync(
                    _diagnosticMessageSink,
                    MessageBus,
                    _constructorArguments,
                    new ExceptionAggregator(Aggregator),
                    CancellationTokenSource
                );

                if (summary.Failed == 0)
                {
                    break;
                }
                else
                {
                    await Task.Delay(delaySeconds * 1000);
                    retryCount++;
                }
            }

            return summary;
        }
    }
}

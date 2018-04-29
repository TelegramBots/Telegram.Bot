using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Telegram.Bot.Tests.Integ.Framework.XunitExtensions
{
    [Serializable]
    public class RetryTestCase : XunitTestCase
    {
        public readonly Type[] ExceptionTypes =
        {
            typeof(ApiRequestException)
        };

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Called by the de-serializer", true)]
        public RetryTestCase()
        {
        }

        public RetryTestCase(
            IMessageSink diagnosticMessageSink, TestMethodDisplay testMethodDisplay, ITestMethod testMethod
        )
            : base(diagnosticMessageSink, testMethodDisplay, testMethod, testMethodArguments: null)
        {
        }

        // This method is called by the xUnit test framework classes to run the test case. We will do the
        // loop here, forwarding on to the implementation in XunitTestCase to do the heavy lifting. We will
        // continue to re-run the test until the aggregator has an error (meaning that some internal error
        // condition happened), or the test runs without failure, or we've hit the maximum number of tries.
        public override async Task<RunSummary> RunAsync(
            IMessageSink diagnosticMessageSink,
            IMessageBus messageBus,
            object[] constructorArguments,
            ExceptionAggregator aggregator,
            CancellationTokenSource cancellationTokenSource
        )
        {
            async Task<RunSummary> RunTestCaseAsync()
            {
                // This is really the only tricky bit: we need to capture and delay messages (since those will
                // contain run status) until we know we've decided to accept the final result;
                var delayedMessageBus = new DelayedMessageBus(messageBus);

                var summary = await base.RunAsync(diagnosticMessageSink, delayedMessageBus, constructorArguments,
                    aggregator, cancellationTokenSource);
                if (summary.Failed > 0)
                {
                    var a = aggregator.HasExceptions;
                    var exception = aggregator.ToException();
                    throw exception;
                }

                delayedMessageBus.Dispose(); // Sends all the delayed messages
                return summary;
            }

            RunSummary summ;
            int count = 0;
            do
            {
                try
                {
                    summ = await RunTestCaseAsync();
                }
                catch (Exception e)
                {
                    count++;
                    summ = new RunSummary {Failed = 1, Total = 1};
                    diagnosticMessageSink.OnMessage(
                        new DiagnosticMessage("Execution of '{0}' failed. Retrying...", DisplayName));
                }
            } while (count < 2);

            return summ;
        }
    }
}

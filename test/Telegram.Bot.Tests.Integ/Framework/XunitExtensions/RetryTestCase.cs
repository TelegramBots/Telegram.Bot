using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Telegram.Bot.Tests.Integ.Framework.XunitExtensions
{
    [Serializable]
    public class RetryTestCase : XunitTestCase
    {
        private int _maxRetries;

        private string _exceptionTypeFullName;

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Called by the de-serializer", true)]
        public RetryTestCase() { }

        public RetryTestCase(IMessageSink diagnosticMessageSink, TestMethodDisplay testMethodDisplay, ITestMethod testMethod, int maxRetries, string exceptionTypeFullName)
            : base(diagnosticMessageSink, testMethodDisplay, testMethod, testMethodArguments: null)
        {
            _maxRetries = maxRetries;
            _exceptionTypeFullName = exceptionTypeFullName;
        }

        // This method is called by the xUnit test framework classes to run the test case. We will do the
        // loop here, forwarding on to the implementation in XunitTestCase to do the heavy lifting. We will
        // continue to re-run the test until the aggregator has an error (meaning that some internal error
        // condition happened), or the test runs without failure, or we've hit the maximum number of tries.
        public override async Task<RunSummary> RunAsync(IMessageSink diagnosticMessageSink,
                                                        IMessageBus messageBus,
                                                        object[] constructorArguments,
                                                        ExceptionAggregator aggregator,
                                                        CancellationTokenSource cancellationTokenSource)
        {
            var runCount = 0;
            const int delaySeconds = 30;

            while (true)
            {
                // This is really the only tricky bit: we need to capture and delay messages (since those will
                // contain run status) until we know we've decided to accept the final result;
                var delayedMessageBus = new DelayedMessageBus(messageBus);

                var summary = await base.RunAsync
                    (diagnosticMessageSink, delayedMessageBus, constructorArguments, aggregator, cancellationTokenSource);
                if (aggregator.HasExceptions ||
                    summary.Failed == 0 ||
                    ++runCount >= _maxRetries ||
                        (summary.Failed == 1 &&
                        !string.IsNullOrEmpty(_exceptionTypeFullName) &&
                        !delayedMessageBus.FailedMessages.ExceptionTypes.Contains(_exceptionTypeFullName))
                        )
                {
                    delayedMessageBus.Dispose();  // Sends all the delayed messages
                    return summary;
                }

                diagnosticMessageSink.OnMessage(
                    new DiagnosticMessage(
                        "Execution of '{0}' failed (attempt #{1}), retrying in {2} seconds...",
                        DisplayName,
                        runCount,
                        delaySeconds));

                await Task.Delay(delaySeconds * 1_000);
            }
        }

        public override void Serialize(IXunitSerializationInfo data)
        {
            base.Serialize(data);

            data.AddValue("MaxRetries", _maxRetries);
            data.AddValue("ExceptionTypeFullName", _exceptionTypeFullName);
        }

        public override void Deserialize(IXunitSerializationInfo data)
        {
            base.Deserialize(data);

            _maxRetries = data.GetValue<int>("MaxRetries");
            _exceptionTypeFullName = data.GetValue<string>("ExceptionTypeFullName");
        }
    }
}

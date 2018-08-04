using System;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Polly;
using Telegram.Bot.Exceptions;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Telegram.Bot.Tests.Integ.Framework.XunitExtensions
{
    [Serializable]
    public class RetryTestCase : XunitTestCase
    {
        private int _maxRetries;

        private int _delaySeconds;

        private string _exceptionTypeFullName;

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Called by the de-serializer", true)]
        public RetryTestCase()
        {
        }

        public RetryTestCase(
            IMessageSink diagnosticMessageSink,
            TestMethodDisplay testMethodDisplay,
            ITestMethod testMethod,
            int maxRetries,
            int delaySeconds,
            string exceptionTypeFullName
        )
            : base(diagnosticMessageSink, testMethodDisplay, TestMethodDisplayOptions.All, testMethod)
        {
            _maxRetries = maxRetries;
            _delaySeconds = delaySeconds;
            _exceptionTypeFullName = exceptionTypeFullName;
        }

        /// <inheritdoc cref="XunitTestCase"/>
        /// <remarks>
        /// This method is called by the xUnit test framework classes to run the test case. We will do the
        /// loop here, forwarding on to the implementation in XunitTestCase to do the heavy lifting.We will
        /// continue to re-run the test until the aggregator has an error(meaning that some internal error
        /// condition happened), or the test runs without failure, or we've hit the maximum number of tries.
        /// </remarks>
        public override async Task<RunSummary> RunAsync(
            IMessageSink diagnosticMessageSink,
            IMessageBus messageBus,
            object[] constructorArguments,
            ExceptionAggregator aggregator,
            CancellationTokenSource cancellationTokenSource
        )
        {
            int runCount = 0;
            while (true)
            {
                // This is really the only tricky bit: we need to capture and delay messages (since those will
                // contain run status) until we know we've decided to accept the final result;
                var delayedMessageBus = new DelayedMessageBus(messageBus);

                string testName = DisplayName;
                if (runCount > 0)
                {
                    testName += $"\n\nRETRY:{runCount}";
                }

                await Policy
                    .Handle<TaskCanceledException>()
                    .Or<HttpRequestException>()
                    .Or<ApiRequestException>()
                    .WaitAndRetry(1, i => TimeSpan.FromSeconds(30))
                    .Execute(() =>
                        TestsFixture.Instance.SendTestCaseNotificationAsync(testName)
                    );

                var summary = await base.RunAsync
                (diagnosticMessageSink, delayedMessageBus, constructorArguments, aggregator,
                    cancellationTokenSource);
                if (aggregator.HasExceptions ||
                    summary.Failed == 0 ||
                    ++runCount > _maxRetries ||
                    (summary.Failed == 1 &&
                     !string.IsNullOrEmpty(_exceptionTypeFullName) &&
                     !delayedMessageBus.FailedMessages.ExceptionTypes.Contains(_exceptionTypeFullName))
                )
                {
                    delayedMessageBus.Dispose(); // Sends all the delayed messages
                    return summary;
                }

                diagnosticMessageSink.OnMessage(new DiagnosticMessage(
                    "Execution of '{0}' failed (attempt #{1}), retrying in {2} seconds...",
                    DisplayName,
                    runCount,
                    _delaySeconds
                ));

                await Task.Delay(_delaySeconds * 1_000);
            }
        }

        public override void Serialize(IXunitSerializationInfo data)
        {
            base.Serialize(data);

            data.AddValue(nameof(OrderedFactAttribute.MaxRetries), _maxRetries);
            data.AddValue(nameof(OrderedFactAttribute.DelaySeconds), _delaySeconds);
            data.AddValue(nameof(OrderedFactAttribute.ExceptionTypeFullName), _exceptionTypeFullName);
        }

        public override void Deserialize(IXunitSerializationInfo data)
        {
            base.Deserialize(data);

            _maxRetries = data.GetValue<int>(nameof(OrderedFactAttribute.MaxRetries));
            _delaySeconds = data.GetValue<int>(nameof(OrderedFactAttribute.DelaySeconds));
            _exceptionTypeFullName = data.GetValue<string>(nameof(OrderedFactAttribute.ExceptionTypeFullName));
        }
    }
}

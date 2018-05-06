using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace Telegram.Bot.Tests.Integ.Framework
{
    /// <summary>
    /// Works like [Fact] but executes in order of definition. Also will retry maxRetries
    /// times if test fails.
    /// </summary>
    [XunitTestCaseDiscoverer(Constants.AssemblyName + ".Framework.XunitExtensions.RetryFactDiscoverer", Constants.AssemblyName)]
    public class OrderedFactAttribute : FactAttribute
    {
        /// <summary>
        /// Line number in source file.
        /// </summary>
        public readonly int LineNumber;

        /// <summary>
        /// Test retry attempts (by default, 2 times).
        /// </summary>
        public int MaxRetries { get;  }

        /// <summary>
        /// Only retry if test failed with specified exception (by default, on TaskCanceledException).
        /// </summary>
        public string ExceptionTypeFullName { get;  }

        /// <summary>
        /// Works like [Fact] but executes in order of definition. Also will retry maxRetries
        /// times if test fails.
        /// </summary>
        /// <param name="line">Line number in source file.</param>
        /// <param name="maxRetries">Test retry attempts.</param>
        /// <param name="exceptionType">Only retry if test failed with specified exception.</param>
        public OrderedFactAttribute(
            [CallerLineNumber] int line = default,
            int maxRetries = 2,
            Type exceptionType = default
        )
        {
            if (line < 1)
                throw new ArgumentOutOfRangeException(nameof(line));

            if (maxRetries < 1)
                throw new ArgumentOutOfRangeException(nameof(maxRetries));

            LineNumber = line;

            MaxRetries = maxRetries;

            ExceptionTypeFullName = exceptionType != null
                ? exceptionType.FullName
                : typeof(TaskCanceledException).FullName;
        }
    }
}

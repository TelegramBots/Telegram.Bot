using System;
using System.Runtime.CompilerServices;
using Xunit;
using Xunit.Sdk;

namespace Telegram.Bot.Tests.Integ.Framework
{
    [XunitTestCaseDiscoverer(Constants.FactDisoverer, Constants.AssemblyName)]
    public class OrderedFactAttribute : FactAttribute
    {
        public readonly int LineNumber;

        public bool Retry { get; set; } = true;

        public OrderedFactAttribute(
            [CallerLineNumber] int line = default
        )
        {
            if (line < 1)
                throw new ArgumentOutOfRangeException(nameof(line));

            LineNumber = line;
        }
    }
}

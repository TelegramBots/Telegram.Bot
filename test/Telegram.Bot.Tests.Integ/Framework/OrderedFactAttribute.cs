using System;
using System.Runtime.CompilerServices;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Framework
{
    public class OrderedFactAttribute : FactAttribute
    {
        public readonly int LineNumber;

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
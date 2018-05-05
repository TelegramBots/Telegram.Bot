using System.Threading.Tasks;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Framework
{
    public abstract class DelayTestsBase
    {
        [OrderedFact]
        public async Task Delay()
        {
            await Task.Delay(60_000);
        }
    }

    [Collection(nameof(DelayTests1))] public class DelayTests1 : DelayTestsBase { }

    [Collection(nameof(DelayTests2))] public class DelayTests2 : DelayTestsBase { }

    [Collection(nameof(DelayTests3))] public class DelayTests3 : DelayTestsBase { }

    [Collection(nameof(DelayTests4))] public class DelayTests4 : DelayTestsBase { }

    [Collection(nameof(DelayTests5))] public class DelayTests5 : DelayTestsBase { }
}

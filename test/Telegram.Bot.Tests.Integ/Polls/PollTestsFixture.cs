using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;

namespace Telegram.Bot.Tests.Integ.Polls
{
    public class PollTestsFixture
    {
        public TestsFixture TestsFixture { get; }

        public Message PollMessage { get; set; }

        public PollTestsFixture(TestsFixture testsFixture)
        {
            TestsFixture = testsFixture;
        }
    }
}

using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;

namespace Telegram.Bot.Tests.Integ.Polls;

public class AnonymousPollTestsFixture
{
    public TestsFixture TestsFixture { get; }

    public Message PollMessage { get; set; }

    public AnonymousPollTestsFixture(TestsFixture testsFixture)
    {
        TestsFixture = testsFixture;
    }
}
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;

namespace Telegram.Bot.Tests.Integ.Polls;

public class AnonymousPollTestsFixture(TestsFixture testsFixture)
{
    public TestsFixture TestsFixture { get; } = testsFixture;

    public Message PollMessage { get; set; }
}
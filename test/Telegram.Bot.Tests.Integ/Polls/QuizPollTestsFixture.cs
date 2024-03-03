using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;

namespace Telegram.Bot.Tests.Integ.Polls;

public class QuizPollTestsFixture(TestsFixture testsFixture)
{
    public TestsFixture TestsFixture { get; } = testsFixture;
    public Message OriginalPollMessage { get; set; }
    public PollAnswer PollAnswer { get; set; }
}
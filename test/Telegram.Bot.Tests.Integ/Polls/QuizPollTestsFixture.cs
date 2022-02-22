using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;

namespace Telegram.Bot.Tests.Integ.Polls;

public class QuizPollTestsFixture
{
    public TestsFixture TestsFixture { get; }
    public Message OriginalPollMessage { get; set; }
    public PollAnswer PollAnswer { get; set; }

    public QuizPollTestsFixture(TestsFixture testsFixture)
    {
        TestsFixture = testsFixture;
    }
}
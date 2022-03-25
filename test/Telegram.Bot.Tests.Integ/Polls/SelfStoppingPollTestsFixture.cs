using System;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;

namespace Telegram.Bot.Tests.Integ.Polls;

public class SelfStoppingPollTestsFixture
{
    public TestsFixture TestsFixture { get; }
    public Message PollMessage { get; set; }
    public int OpenPeriod { get; set; }
    public DateTime CloseDate { get; set; }

    public SelfStoppingPollTestsFixture(TestsFixture testsFixture)
    {
        TestsFixture = testsFixture;
    }
}
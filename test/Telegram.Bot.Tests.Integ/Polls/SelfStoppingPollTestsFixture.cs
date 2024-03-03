using System;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;

namespace Telegram.Bot.Tests.Integ.Polls;

public class SelfStoppingPollTestsFixture(TestsFixture testsFixture)
{
    public TestsFixture TestsFixture { get; } = testsFixture;
    public Message PollMessage { get; set; }
    public int OpenPeriod { get; set; }
    public DateTime CloseDate { get; set; }
}
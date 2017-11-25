using System.Collections.Generic;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;

namespace Telegram.Bot.Tests.Integ.SendingMessages
{
    public class AlbumsTestFixture
    {
        public IEnumerable<Message> PhotoMessages { get; set; }

        public TestsFixture TestsFixture { get; }

        public AlbumsTestFixture(TestsFixture testsFixture)
        {
            TestsFixture = testsFixture;
        }
    }
}

using Xunit;

namespace Telegram.Bot.Tests.Integ.Common
{
    [CollectionDefinition(CommonConstants.TestCollectionName)]
    [TestCaseOrderer("Telegram.Bot.Tests.TestCaseOrderer", "Telegram.Bot.Tests")]
    public class AllCollectionsFixture : ICollectionFixture<BotClientFixture>
    {

    }
}

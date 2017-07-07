using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Tests.Integ.XunitExtensions;
using Xunit;

[assembly: TestFramework("Telegram.Bot.Tests.Integ.XunitExtensions.XunitTestFrameworkWithAssemblyFixture", "Telegram.Bot.Tests.Integ")]
[assembly: AssemblyFixture(typeof(BotClientFixture))]
[assembly: TestCollectionOrderer("Telegram.Bot.Tests.Integ.Common.TestCollectionOrderer", "Telegram.Bot.Tests.Integ")]
[assembly: CollectionBehavior(DisableTestParallelization = true)]

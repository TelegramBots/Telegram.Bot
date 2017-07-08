using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Tests.Integ.XunitExtensions;
using Xunit;

[assembly: TestFramework("Telegram.Bot.Tests.Integ.XunitExtensions.XunitTestFrameworkWithAssemblyFixture", CommonConstants.AssemblyName)]
[assembly: AssemblyFixture(typeof(BotClientFixture))]
[assembly: TestCollectionOrderer("Telegram.Bot.Tests.Integ.Common.TestCollectionOrderer", CommonConstants.AssemblyName)]
[assembly: CollectionBehavior(DisableTestParallelization = true)]

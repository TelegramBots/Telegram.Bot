using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Tests.Integ.XunitExtensions;
using Xunit;

[assembly: TestFramework("Telegram.Bot.Tests.Integ.XunitExtensions.XunitTestFrameworkWithAssemblyFixture", Constants.AssemblyName)]
[assembly: AssemblyFixture(typeof(TestsFixture))]
[assembly: TestCollectionOrderer("Telegram.Bot.Tests.Integ.Common.TestCollectionOrderer", Constants.AssemblyName)]
[assembly: CollectionBehavior(DisableTestParallelization = true)]

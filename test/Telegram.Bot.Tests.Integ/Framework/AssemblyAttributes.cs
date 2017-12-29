using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Tests.Integ.Framework.XunitExtensions;
using Xunit;

[assembly: TestFramework("Telegram.Bot.Tests.Integ.Framework.XunitExtensions.XunitTestFrameworkWithAssemblyFixture", Constants.AssemblyName)]
[assembly: AssemblyFixture(typeof(TestsFixture))]
[assembly: TestCollectionOrderer("Telegram.Bot.Tests.Integ.Framework.TestCollectionOrderer", Constants.AssemblyName)]
[assembly: CollectionBehavior(DisableTestParallelization = true)]

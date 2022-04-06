using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Telegram.Bot.Tests.Integ.Framework.XunitExtensions;

public class XunitTestFrameworkWithAssemblyFixture : XunitTestFramework
{
    public XunitTestFrameworkWithAssemblyFixture(IMessageSink messageSink)
        : base(messageSink)
    { }

    protected override ITestFrameworkExecutor CreateExecutor(AssemblyName assemblyName)
        => new XunitTestFrameworkExecutorWithAssemblyFixture(
            assemblyName,
            SourceInformationProvider,
            DiagnosticMessageSink
        );
}
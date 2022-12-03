using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Telegram.Bot.Tests.Integ.Framework;

public sealed class ConfigurationProvider : IDisposable
{
    private readonly IServiceProvider _services;

    public TestConfiguration Configuration { get; }

    public ConfigurationProvider()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", true)
            .AddEnvironmentVariables("TelegramBot_")
            .Build();

        var serviceCollection = new ServiceCollection();

        serviceCollection
            .AddOptions<TestConfiguration>()
            .ValidateDataAnnotations()
            .PostConfigure(x =>
            {
                if (x.AllowedUserNamesString is not null)
                {
                    x.AllowedUserNames = x.AllowedUserNamesString
                        .Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                        .ToArray();
                }
            });

        serviceCollection.Configure<TestConfiguration>(configuration);

        _services = serviceCollection.BuildServiceProvider();

        Configuration = _services.GetRequiredService<IOptions<TestConfiguration>>().Value;
    }

    public void Dispose()
    {
        if (_services is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }
}

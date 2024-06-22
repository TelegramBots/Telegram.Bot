using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot;

public static partial class TelegramBotClientExtensions
{
    /// <summary>
    /// Configure JsonOptions for Telegram.Bot (de)serialization
    /// </summary>
    /// <param name="services">The Microsoft.Extensions.DependencyInjection.IServiceCollection to add the services to.</param>
    /// <param name="opt">Accessor to JsonSerializerOptions</param>
    /// <returns></returns>
    public static IServiceCollection ConfigureTelegramBot<TOptions>(this IServiceCollection services, Func<TOptions, JsonSerializerOptions> opt)
        where TOptions : class
    {
        return services.Configure<TOptions>(options => JsonSerializerOptionsProvider.Configure(opt(options)));
    }
}

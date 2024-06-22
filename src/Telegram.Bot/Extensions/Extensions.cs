using System.Runtime.CompilerServices;

namespace Telegram.Bot.Extensions
{
    /// <summary>
    /// Extension Methods
    /// </summary>
    internal static class ObjectExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T ThrowIfNull<T>(
            this T? value,
            [CallerArgumentExpression(nameof(value))] string? parameterName = default
        ) =>
            value ?? throw new ArgumentNullException(parameterName);
    }
}

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Helpers for API service configuration
    /// </summary>
    public static class TelegramBotConfigureExtensions
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
}

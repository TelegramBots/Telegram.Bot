using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
#if NET6_0_OR_GREATER
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
#endif

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
    /// <summary>Helpers for WebApp service configuration</summary>
    public static class TelegramBotConfigureExtensions
    {
        /// <summary>Configure WebAPI JsonOptions for Telegram.Bot (de)serialization</summary>
        /// <param name="services">The IServiceCollection to add the services to.</param>
        /// <param name="opt">Accessor to JsonSerializerOptions</param>
        public static IServiceCollection ConfigureTelegramBot<TOptions>(this IServiceCollection services, Func<TOptions, JsonSerializerOptions> opt)
            where TOptions : class
            => services.Configure<TOptions>(options => JsonSerializerOptionsProvider.Configure(opt(options)));

#if NET6_0_OR_GREATER
        /// <summary>Configure ASP.NET MVC Json (de)serialization for Telegram.Bot types</summary>
        /// <param name="services">The IServiceCollection to add the services to.</param>
        public static IServiceCollection ConfigureTelegramBotMvc(this IServiceCollection services)
            => services.Configure<MvcOptions>(options =>
            {
                options.InputFormatters.Insert(0, InputFormatter);
                options.OutputFormatters.Insert(0, OutputFormatter);
            });

        private static readonly TelegramBotOutputFormatter OutputFormatter = new();
        private static readonly TelegramBotInputFormatter InputFormatter = new();

#pragma warning disable MA0004 // See https://learn.microsoft.com/en-us/aspnet/core/web-api/advanced/custom-formatters?view=aspnetcore-8.0
        class TelegramBotInputFormatter : TextInputFormatter
        {
            public TelegramBotInputFormatter()
            {
                SupportedEncodings.Add(Encoding.UTF8);
                SupportedMediaTypes.Add("application/json");
            }

            protected override bool CanReadType(Type type) => type == typeof(Update);

            public sealed override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
            {
                var model = await JsonSerializer.DeserializeAsync(context.HttpContext.Request.Body, context.ModelType, JsonSerializerOptionsProvider.Options, context.HttpContext.RequestAborted);
                return await InputFormatterResult.SuccessAsync(model);
            }
        }

        class TelegramBotOutputFormatter : TextOutputFormatter
        {
            public TelegramBotOutputFormatter()
            {
                SupportedEncodings.Add(Encoding.UTF8);
                SupportedMediaTypes.Add("application/json");
            }

            protected override bool CanWriteType(Type? type) => typeof(IRequest).IsAssignableFrom(type);

            public sealed override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
            {
                var stream = context.HttpContext.Response.Body;
                await JsonSerializer.SerializeAsync(stream, context.Object, JsonSerializerOptionsProvider.Options, context.HttpContext.RequestAborted);
            }
        }
#pragma warning restore MA0004 // Use Task.ConfigureAwait
#endif
    }
}

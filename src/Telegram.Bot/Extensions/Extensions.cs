using System.Runtime.CompilerServices;
using Telegram.Bot;
#if NET6_0_OR_GREATER
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.WebUtilities;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
#endif

#pragma warning disable IDE0130, MA0003

namespace Telegram.Bot.Extensions
{
    internal static class ObjectExtensions
    {
#if NETCOREAPP3_1_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T ThrowIfNull<T>(this T? value, [CallerArgumentExpression(nameof(value))] string? parameterName = default)
            => value ?? throw new ArgumentNullException(parameterName);
#else
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T ThrowIfNull<T>(this T? value) => value ?? throw new ArgumentNullException(null);
#endif
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
            => services.Configure<TOptions>(options => JsonBotAPI.Configure(opt(options)));

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
        private class TelegramBotInputFormatter : TextInputFormatter
        {
            public TelegramBotInputFormatter()
            {
                SupportedEncodings.Add(Encoding.UTF8);
                SupportedMediaTypes.Add("application/json");
            }

            protected override bool CanReadType(Type type) => type == typeof(Update);

            public sealed override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
            {
                var model = await JsonSerializer.DeserializeAsync(context.HttpContext.Request.Body, context.ModelType, JsonBotAPI.Options, context.HttpContext.RequestAborted);
                return await InputFormatterResult.SuccessAsync(model);
            }
        }

        private class TelegramBotOutputFormatter : TextOutputFormatter
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
                await JsonSerializer.SerializeAsync(stream, context.Object, JsonBotAPI.Options, context.HttpContext.RequestAborted);
            }
        }
#pragma warning restore MA0004 // Use Task.ConfigureAwait
#endif
    }
}

#if NET6_0_OR_GREATER
namespace Telegram.Bot
{
    /// <summary>Provides methods to authenticate data coming from Telegram web requests</summary>
    public static class AuthHelpers
    {
#pragma warning disable MA0002, MA0006
        /// <summary>Used to parse and validate <see href="https://core.telegram.org/bots/webapps#validating-data-received-via-the-mini-app">Telegram.WebApp.initData</see> or <see href="https://core.telegram.org/widgets/login#receiving-authorization-data">LoginWidget requests</see></summary>
        /// <param name="initData">Data in the form of a <see href="https://en.wikipedia.org/wiki/Query_string">query string</see>, passed to your web app</param>
        /// <param name="botToken">The bot token</param>
        /// <param name="loginWidget">true to validate a LoginWidget request, false to validate a WebApp.initData</param>
        /// <returns>On success, data fields in a sorted dictionary (without the security hash)</returns>
        /// <exception cref="SecurityException">Authentication failed</exception>
        public static SortedDictionary<string, string> ParseValidateData(string? initData, string botToken, bool loginWidget = false)
        {
            var query = QueryHelpers.ParseQuery(initData);
            return ParseValidateData(query.ToDictionary(kvp => kvp.Key, kvp => (string?)kvp.Value ?? ""), botToken, loginWidget);
        }

        /// <summary>Used to parse and validate <see href="https://core.telegram.org/bots/webapps#validating-data-received-via-the-mini-app">Telegram.WebApp.initData</see> or <see href="https://core.telegram.org/widgets/login#receiving-authorization-data">LoginWidget requests</see></summary>
        /// <param name="fields">Data fields, possibly obtained from the <see href="https://core.telegram.org/widgets/login#receiving-authorization-data">data-onauth</see> function</param>
        /// <param name="botToken">The bot token</param>
        /// <param name="loginWidget">true to validate a LoginWidget request, false to validate a WebApp.initData</param>
        /// <returns>On success, data fields in a sorted dictionary (without the security hash)</returns>
        /// <exception cref="SecurityException">Authentication failed</exception>
        public static SortedDictionary<string, string> ParseValidateData(IDictionary<string, string> fields, string botToken, bool loginWidget = false)
            => ParseValidateData(new SortedDictionary<string, string>(fields), botToken, loginWidget);

        private static SortedDictionary<string, string> ParseValidateData(SortedDictionary<string, string> fields, string botToken, bool loginWidget = false)
        {
            if (fields.Remove("hash", out var hash))
            {
                var dataCheckString = string.Join('\n', fields.Select(kvp => $"{kvp.Key}={kvp.Value}"));
                var secretKey = loginWidget
                    ? SHA256.HashData(Encoding.ASCII.GetBytes(botToken))
                    : HMACSHA256.HashData(Encoding.ASCII.GetBytes("WebAppData"), Encoding.ASCII.GetBytes(botToken));
                var computedHash = HMACSHA256.HashData(secretKey, Encoding.UTF8.GetBytes(dataCheckString));
                if (computedHash.SequenceEqual(Convert.FromHexString(hash)))
                    return fields;
            }
            throw new SecurityException("Invalid data hash");
        }
    }
#pragma warning restore MA0002 // IEqualityComparer<string> or IComparer<string> is missing
}
#endif

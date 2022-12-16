using System.Globalization;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Telegram.Bot;

/// <summary>
/// This class is used to provide configuration for <see cref="TelegramBotClient"/>
/// </summary>
[PublicAPI]
public class TelegramBotClientOptions
{
    const string BaseTelegramUrl = "https://api.telegram.org";

    /// <summary>
    /// API token
    /// </summary>
    public string Token { get; }

    /// <summary>
    /// Used to change base url to your private bot api server URL. It looks like
    /// http://localhost:8081. Path, query and fragment will be omitted if present.
    /// </summary>
    public string? BaseUrl { get; }

    /// <summary>
    /// Indicates that test environment will be used
    /// </summary>
    public bool UseTestEnvironment { get; }

    /// <summary>
    /// Unique identifier for the bot from bot token. For example, for the bot token
    /// "1234567:4TT8bAc8GHUspu3ERYn-KGcvsvGB9u_n4ddy", the bot id is "1234567".
    /// Token format is not public API so this property is optional and may stop working
    /// in the future if Telegram changes it's token format.
    /// </summary>
    public long? BotId { get; }

    /// <summary>
    /// Indicates that local bot server will be used
    /// </summary>
    public bool LocalBotServer { get; }

    /// <summary>
    /// Contains base url for downloading files
    /// </summary>
    public string BaseFileUrl { get; }

    /// <summary>
    /// Contains base url for making requests
    /// </summary>
    public string BaseRequestUrl { get; }

    /// <summary>
    /// Create a new <see cref="TelegramBotClientOptions"/> instance.
    /// </summary>
    /// <param name="token">API token</param>
    /// <param name="baseUrl">
    /// Used to change base URL to your private Bot API server URL. It looks like
    /// http://localhost:8081. Path, query and fragment will be omitted if present.
    /// </param>
    /// <param name="useTestEnvironment"></param>
    /// <exception cref="ArgumentException">
    /// Thrown if <paramref name="token"/> format is invalid
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown if <paramref name="baseUrl"/> format is invalid
    /// </exception>
    public TelegramBotClientOptions(string token, string? baseUrl = default, bool useTestEnvironment = false)
    {
        Token = token ?? throw new ArgumentNullException(nameof(token));
        BaseUrl = baseUrl;
        UseTestEnvironment = useTestEnvironment;

        BotId = GetIdFromToken(token);

        LocalBotServer = baseUrl is not null;
        var effectiveBaseUrl = LocalBotServer
            ? ExtractBaseUrl(baseUrl)
            : BaseTelegramUrl;

        BaseRequestUrl = useTestEnvironment
            ? $"{effectiveBaseUrl}/bot{token}/test"
            : $"{effectiveBaseUrl}/bot{token}";

        BaseFileUrl = useTestEnvironment
            ? $"{effectiveBaseUrl}/file/bot{token}/test"
            : $"{effectiveBaseUrl}/file/bot{token}";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static long? GetIdFromToken(string token)
        {
#if NET6_0_OR_GREATER
            var span = token.AsSpan();
            var index = span.IndexOf(':');

            if (index is < 1 or > 16) { return null; }

            var botIdSpan = span[..index];
            if (!long.TryParse(botIdSpan, NumberStyles.Integer, CultureInfo.InvariantCulture, out var botId)) { return null; }
#else
            var index = token.IndexOf(value: ':');

            if (index is < 1 or > 16) { return null; }

            var botIdSpan = token.Substring(startIndex: 0, length: index);
            if (!long.TryParse(botIdSpan, NumberStyles.Integer, CultureInfo.InvariantCulture, out var botId)) { return null; }
#endif

            return botId;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static string ExtractBaseUrl(string? baseUrl)
    {
        if (baseUrl is null) { throw new ArgumentNullException(paramName: nameof(baseUrl)); }

        if (!Uri.TryCreate(uriString: baseUrl, uriKind: UriKind.Absolute, out var baseUri)
            || string.IsNullOrEmpty(value: baseUri.Scheme)
            || string.IsNullOrEmpty(value: baseUri.Authority))
        {
            throw new ArgumentException(
                message: """Invalid format. A valid base URL should look like "http://localhost:8081" """,
                paramName: nameof(baseUrl)
            );
        }

        return $"{baseUri.Scheme}://{baseUri.Authority}";
    }
}

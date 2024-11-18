using System.Globalization;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Telegram.Bot;

/// <summary>This class is used to provide configuration for <see cref="TelegramBotClient"/></summary>
[PublicAPI]
public class TelegramBotClientOptions
{
    private const string BaseTelegramUrl = "https://api.telegram.org";

    /// <summary>API token</summary>
    public string Token { get; }

    /// <summary>Used to change base url to your private bot api server URL. It looks like http://localhost:8081. Path, query and fragment will be omitted if present.</summary>
    public string? BaseUrl { get; }

    /// <summary>Indicates that test environment will be used</summary>
    public bool UseTestEnvironment { get; }

    /// <summary>Unique identifier for the bot from bot token, extracted from the first part of the bot token.
    /// Token format is not public API so this property is optional and may stop working in the future if Telegram changes it's token format.</summary>
    public long BotId { get; }

    /// <summary>Indicates that local bot server will be used</summary>
    public bool LocalBotServer { get; }

    /// <summary>Contains base url for downloading files</summary>
    public string BaseFileUrl { get; }

    /// <summary>Contains base url for making requests</summary>
    public string BaseRequestUrl { get; }

    /// <summary>Automatic retry of failed requests "Too Many Requests: retry after X" when X is less or equal to RetryThreshold</summary>
    public int RetryThreshold { get; set; } = 60;

    /// <summary><see cref="RetryThreshold">Automatic retry</see> will be attempted for up to RetryCount requests</summary>
    public int RetryCount { get; set; } = 3;

    /// <summary>Create a new <see cref="TelegramBotClientOptions"/> instance.</summary>
    /// <param name="token">API token</param>
    /// <param name="baseUrl">Used to change base URL to your private Bot API server URL. It looks like http://localhost:8081. Path, query and fragment will be omitted if present.</param>
    /// <param name="useTestEnvironment">Indicates that test environment will be used</param>
    /// <exception cref="ArgumentException">Thrown if <paramref name="token"/> or <paramref name="baseUrl"/> format is invalid</exception>
    public TelegramBotClientOptions(string token, string? baseUrl = default, bool useTestEnvironment = false)
    {
        Token = token ?? throw new ArgumentNullException(nameof(token));
        BaseUrl = baseUrl;
        UseTestEnvironment = useTestEnvironment;

        int index = token.IndexOf(':');
        if (index is < 1 or > 16 || !long.TryParse(token[..index], NumberStyles.Integer, CultureInfo.InvariantCulture, out var botId))
            throw new ArgumentException("Bot token invalid", nameof(token));
        BotId = botId;

        LocalBotServer = baseUrl is not null;
        var effectiveBaseUrl = LocalBotServer ? ExtractBaseUrl(baseUrl) : BaseTelegramUrl;
        BaseRequestUrl = useTestEnvironment ? $"{effectiveBaseUrl}/bot{token}/test" : $"{effectiveBaseUrl}/bot{token}";
        BaseFileUrl = useTestEnvironment ? $"{effectiveBaseUrl}/file/bot{token}/test" : $"{effectiveBaseUrl}/file/bot{token}";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string ExtractBaseUrl(string? baseUrl)
    {
        if (!Uri.TryCreate(baseUrl, UriKind.Absolute, out var baseUri)
            || string.IsNullOrEmpty(baseUri.Scheme) || string.IsNullOrEmpty(baseUri.Authority))
            throw new ArgumentException("Invalid format. A valid base URL should look like \"http://localhost:8081\"", nameof(baseUrl));
        return $"{baseUri.Scheme}://{baseUri.Authority}";
    }
}

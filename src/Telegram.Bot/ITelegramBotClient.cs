using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Telegram.Bot.Args;
using Telegram.Bot.Exceptions;

namespace Telegram.Bot;

#pragma warning disable CS1591
public delegate ValueTask AsyncEventHandler<in TArgs>(ITelegramBotClient botClient, TArgs args, CancellationToken cancellationToken = default);
#pragma warning restore CS1591

/// <summary>A client interface to use the Telegram Bot API</summary>
[PublicAPI]
public interface ITelegramBotClient
{
    /// <summary><see langword="true"/> when the bot is using local Bot API server</summary>
    bool LocalBotServer { get; }

    /// <summary>API token</summary>
    string Token { get; }

    /// <summary>Unique identifier for the bot from bot token, extracted from the first part of the bot token.
    /// Token format is not public API so this property is optional and may stop working in the future if Telegram changes it's token format.</summary>
    long BotId { get; }

    /// <summary>Timeout for requests</summary>
    TimeSpan Timeout { get; set; }

    /// <summary>Instance of <see cref="IExceptionParser"/> to parse errors from Bot API into <see cref="ApiRequestException"/></summary>
    /// <remarks>This property is not thread safe</remarks>
    IExceptionParser ExceptionsParser { get; set; }

    /// <summary>Occurs before sending a request to API</summary>
    event AsyncEventHandler<ApiRequestEventArgs>? OnMakingApiRequest;

    /// <summary>Occurs after receiving the response to an API request</summary>
    event AsyncEventHandler<ApiResponseEventArgs>? OnApiResponseReceived;

    /// <summary>Send a request to Bot API</summary>
    /// <typeparam name="TResponse">Type of expected result in the response object</typeparam>
    /// <param name="request">API request object</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Result of the API request</returns>
    Task<TResponse> SendRequest<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);

    /// <summary>Method MakeRequest was renamed as <see cref="SendRequest">SendRequest</see></summary>
    [Obsolete("Method MakeRequest was renamed as SendRequest")]
    Task<TResponse> MakeRequest<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);

    /// <summary>Method MakeRequestAsync was renamed as <see cref="SendRequest">SendRequest</see></summary>
    [Obsolete("Method MakeRequestAsync was renamed as SendRequest")]
    Task<TResponse> MakeRequestAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);

    /// <summary>Test the API token</summary>
    /// <param name="cancellationToken"></param>
    /// <returns><see langword="true"/> if token is valid</returns>
    Task<bool> TestApi(CancellationToken cancellationToken = default);

    /// <summary>Use this method to download a file. Get <paramref name="filePath"/> by calling
    /// <see cref="TelegramBotClientExtensions.GetFile(ITelegramBotClient, string, CancellationToken)"/></summary>
    /// <param name="filePath">Path to file on server</param>
    /// <param name="destination">Destination stream to write file to</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <exception cref="ArgumentException">filePath is <see langword="null"/>, empty or too short</exception>
    /// <exception cref="ArgumentNullException"><paramref name="destination"/> is <see langword="null"/></exception>
    Task DownloadFile(string filePath, Stream destination, CancellationToken cancellationToken = default);
}

public static partial class TelegramBotClientExtensions
{
    /// <summary>Use this method to get basic info about a file download it. For the moment, bots can download filesof up to 20MB in size.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="fileId">File identifier to get info about</param>
    /// <param name="destination">Destination stream to write file to</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>On success, a <see cref="File"/> object is returned.</returns>
    public static async Task<File> GetInfoAndDownloadFile(this ITelegramBotClient botClient, string fileId, Stream destination,
        CancellationToken cancellationToken = default)
    {
        var file = await botClient.ThrowIfNull().SendRequest(new Requests.GetFileRequest { FileId = fileId },
            cancellationToken).ConfigureAwait(false);
        await botClient.DownloadFile(filePath: file.FilePath!, destination, cancellationToken).ConfigureAwait(false);
        return file;
    }
}

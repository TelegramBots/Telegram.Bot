using System.IO;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Telegram.Bot.Args;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Requests.Abstractions;

namespace Telegram.Bot;

/// <summary>
/// A client interface to use the Telegram Bot API
/// </summary>
[PublicAPI]
public interface ITelegramBotClient
{
    /// <summary>
    ///
    /// </summary>
    bool LocalBotServer { get; }

    /// <summary>
    /// Unique identifier for the bot from bot token. For example, for the bot token
    /// "1234567:4TT8bAc8GHUspu3ERYn-KGcvsvGB9u_n4ddy", the bot id is "1234567".
    /// Token format is not public API so this property is optional and may stop working
    /// in the future if Telegram changes it's token format.
    /// </summary>
    long? BotId { get; }

    /// <summary>
    /// Timeout for requests
    /// </summary>
    TimeSpan Timeout { get; set; }

    /// <summary>
    /// Instance of <see cref="IExceptionParser"/> to parse errors from Bot API into
    /// <see cref="ApiRequestException"/>
    /// </summary>
    /// <remarks>This property is not thread safe</remarks>
    IExceptionParser ExceptionsParser { get; set; }

    /// <summary>
    /// Occurs before sending a request to API
    /// </summary>
    event AsyncEventHandler<ApiRequestEventArgs>? OnMakingApiRequest;

    /// <summary>
    /// Occurs after receiving the response to an API request
    /// </summary>
    event AsyncEventHandler<ApiResponseEventArgs>? OnApiResponseReceived;

    /// <summary>
    /// Send a request to Bot API
    /// </summary>
    /// <typeparam name="TResponse">Type of expected result in the response object</typeparam>
    /// <param name="request">API request object</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Result of the API request</returns>
    Task<TResponse> MakeRequestAsync<TResponse>(
        IRequest<TResponse> request,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Test the API token
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns><see langword="true"/> if token is valid</returns>
    Task<bool> TestApiAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Use this method to download a file. Get <paramref name="filePath"/> by calling
    /// <see cref="TelegramBotClientExtensions.GetFileAsync(ITelegramBotClient, string, CancellationToken)"/>
    /// </summary>
    /// <param name="filePath">Path to file on server</param>
    /// <param name="destination">Destination stream to write file to</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation.
    /// </param>
    /// <exception cref="ArgumentException">filePath is <c>null</c>, empty or too short</exception>
    /// <exception cref="ArgumentNullException"><paramref name="destination"/> is <c>null</c></exception>
    Task DownloadFileAsync(
        string filePath,
        Stream destination,
        CancellationToken cancellationToken = default
    );
}

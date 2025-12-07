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

    /// <summary>Test the API token</summary>
    /// <param name="cancellationToken"></param>
    /// <returns><see langword="true"/> if token is valid</returns>
    Task<bool> TestApi(CancellationToken cancellationToken = default);

    /// <summary>Use this method to download a file after calling <see cref="TelegramBotClientExtensions.GetFile">GetFile</see></summary>
    /// <param name="filePath">Path to file on Telegram</param>
    /// <param name="destination">Destination stream to write file to</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <exception cref="ArgumentException">filePath is <see langword="null"/>, empty or too short</exception>
    /// <exception cref="ArgumentNullException"><paramref name="destination"/> is <see langword="null"/></exception>
    Task DownloadFile(string filePath, Stream destination, CancellationToken cancellationToken = default);

    /// <summary>Use this method to download a file after calling <see cref="TelegramBotClientExtensions.GetFile">GetFile</see></summary>
    /// <param name="file">File on Telegram</param>
    /// <param name="destination">Destination stream to write file to</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <exception cref="ArgumentException">filePath is <see langword="null"/>, empty or too short</exception>
    /// <exception cref="ArgumentNullException"><paramref name="destination"/> is <see langword="null"/></exception>
    Task DownloadFile(TGFile file, Stream destination, CancellationToken cancellationToken = default);
}

public static partial class TelegramBotClientExtensions
{
    /// <summary>Use this method to get basic info about a file and download it. For the moment, bots can download filesof up to 20MB in size.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="fileId">File identifier to get info about</param>
    /// <param name="destination">Destination stream to write file to</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>On success, a <see cref="TGFile"/> object is returned.</returns>
    public static async Task<TGFile> GetInfoAndDownloadFile(this ITelegramBotClient botClient, string fileId, Stream destination,
        CancellationToken cancellationToken = default)
    {
        var file = await botClient.ThrowIfNull().SendRequest(new Requests.GetFileRequest { FileId = fileId },
            cancellationToken).ConfigureAwait(false);
        await botClient.DownloadFile(filePath: file.FilePath!, destination, cancellationToken).ConfigureAwait(false);
        return file;
    }

    /// <summary>Downloads an encrypted Passport file, decrypts it, and writes the content to <paramref name="destination"/> stream</summary>
    /// <param name="botClient">Instance of bot client</param>
    /// <param name="passportFile"></param>
    /// <param name="fileCredentials"></param>
    /// <param name="destination"></param>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <returns>File information of the encrypted Passport file on Telegram servers.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static async Task<TGFile> DownloadAndDecryptPassportFileAsync(this ITelegramBotClient botClient, PassportFile passportFile,
        FileCredentials fileCredentials, Stream destination, CancellationToken cancellationToken = default)
    {
        if (passportFile == null) throw new ArgumentNullException(nameof(passportFile));
        if (fileCredentials == null) throw new ArgumentNullException(nameof(fileCredentials));
        if (destination == null) throw new ArgumentNullException(nameof(destination));
        using var encryptedContentStream = passportFile.FileSize > 0 ? new MemoryStream((int)passportFile.FileSize) : new MemoryStream();
        var fileInfo = await botClient.ThrowIfNull().GetInfoAndDownloadFile(passportFile.FileId, encryptedContentStream, cancellationToken).ConfigureAwait(false);
        encryptedContentStream.Position = 0;
        await new Passport.Decrypter().DecryptFileAsync(encryptedContentStream, fileCredentials, destination, cancellationToken).ConfigureAwait(false);
        return fileInfo;
    }
}

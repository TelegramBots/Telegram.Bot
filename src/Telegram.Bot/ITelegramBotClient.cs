using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;
using File = Telegram.Bot.Types.File;

namespace Telegram.Bot
{
    /// <summary>
    /// A client interface to use the Telegram Bot API
    /// </summary>
    public interface ITelegramBotClient
    {
        /// <summary>
        /// Unique identifier for the bot from bot token. For example, for the bot token
        /// "1234567:4TT8bAc8GHUspu3ERYn-KGcvsvGB9u_n4ddy", the bot id is "1234567".
        /// </summary>
        int BotId { get; }

        #region Config Properties

        /// <summary>
        /// Timeout for requests
        /// </summary>
        TimeSpan Timeout { get; set; }

        #endregion  Config Properties

        #region Events

        /// <summary>
        /// Occurs before sending a request to API
        /// </summary>
        event EventHandler<ApiRequestEventArgs> MakingApiRequest;

        /// <summary>
        /// Occurs after receiving the response to an API request
        /// </summary>
        event EventHandler<ApiResponseEventArgs> ApiResponseReceived;

        #endregion Events

        #region Helpers

        /// <summary>
        /// Send a request to Bot API
        /// </summary>
        /// <typeparam name="TResult">Type of expected result in the response object</typeparam>
        /// <param name="request">API request object</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of the API request</returns>
        /// <exception cref="ApiRequestException"></exception>
        Task<TResult> MakeRequestAsync<TResult>(
            IRequest<TResult> request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Send a request to Bot API
        /// </summary>
        /// <typeparam name="TResult">Type of expected result in the response object</typeparam>
        /// <param name="request">API request object</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of the API request</returns>
        /// <returns></returns>
        Task<ApiResponse<TResult>?> SendRequestAsync<TResult>(
            IRequest<TResult> request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Test the API token
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns><c>true</c> if token is valid</returns>
        Task<bool> TestApiAsync(CancellationToken cancellationToken = default);

        #endregion Helpers

        /// <summary>
        /// Use this method to download a file. Get <paramref name="filePath"/> by sending
        /// <see cref="Telegram.Bot.Requests.GetFileRequest"/>
        /// </summary>
        /// <param name="filePath">Path to file on server</param>
        /// <param name="destination">Destination stream to write file to</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        Task DownloadFileAsync(
            string filePath,
            Stream destination,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Use this method to get basic info about a file and download it.
        /// </summary>
        /// <param name="fileId">File identifier to get info about</param>
        /// <param name="destination">Destination stream to write file to</param>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used by other objects or threads to receive notice
        /// of cancellation.
        /// </param>
        /// <returns>File info</returns>
        Task<File> GetInfoAndDownloadFileAsync(
            string fileId,
            Stream destination,
            CancellationToken cancellationToken = default);
    }
}

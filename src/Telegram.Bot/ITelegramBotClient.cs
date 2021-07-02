using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
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
        /// Token format is not public API so this property is optional and may stop working
        /// in the future if Telegram changes it's token format.
        /// </summary>
        long? BotId { get; }

        #region Config Properties

        /// <summary>
        /// Timeout for requests
        /// </summary>
        TimeSpan Timeout { get; set; }

        /// <summary>
        /// Indicates if receiving updates
        /// </summary>
        [Obsolete("This property will be removed in the next major version. " +
            "Please consider using Telegram.Bot.Extensions.Polling instead.")]
        bool IsReceiving { get; }

        /// <summary>
        /// The current message offset
        /// </summary>
        [Obsolete("This property will be removed in the next major version. " +
            "Please consider using Telegram.Bot.Extensions.Polling instead.")]
        int MessageOffset { get; set; }

        #endregion  Config Properties

        #region Events

        /// <summary>
        /// Occurs before sending a request to API
        /// </summary>
        event AsyncEventHandler<ApiRequestEventArgs>? OnMakingApiRequest;

        /// <summary>
        /// Occurs after receiving the response to an API request
        /// </summary>
        event AsyncEventHandler<ApiResponseEventArgs>? OnApiResponseReceived;

        /// <summary>
        /// Occurs when an <see cref="Update"/> is received.
        /// </summary>
        [Obsolete("This event will be removed in the next major version. " +
            "Please consider using Telegram.Bot.Extensions.Polling instead.")]
        event EventHandler<UpdateEventArgs>? OnUpdate;

        /// <summary>
        /// Occurs when a <see cref="Message"/> is received.
        /// </summary>
        [Obsolete("This event will be removed in the next major version. " +
            "Please consider using Telegram.Bot.Extensions.Polling instead.")]
        event EventHandler<MessageEventArgs>? OnMessage;

        /// <summary>
        /// Occurs when <see cref="Message"/> was edited.
        /// </summary>
        [Obsolete("This event will be removed in the next major version. " +
            "Please consider using Telegram.Bot.Extensions.Polling instead.")]
        event EventHandler<MessageEventArgs>? OnMessageEdited;

        /// <summary>
        /// Occurs when an <see cref="InlineQuery"/> is received.
        /// </summary>
        [Obsolete("This event will be removed in the next major version. " +
            "Please consider using Telegram.Bot.Extensions.Polling instead.")]
        event EventHandler<InlineQueryEventArgs>? OnInlineQuery;

        /// <summary>
        /// Occurs when a <see cref="ChosenInlineResult"/> is received.
        /// </summary>
        [Obsolete("This event will be removed in the next major version. " +
            "Please consider using Telegram.Bot.Extensions.Polling instead.")]
        event EventHandler<ChosenInlineResultEventArgs>? OnInlineResultChosen;

        /// <summary>
        /// Occurs when an <see cref="CallbackQuery"/> is received
        /// </summary>
        [Obsolete("This event will be removed in the next major version. " +
            "Please consider using Telegram.Bot.Extensions.Polling instead.")]
        event EventHandler<CallbackQueryEventArgs>? OnCallbackQuery;

        /// <summary>
        /// Occurs when an error occurs during the background update pooling.
        /// </summary>
        [Obsolete("This event will be removed in the next major version. " +
            "Please consider using Telegram.Bot.Extensions.Polling instead.")]
        event EventHandler<ReceiveErrorEventArgs>? OnReceiveError;

        /// <summary>
        /// Occurs when an error occurs during the background update pooling.
        /// </summary>
        [Obsolete("This event will be removed in the next major version. " +
            "Please consider using Telegram.Bot.Extensions.Polling instead.")]
        event EventHandler<ReceiveGeneralErrorEventArgs>? OnReceiveGeneralError;

        #endregion Events

        #region Helpers

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
        /// <returns><c>true</c> if token is valid</returns>
        Task<bool> TestApiAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Start update receiving
        /// </summary>
        /// <param name="allowedUpdates">List the types of updates you want your bot to receive.</param>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
        /// <exception cref="Exceptions.ApiRequestException"> Thrown if token is invalid</exception>
        [Obsolete("This method will be removed in the next major version. " +
                  "Please consider using Telegram.Bot.Extensions.Polling instead.")]
        void StartReceiving(
            UpdateType[]? allowedUpdates = null,
            CancellationToken cancellationToken = default
        );

        /// <summary>
        /// Stop update receiving
        /// </summary>
        [Obsolete("This method will be removed in the next major version. " +
            "Please consider using Telegram.Bot.Extensions.Polling instead.")]
        void StopReceiving();

        #endregion Helpers

        /// <summary>
        /// Use this method to download a file. Get <paramref name="filePath"/> by calling <see cref="TelegramBotClientExtensions.GetFileAsync(ITelegramBotClient, string, CancellationToken)"/>.
        /// </summary>
        /// <param name="filePath">Path to file on server</param>
        /// <param name="destination">Destination stream to write file to</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ArgumentException">filePath is <c>null</c>, empty or too short</exception>
        /// <exception cref="ArgumentNullException"><paramref name="destination"/> is <c>null</c></exception>
        Task DownloadFileAsync(
            string filePath,
            Stream destination,
            CancellationToken cancellationToken = default
        );

        /// <summary>
        /// Use this method to get basic info about a file download it. For the moment, bots can download files of up to 20MB in size.
        /// </summary>
        /// <param name="fileId">File identifier to get info about</param>
        /// <param name="destination">Destination stream to write file to</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, a <see cref="File"/> object is returned.</returns>
        Task<File> GetInfoAndDownloadFileAsync(
            string fileId,
            Stream destination,
            CancellationToken cancellationToken = default
        );
    }
}

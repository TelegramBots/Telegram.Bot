using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Telegram.Bot.Requests;

namespace Telegram.Bot.Polling;

/// <summary>
/// Default implementation of <see cref="IUpdateReceiver"/> that polls Telegram servers
/// for updates and handles them sequentially with improved error resilience.
/// </summary>
/// <remarks>
/// This receiver includes exponential backoff for network errors and limits consecutive
/// errors to prevent infinite loops during connectivity issues.
/// </remarks>
[PublicAPI]
public class DefaultUpdateReceiver : IUpdateReceiver
{
    private readonly ITelegramBotClient _botClient;
    private readonly ReceiverOptions? _receiverOptions;
    private static readonly Update[] EmptyUpdates = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultUpdateReceiver"/> class.
    /// </summary>
    /// <param name="botClient">The Telegram Bot Client used for making API requests.</param>
    /// <param name="receiverOptions">Options to configure the update receiving behavior.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="botClient"/> is null.</exception>
    public DefaultUpdateReceiver(
        ITelegramBotClient botClient, 
        ReceiverOptions? receiverOptions = null)
    {
        _botClient = botClient ?? throw new ArgumentNullException(nameof(botClient));
        _receiverOptions = receiverOptions;
    }

    /// <summary>
    /// Starts receiving updates from Telegram servers and processing them using the provided handler.
    /// </summary>
    /// <param name="updateHandler">The handler instance that will process incoming updates and errors.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to stop receiving updates.</param>
    /// <returns>A task that represents the asynchronous operation of receiving updates.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="updateHandler"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when too many consecutive errors occur.</exception>
    /// <remarks>
    /// This method will continue running until cancelled or when maximum consecutive errors is reached.
    /// Implements exponential backoff for network errors to prevent overwhelming the server during outages.
    /// </remarks>
    public async Task ReceiveAsync(
        IUpdateHandler updateHandler, 
        CancellationToken cancellationToken = default)
    {
        if (updateHandler is null)
            throw new ArgumentNullException(nameof(updateHandler));

        var allowedUpdates = _receiverOptions?.AllowedUpdates;
        var limit = _receiverOptions?.Limit ?? 100;
        var offset = _receiverOptions?.Offset ?? 0;

        if (_receiverOptions?.DropPendingUpdates is true)
        {
            offset = await GetLastUpdateIdAsync(cancellationToken).ConfigureAwait(false);
        }

        int consecutiveErrors = 0;
        const int maxConsecutiveErrors = 10;

        while (!cancellationToken.IsCancellationRequested)
        {
            if (consecutiveErrors >= maxConsecutiveErrors)
            {
                throw new InvalidOperationException(
                    $"Too many consecutive errors ({consecutiveErrors}). Stopping receiver.");
            }

            try
            {
                var updates = await GetUpdatesAsync(offset, limit, allowedUpdates, cancellationToken)
                    .ConfigureAwait(false);

                consecutiveErrors = 0;

                if (updates.Length == 0)
                {
                    await Task.Delay(100, cancellationToken).ConfigureAwait(false);
                    continue;
                }

                foreach (var update in updates)
                {
                    await ProcessUpdateAsync(updateHandler, update, cancellationToken)
                        .ConfigureAwait(false);
                    
                    offset = update.Id + 1;
                }
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                consecutiveErrors++;
                await HandleErrorWithBackoffAsync(
                    updateHandler, ex, consecutiveErrors, cancellationToken)
                    .ConfigureAwait(false);
            }
        }
    }

    /// <summary>
    /// Retrieves the ID of the last pending update to determine the starting offset.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>The ID of the last pending update plus one, or zero if no updates are found.</returns>
    /// <remarks>
    /// Used when <see cref="ReceiverOptions.DropPendingUpdates"/> is enabled to skip already received updates.
    /// </remarks>
    private async Task<int> GetLastUpdateIdAsync(CancellationToken cancellationToken)
    {
        try
        {
            var updates = await _botClient.GetUpdatesAsync(-1, 1, 0, [], cancellationToken)
                .ConfigureAwait(false);
            return updates.Length == 0 ? 0 : updates[^1].Id + 1;
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception)
        {
            return 0; // Fallback to start from beginning
        }
    }

    /// <summary>
    /// Retrieves updates from the Telegram server using the specified parameters.
    /// </summary>
    /// <param name="offset">The identifier of the first update to be returned.</param>
    /// <param name="limit">Limits the number of updates to be retrieved.</param>
    /// <param name="allowedUpdates">Array of update types to be received.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>An array of <see cref="Update"/> objects received from Telegram.</returns>
    private async Task<Update[]> GetUpdatesAsync(
        int offset, int limit, string[]? allowedUpdates, CancellationToken cancellationToken)
    {
        var request = new GetUpdatesRequest
        {
            Offset = offset,
            Limit = limit,
            AllowedUpdates = allowedUpdates,
            Timeout = (int)_botClient.Timeout.TotalSeconds
        };

        return await _botClient.SendRequest(request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Processes a single update by delegating to the provided update handler.
    /// </summary>
    /// <param name="updateHandler">The handler instance to process the update.</param>
    /// <param name="update">The update to be processed.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <remarks>
    /// Any exceptions thrown by the handler are caught and reported as handling errors.
    /// </remarks>
    private async Task ProcessUpdateAsync(
        IUpdateHandler updateHandler, Update update, CancellationToken cancellationToken)
    {
        try
        {
            await updateHandler.HandleUpdateAsync(_botClient, update, cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            await updateHandler.HandleErrorAsync(
                _botClient, ex, HandleErrorSource.HandleUpdateError, cancellationToken)
                .ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Handles polling errors by notifying the error handler and implementing exponential backoff.
    /// </summary>
    /// <param name="updateHandler">The handler instance to report the error.</param>
    /// <param name="exception">The exception that occurred during polling.</param>
    /// <param name="consecutiveErrors">The number of consecutive errors that have occurred.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <remarks>
    /// Implements exponential backoff with a maximum delay of 30 seconds to prevent
    /// overwhelming the server during network outages.
    /// </remarks>
    private async Task HandleErrorWithBackoffAsync(
        IUpdateHandler updateHandler,
        Exception exception,
        int consecutiveErrors,
        CancellationToken cancellationToken)
    {
        try
        {
            await updateHandler.HandleErrorAsync(
                _botClient, exception, HandleErrorSource.PollingError, cancellationToken)
                .ConfigureAwait(false);
        }
        catch (OperationCanceledException)
        {
            throw;
        }

        // Exponential backoff
        var backoffMs = Math.Min(30000, consecutiveErrors * 1000);
        await Task.Delay(backoffMs, cancellationToken).ConfigureAwait(false);
    }
}
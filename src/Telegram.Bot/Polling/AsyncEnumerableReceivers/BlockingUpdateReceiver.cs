#if NET6_0_OR_GREATER
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Telegram.Bot.Requests;
using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Polling;

/// <summary>
/// Supports asynchronous iteration over <see cref="Update"/>s
/// </summary>
[PublicAPI]
public class BlockingUpdateReceiver : IAsyncEnumerable<Update>
{
    readonly ReceiverOptions? _receiverOptions;
    readonly ITelegramBotClient _botClient;
    readonly Func<Exception, CancellationToken, Task>? _pollingErrorHandler;

    int _inProcess;

    /// <summary>
    /// Constructs a new <see cref="BlockingUpdateReceiver"/> for the specified <see cref="ITelegramBotClient"/>
    /// </summary>
    /// <param name="botClient">The <see cref="ITelegramBotClient"/> used for making GetUpdates calls</param>
    /// <param name="receiverOptions"></param>
    /// <param name="pollingErrorHandler">
    /// The function used to handle <see cref="Exception"/>s thrown by ReceiveUpdates
    /// </param>
    public BlockingUpdateReceiver(
        ITelegramBotClient botClient,
        ReceiverOptions? receiverOptions = default,
        Func<Exception, CancellationToken, Task>? pollingErrorHandler = default)
    {
        _botClient = botClient ?? throw new ArgumentNullException(nameof(botClient));
        _receiverOptions = receiverOptions;
        _pollingErrorHandler = pollingErrorHandler;
    }

    /// <summary>
    /// Gets the <see cref="IAsyncEnumerator{Update}"/>. This method may only be called once.
    /// </summary>
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken"/> with which you can stop receiving
    /// </param>
    public IAsyncEnumerator<Update> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        if (Interlocked.CompareExchange(ref _inProcess, 1, 0) is 1)
        {
            throw new InvalidOperationException(nameof(GetAsyncEnumerator) + " may only be called once");
        }

        return new Enumerator(receiver: this, cancellationToken: cancellationToken);
    }

    class Enumerator : IAsyncEnumerator<Update>
    {
        readonly BlockingUpdateReceiver _receiver;
        readonly CancellationTokenSource _cts;
        readonly CancellationToken _token;
        readonly UpdateType[]? _allowedUpdates;
        readonly int? _limit;

        Update[] _updateArray = Array.Empty<Update>();
        int _updateIndex;
        int _messageOffset;
        bool _updatesThrown;

        public Enumerator(BlockingUpdateReceiver receiver, CancellationToken cancellationToken)
        {
            _receiver = receiver;
            _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, default);
            _token = _cts.Token;
            _messageOffset = receiver._receiverOptions?.Offset ?? 0;
            _limit = receiver._receiverOptions?.Limit ?? default;
            _allowedUpdates = receiver._receiverOptions?.AllowedUpdates;
        }

        public ValueTask<bool> MoveNextAsync()
        {
            _token.ThrowIfCancellationRequested();

            _updateIndex += 1;

            return _updateIndex < _updateArray.Length
                ? new(true)
                : new(ReceiveUpdatesAsync());
        }

        async Task<bool> ReceiveUpdatesAsync()
        {
            var shouldThrowPendingUpdates = (
                _updatesThrown,
                _receiver._receiverOptions?.ThrowPendingUpdates ?? false
            );

            if (shouldThrowPendingUpdates is (false, true))
            {
                try
                {
                    _messageOffset = await _receiver._botClient.ThrowOutPendingUpdatesAsync(
                        cancellationToken: _token
                    ).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    // ignored
                }
                finally
                {
                    _updatesThrown = true;
                }
            }

            _updateArray = Array.Empty<Update>();
            _updateIndex = 0;

            while (_updateArray.Length is 0)
            {
                try
                {
                    _updateArray = await _receiver._botClient
                        .MakeRequestAsync(
                            request: new GetUpdatesRequest
                            {
                                Offset = _messageOffset,
                                Timeout = (int) _receiver._botClient.Timeout.TotalSeconds,
                                Limit = _limit,
                                AllowedUpdates = _allowedUpdates,
                            },
                            cancellationToken: _token
                        )
                        .ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception ex) when (_receiver._pollingErrorHandler is not null)
                {
                    await _receiver._pollingErrorHandler(ex, _token).ConfigureAwait(false);
                }
            }

            _messageOffset = _updateArray[^1].Id + 1;
            return true;
        }

        public Update Current => _updateArray[_updateIndex];

        public ValueTask DisposeAsync()
        {
            _cts.Cancel();
            _cts.Dispose();
            return new();
        }
    }
}
#endif

#if NET6_0_OR_GREATER
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Telegram.Bot.Requests;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Polling;

/// <summary>Supports asynchronous iteration over <see cref="Update"/>s.</summary>
/// <remarks>Constructs a new <see cref="BlockingUpdateReceiver"/> with the specified <see cref="ITelegramBotClient"/></remarks>
/// <param name="botClient">The <see cref="ITelegramBotClient"/> used for making GetUpdates calls</param>
/// <param name="receiverOptions"></param>
/// <param name="pollingErrorHandler">The function used to handle <see cref="Exception"/>s thrown by GetUpdates requests</param>
[PublicAPI]
public class BlockingUpdateReceiver(ITelegramBotClient botClient, ReceiverOptions? receiverOptions = default,
    Func<Exception, CancellationToken, Task>? pollingErrorHandler = default) : IAsyncEnumerable<Update>
{
    private readonly ITelegramBotClient _botClient = botClient ?? throw new ArgumentNullException(nameof(botClient));
    private readonly ReceiverOptions? _receiverOptions = receiverOptions;
    private readonly Func<Exception, CancellationToken, Task>? _pollingErrorHandler = pollingErrorHandler;
    private int _inProcess;

    /// <summary>Gets the <see cref="IAsyncEnumerator{Update}"/>. This method may only be called once.</summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> with which you can stop receiving</param>
    public IAsyncEnumerator<Update> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        if (Interlocked.CompareExchange(ref _inProcess, 1, 0) is 1)
            throw new InvalidOperationException(nameof(GetAsyncEnumerator) + " may only be called once");
        return new Enumerator(receiver: this, cancellationToken: cancellationToken);
    }

    private class Enumerator : IAsyncEnumerator<Update>
    {
        private readonly BlockingUpdateReceiver _receiver;
        private readonly CancellationTokenSource _cts;
        private readonly CancellationToken _token;
        private readonly UpdateType[]? _allowedUpdates;
        private readonly int? _limit;
        private Update[] _updateArray = [];
        private int _updateIndex;
        private int _messageOffset;
        private bool _updatesThrown;

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
            return _updateIndex < _updateArray.Length ? new(result: true) : new(ReceiveUpdatesAsync());
        }

        private async Task<bool> ReceiveUpdatesAsync()
        {
            var shouldDropPendingUpdates = (_updatesThrown, _receiver._receiverOptions?.DropPendingUpdates ?? false);
            if (shouldDropPendingUpdates is (false, true))
            {
                try
                {
                    var updates = await _receiver._botClient.GetUpdates(-1, 1, 0, [], _token).ConfigureAwait(false);
                    _messageOffset = updates.Length == 0 ? 0 : updates[^1].Id + 1;
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

            _updateArray = [];
            _updateIndex = 0;

            while (_updateArray.Length is 0)
            {
                try
                {
                    _updateArray = await _receiver._botClient.SendRequest(new GetUpdatesRequest
                    {
                        Offset = _messageOffset,
                        Limit = _limit,
                        Timeout = (int)_receiver._botClient.Timeout.TotalSeconds,
                        AllowedUpdates = _allowedUpdates,
                    }, cancellationToken: _token).ConfigureAwait(false);
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

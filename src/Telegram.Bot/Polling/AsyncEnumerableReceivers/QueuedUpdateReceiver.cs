#if NET6_0_OR_GREATER
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Telegram.Bot.Requests;
using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Polling;

/// <summary>
/// Supports asynchronous iteration over <see cref="Update"/>s.
/// <para>Updates are received on a different thread and enqueued.</para>
/// </summary>
[PublicAPI]
#pragma warning disable CA1001
public class QueuedUpdateReceiver : IAsyncEnumerable<Update>
#pragma warning restore CA1001
{
    readonly ITelegramBotClient _botClient;
    readonly ReceiverOptions? _receiverOptions;
    readonly Func<Exception, CancellationToken, Task>? _pollingErrorHandler;

    int _inProcess;
    Enumerator? _enumerator;

    /// <summary>
    /// Constructs a new <see cref="QueuedUpdateReceiver"/> for the specified <see cref="ITelegramBotClient"/>
    /// </summary>
    /// <param name="botClient">The <see cref="ITelegramBotClient"/> used for making GetUpdates calls</param>
    /// <param name="receiverOptions"></param>
    /// <param name="pollingErrorHandler">
    /// The function used to handle <see cref="Exception"/>s thrown by GetUpdates requests
    /// </param>
    public QueuedUpdateReceiver(
        ITelegramBotClient botClient,
        ReceiverOptions? receiverOptions = default,
        Func<Exception, CancellationToken, Task>? pollingErrorHandler = default)
    {
        _botClient = botClient ?? throw new ArgumentNullException(nameof(botClient));
        _receiverOptions = receiverOptions;
        _pollingErrorHandler = pollingErrorHandler;
    }

    /// <summary>
    /// Indicates how many <see cref="Update"/>s are ready to be returned the enumerator
    /// </summary>
    public int PendingUpdates => _enumerator?.PendingUpdates ?? 0;

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

        _enumerator = new(receiver: this, cancellationToken: cancellationToken);

        return _enumerator;
    }

    class Enumerator : IAsyncEnumerator<Update>
    {
        readonly QueuedUpdateReceiver _receiver;
        readonly CancellationTokenSource _cts;
        readonly CancellationToken _token;
        readonly UpdateType[]? _allowedUpdates;
        readonly int? _limit;

        Exception? _uncaughtException;

        readonly Channel<Update> _channel;
        Update? _current;

        int _pendingUpdates;
        int _messageOffset;

        public int PendingUpdates => _pendingUpdates;

        public Enumerator(QueuedUpdateReceiver receiver, CancellationToken cancellationToken)
        {
            _receiver = receiver;
            _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, default);
            _token = _cts.Token;
            _messageOffset = receiver._receiverOptions?.Offset ?? 0;
            _limit = receiver._receiverOptions?.Limit ?? default;
            _allowedUpdates = receiver._receiverOptions?.AllowedUpdates;

            _channel = Channel.CreateUnbounded<Update>(
                new()
                {
                    SingleReader = true,
                    SingleWriter = true
                }
            );

#pragma warning disable CA2016
            Task.Run(ReceiveUpdatesAsync);
#pragma warning restore CA2016
        }

        public ValueTask<bool> MoveNextAsync()
        {
            if (_uncaughtException is not null) { throw _uncaughtException; }

            _token.ThrowIfCancellationRequested();

            if (_channel.Reader.TryRead(out _current))
            {
                Interlocked.Decrement(ref _pendingUpdates);
                return new(true);
            }

            return new(ReadAsync());
        }

        async Task<bool> ReadAsync()
        {
            _current = await _channel.Reader.ReadAsync(_token).ConfigureAwait(false);
            Interlocked.Decrement(ref _pendingUpdates);
            return true;
        }

        async Task ReceiveUpdatesAsync()
        {
            if (_receiver._receiverOptions?.ThrowPendingUpdates is true)
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
            }

            while (!_cts.IsCancellationRequested)
            {
                try
                {
                    Update[] updateArray = await _receiver._botClient
                        .MakeRequestAsync(
                            request: new GetUpdatesRequest
                            {
                                Offset = _messageOffset,
                                Timeout = (int)_receiver._botClient.Timeout.TotalSeconds,
                                AllowedUpdates = _allowedUpdates,
                                Limit = _limit,
                            },
                            cancellationToken: _token
                        )
                        .ConfigureAwait(false);

                    if (updateArray.Length > 0)
                    {
                        _messageOffset = updateArray[^1].Id + 1;

                        Interlocked.Add(ref _pendingUpdates, updateArray.Length);

                        ChannelWriter<Update> writer = _channel.Writer;
                        foreach (Update update in updateArray)
                        {
                            // ReSharper disable once RedundantAssignment
                            var success = writer.TryWrite(update);
                            Debug.Assert(success, "TryWrite should succeed as we are using an unbounded channel");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    // Ignore
                }
#pragma warning disable CA1031
                catch (Exception ex)
#pragma warning restore CA1031
                {
                    Debug.Assert(_uncaughtException is null);

                    // If there is no errorHandler or the errorHandler throws, stop receiving
                    if (_receiver._pollingErrorHandler is null)
                    {
                        _uncaughtException = ex;
                        _cts.Cancel();
                    }
                    else
                    {
                        try
                        {
                            await _receiver._pollingErrorHandler(ex, _token).ConfigureAwait(false);
                        }
#pragma warning disable CA1031
                        catch (Exception errorHandlerException)
#pragma warning restore CA1031
                        {
                            _uncaughtException = new AggregateException(
                                message: "Exception was not caught by the errorHandler.",
                                ex,
                                errorHandlerException
                            );
                            _cts.Cancel();
                        }
                    }

                    if (_uncaughtException is not null)
                    {
#pragma warning disable CA2201
                        _uncaughtException = new(
                            message: "Exception was not caught by the errorHandler.",
                            innerException: _uncaughtException
                        );
#pragma warning restore CA2201
                    }
                }
            }
        }

        public Update Current => _current!; // _current being null indicates MoveNextAsync was never called

        public ValueTask DisposeAsync()
        {
            _cts.Cancel();
            _cts.Dispose();
            return new();
        }
    }
}
#endif

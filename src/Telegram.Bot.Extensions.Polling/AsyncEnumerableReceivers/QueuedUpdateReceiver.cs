#if NETSTANDARD2_1
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Extensions.Polling
{
    /// <summary>
    /// Supports asynchronous iteration over <see cref="Update"/>s.
    /// <para>Updates are received on a different thread and enqueued.</para>
    /// </summary>
    public class QueuedUpdateReceiver : IAsyncEnumerable<Update>
    {
        private readonly ITelegramBotClient _botClient;
        private readonly UpdateType[]? _allowedUpdates;
        private readonly Func<Exception, CancellationToken, Task>? _errorHandler;

        private Enumerator? _enumerator;

        /// <summary>
        /// Constructs a new <see cref="QueuedUpdateReceiver"/> for the specified <see cref="ITelegramBotClient"/>
        /// </summary>
        /// <param name="botClient">The <see cref="ITelegramBotClient"/> used for making GetUpdates calls</param>
        /// <param name="allowedUpdates">Indicates which <see cref="UpdateType"/>s are allowed to be received. null means all updates</param>
        /// <param name="errorHandler">The function used to handle <see cref="Exception"/>s thrown by GetUpdates requests</param>
        public QueuedUpdateReceiver(
            ITelegramBotClient botClient,
            UpdateType[]? allowedUpdates = default,
            Func<Exception, CancellationToken, Task>? errorHandler = default)
        {
            _botClient = botClient ?? throw new ArgumentNullException(nameof(botClient));
            _allowedUpdates = allowedUpdates;
            _errorHandler = errorHandler;
        }

        /// <summary>
        /// Indicates how many <see cref="Update"/>s are ready to be returned the enumerator
        /// </summary>
        public int PendingUpdates => _enumerator?.PendingUpdates ?? 0;

        /// <summary>
        /// Gets the <see cref="IAsyncEnumerator{Update}"/>. This method may only be called once.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> with which you can stop receiving</param>
        public IAsyncEnumerator<Update> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            var enumerator = new Enumerator(this, cancellationToken);

            if (Interlocked.CompareExchange(ref _enumerator, enumerator, null) != null)
                throw new InvalidOperationException(nameof(GetAsyncEnumerator) + " may only be called once");

            return enumerator;
        }

        private class Enumerator : IAsyncEnumerator<Update>
        {
            private readonly QueuedUpdateReceiver _receiver;
            private readonly CancellationTokenSource _cts;

            private Exception? _uncaughtException;

            private readonly Channel<Update> _channel;
            private Update? _current;

            private int _pendingUpdates;
            public int PendingUpdates => _pendingUpdates;

            public Enumerator(QueuedUpdateReceiver receiver, CancellationToken cancellationToken)
            {
                _receiver = receiver;
                _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, default);

                _channel = Channel.CreateUnbounded<Update>(new UnboundedChannelOptions()
                {
                    SingleReader = true,
                    SingleWriter = true
                });

                Task.Run(ReceiveUpdatesAsync);
            }

            public ValueTask<bool> MoveNextAsync()
            {
                if (_uncaughtException != null)
                    throw _uncaughtException;

                _cts.Token.ThrowIfCancellationRequested();

                if (_channel.Reader.TryRead(out _current))
                {
                    Interlocked.Decrement(ref _pendingUpdates);
                    return new ValueTask<bool>(true);
                }

                return new ValueTask<bool>(ReadAsync());
            }

            public async Task<bool> ReadAsync()
            {
                _current = await _channel.Reader.ReadAsync(_cts.Token);
                Interlocked.Decrement(ref _pendingUpdates);
                return true;
            }

            private async Task ReceiveUpdatesAsync()
            {
                int messageOffset = 0;
                while (!_cts.IsCancellationRequested)
                {
                    try
                    {
                        Update[] updateArray = await _receiver._botClient.MakeRequestAsync(new GetUpdatesRequest()
                        {
                            Offset = messageOffset,
                            Timeout = (int)_receiver._botClient.Timeout.TotalSeconds,
                            AllowedUpdates = _receiver._allowedUpdates,
                        }, _cts.Token).ConfigureAwait(false);

                        if (updateArray.Length > 0)
                        {
                            messageOffset = updateArray[^1].Id + 1;

                            Interlocked.Add(ref _pendingUpdates, updateArray.Length);

                            ChannelWriter<Update> writer = _channel.Writer;
                            foreach (Update update in updateArray)
                            {
                                bool success = writer.TryWrite(update);
                                Debug.Assert(success, "TryWrite should succeed as we are using an unbounded channel");
                            }
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        // Ignore
                    }
                    catch (Exception ex)
                    {
                        Debug.Assert(_uncaughtException is null);

                        // If there is no errorHandler or the errorHandler throws, stop receiving
                        if (_receiver._errorHandler is null)
                        {
                            _uncaughtException = ex;
                            _cts.Cancel();
                        }
                        else
                        {
                            try
                            {
                                await _receiver._errorHandler(ex, _cts.Token).ConfigureAwait(false);
                            }
                            catch (Exception errorHandlerException)
                            {
                                _uncaughtException = new AggregateException(ex, errorHandlerException);
                                _cts.Cancel();
                            }
                        }

                        if (_uncaughtException != null)
                            _uncaughtException = new Exception("Exception was not caught by the errorHandler.", _uncaughtException);
                    }
                }
            }

            public Update Current => _current!; // _current being null indicates MoveNextAsync was never called

            public ValueTask DisposeAsync()
            {
                _cts.Cancel();
                return new ValueTask();
            }
        }
    }
}
#endif

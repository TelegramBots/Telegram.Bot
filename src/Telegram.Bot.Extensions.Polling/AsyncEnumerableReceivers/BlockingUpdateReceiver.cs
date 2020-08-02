#if NETSTANDARD2_1
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Extensions.Polling
{
    /// <summary>
    /// Supports asynchronous iteration over <see cref="Update"/>s
    /// </summary>
    public class BlockingUpdateReceiver : IAsyncEnumerable<Update>
    {
        private readonly ITelegramBotClient _botClient;
        private readonly UpdateType[]? _allowedUpdates;
        private readonly Func<Exception, CancellationToken, Task>? _errorHandler;

        private Enumerator? _enumerator;

        /// <summary>
        /// Constructs a new <see cref="BlockingUpdateReceiver"/> for the specified <see cref="ITelegramBotClient"/>
        /// </summary>
        /// <param name="botClient">The <see cref="ITelegramBotClient"/> used for making GetUpdates calls</param>
        /// <param name="allowedUpdates">Indicates which <see cref="UpdateType"/>s are allowed to be received. null means all updates</param>
        /// <param name="errorHandler">The function used to handle <see cref="Exception"/>s thrown by ReceiveUpdates</param>
        public BlockingUpdateReceiver(
            ITelegramBotClient botClient,
            UpdateType[]? allowedUpdates = default,
            Func<Exception, CancellationToken, Task>? errorHandler = default)
        {
            _botClient = botClient ?? throw new ArgumentNullException(nameof(botClient));
            _allowedUpdates = allowedUpdates;
            _errorHandler = errorHandler;
        }

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
            private readonly BlockingUpdateReceiver _receiver;
            private readonly CancellationTokenSource _cts;

            private Update[] _updateArray = Array.Empty<Update>();
            private int _updateIndex;
            private int _messageOffset;

            public Enumerator(BlockingUpdateReceiver receiver, CancellationToken cancellationToken)
            {
                _receiver = receiver;
                _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, default);
            }

            public ValueTask<bool> MoveNextAsync()
            {
                _cts.Token.ThrowIfCancellationRequested();

                if (++_updateIndex < _updateArray.Length)
                    return new ValueTask<bool>(true);

                return new ValueTask<bool>(ReceiveUpdatesAsync());
            }

            private async Task<bool> ReceiveUpdatesAsync()
            {
                _updateArray = Array.Empty<Update>();
                _updateIndex = 0;

                while (_updateArray.Length == 0)
                {
                    try
                    {
                        _updateArray = await _receiver._botClient.MakeRequestAsync(new GetUpdatesRequest()
                        {
                            Offset = _messageOffset,
                            Timeout = (int)_receiver._botClient.Timeout.TotalSeconds,
                            AllowedUpdates = _receiver._allowedUpdates,
                        }, _cts.Token).ConfigureAwait(false);
                    }
                    catch (Exception ex) when (!(ex is OperationCanceledException) && _receiver._errorHandler != null)
                    {
                        await _receiver._errorHandler(ex, _cts.Token).ConfigureAwait(false);
                    }
                }

                _messageOffset = _updateArray[^1].Id + 1;
                return true;
            }

            public Update Current => _updateArray[_updateIndex];

            public ValueTask DisposeAsync()
            {
                _cts.Cancel();
                return new ValueTask();
            }
        }
    }
}
#endif

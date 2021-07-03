#if NETCOREAPP3_1_OR_GREATER
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Extensions.Polling
{
    /// <summary>
    /// Supports asynchronous iteration over <see cref="Update"/>s
    /// </summary>
    public class BlockingUpdateReceiver : IAsyncEnumerable<Update>
    {
        readonly ITelegramBotClient _botClient;
        readonly UpdateType[]? _allowedUpdates;
        readonly Func<Exception, CancellationToken, Task>? _errorHandler;

        int _inProcess;

        /// <summary>
        /// Constructs a new <see cref="BlockingUpdateReceiver"/> for the specified <see cref="ITelegramBotClient"/>
        /// </summary>
        /// <param name="botClient">The <see cref="ITelegramBotClient"/> used for making GetUpdates calls</param>
        /// <param name="allowedUpdates">
        /// Indicates which <see cref="UpdateType"/>s are allowed to be received. null means all updates
        /// </param>
        /// <param name="errorHandler">
        /// The function used to handle <see cref="Exception"/>s thrown by ReceiveUpdates
        /// </param>
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
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> with which you can stop receiving
        /// </param>
        public IAsyncEnumerator<Update> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            if (Interlocked.CompareExchange(ref _inProcess, 1, 0) == 1)
            {
                throw new InvalidOperationException(nameof(GetAsyncEnumerator) + " may only be called once");
            }

            return new Enumerator(this, cancellationToken);
        }

        class Enumerator : IAsyncEnumerator<Update>
        {
            readonly BlockingUpdateReceiver _receiver;
            readonly CancellationTokenSource _cts;
            readonly CancellationToken _token;

            Update[] _updateArray = Array.Empty<Update>();
            int _updateIndex;
            int _messageOffset;

            public Enumerator(BlockingUpdateReceiver receiver, CancellationToken cancellationToken)
            {
                _receiver = receiver;
                _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, default);
                _token = _cts.Token;
            }

            public ValueTask<bool> MoveNextAsync()
            {
                _token.ThrowIfCancellationRequested();

                _updateIndex += 1;

                return _updateIndex < _updateArray.Length
                    ? new ValueTask<bool>(true)
                    : new ValueTask<bool>(ReceiveUpdatesAsync());
            }

            async Task<bool> ReceiveUpdatesAsync()
            {
                _updateArray = Array.Empty<Update>();
                _updateIndex = 0;

                while (_updateArray.Length == 0)
                {
                    try
                    {
                        _updateArray = await _receiver._botClient
                            .MakeRequestAsync(
                                new GetUpdatesRequest
                                {
                                    Offset = _messageOffset,
                                    Timeout = (int) _receiver._botClient.Timeout.TotalSeconds,
                                    AllowedUpdates = _receiver._allowedUpdates,
                                },
                                _token
                            )
                            .ConfigureAwait(false);
                    }
                    catch (OperationCanceledException)
                    {
                        throw;
                    }
                    catch (Exception ex) when (_receiver._errorHandler is not null)
                    {
                        await _receiver._errorHandler(ex, _token).ConfigureAwait(false);
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
                return new ValueTask();
            }
        }
    }
}
#endif

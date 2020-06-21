#if NETSTANDARD2_1
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Extensions.Polling.Extensions;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;

namespace Telegram.Bot.Extensions.Polling
{
    /// <summary>
    /// Supports asynchronous iteration over <see cref="Update"/>s
    /// </summary>
    public class BlockingUpdateReceiver : IYieldingUpdateReceiver
    {
        private static readonly Update[] EmptyUpdates = Array.Empty<Update>();

        private readonly ITelegramBotClient _botClient;
        private readonly ReceiveOptions? _receiveOptions;
        private readonly Func<Exception, CancellationToken, Task>? _errorHandler;
        private readonly CancellationToken _cancellationToken;

        private int _updateIndex = 0;
        private Update[] _updateArray = EmptyUpdates;
        private int _messageOffset;
        private bool _updatesThrownOut;

        /// <summary>
        /// Constructs a new <see cref="BlockingUpdateReceiver"/> for the specified <see cref="ITelegramBotClient"/>
        /// </summary>
        /// <param name="botClient">The <see cref="ITelegramBotClient"/> used for making GetUpdates calls</param>
        /// <param name="receiveOptions">Options used to configure getUpdates request</param>
        /// <param name="errorHandler">The function used to handle <see cref="Exception"/>s</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> with which you can stop receiving</param>
        public BlockingUpdateReceiver(
            ITelegramBotClient botClient,
            ReceiveOptions? receiveOptions = default,
            Func<Exception, CancellationToken, Task>? errorHandler = default,
            CancellationToken cancellationToken = default)
        {
            _botClient = botClient ?? throw new ArgumentNullException(nameof(botClient));
            _receiveOptions = receiveOptions;
            _errorHandler = errorHandler;
            _messageOffset = _receiveOptions?.Offset ?? 0;
            _cancellationToken = cancellationToken;
        }

        /// <summary>
        /// Indicates how many <see cref="Update"/>s are ready to be returned by <see cref="YieldUpdatesAsync"/>
        /// </summary>
        public int PendingUpdates => _updateArray.Length - _updateIndex;

        /// <summary>
        /// Yields <see cref="Update"/>s as they are received (or inside <see cref="PendingUpdates"/>).
        /// <para>GetUpdates will be called AFTER all the <see cref="Update"/>s are processed</para>
        /// </summary>
        /// <returns>An <see cref="IAsyncEnumerable{T}"/> of <see cref="Update"/></returns>
        public async IAsyncEnumerable<Update> YieldUpdatesAsync()
        {
            // Access to YieldUpdatesAsync is not thread-safe!

            var allowedUpdates = _receiveOptions?.AllowedUpdates;
            var limit = _receiveOptions?.Limit ?? default;

            if (_receiveOptions?.ThrowPendingUpdates == true && !_updatesThrownOut)
            {
                try
                {
                    var newMessageOffset = await _botClient
                        .ThrowOutPendingUpdatesAsync(_cancellationToken);

                    if (newMessageOffset != null)
                        _messageOffset = newMessageOffset.Value;

                    _updatesThrownOut = true;
                }
                catch (OperationCanceledException)
                {
                    // ignored
                }
            }

            while (!_cancellationToken.IsCancellationRequested)
            {
                while (_updateIndex < _updateArray.Length)
                {
                    // It is vital that we increment before yielding
                    _updateIndex++;
                    yield return _updateArray[_updateIndex - 1];
                }

                _updateArray = EmptyUpdates;
                _updateIndex = 0;

                while (!_cancellationToken.IsCancellationRequested && _updateArray.Length == 0)
                {
                    int timeout = (int)_botClient.Timeout.TotalSeconds;
                    try
                    {
                        _updateArray = await _botClient.MakeRequestAsync(new GetUpdatesRequest
                        {
                            Offset = _messageOffset,
                            Limit = limit,
                            Timeout = timeout,
                            AllowedUpdates = allowedUpdates,
                        }, _cancellationToken).ConfigureAwait(false);
                    }
                    catch (OperationCanceledException)
                    {
                        // Ignore
                    }
                    catch (Exception ex)
                    {
                        if (_errorHandler != null)
                        {
                            try
                            {
                                await _errorHandler(ex, _cancellationToken).ConfigureAwait(false);
                            }
                            catch (OperationCanceledException)
                            {
                                // ignore
                            }
                        }
                    }
                }

                if (_updateArray.Length > 0)
                    _messageOffset = _updateArray[^1].Id + 1;
            }
        }
    }
}
#endif

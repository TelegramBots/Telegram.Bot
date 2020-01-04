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
    public class BlockingUpdateReceiver : IYieldingUpdateReceiver
    {
        private static readonly Update[] EmptyUpdates = { };

        /// <summary>
        /// The <see cref="ITelegramBotClient"/> used for making GetUpdates calls
        /// </summary>
        public readonly ITelegramBotClient BotClient;

        /// <summary>
        /// Constructs a new <see cref="BlockingUpdateReceiver"/> for the specified <see cref="ITelegramBotClient"/>
        /// </summary>
        /// <param name="botClient">The <see cref="ITelegramBotClient"/> used for making GetUpdates calls</param>
        /// <param name="allowedUpdates">Indicates which <see cref="UpdateType"/>s are allowed to be received. null means all updates</param>
        /// <param name="errorHandler">The function used to handle <see cref="Exception"/>s</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> with which you can stop receiving</param>
        public BlockingUpdateReceiver(
            ITelegramBotClient botClient,
            UpdateType[]? allowedUpdates = default,
            Func<Exception, CancellationToken, Task>? errorHandler = default,
            CancellationToken cancellationToken = default)
        {
            BotClient = botClient ?? throw new ArgumentNullException(nameof(botClient));
            _allowedUpdates = allowedUpdates;
            this.errorHandler = errorHandler;
            _cancellationToken = cancellationToken;
        }

        private readonly UpdateType[]? _allowedUpdates;
        private readonly Func<Exception, CancellationToken, Task>? errorHandler;
        private readonly CancellationToken _cancellationToken;
        private int _updateIndex = 0;
        private Update[] _updateArray = EmptyUpdates;
        private int _messageOffset;

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
                    int timeout = (int)BotClient.Timeout.TotalSeconds;
                    try
                    {
                        _updateArray = await BotClient.MakeRequestAsync(new GetUpdatesRequest()
                        {
                            Offset = _messageOffset,
                            Timeout = timeout,
                            AllowedUpdates = _allowedUpdates,
                        }, _cancellationToken).ConfigureAwait(false);
                    }
                    catch (OperationCanceledException)
                    {
                        // Ignore
                    }
                    catch (Exception ex)
                    {
                        if (errorHandler != null)
                        {
                            await errorHandler(ex, _cancellationToken).ConfigureAwait(false);
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

#if NETCOREAPP3_0
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Extensions.Polling
{
    public class BlockingUpdateReceiver : IYieldingUpdateReceiver
    {
        private static readonly Update[] EmptyUpdates = { };

        public readonly ITelegramBotClient BotClient;

        private readonly UpdateType[]? _allowedUpdates;
        private readonly Func<Exception, Task>? _exceptionHandler;
        private readonly CancellationToken _cancellationToken;

        public BlockingUpdateReceiver(
            ITelegramBotClient botClient,
            UpdateType[]? allowedUpdates = default,
            Func<Exception, Task>? exceptionHandler = default,
            CancellationToken cancellationToken = default)
        {
            BotClient = botClient;
            _allowedUpdates = allowedUpdates;
            _exceptionHandler = exceptionHandler;
            _cancellationToken = cancellationToken;
        }

        private Update[] _updateArray = EmptyUpdates;
        private int _updateIndex = 0;

        public int PendingUpdates => _updateArray.Length - _updateIndex;
        public int MessageOffset { get; private set; }

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
                        _updateArray = await BotClient.GetUpdatesAsync(
                            offset: MessageOffset,
                            timeout: timeout,
                            allowedUpdates: _allowedUpdates,
                            cancellationToken: _cancellationToken
                        ).ConfigureAwait(false);
                    }
                    catch (OperationCanceledException)
                    {
                        // Ignore
                    }
                    catch (Exception ex)
                    {
                        if (_exceptionHandler != null)
                        {
                            await _exceptionHandler(ex).ConfigureAwait(false);
                        }
                    }
                }

                if (_updateArray.Length > 0)
                    MessageOffset = _updateArray[^1].Id + 1;
            }
        }
    }
}
#endif
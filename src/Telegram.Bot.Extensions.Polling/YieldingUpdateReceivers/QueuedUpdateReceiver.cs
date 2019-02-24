#if NETCOREAPP3_0
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Extensions.Polling
{
    public class QueuedUpdateReceiver : IYieldingUpdateReceiver
    {
        private static readonly Update[] EmptyUpdates = { };

        public readonly ITelegramBotClient BotClient;

        public QueuedUpdateReceiver(ITelegramBotClient botClient)
        {
            BotClient = botClient;
        }

        public bool IsReceiving { get; private set; }

        private readonly object _lock = new object();
        private CancellationTokenSource? _cancellationTokenSource;
        private TaskCompletionSource<bool> _tcs = new TaskCompletionSource<bool>();
        private int _consumerQueueIndex = 0;
        private int _consumerQueueInnerIndex = 0;
        private List<Update[]?> _consumerQueue = new List<Update[]?>(16);
        private List<Update[]?> _producerQueue = new List<Update[]?>(16);

        private int _pendingUpdates;
        public int PendingUpdates => _pendingUpdates;
        public int MessageOffset { get; private set; }


        public void StartReceiving(
            UpdateType[]? allowedUpdates = default,
            Func<Exception, Task>? errorHandler = default,
            CancellationToken cancellationToken = default)
        {
            lock (_lock)
            {
                if (IsReceiving)
                    throw new InvalidOperationException("Receiving is already in progress");

                IsReceiving = true;

                if (cancellationToken.IsCancellationRequested)
                    return;

                _cancellationTokenSource = new CancellationTokenSource();
                cancellationToken.Register(() => _cancellationTokenSource?.Cancel());
                cancellationToken = _cancellationTokenSource.Token;
            }

            StartReceivingInternal(allowedUpdates, errorHandler, cancellationToken);
        }

        private void StartReceivingInternal(
            UpdateType[]? allowedUpdates = default,
            Func<Exception, Task>? errorHandler = default,
            CancellationToken cancellationToken = default)
        {
            Debug.Assert(IsReceiving);
            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    int timeout = (int)BotClient.Timeout.TotalSeconds;
                    var updates = EmptyUpdates;
                    try
                    {
                        updates = await BotClient.GetUpdatesAsync(
                            offset: MessageOffset,
                            timeout: timeout,
                            allowedUpdates: allowedUpdates,
                            cancellationToken: cancellationToken
                        ).ConfigureAwait(false);
                    }
                    catch (OperationCanceledException)
                    {
                        // Ignore
                    }
                    catch (Exception ex)
                    {
                        if (errorHandler != null)
                        {
                            // If the error handler throws then the consumer of this IAsyncEnumerable will wait forever
                            await errorHandler(ex).ConfigureAwait(false);
                        }
                    }

                    if (updates.Length > 0)
                    {
                        Interlocked.Add(ref _pendingUpdates, updates.Length);
                        MessageOffset = updates[^1].Id + 1;

                        lock (_lock)
                        {
                            _producerQueue.Add(updates);
                            _tcs.TrySetResult(true);
                        }
                    }
                }

                Debug.Assert(_cancellationTokenSource != null);
                Debug.Assert(IsReceiving);
                _cancellationTokenSource = null;
                IsReceiving = false;
            });
        }

        public void StopReceiving()
        {
            lock (_lock)
            {
                if (!IsReceiving || _cancellationTokenSource == null)
                    return;

                _cancellationTokenSource.Cancel();
            }

            // IsReceiving is set to false by the receiver
        }

        public async IAsyncEnumerable<Update> YieldUpdatesAsync()
        {
            // Access to YieldUpdatesAsync is not thread-safe!

            while (true)
            {
                while (_consumerQueueIndex < _consumerQueue.Count)
                {
                    Debug.Assert(_consumerQueue[_consumerQueueIndex] != null);
                    Update[] updateArray = _consumerQueue[_consumerQueueIndex]!;

                    while (_consumerQueueInnerIndex < updateArray.Length)
                    {
                        Interlocked.Decrement(ref _pendingUpdates);

                        // It is vital that we increment before yielding
                        _consumerQueueInnerIndex++;
                        yield return updateArray[_consumerQueueInnerIndex - 1];
                    }

                    // We have reached the end of the current Update[]
                    Debug.Assert(_consumerQueue[_consumerQueueIndex] != null);
                    Debug.Assert(_consumerQueueInnerIndex == _consumerQueue[_consumerQueueIndex]!.Length);

                    // Let the GC collect the Update[], this amortizes the cost of GC on queue swaps
                    _consumerQueue[_consumerQueueIndex] = null;

                    _consumerQueueIndex++;
                    _consumerQueueInnerIndex = 0;
                }

                Debug.Assert(_consumerQueueIndex == _consumerQueue.Count);
                Debug.Assert(_consumerQueueInnerIndex == 0);

                _consumerQueueIndex = 0;

                // We still have to clear all the null references
                Debug.Assert(_consumerQueue.TrueForAll(updateArray => updateArray == null));
                _consumerQueue.Clear();

                // now wait for new updates
                // (no need for await if it's obvious we already have updates pending)
                if (_producerQueue.Count == 0)
                    await _tcs.Task.ConfigureAwait(false);

                lock (_lock)
                {
                    // Swap
                    var temp = _producerQueue;
                    _producerQueue = _consumerQueue;
                    _consumerQueue = temp;

                    // Reset the TCS
                    _tcs = new TaskCompletionSource<bool>();
                }

                Debug.Assert(_consumerQueue.Count > 0);
            }
        }
    }
}
#endif
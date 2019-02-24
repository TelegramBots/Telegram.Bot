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

        public bool StartedReceiving { get; private set; }

        private int _queuedUpdates;
        public int QueuedUpdates => _queuedUpdates;

        public async IAsyncEnumerable<Update> YieldUpdatesAsync(
            UpdateType[]? allowedUpdates = default,
            Func<Exception, Task>? errorHandler = default,
            CancellationToken cancellationToken = default)
        {
            if (StartedReceiving) throw new InvalidOperationException("YieldUpdatesAsync has already been called");
            StartedReceiving = true;

            // Only access producerQueue and exchange tcs under a lock
            // We could use other objects instead, but this way it's very obvious what we're doing
            object lockObject = new object();

            List<Update[]> producerQueue = new List<Update[]>();
            List<Update[]> consumerQueue = new List<Update[]>();
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            _ = Task.Run(async () =>
            {
                int messageOffset = 0;
                while (!cancellationToken.IsCancellationRequested)
                {
                    int timeout = (int)BotClient.Timeout.TotalSeconds;
                    var updates = EmptyUpdates;
                    try
                    {
                        updates = await BotClient.GetUpdatesAsync(
                            offset: messageOffset,
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
                        Interlocked.Add(ref _queuedUpdates, updates.Length);
                        messageOffset = updates[^1].Id + 1;

                        lock (lockObject)
                        {
                            producerQueue.Add(updates);
                            tcs.TrySetResult(true);
                        }
                    }
                }
            });

            while (!cancellationToken.IsCancellationRequested)
            {
                // Don't await if it's obvious we already have updates
                if (producerQueue.Count == 0)
                    await tcs.Task.ConfigureAwait(false);

                lock (lockObject)
                {
                    // Swap
                    var temp = producerQueue;
                    producerQueue = consumerQueue;
                    consumerQueue = temp;
                }

                Debug.Assert(consumerQueue.Count > 0);
                foreach (var updateArray in consumerQueue)
                {
                    Debug.Assert(updateArray.Length > 0);
                    foreach (var update in updateArray)
                    {
                        Interlocked.Decrement(ref _queuedUpdates);
                        yield return update;
                    }
                }

                consumerQueue.Clear();

                // We could save an allocation here by having a custom TCS,
                // but it does not matter as we're also allocating Updates and making network requests
                lock (lockObject) tcs = new TaskCompletionSource<bool>();
            }
        }
    }
}
#endif
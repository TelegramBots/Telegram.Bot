using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Tests.Integ.Common
{
    public static class UpdateReceiver
    {
        public static async Task DiscardNewUpdates(ITelegramBotClient botClient,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (cancellationToken == default(CancellationToken))
            {
                var source = new CancellationTokenSource(TimeSpan.FromSeconds(30));
                cancellationToken = source.Token;
            }

            int offset = 0;

            while (!cancellationToken.IsCancellationRequested)
            {
                var updates = await botClient.GetUpdatesAsync(offset,
                    cancellationToken: cancellationToken);

                if (updates.Any())
                {
                    offset = updates.Last().Id + 1;
                }
                else
                {
                    break;
                }
            }
        }

        public static async Task<Update[]> GetUpdates(
            ITelegramBotClient botClient,
            Func<Update, bool> predicate,
            int offset = 0,
            CancellationToken cancellationToken = default(CancellationToken),
            params UpdateType[] updateTypes)
        {
            if (cancellationToken == default(CancellationToken))
            {
                var source = new CancellationTokenSource(TimeSpan.FromMinutes(2));
                cancellationToken = source.Token;
            }

            Update[] matchingUpdates = null;

            while (!cancellationToken.IsCancellationRequested)
            {
                var updates = await botClient.GetUpdatesAsync(offset,
                    allowedUpdates: updateTypes,
                    cancellationToken: cancellationToken);

                matchingUpdates = updates.
                    Where(predicate)
                    .ToArray();

                if (updates.Any())
                {
                    break;
                }
                else
                {
                    offset = updates.LastOrDefault()?.Id + 1 ?? 0;
                    await Task.Delay(5_000, cancellationToken);
                }
            }

            return matchingUpdates;
        }
    }
}

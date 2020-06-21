using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Extensions.Polling.Extensions
{
    internal static class TelegramBotClientExtensions
    {
        /// <summary>
        /// Will attempt to throw the last update using offset set to -1.
        /// </summary>
        /// <param name="botClient"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// Update ID of the last <see cref="Update"/> increased by 1 if there were any
        /// </returns>
        internal static async Task<int?> ThrowOutPendingUpdatesAsync(
            this ITelegramBotClient botClient,
            CancellationToken cancellationToken = default)
        {
            var timeout = (int) botClient.Timeout.TotalSeconds;
            var request = new GetUpdatesRequest
            {
                Limit = 1,
                Offset = -1,
                Timeout = timeout,
                AllowedUpdates = Array.Empty<UpdateType>(),
            };
            var updates = await botClient.MakeRequestAsync(request, cancellationToken)
                .ConfigureAwait(false);

            if (updates.Length > 0) return updates[0].Id + 1;

            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Tests.Integ.Common
{
    public class UpdateReceiver
    {
        private readonly ITelegramBotClient _botClient;

        private readonly string[] _allowedUsernames;

        public UpdateReceiver(ITelegramBotClient botClient, params string[] allowedUsernames)
        {
            _botClient = botClient;
            _allowedUsernames = allowedUsernames;
        }

        public async Task DiscardNewUpdatesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            if (cancellationToken == default(CancellationToken))
            {
                var source = new CancellationTokenSource(TimeSpan.FromSeconds(30));
                cancellationToken = source.Token;
            }

            int offset = 0;

            while (!cancellationToken.IsCancellationRequested)
            {
                var updates = await _botClient.GetUpdatesAsync(offset,
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

        public async Task<Update[]> GetUpdatesAsync(
            Func<Update, bool> predicate = null,
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
                IEnumerable<Update> updates = await GetOnlyAllowedUpdatesAsync(offset, cancellationToken, updateTypes);

                if (predicate is null)
                {
                    updates = updates.Where(u => updateTypes.Contains(u.Type));
                }
                else
                {
                    updates = updates
                        .Where(u => updateTypes.Contains(u.Type))
                        .Where(predicate);
                }

                matchingUpdates = updates.ToArray();

                if (updates.Any())
                {
                    break;
                }
                else
                {
                    offset = updates.LastOrDefault()?.Id + 1 ?? 0;
                    await Task.Delay(1_500, cancellationToken);
                }
            }

            return matchingUpdates;
        }

        private async Task<Update[]> GetOnlyAllowedUpdatesAsync(int offset, CancellationToken cToken, params UpdateType[] types)
        {
            var updates = await _botClient.GetUpdatesAsync(offset,
                allowedUpdates: types,
                cancellationToken: cToken);

            var allowedUpdates = updates.Where(IsAllowed).ToArray();

            return allowedUpdates;
        }

        private bool IsAllowed(Update update)
        {
            if (_allowedUsernames?.All(string.IsNullOrWhiteSpace) == true)
            {
                return true;
            }

            bool isAllowed;

            switch (update.Type)
            {
                case UpdateType.MessageUpdate:
                    isAllowed = _allowedUsernames
                        .Contains(update.Message.From.Username, StringComparer.OrdinalIgnoreCase);
                    break;
                case UpdateType.InlineQueryUpdate:
                    isAllowed = _allowedUsernames
                        .Contains(update.InlineQuery.From.Username, StringComparer.OrdinalIgnoreCase);
                    break;
                case UpdateType.CallbackQueryUpdate:
                    isAllowed = _allowedUsernames
                        .Contains(update.CallbackQuery.From.Username, StringComparer.OrdinalIgnoreCase);
                    break;
                case UpdateType.PreCheckoutQueryUpdate:
                    isAllowed = _allowedUsernames
                        .Contains(update.PreCheckoutQuery.From.Username, StringComparer.OrdinalIgnoreCase);
                    break;
                case UpdateType.ShippingQueryUpdate:
                    isAllowed = _allowedUsernames
                        .Contains(update.ShippingQuery.From.Username, StringComparer.OrdinalIgnoreCase);
                    break;
                case UpdateType.ChosenInlineResultUpdate:
                case UpdateType.EditedMessage:
                case UpdateType.ChannelPost:
                case UpdateType.EditedChannelPost:
                case UpdateType.All:
                    isAllowed = false;
                    break;
                case UpdateType.UnknownUpdate:
                    throw new ArgumentException("Unkown update found!");
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return isAllowed;
        }
    }
}

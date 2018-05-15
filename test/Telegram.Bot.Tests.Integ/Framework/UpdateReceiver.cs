using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Tests.Integ.Framework
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

        public async Task DiscardNewUpdatesAsync(CancellationToken cancellationToken = default)
        {
            if (cancellationToken == default)
            {
                var source = new CancellationTokenSource(TimeSpan.FromSeconds(30));
                cancellationToken = source.Token;
            }

            int offset = 0;

            while (!cancellationToken.IsCancellationRequested)
            {
                var updates = await _botClient.GetUpdatesAsync(offset,
                    allowedUpdates: new UpdateType[0],
                    cancellationToken: cancellationToken);

                if (updates.Length == 0)
                    break;

                offset = updates.Last().Id + 1;
            }
        }

        public async Task<Update[]> GetUpdatesAsync(
            Func<Update, bool> predicate = default,
            int offset = default,
            bool safeUpdates = true,
            CancellationToken cancellationToken = default,
            params UpdateType[] updateTypes)
        {
            if (cancellationToken == default)
                cancellationToken = new CancellationTokenSource(TimeSpan.FromMinutes(2)).Token;

            Update[] matchingUpdates = Array.Empty<Update>();

            while (!cancellationToken.IsCancellationRequested)
            {
                IEnumerable<Update> updates = safeUpdates
                    ? await GetOnlyAllowedUpdatesAsync(offset, cancellationToken, updateTypes)
                    : await _botClient.GetUpdatesAsync(offset, allowedUpdates: updateTypes, cancellationToken: cancellationToken);
                updates = updates
                    .Where(u =>
                        updateTypes.Contains(u.Type) &&
                        (predicate == null || predicate(u))
                    );

                matchingUpdates = updates.ToArray();

                if (updates.Any())
                    break;

                offset = updates.LastOrDefault()?.Id + 1 ?? 0;
                await Task.Delay(1_500, cancellationToken);
            }

            return matchingUpdates;
        }

        public async Task<Update> GetCallbackQueryUpdateAsync(
            int messageId = default,
            string data = default,
            bool discardNewUpdates = true,
            CancellationToken cancellationToken = default)
        {
            Func<Update, bool> predicate = u =>
                (messageId == default || u.CallbackQuery.Message.MessageId == messageId) &&
                (data == default || u.CallbackQuery.Data == data);

            var updates = await GetUpdatesAsync(predicate,
                cancellationToken: cancellationToken,
                updateTypes: UpdateType.CallbackQuery);

            if (discardNewUpdates)
                await DiscardNewUpdatesAsync(cancellationToken);

            var update = updates.First();
            return update;
        }

        public async Task<Update> GetInlineQueryUpdateAsync(bool discardNewUpdates = true,
            CancellationToken cancellationToken = default)
        {
            var updates = await GetUpdatesAsync(
                cancellationToken: cancellationToken,
                updateTypes: UpdateType.InlineQuery);

            if (discardNewUpdates)
                await DiscardNewUpdatesAsync(cancellationToken);

            var update = updates.First();
            return update;
        }

        /// <summary>
        /// Receive the chosen inline query result and the message that was sent to chat. Use this method only after bot answers to an inline query.
        /// </summary>
        /// <param name="messageType">Type of message for chosen inline query e.g. Text message for article results</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Message update generated for chosen result, and the update for chosen inline query result</returns>
        public async Task<(Update MessageUpdate, Update ChosenResultUpdate)> GetInlineQueryResultUpdates
            (MessageType messageType, CancellationToken cancellationToken = default)
        {
            Update messageUpdate = default;
            Update chosenResultUpdate = default;

            while (
                !cancellationToken.IsCancellationRequested &&
                (messageUpdate == null || chosenResultUpdate == null)
            )
            {
                await Task.Delay(1_000, cancellationToken);
                var updates = await GetUpdatesAsync(
                    u => u.Message?.Type == messageType || u.ChosenInlineResult != null,
                    cancellationToken: cancellationToken,
                    updateTypes: new[] { UpdateType.Message, UpdateType.ChosenInlineResult }
                );

                messageUpdate = updates.SingleOrDefault(u => u.Message?.Type == messageType);
                chosenResultUpdate = updates.SingleOrDefault(u => u.Type == UpdateType.ChosenInlineResult);
            }

            return (messageUpdate, chosenResultUpdate);
        }

        private async Task<Update[]> GetOnlyAllowedUpdatesAsync(
            int offset, CancellationToken cancellationToken, params UpdateType[] types)
        {
            var updates = await _botClient.GetUpdatesAsync(
                offset,
                allowedUpdates: types,
                cancellationToken: cancellationToken);

            var allowedUpdates = updates.Where(IsAllowed).ToArray();
            return allowedUpdates;
        }

        private bool IsAllowed(Update update)
        {
            if (_allowedUsernames is null || _allowedUsernames.All(string.IsNullOrWhiteSpace))
                return true;

            bool isAllowed;

            switch (update.Type)
            {
                case UpdateType.Message:
                case UpdateType.InlineQuery:
                case UpdateType.CallbackQuery:
                case UpdateType.PreCheckoutQuery:
                case UpdateType.ShippingQuery:
                case UpdateType.ChosenInlineResult:
                    isAllowed = _allowedUsernames.Contains(update.GetUser().Username, StringComparer.OrdinalIgnoreCase);
                    break;
                case UpdateType.EditedMessage:
                case UpdateType.ChannelPost:
                case UpdateType.EditedChannelPost:
                    isAllowed = false;
                    break;
                case UpdateType.Unknown:
                    throw new ArgumentException("Unknown update found!");
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return isAllowed;
        }
    }
}

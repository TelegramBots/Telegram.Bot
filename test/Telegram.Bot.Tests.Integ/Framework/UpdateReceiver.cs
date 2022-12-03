using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

#nullable enable

namespace Telegram.Bot.Tests.Integ.Framework;

public enum UpdatePosition
{
    First,
    Last,
    Single
}

public class UpdateReceiver
{
    readonly ITelegramBotClient _botClient;

    public List<string> AllowedUsernames { get; }

    public UpdateReceiver(ITelegramBotClient botClient, IEnumerable<string>? allowedUsernames)
    {
        _botClient = botClient;
        AllowedUsernames = allowedUsernames?.ToList() ?? new();
    }

    public async Task DiscardNewUpdatesAsync(CancellationToken cancellationToken = default)
    {
        CancellationTokenSource? cts = default;

        try
        {
            if (cancellationToken == default)
            {
                cts = new(TimeSpan.FromSeconds(30));
                cancellationToken = cts.Token;
            }

            int offset = -1;

            while (!cancellationToken.IsCancellationRequested)
            {
                var updates = await _botClient.GetUpdatesAsync(
                    offset: offset,
                    allowedUpdates: Array.Empty<UpdateType>(),
                    cancellationToken: cancellationToken
                );

                if (updates.Length == 0) break;

                offset = updates[^1].Id + 1;
            }
        }
        finally
        {
            cts?.Dispose();
        }
    }

    public async Task<Update[]> GetUpdatesAsync(
        Func<Update, bool>? predicate = default,
        int offset = 0,
        CancellationToken cancellationToken = default,
        params UpdateType[] updateTypes)
    {
        CancellationTokenSource? cts = default;
        predicate ??= PassthroughPredicate;

        try
        {
            if (cancellationToken == default)
            {
                cts = new(TimeSpan.FromMinutes(2));
                cancellationToken = cts.Token;
            }

            Update[] matchingUpdates = Array.Empty<Update>();

            while (!cancellationToken.IsCancellationRequested)
            {
                Update[] updates = await GetOnlyAllowedUpdatesAsync(
                    offset: offset,
                    types: updateTypes,
                    cancellationToken: cancellationToken
                );

                matchingUpdates = updates
                    .Where(u => updateTypes.Contains(u.Type) && predicate(u))
                    .ToArray();

                if (matchingUpdates.Length > 0) { break; }

                offset = updates.LastOrDefault()?.Id + 1 ?? 0;
                await Task.Delay(TimeSpan.FromSeconds(1.5), cancellationToken);
            }

            cancellationToken.ThrowIfCancellationRequested();

            return matchingUpdates;
        }
        finally
        {
            cts?.Dispose();
        }

        static bool PassthroughPredicate(Update _) => true;
    }

    public async Task<Update> GetUpdateAsync(
        Func<Update, bool>? predicate = default,
        int offset = 0,
        UpdatePosition updatePosition = UpdatePosition.Last,
        CancellationToken cancellationToken = default,
        params UpdateType[] updateTypes)
    {
        Update[] updates = await GetUpdatesAsync(
            predicate: predicate,
            offset: offset,
            updateTypes: updateTypes,
            cancellationToken: cancellationToken
        );

        return updatePosition switch
        {
            UpdatePosition.First => updates.First(),
            UpdatePosition.Last => updates.Last(),
            UpdatePosition.Single => updates.Single(),
            _ => throw new InvalidOperationException()
        };
    }

    public async Task<Update> GetCallbackQueryUpdateAsync(
        int? messageId = default,
        string? data = default,
        bool discardNewUpdates = true,
        CancellationToken cancellationToken = default)
    {
        if (discardNewUpdates) { await DiscardNewUpdatesAsync(cancellationToken); }

        var updates = await GetUpdatesAsync(
            predicate: u => (messageId is null || u.CallbackQuery?.Message?.MessageId == messageId) &&
                            (data is null || u.CallbackQuery?.Data == data),
            updateTypes: new [] { UpdateType.CallbackQuery },
            cancellationToken: cancellationToken
        );

        if (discardNewUpdates) { await DiscardNewUpdatesAsync(cancellationToken); }

        return updates.First();
    }

    public async Task<Update> GetInlineQueryUpdateAsync(
        bool discardNewUpdates = true,
        CancellationToken cancellationToken = default)
    {
        if (discardNewUpdates) { await DiscardNewUpdatesAsync(cancellationToken); }

        var updates = await GetUpdatesAsync(
            updateTypes: new [] { UpdateType.InlineQuery },
            cancellationToken: cancellationToken
        );

        if (discardNewUpdates) { await DiscardNewUpdatesAsync(cancellationToken); }

        return updates.First();
    }

    /// <summary>
    /// Receive the chosen inline query result and the message that was sent to chat. Use this method only after
    /// bot answers to an inline query.
    /// </summary>
    /// <param name="chatId">Id of the chat where the message from inline query is excepted</param>
    /// <param name="messageType">Type of message for chosen inline query e.g. Text message for article results</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Message update generated for chosen result, and the update for chosen inline query result</returns>
    public async Task<(Update MessageUpdate, Update ChosenResultUpdate)> GetInlineQueryResultUpdates(
        long chatId,
        MessageType messageType,
        CancellationToken cancellationToken = default)
    {
        Update? messageUpdate = default;
        Update? chosenResultUpdate = default;

        while (ShouldContinue(cancellationToken, (messageUpdate, chosenResultUpdate)))
        {
            await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            var updates = await GetUpdatesAsync(
                predicate: u => (u.Message is { Chat.Id: var id, Type: var type } &&
                                 id == chatId && type == messageType) ||
                                u.ChosenInlineResult is not null,
                cancellationToken: cancellationToken,
                updateTypes: new[] { UpdateType.Message, UpdateType.ChosenInlineResult }
            );

            messageUpdate = updates.SingleOrDefault(u => u.Message?.Type == messageType);
            chosenResultUpdate = updates.SingleOrDefault(u => u.Type == UpdateType.ChosenInlineResult);
        }

        cancellationToken.ThrowIfCancellationRequested();

        return (messageUpdate!, chosenResultUpdate!);

        static bool ShouldContinue(
            CancellationToken cancellationToken,
            (Update? update1, Update? update2) updates
        ) =>
            !cancellationToken.IsCancellationRequested && updates is not ({}, {});
    }

    async Task<Update[]> GetOnlyAllowedUpdatesAsync(
        int offset,
        CancellationToken cancellationToken,
        params UpdateType[] types)
    {
        var updates = await _botClient.GetUpdatesAsync(
            offset: offset,
            timeout: 120,
            allowedUpdates: types,
            cancellationToken: cancellationToken
        );

        return updates.Where(IsAllowed).ToArray();
    }

    bool IsAllowed(Update update)
    {
        if (AllowedUsernames.All(string.IsNullOrWhiteSpace)) { return true; }

        return update.Type switch
        {
            UpdateType.Message
                or UpdateType.InlineQuery
                or UpdateType.CallbackQuery
                or UpdateType.PreCheckoutQuery
                or UpdateType.ShippingQuery
                or UpdateType.ChosenInlineResult
                or UpdateType.PollAnswer
                or UpdateType.ChatMember
                or UpdateType.MyChatMember
                or UpdateType.ChatJoinRequest =>
                AllowedUsernames.Contains(
                    update.GetUser().Username,
                    StringComparer.OrdinalIgnoreCase
                ),
            UpdateType.Poll => true,
            UpdateType.EditedMessage
                or UpdateType.ChannelPost
                or UpdateType.EditedChannelPost => false,
            _ => throw new ArgumentOutOfRangeException(
                paramName: nameof(update.Type),
                actualValue: update.Type,
                message: $"Unsupported update type {update.Type}"
            ),
        };
    }
}

using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to get the current value of the bot`s menu button in a private chat, or the default menu button.
/// Returns <see cref="MenuButton"/> on success.
/// </summary>
public class GetChatMenuButtonRequest : RequestBase<MenuButton>, IChatTargetable
{
    /// <summary>
    /// Optional. Unique identifier for the target private chat. If not specified, default bot`s menu button
    /// will be changed
    /// </summary>
    public long ChatId { get; }

    /// <inheritdoc />
    ChatId IChatTargetable.ChatId => ChatId;

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public GetChatMenuButtonRequest(long chatId)
        : base("getChatMenuButton")
    { }
}

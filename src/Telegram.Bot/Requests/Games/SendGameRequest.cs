namespace Telegram.Bot.Requests;

/// <summary>Use this method to send a game.<para>Returns: The sent <see cref="Message"/> is returned.</para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SendGameRequest() : RequestBase<Message>("sendGame"), IChatTargetable, IBusinessConnectable
{
    /// <summary>Unique identifier for the target chat</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long ChatId { get; set; }

    /// <summary>Short name of the game, serves as the unique identifier for the game. Set up your games via <a href="https://t.me/botfather">@BotFather</a>.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string GameShortName { get; set; }

    /// <summary>Unique identifier for the target message thread (topic) of the forum; for forum supergroups only</summary>
    public int? MessageThreadId { get; set; }

    /// <summary>Sends the message <a href="https://telegram.org/blog/channels-2-0#silent-messages">silently</a>. Users will receive a notification with no sound.</summary>
    public bool DisableNotification { get; set; }

    /// <summary>Protects the contents of the sent message from forwarding and saving</summary>
    public bool ProtectContent { get; set; }

    /// <summary>Pass <see langword="true"/> to allow up to 1000 messages per second, ignoring <a href="https://core.telegram.org/bots/faq#how-can-i-message-all-of-my-bot-39s-subscribers-at-once">broadcasting limits</a> for a fee of 0.1 Telegram Stars per message. The relevant Stars will be withdrawn from the bot's balance</summary>
    public bool AllowPaidBroadcast { get; set; }

    /// <summary>Unique identifier of the message effect to be added to the message; for private chats only</summary>
    public string? MessageEffectId { get; set; }

    /// <summary>Description of the message to reply to</summary>
    public ReplyParameters? ReplyParameters { get; set; }

    /// <summary>An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>. If empty, one 'Play GameTitle' button will be shown. If not empty, the first button must launch the game.</summary>
    public InlineKeyboardMarkup? ReplyMarkup { get; set; }

    /// <summary>Unique identifier of the business connection on behalf of which the message will be sent</summary>
    public string? BusinessConnectionId { get; set; }

    /// <inheritdoc/>
    ChatId IChatTargetable.ChatId => ChatId;
}

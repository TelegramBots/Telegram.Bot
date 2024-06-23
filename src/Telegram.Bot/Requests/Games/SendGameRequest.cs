namespace Telegram.Bot.Requests;

/// <summary>Use this method to send a game.<para>Returns: The sent <see cref="Message"/> is returned.</para></summary>
public partial class SendGameRequest : RequestBase<Message>, IChatTargetable, IBusinessConnectable
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

    /// <summary>Unique identifier of the message effect to be added to the message; for private chats only</summary>
    public string? MessageEffectId { get; set; }

    /// <summary>Description of the message to reply to</summary>
    public ReplyParameters? ReplyParameters { get; set; }

    /// <summary>An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>. If empty, one 'Play GameTitle' button will be shown. If not empty, the first button must launch the game.</summary>
    public InlineKeyboardMarkup? ReplyMarkup { get; set; }

    /// <summary>Unique identifier of the business connection on behalf of which the message will be sent</summary>
    public string? BusinessConnectionId { get; set; }

    /// <summary>Initializes an instance of <see cref="SendGameRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat</param>
    /// <param name="gameShortName">Short name of the game, serves as the unique identifier for the game. Set up your games via <a href="https://t.me/botfather">@BotFather</a>.</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public SendGameRequest(long chatId, string gameShortName) : this()
    {
        ChatId = chatId;
        GameShortName = gameShortName;
    }

    /// <summary>Instantiates a new <see cref="SendGameRequest"/></summary>
    public SendGameRequest() : base("sendGame") { }

    /// <inheritdoc />
    ChatId IChatTargetable.ChatId => ChatId;
}

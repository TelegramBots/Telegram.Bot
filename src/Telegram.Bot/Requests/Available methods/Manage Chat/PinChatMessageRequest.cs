namespace Telegram.Bot.Requests;

/// <summary>Use this method to add a message to the list of pinned messages in a chat. If the chat is not a private chat, the bot must be an administrator in the chat for this to work and must have the 'CanPinMessages' administrator right in a supergroup or 'CanEditMessages' administrator right in a channel.<para>Returns: </para></summary>
public partial class PinChatMessageRequest : RequestBase<bool>, IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Identifier of a message to pin</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MessageId { get; set; }

    /// <summary>Pass <see langword="true"/> if it is not necessary to send a notification to all chat members about the new pinned message. Notifications are always disabled in channels and private chats.</summary>
    public bool DisableNotification { get; set; }

    /// <summary>Initializes an instance of <see cref="PinChatMessageRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="messageId">Identifier of a message to pin</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public PinChatMessageRequest(ChatId chatId, int messageId) : this()
    {
        ChatId = chatId;
        MessageId = messageId;
    }

    /// <summary>Instantiates a new <see cref="PinChatMessageRequest"/></summary>
    public PinChatMessageRequest() : base("pinChatMessage") { }
}

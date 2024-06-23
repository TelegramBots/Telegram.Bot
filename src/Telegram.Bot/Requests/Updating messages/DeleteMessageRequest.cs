namespace Telegram.Bot.Requests;

/// <summary>Use this method to delete a message, including service messages, with the following limitations:<br/>- A message can only be deleted if it was sent less than 48 hours ago.<br/>- Service messages about a supergroup, channel, or forum topic creation can't be deleted.<br/>- A dice message in a private chat can only be deleted if it was sent more than 24 hours ago.<br/>- Bots can delete outgoing messages in private chats, groups, and supergroups.<br/>- Bots can delete incoming messages in private chats.<br/>- Bots granted <em>CanPostMessages</em> permissions can delete outgoing messages in channels.<br/>- If the bot is an administrator of a group, it can delete any message there.<br/>- If the bot has <em>CanDeleteMessages</em> permission in a supergroup or a channel, it can delete any message there.<br/>Returns <em>True</em> on success.<para>Returns: </para></summary>
public partial class DeleteMessageRequest : RequestBase<bool>, IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Identifier of the message to delete</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MessageId { get; set; }

    /// <summary>Initializes an instance of <see cref="DeleteMessageRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="messageId">Identifier of the message to delete</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public DeleteMessageRequest(ChatId chatId, int messageId) : this()
    {
        ChatId = chatId;
        MessageId = messageId;
    }

    /// <summary>Instantiates a new <see cref="DeleteMessageRequest"/></summary>
    public DeleteMessageRequest() : base("deleteMessage") { }
}

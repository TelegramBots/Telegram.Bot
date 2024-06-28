namespace Telegram.Bot.Requests;

/// <summary>Use this method to get information about a member of a chat. The method is only guaranteed to work for other users if the bot is an administrator in the chat.<para>Returns: A <see cref="ChatMember"/> object on success.</para></summary>
public partial class GetChatMemberRequest : RequestBase<ChatMember>, IChatTargetable, IUserTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target supergroup or channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Unique identifier of the target user</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>Initializes an instance of <see cref="GetChatMemberRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup or channel (in the format <c>@channelusername</c>)</param>
    /// <param name="userId">Unique identifier of the target user</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public GetChatMemberRequest(ChatId chatId, long userId) : this()
    {
        ChatId = chatId;
        UserId = userId;
    }

    /// <summary>Instantiates a new <see cref="GetChatMemberRequest"/></summary>
    public GetChatMemberRequest() : base("getChatMember") { }
}

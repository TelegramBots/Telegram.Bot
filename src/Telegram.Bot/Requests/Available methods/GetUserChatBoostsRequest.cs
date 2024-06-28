namespace Telegram.Bot.Requests;

/// <summary>Use this method to get the list of boosts added to a chat by a user. Requires administrator rights in the chat.<para>Returns: A <see cref="UserChatBoosts"/> object.</para></summary>
public partial class GetUserChatBoostsRequest : RequestBase<UserChatBoosts>, IChatTargetable, IUserTargetable
{
    /// <summary>Unique identifier for the chat or username of the channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Unique identifier of the target user</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>Initializes an instance of <see cref="GetUserChatBoostsRequest"/></summary>
    /// <param name="chatId">Unique identifier for the chat or username of the channel (in the format <c>@channelusername</c>)</param>
    /// <param name="userId">Unique identifier of the target user</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public GetUserChatBoostsRequest(ChatId chatId, long userId) : this()
    {
        ChatId = chatId;
        UserId = userId;
    }

    /// <summary>Instantiates a new <see cref="GetUserChatBoostsRequest"/></summary>
    public GetUserChatBoostsRequest() : base("getUserChatBoosts") { }
}

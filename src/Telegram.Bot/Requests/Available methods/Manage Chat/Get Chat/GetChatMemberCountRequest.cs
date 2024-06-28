namespace Telegram.Bot.Requests;

/// <summary>Use this method to get the number of members in a chat.<para>Returns: <em>Int</em> on success.</para></summary>
public partial class GetChatMemberCountRequest : RequestBase<int>, IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target supergroup or channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Initializes an instance of <see cref="GetChatMemberCountRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup or channel (in the format <c>@channelusername</c>)</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public GetChatMemberCountRequest(ChatId chatId) : this() => ChatId = chatId;

    /// <summary>Instantiates a new <see cref="GetChatMemberCountRequest"/></summary>
    public GetChatMemberCountRequest() : base("getChatMemberCount") { }
}

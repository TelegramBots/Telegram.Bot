namespace Telegram.Bot.Requests;

/// <summary>Use this method for your bot to leave a group, supergroup or channel.<para>Returns: </para></summary>
public partial class LeaveChatRequest : RequestBase<bool>, IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target supergroup or channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Initializes an instance of <see cref="LeaveChatRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup or channel (in the format <c>@channelusername</c>)</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public LeaveChatRequest(ChatId chatId) : this() => ChatId = chatId;

    /// <summary>Instantiates a new <see cref="LeaveChatRequest"/></summary>
    public LeaveChatRequest() : base("leaveChat") { }
}

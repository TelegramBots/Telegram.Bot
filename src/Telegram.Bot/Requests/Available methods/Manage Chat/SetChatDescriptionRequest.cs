namespace Telegram.Bot.Requests;

/// <summary>Use this method to change the description of a group, a supergroup or a channel. The bot must be an administrator in the chat for this to work and must have the appropriate administrator rights.<para>Returns: </para></summary>
public partial class SetChatDescriptionRequest : RequestBase<bool>, IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>New chat description, 0-255 characters</summary>
    public string? Description { get; set; }

    /// <summary>Initializes an instance of <see cref="SetChatDescriptionRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public SetChatDescriptionRequest(ChatId chatId) : this() => ChatId = chatId;

    /// <summary>Instantiates a new <see cref="SetChatDescriptionRequest"/></summary>
    public SetChatDescriptionRequest() : base("setChatDescription") { }
}

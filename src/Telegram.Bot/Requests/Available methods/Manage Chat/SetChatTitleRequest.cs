namespace Telegram.Bot.Requests;

/// <summary>Use this method to change the title of a chat. Titles can't be changed for private chats. The bot must be an administrator in the chat for this to work and must have the appropriate administrator rights.<para>Returns: </para></summary>
public partial class SetChatTitleRequest : RequestBase<bool>, IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>New chat title, 1-128 characters</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Title { get; set; }

    /// <summary>Initializes an instance of <see cref="SetChatTitleRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="title">New chat title, 1-128 characters</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public SetChatTitleRequest(ChatId chatId, string title) : this()
    {
        ChatId = chatId;
        Title = title;
    }

    /// <summary>Instantiates a new <see cref="SetChatTitleRequest"/></summary>
    public SetChatTitleRequest() : base("setChatTitle") { }
}

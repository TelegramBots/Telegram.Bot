namespace Telegram.Bot.Requests;

/// <summary>Use this method to set a custom title for an administrator in a supergroup promoted by the bot.<para>Returns: </para></summary>
public partial class SetChatAdministratorCustomTitleRequest : RequestBase<bool>, IChatTargetable, IUserTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Unique identifier of the target user</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>New custom title for the administrator; 0-16 characters, emoji are not allowed</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string CustomTitle { get; set; }

    /// <summary>Initializes an instance of <see cref="SetChatAdministratorCustomTitleRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</param>
    /// <param name="userId">Unique identifier of the target user</param>
    /// <param name="customTitle">New custom title for the administrator; 0-16 characters, emoji are not allowed</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public SetChatAdministratorCustomTitleRequest(ChatId chatId, long userId, string customTitle) : this()
    {
        ChatId = chatId;
        UserId = userId;
        CustomTitle = customTitle;
    }

    /// <summary>Instantiates a new <see cref="SetChatAdministratorCustomTitleRequest"/></summary>
    public SetChatAdministratorCustomTitleRequest() : base("setChatAdministratorCustomTitle") { }
}

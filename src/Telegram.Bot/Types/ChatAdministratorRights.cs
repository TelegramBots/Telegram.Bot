namespace Telegram.Bot.Types;

/// <summary>
/// Represents the rights of an administrator in a chat.
/// </summary>
public partial class ChatAdministratorRights
{
    /// <summary>
    /// <see langword="true"/>, if the user's presence in the chat is hidden
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool IsAnonymous { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the administrator can access the chat event log, get boost list, see hidden supergroup and channel members, report spam messages and ignore slow mode. Implied by any other administrator privilege.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool CanManageChat { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the administrator can delete messages of other users
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool CanDeleteMessages { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the administrator can manage video chats
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool CanManageVideoChats { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the administrator can restrict, ban or unban chat members, or access supergroup statistics
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool CanRestrictMembers { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the administrator can add new administrators with a subset of their own privileges or demote administrators that they have promoted, directly or indirectly (promoted by administrators that were appointed by the user)
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool CanPromoteMembers { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the user is allowed to change the chat title, photo and other settings
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool CanChangeInfo { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the user is allowed to invite new users to the chat
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool CanInviteUsers { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the administrator can post stories to the chat
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool CanPostStories { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the administrator can edit stories posted by other users, post stories to the chat page, pin chat stories, and access the chat's story archive
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool CanEditStories { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the administrator can delete stories posted by other users
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool CanDeleteStories { get; set; }

    /// <summary>
    /// <em>Optional</em>. <see langword="true"/>, if the administrator can post messages in the channel, or access channel statistics; for channels only
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool CanPostMessages { get; set; }

    /// <summary>
    /// <em>Optional</em>. <see langword="true"/>, if the administrator can edit messages of other users and can pin messages; for channels only
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool CanEditMessages { get; set; }

    /// <summary>
    /// <em>Optional</em>. <see langword="true"/>, if the user is allowed to pin messages; for groups and supergroups only
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool CanPinMessages { get; set; }

    /// <summary>
    /// <em>Optional</em>. <see langword="true"/>, if the user is allowed to create, rename, close, and reopen forum topics; for supergroups only
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool CanManageTopics { get; set; }
}

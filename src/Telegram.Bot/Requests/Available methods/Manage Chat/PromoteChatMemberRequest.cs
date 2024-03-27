using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to promote or demote a user in a supergroup or a channel. The bot must be
/// an administrator in the chat for this to work and must have the appropriate admin rights.
/// Pass <see langword="false"/> for all boolean parameters to demote a user. Returns <see langword="true"/> on success.
/// </summary>
public class PromoteChatMemberRequest : RequestBase<bool>, IChatTargetable, IUserTargetable
{
    /// <inheritdoc />
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; init; }

    /// <inheritdoc />
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; init; }

    /// <summary>
    /// Pass <see langword="true"/>, if the administrator's presence in the chat is hidden
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? IsAnonymous { get; set; }

    /// <summary>
    /// Pass <see langword="true"/>, if the administrator can access the chat event log, chat statistics, message
    /// statistics in channels, see channel members, see anonymous administrators in supergroups
    /// and ignore slow mode. Implied by any other administrator privilege
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanManageChat { get; set; }

    /// <summary>
    /// Pass <see langword="true"/>, if the administrator can create channel posts, channels only
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanPostMessages { get; set; }

    /// <summary>
    /// Pass <see langword="true"/>, if the administrator can edit messages of other users and can pin messages,
    /// channels only
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanEditMessages { get; set; }

    /// <summary>
    /// Pass <see langword="true"/>, if the administrator can delete messages of other users
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanDeleteMessages { get; set; }

    /// <summary>
    /// Pass <see langword="true"/> if the administrator can post stories in the channel; channels only
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanPostStories { get; set; }

    /// <summary>
    /// Pass <see langword="true"/> if the administrator can edit stories posted by other users; channels only
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanEditStories { get; set; }

    /// <summary>
    /// Pass <see langword="true"/> if the administrator can delete stories posted by other users; channels only
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanDeleteStories { get; set; }

    /// <summary>
    /// Pass <see langword="true"/>, if the administrator can manage video chats
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanManageVideoChat { get; set; }

    /// <summary>
    /// Pass <see langword="true"/>, if the administrator can restrict, ban or unban chat members
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanRestrictMembers { get; set; }

    /// <summary>
    /// Pass <see langword="true"/>, if the administrator can add new administrators with a subset of their own
    /// privileges or demote administrators that he has promoted, directly or indirectly
    /// (promoted by administrators that were appointed by him)
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanPromoteMembers { get; set; }

    /// <summary>
    /// Pass <see langword="true"/>, if the administrator can change chat title, photo and other settings
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanChangeInfo { get; set; }

    /// <summary>
    /// Pass <see langword="true"/>, if the administrator can invite new users to the chat
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanInviteUsers { get; set; }

    /// <summary>
    /// Pass <see langword="true"/>, if the administrator can pin messages, supergroups only
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanPinMessages { get; set; }

    /// <summary>
    /// Pass <see langword="true"/> if the user is allowed to create, rename, close, and reopen forum topics, supergroups only
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanManageTopics { get; set; }

    /// <summary>
    /// Initializes a new request with chatId and userId
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="userId">Unique identifier of the target user</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public PromoteChatMemberRequest(ChatId chatId, long userId)
        : this()
    {
        ChatId = chatId;
        UserId = userId;
    }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public PromoteChatMemberRequest()
        : base("promoteChatMember")
    { }
}

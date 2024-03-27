using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to set default chat permissions for all members. The bot must be an administrator
/// in the group or a supergroup for this to work and must have the can_restrict_members admin rights.
/// Returns <see langword="true"/> on success.
/// </summary>
public class SetChatPermissionsRequest : RequestBase<bool>, IChatTargetable
{
    /// <inheritdoc />
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; init; }

    /// <summary>
    /// New default chat permissions
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatPermissions Permissions { get; init; }

    /// <summary>
    /// Pass <see langword="true"/> if chat permissions are set independently. Otherwise, the
    /// <see cref="ChatPermissions.CanSendOtherMessages"/>, and <see cref="ChatPermissions.CanAddWebPagePreviews"/>
    /// permissions will imply the <see cref="ChatPermissions.CanSendMessages"/>,
    /// <see cref="ChatPermissions.CanSendAudios"/>, <see cref="ChatPermissions.CanSendDocuments"/>,
    /// <see cref="ChatPermissions.CanSendPhotos"/>, <see cref="ChatPermissions.CanSendVideos"/>,
    /// <see cref="ChatPermissions.CanSendVideoNotes"/>, and <see cref="ChatPermissions.CanSendVoiceNotes"/>
    /// permissions; the <see cref="ChatPermissions.CanSendPolls"/> permission will imply the
    /// <see cref="ChatPermissions.CanSendMessages"/> permission.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? UseIndependentChatPermissions { get; set; }

    /// <summary>
    /// Initializes a new request with chatId and new default permissions
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="permissions">New default chat permissions</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public SetChatPermissionsRequest(ChatId chatId, ChatPermissions permissions)
        : this()
    {
        ChatId = chatId;
        Permissions = permissions;
    }

    /// <summary>
    /// Initializes a new request with chatId and new default permissions
    /// </summary>
    public SetChatPermissionsRequest()
        : base("setChatPermissions")
    { }
}

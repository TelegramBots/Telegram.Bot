using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to set default chat permissions for all members. The bot must be an administrator
/// in the group or a supergroup for this to work and must have the can_restrict_members admin rights.
/// Returns <see langword="true"/> on success.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class SetChatPermissionsRequest : RequestBase<bool>, IChatTargetable
{
    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public ChatId ChatId { get; }

    /// <summary>
    /// New default chat permissions
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public ChatPermissions Permissions { get; }

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
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? UseIndependentChatPermissions { get; set; }

    /// <summary>
    /// Initializes a new request with chatId and new default permissions
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="permissions">New default chat permissions</param>
    public SetChatPermissionsRequest(ChatId chatId, ChatPermissions permissions)
        : base("setChatPermissions")
    {
        ChatId = chatId;
        Permissions = permissions;
    }
}

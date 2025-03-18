// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to unban a previously banned user in a supergroup or channel. The user will <b>not</b> return to the group or channel automatically, but will be able to join via link, etc. The bot must be an administrator for this to work. By default, this method guarantees that after the call the user is not a member of the chat, but will be able to join it. So if the user is a member of the chat they will also be <b>removed</b> from the chat. If you don't want this, use the parameter <see cref="OnlyIfBanned">OnlyIfBanned</see>.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class UnbanChatMemberRequest() : RequestBase<bool>("unbanChatMember"), IChatTargetable, IUserTargetable
{
    /// <summary>Unique identifier for the target group or username of the target supergroup or channel (in the format <c>@channelusername</c>)</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Unique identifier of the target user</summary>
    [JsonPropertyName("user_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>Do nothing if the user is not banned</summary>
    [JsonPropertyName("only_if_banned")]
    public bool OnlyIfBanned { get; set; }
}

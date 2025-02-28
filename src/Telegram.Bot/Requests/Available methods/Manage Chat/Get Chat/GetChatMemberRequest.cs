// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to get information about a member of a chat. The method is only guaranteed to work for other users if the bot is an administrator in the chat.<para>Returns: A <see cref="ChatMember"/> object on success.</para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class GetChatMemberRequest() : RequestBase<ChatMember>("getChatMember"), IChatTargetable, IUserTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target supergroup or channel (in the format <c>@channelusername</c>)</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Unique identifier of the target user</summary>
    [JsonPropertyName("user_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }
}

// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to get the list of boosts added to a chat by a user. Requires administrator rights in the chat.<para>Returns: A <see cref="UserChatBoosts"/> object.</para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class GetUserChatBoostsRequest() : RequestBase<UserChatBoosts>("getUserChatBoosts"), IChatTargetable, IUserTargetable
{
    /// <summary>Unique identifier for the chat or username of the channel (in the format <c>@channelusername</c>)</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Unique identifier of the target user</summary>
    [JsonPropertyName("user_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }
}

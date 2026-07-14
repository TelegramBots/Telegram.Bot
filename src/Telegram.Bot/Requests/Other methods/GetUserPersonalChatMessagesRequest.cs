// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to get the last messages from the personal chat (i.e., the chat currently added to their profile) of a given user.<para>Returns: An Array of <see cref="Message"/> objects is returned.</para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class GetUserPersonalChatMessagesRequest() : RequestBase<Message[]>("getUserPersonalChatMessages"), IUserTargetable
{
    /// <summary>Unique identifier for the target user</summary>
    [JsonPropertyName("user_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>The maximum number of messages to return; 1-20</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int Limit { get; set; }
}

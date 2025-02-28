// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Stores a message that can be sent by a user of a Mini App.<para>Returns: A <see cref="PreparedInlineMessage"/> object.</para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SavePreparedInlineMessageRequest() : RequestBase<PreparedInlineMessage>("savePreparedInlineMessage"), IUserTargetable
{
    /// <summary>Unique identifier of the target user that can use the prepared message</summary>
    [JsonPropertyName("user_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>An object describing the message to be sent</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InlineQueryResult Result { get; set; }

    /// <summary>Pass <see langword="true"/> if the message can be sent to private chats with users</summary>
    [JsonPropertyName("allow_user_chats")]
    public bool AllowUserChats { get; set; }

    /// <summary>Pass <see langword="true"/> if the message can be sent to private chats with bots</summary>
    [JsonPropertyName("allow_bot_chats")]
    public bool AllowBotChats { get; set; }

    /// <summary>Pass <see langword="true"/> if the message can be sent to group and supergroup chats</summary>
    [JsonPropertyName("allow_group_chats")]
    public bool AllowGroupChats { get; set; }

    /// <summary>Pass <see langword="true"/> if the message can be sent to channel chats</summary>
    [JsonPropertyName("allow_channel_chats")]
    public bool AllowChannelChats { get; set; }
}

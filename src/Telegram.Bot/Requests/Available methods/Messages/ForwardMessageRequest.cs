// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to forward messages of any kind. Service messages and messages with protected content can't be forwarded.<para>Returns: The sent <see cref="Message"/> is returned.</para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class ForwardMessageRequest() : RequestBase<Message>("forwardMessage"), IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Unique identifier for the chat where the original message was sent (or channel username in the format <c>@channelusername</c>)</summary>
    [JsonPropertyName("from_chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId FromChatId { get; set; }

    /// <summary>Message identifier in the chat specified in <see cref="FromChatId">FromChatId</see></summary>
    [JsonPropertyName("message_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MessageId { get; set; }

    /// <summary>Unique identifier for the target message thread (topic) of the forum; for forum supergroups only</summary>
    [JsonPropertyName("message_thread_id")]
    public int? MessageThreadId { get; set; }

    /// <summary>New start timestamp for the forwarded video in the message</summary>
    [JsonPropertyName("video_start_timestamp")]
    public int? VideoStartTimestamp { get; set; }

    /// <summary>Sends the message <a href="https://telegram.org/blog/channels-2-0#silent-messages">silently</a>. Users will receive a notification with no sound.</summary>
    [JsonPropertyName("disable_notification")]
    public bool DisableNotification { get; set; }

    /// <summary>Protects the contents of the forwarded message from forwarding and saving</summary>
    [JsonPropertyName("protect_content")]
    public bool ProtectContent { get; set; }
}

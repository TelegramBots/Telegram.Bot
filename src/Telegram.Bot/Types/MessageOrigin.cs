// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object describes the origin of a message. It can be one of<br/><see cref="MessageOriginUser"/>, <see cref="MessageOriginHiddenUser"/>, <see cref="MessageOriginChat"/>, <see cref="MessageOriginChannel"/></summary>
[JsonConverter(typeof(PolymorphicJsonConverter<MessageOrigin>))]
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType(typeof(MessageOriginUser), "user")]
[CustomJsonDerivedType(typeof(MessageOriginHiddenUser), "hidden_user")]
[CustomJsonDerivedType(typeof(MessageOriginChat), "chat")]
[CustomJsonDerivedType(typeof(MessageOriginChannel), "channel")]
public abstract partial class MessageOrigin
{
    /// <summary>Type of the message origin</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public abstract MessageOriginType Type { get; }

    /// <summary>Date the message was sent originally</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime Date { get; set; }
}

/// <summary>The message was originally sent by a known user.</summary>
public partial class MessageOriginUser : MessageOrigin
{
    /// <summary>Type of the message origin, always <see cref="MessageOriginType.User"/></summary>
    public override MessageOriginType Type => MessageOriginType.User;

    /// <summary>User that sent the message originally</summary>
    [JsonPropertyName("sender_user")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public User SenderUser { get; set; } = default!;
}

/// <summary>The message was originally sent by an unknown user.</summary>
public partial class MessageOriginHiddenUser : MessageOrigin
{
    /// <summary>Type of the message origin, always <see cref="MessageOriginType.HiddenUser"/></summary>
    public override MessageOriginType Type => MessageOriginType.HiddenUser;

    /// <summary>Name of the user that sent the message originally</summary>
    [JsonPropertyName("sender_user_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string SenderUserName { get; set; } = default!;
}

/// <summary>The message was originally sent on behalf of a chat to a group chat.</summary>
public partial class MessageOriginChat : MessageOrigin
{
    /// <summary>Type of the message origin, always <see cref="MessageOriginType.Chat"/></summary>
    public override MessageOriginType Type => MessageOriginType.Chat;

    /// <summary>Chat that sent the message originally</summary>
    [JsonPropertyName("sender_chat")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Chat SenderChat { get; set; } = default!;

    /// <summary><em>Optional</em>. For messages originally sent by an anonymous chat administrator, original message author signature</summary>
    [JsonPropertyName("author_signature")]
    public string? AuthorSignature { get; set; }
}

/// <summary>The message was originally sent to a channel chat.</summary>
public partial class MessageOriginChannel : MessageOrigin
{
    /// <summary>Type of the message origin, always <see cref="MessageOriginType.Channel"/></summary>
    public override MessageOriginType Type => MessageOriginType.Channel;

    /// <summary>Channel chat to which the message was originally sent</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Chat Chat { get; set; } = default!;

    /// <summary>Unique message identifier inside the chat</summary>
    [JsonPropertyName("message_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int MessageId { get; set; }

    /// <summary><em>Optional</em>. Signature of the original post author</summary>
    [JsonPropertyName("author_signature")]
    public string? AuthorSignature { get; set; }
}

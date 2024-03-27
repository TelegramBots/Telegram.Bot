using Telegram.Bot.Serialization;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types;

/// <summary>
/// This object describes the origin of a message. It can be one of
/// <list type="bullet">
/// <item><see cref="MessageOriginUser"/></item>
/// <item><see cref="MessageOriginHiddenUser"/></item>
/// <item><see cref="MessageOriginChat"/></item>
/// <item><see cref="MessageOriginChannel"/></item>
/// </list>
/// </summary>
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType<MessageOriginUser>("user")]
[CustomJsonDerivedType<MessageOriginHiddenUser>("hidden_user")]
[CustomJsonDerivedType<MessageOriginChat>("chat")]
[CustomJsonDerivedType<MessageOriginChannel>("channel")]
public abstract class MessageOrigin
{
    /// <summary>
    /// Type of the message origin
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public abstract MessageOriginType Type { get; }

    /// <summary>
    /// Date the message was sent originally
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime Date { get; set; }
}

/// <summary>
/// The message was originally sent by a known user.
/// </summary>
public class MessageOriginUser : MessageOrigin
{
    /// <summary>
    /// Type of the message origin, always <see cref="MessageOriginType.User"/>
    /// </summary>
    public override MessageOriginType Type => MessageOriginType.User;

    /// <summary>
    /// User that sent the message originally
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public User SenderUser { get; set; } = default!;
}

/// <summary>
/// The message was originally sent by an unknown user.
/// </summary>
public class MessageOriginHiddenUser : MessageOrigin
{
    /// <summary>
    /// Type of the message origin, always <see cref="MessageOriginType.HiddenUser"/>
    /// </summary>
    public override MessageOriginType Type => MessageOriginType.HiddenUser;

    /// <summary>
    /// Name of the user that sent the message originally
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string SenderUserName { get; set; } = default!;
}

/// <summary>
/// The message was originally sent on behalf of a chat to a group chat.
/// </summary>
public class MessageOriginChat : MessageOrigin
{
    /// <summary>
    /// Type of the message origin, always <see cref="MessageOriginType.Chat"/>
    /// </summary>
    public override MessageOriginType Type => MessageOriginType.Chat;

    /// <summary>
    /// Chat that sent the message originally
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Chat SenderChat { get; set; } = default!;

    /// <summary>
    /// Optional. For messages originally sent by an anonymous chat administrator,
    /// original message author signature
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? AuthorSignature { get; set; }
}

/// <summary>
/// The message was originally sent to a channel chat.
/// </summary>
public class MessageOriginChannel : MessageOrigin
{
    /// <summary>
    /// Type of the message origin, always <see cref="MessageOriginType.Channel"/>
    /// </summary>
    public override MessageOriginType Type => MessageOriginType.Channel;

    /// <summary>
    /// Channel chat to which the message was originally sent
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Chat Chat { get; set; } = default!;

    /// <summary>
    /// Unique message identifier inside the chat
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int MessageId { get; set; }

    /// <summary>
    /// Optional. Signature of the original post author
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? AuthorSignature { get; set; }
}

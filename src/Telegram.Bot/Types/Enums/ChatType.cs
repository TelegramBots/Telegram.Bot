namespace Telegram.Bot.Types.Enums;

/// <summary>
/// Type of the <see cref="Chat"/>, from which the inline query was sent
/// </summary>
[JsonConverter(typeof(ChatTypeConverter))]
public enum ChatType
{
    /// <summary>
    /// Normal one to one <see cref="Chat"/>
    /// </summary>
    Private = 1,

    /// <summary>
    /// Normal group chat
    /// </summary>
    Group,

    /// <summary>
    /// A channel
    /// </summary>
    Channel,

    /// <summary>
    /// A supergroup
    /// </summary>
    Supergroup,

    /// <summary>
    /// “sender” for a private chat with the inline query sender
    /// </summary>
    Sender
}

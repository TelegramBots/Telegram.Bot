namespace Telegram.Bot.Types.Enums;

/// <summary>Type of the <see cref="Chat"/>, from which the inline query was sent</summary>
[JsonConverter(typeof(EnumConverter<ChatType>))]
public enum ChatType
{
    /// <summary>Normal one-to-one chat with a user or bot</summary>
    Private = 1,
    /// <summary>Normal group chat</summary>
    Group,
    /// <summary>A channel</summary>
    Channel,
    /// <summary>A supergroup</summary>
    Supergroup,
    /// <summary>Value possible only in <see cref="InlineQuery.ChatType"/>: private chat with the inline query sender</summary>
    Sender,
}

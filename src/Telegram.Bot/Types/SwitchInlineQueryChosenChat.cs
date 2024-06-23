namespace Telegram.Bot.Types;

/// <summary>This object represents an inline button that switches the current user to inline mode in a chosen chat, with an optional default inline query.</summary>
public partial class SwitchInlineQueryChosenChat
{
    /// <summary><em>Optional</em>. The default inline query to be inserted in the input field. If left empty, only the bot's username will be inserted</summary>
    public string? Query { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if private chats with users can be chosen</summary>
    public bool AllowUserChats { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if private chats with bots can be chosen</summary>
    public bool AllowBotChats { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if group and supergroup chats can be chosen</summary>
    public bool AllowGroupChats { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if channel chats can be chosen</summary>
    public bool AllowChannelChats { get; set; }
}

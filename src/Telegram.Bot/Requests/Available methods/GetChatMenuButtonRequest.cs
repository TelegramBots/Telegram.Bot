namespace Telegram.Bot.Requests;

/// <summary>Use this method to get the current value of the bot's menu button in a private chat, or the default menu button.<para>Returns: <see cref="MenuButton"/> on success.</para></summary>
public partial class GetChatMenuButtonRequest : RequestBase<MenuButton>
{
    /// <summary>Unique identifier for the target private chat. If not specified, default bot's menu button will be returned</summary>
    public long? ChatId { get; set; }

    /// <summary>Instantiates a new <see cref="GetChatMenuButtonRequest"/></summary>
    public GetChatMenuButtonRequest() : base("getChatMenuButton") { }
}

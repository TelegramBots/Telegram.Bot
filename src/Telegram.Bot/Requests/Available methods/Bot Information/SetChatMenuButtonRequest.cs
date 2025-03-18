// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to change the bot's menu button in a private chat, or the default menu button.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SetChatMenuButtonRequest() : RequestBase<bool>("setChatMenuButton")
{
    /// <summary>Unique identifier for the target private chat. If not specified, default bot's menu button will be changed</summary>
    [JsonPropertyName("chat_id")]
    public long? ChatId { get; set; }

    /// <summary>An object for the bot's new menu button. Defaults to <see cref="MenuButtonDefault"/></summary>
    [JsonPropertyName("menu_button")]
    public MenuButton? MenuButton { get; set; }
}

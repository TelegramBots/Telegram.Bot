// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to get the current value of the bot's menu button in a private chat, or the default menu button.<para>Returns: <see cref="MenuButton"/> on success.</para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class GetChatMenuButtonRequest() : RequestBase<MenuButton>("getChatMenuButton")
{
    /// <summary>Unique identifier for the target private chat. If not specified, default bot's menu button will be returned</summary>
    [JsonPropertyName("chat_id")]
    public long? ChatId { get; set; }
}

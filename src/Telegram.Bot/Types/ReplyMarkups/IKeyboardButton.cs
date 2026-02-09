
namespace Telegram.Bot.Types.ReplyMarkups;

/// <summary>Marker interface for a regular or inline button of the reply keyboard</summary>
public interface IKeyboardButton
{
    /// <summary>Text of the button. If none of the optional fields are used, it will be sent as a message when the button is pressed</summary>
    string Text { get; }

    /// <summary><em>Optional</em>. Unique identifier of the custom emoji shown before the text of the button. Can only be used by bots that purchased additional usernames on <a href="https://fragment.com">Fragment</a> or in the messages directly sent by the bot to private, group and supergroup chats if the owner of the bot has a Telegram Premium subscription.</summary>
    public string? IconCustomEmojiId { get; set; }

    /// <summary><em>Optional</em>. Style of the button. Must be one of <see cref="KeyboardButtonStyle.Danger">Danger</see> (red), <see cref="KeyboardButtonStyle.Success">Success</see> (green) or <see cref="KeyboardButtonStyle.Primary">Primary</see> (blue). If omitted, then an app-specific style is used.</summary>
    public KeyboardButtonStyle? Style { get; set; }
}

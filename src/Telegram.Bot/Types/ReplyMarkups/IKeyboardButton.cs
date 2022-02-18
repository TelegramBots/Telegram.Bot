namespace Telegram.Bot.Types.ReplyMarkups;

/// <summary>
/// Marker interface for a regular or inline button of the reply keyboard
/// </summary>
public interface IKeyboardButton
{
    /// <summary>
    /// Text of the button. If none of the optional fields are used, it will be sent as a message when the button is pressed
    /// </summary>
    string Text { get; set; }
}
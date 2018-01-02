namespace Telegram.Bot.Types.ReplyMarkups.Buttons
{
    /// <summary>
    /// This interface represents one button of the reply keyboard (regular or inline)
    /// </summary>
    public interface IKeyboardButton
    {
        /// <summary>
        /// Label text on the button
        /// </summary>
        string Text { get; set; }
    }
}

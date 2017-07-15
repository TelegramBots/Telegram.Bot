using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents one button of an inline keyboard that opens a game when pressed
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class InlineKeyboardCallbackGameButton : InlineKeyboardButton
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardButton"/> class.
        /// </summary>
        /// <param name="text">Text of the button</param>
        /// <param name="callbackGame">Description of the game that will be launched when the user presses the button.</param>
        public InlineKeyboardCallbackGameButton(string text, CallbackGame callbackGame) : base(text)
        {
            CallbackGame = callbackGame;
        }
    }
}

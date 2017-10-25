using Newtonsoft.Json;

namespace Telegram.Bot.Types.InlineKeyboardButtons
{
    /// <summary>
    /// This object represents one button of an inline keyboard that opens a game when pressed
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class InlineKeyboardCallbackGameButton : InlineKeyboardButton
    {
        /// <summary>
        /// Optimal. Description of the game that will be launched when the user presses the button.
        /// </summary>
        /// <remarks>
        /// Note: This type of button must always be the first button in the first row.
        /// </remarks>
        [JsonProperty]
        public CallbackGame CallbackGame { get; set; }

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

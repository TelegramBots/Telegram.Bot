using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types.ReplyMarkups.Buttons
{
    /// <summary>
    /// This object represents one button of an inline keyboard that opens a game when pressed
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class InlineKeyboardCallbackGameButton : InlineKeyboardButton
    {
        /// <summary>
        /// Optimal. Description of the game that will be launched when the user presses the button.
        /// </summary>
        /// <remarks>
        /// Note: This type of button must always be the first button in the first row.
        /// </remarks>
        [JsonProperty(Required = Required.Always)]
        public CallbackGame CallbackGame { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardCallbackGameButton"/> class.
        /// </summary>
        /// <param name="text">Label text on the button</param>
        /// <param name="callbackGame">Description of the game that will be launched when the user presses the button.</param>
        public InlineKeyboardCallbackGameButton(string text, CallbackGame callbackGame)
            : base(text)
        {
            CallbackGame = callbackGame;
        }
    }
}

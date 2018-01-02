using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types.ReplyMarkups.Buttons
{
    /// <summary>
    /// This object represents one button of an inline keyboard that ask the user to pay a certain amount when pressed. This button always MUST be the first button in a row.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn,
        NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class InlineKeyboardPayButton : InlineKeyboardButton
    {
        /// <summary>
        /// Optional. Specify True, to send a Pay button.
        /// </summary>
        /// <remarks>
        /// Note: This type of button must always be the first button in the first row.
        /// </remarks>
        [JsonProperty]
        public bool Pay => true;

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardPayButton"/> class.
        /// </summary>
        /// <param name="text">Label text on the button</param>
        public InlineKeyboardPayButton(string text)
            : base(text) { }
    }
}

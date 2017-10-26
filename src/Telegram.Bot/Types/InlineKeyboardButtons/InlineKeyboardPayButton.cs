using Newtonsoft.Json;

namespace Telegram.Bot.Types.InlineKeyboardButtons
{
    /// <summary>
    /// This object represents one button of an inline keyboard that ask the user to pay a certain amount when pressed. This button always MUST be the first button in a row.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class InlineKeyboardPayButton : InlineKeyboardButton
    {
        /// <summary>
        /// Optional. Specify True, to send a Pay button.
        /// </summary>
        /// <remarks>
        /// Note: This type of button must always be the first button in the first row.
        /// </remarks>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public bool Pay { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardButton"/> class.
        /// </summary>
        /// <param name="text">The text on the button.</param>
        /// <param name="pay">Specify true to send a pay button.</param>
        public InlineKeyboardPayButton(string text, bool pay) : base(text)
        {
            Pay = pay;
        }
    }
}

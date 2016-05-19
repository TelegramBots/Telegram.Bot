using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents an inline keyboard that appears right next to the message it belongs to.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class InlineKeyboardMarkup : IReplyMarkup
    {
        /// <summary>
        /// Array of button rows, each represented by an Array of InlineKeyboardButton objects
        /// </summary>
        [JsonProperty("inline_keyboard", Required = Required.Always)]
        public InlineKeyboardButton[][] InlineKeyboard { get; set; }

        public InlineKeyboardMarkup() { }

        public InlineKeyboardMarkup(InlineKeyboardButton[] inlineKeyboardRow)
        {
            InlineKeyboard = new[]
            {
                inlineKeyboardRow
            };
        }

        public InlineKeyboardMarkup(InlineKeyboardButton[][] inlineKeyboard)
        {
            InlineKeyboard = inlineKeyboard;
        }
    }
}

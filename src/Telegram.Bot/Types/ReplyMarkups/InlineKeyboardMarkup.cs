using Newtonsoft.Json;
using Telegram.Bot.Types.InlineKeyboardButtons;

namespace Telegram.Bot.Types.ReplyMarkups
{
    /// <summary>
    /// This object represents an inline keyboard that appears right next to the <see cref="Message"/> it belongs to.
    /// </summary>
    /// <remarks>
    /// Inline keyboards are currently being tested and are not available in channels yet. For now, feel free to use them in one-on-one chats or groups.
    /// </remarks>
    [JsonObject(MemberSerialization.OptIn)]
    public class InlineKeyboardMarkup : IReplyMarkup
    {
        /// <summary>
        /// Array of <see cref="InlineKeyboardButton"/> rows, each represented by an Array of <see cref="InlineKeyboardButton"/>.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public InlineKeyboardButton[][] InlineKeyboard { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardMarkup"/> class.
        /// </summary>
        public InlineKeyboardMarkup() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardMarkup"/> class.
        /// </summary>
        /// <param name="inlineKeyboardRow">The inline keyboard row.</param>
        public InlineKeyboardMarkup(InlineKeyboardButton[] inlineKeyboardRow)
        {
            InlineKeyboard = new[]
            {
                inlineKeyboardRow
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardMarkup"/> class.
        /// </summary>
        /// <param name="inlineKeyboard">The inline keyboard.</param>
        public InlineKeyboardMarkup(InlineKeyboardButton[][] inlineKeyboard)
        {
            InlineKeyboard = inlineKeyboard;
        }
    }
}

using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents one button of an inline keyboard that asks the user to switch to a chat 
    /// and starts an inline query there
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class InlineKeyboardSwitchCallbackQueryButton : InlineKeyboardButton
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardButton"/> class.
        /// </summary>
        /// <param name="text">Text of the button</param>
        /// <param name="switchInlineQuery">Pressing the button will prompt the user to select one of their chats, open that chat and insert the bot‘s username and the specified inline query in the input field. Can be empty, in which case just the bot’s username will be inserted.</param>
        public InlineKeyboardSwitchCallbackQueryButton(string text, string switchInlineQuery = "") : base(text)
        {
            SwitchInlineQuery = switchInlineQuery;
        }
    }
}

using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents one button of an inline keyboard. Pressing the button will insert the bot‘s username and the specified inline query in the current chat's input field.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class InlineKeyboardSwitchCallbackQueryCurrentChatButton : InlineKeyboardButton
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardButton"/> class.
        /// </summary>
        /// <param name="text">Text of the button</param>
        /// <param name="switchInlineQueryCurrentChat">Pressing the button will insert the bot‘s username and the specified inline query in the current chat's input field. Can be empty, in which case only the bot’s username will be inserted.</param>
        public InlineKeyboardSwitchCallbackQueryCurrentChatButton(string text, string switchInlineQueryCurrentChat = null) : base(text)
        {
            SwitchInlineQueryCurrentChat = switchInlineQueryCurrentChat;
        }
    }
}

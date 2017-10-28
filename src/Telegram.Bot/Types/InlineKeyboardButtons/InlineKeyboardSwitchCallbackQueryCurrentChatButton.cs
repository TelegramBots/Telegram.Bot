using Newtonsoft.Json;

namespace Telegram.Bot.Types.InlineKeyboardButtons
{
    /// <summary>
    /// This object represents one button of an inline keyboard. Pressing the button will insert the bot‘s username and the specified inline query in the current chat's input field.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class InlineKeyboardSwitchInlineQueryCurrentChatButton : InlineKeyboardButton
    {
        /// <summary>
        /// Optional. If set, pressing the button will insert the bot‘s username and the specified inline query in the current chat's input field. Can be empty, in which case only the bot’s username will be inserted.
        /// </summary>
        /// <remarks>
        /// Note: This offers a quick way for the user to open your bot in inline mode in the same chat – good for selecting something from multiple options.
        /// </remarks>
        [JsonProperty]
        public string SwitchInlineQueryCurrentChat { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardButton"/> class.
        /// </summary>
        /// <param name="text">Text of the button</param>
        /// <param name="switchInlineQueryCurrentChat">Pressing the button will insert the bot‘s username and the specified inline query in the current chat's input field. Can be empty, in which case only the bot’s username will be inserted.</param>
        public InlineKeyboardSwitchInlineQueryCurrentChatButton(string text, string switchInlineQueryCurrentChat = null) : base(text)
        {
            SwitchInlineQueryCurrentChat = switchInlineQueryCurrentChat;
        }
    }
}

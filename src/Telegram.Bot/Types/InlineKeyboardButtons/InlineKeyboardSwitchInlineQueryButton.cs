using Newtonsoft.Json;

namespace Telegram.Bot.Types.InlineKeyboardButtons
{
    /// <summary>
    /// This object represents one button of an inline keyboard that asks the user to switch to a chat
    /// and starts an inline query there
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class InlineKeyboardSwitchInlineQueryButton : InlineKeyboardButton
    {
        /// <summary>
        /// Optional. If set, pressing the button will prompt the user to select one of their chats, open that chat and insert the bot's username and the specified <see cref="InlineQuery"/> in the input field. Can be empty, in which case just the bot's username will be inserted.
        /// </summary>
        /// <remarks>
        /// Note: This offers an easy way for users to start using your bot in inline mode when they are currently in a private chat with it. Especially useful when combined with switchPm[...] parameters (see <see cref="TelegramBotClient.AnswerInlineQueryAsync"/>)  – in this case the user will be automatically returned to the chat they switched from, skipping the chat selection screen.
        /// </remarks>
        [JsonProperty]
        public string SwitchInlineQuery { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardButton"/> class.
        /// </summary>
        /// <param name="text">Text of the button</param>
        /// <param name="switchInlineQuery">Pressing the button will prompt the user to select one of their chats, open that chat and insert the bot‘s username and the specified inline query in the input field. Can be empty, in which case just the bot’s username will be inserted.</param>
        public InlineKeyboardSwitchInlineQueryButton(string text, string switchInlineQuery = "") : base(text)
        {
            SwitchInlineQuery = switchInlineQuery;
        }
    }
}

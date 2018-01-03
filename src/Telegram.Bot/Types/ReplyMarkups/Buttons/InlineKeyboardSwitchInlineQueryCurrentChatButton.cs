using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types.ReplyMarkups.Buttons
{
    /// <summary>
    /// This object represents one button of an inline keyboard. Pressing the button will insert the bot‘s username and the specified inline query in the current chat's input field.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class InlineKeyboardSwitchInlineQueryCurrentChatButton : InlineKeyboardButton
    {
        /// <summary>
        /// Optional. If set, pressing the button will insert the bot‘s username and the specified inline query in the current chat's input field. Can be empty, in which case only the bot’s username will be inserted.
        /// </summary>
        /// <remarks>
        /// Note: This offers a quick way for the user to open your bot in inline mode in the same chat – good for selecting something from multiple options.
        /// </remarks>
        [JsonProperty(Required = Required.Always)]
        public string SwitchInlineQueryCurrentChat { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardSwitchInlineQueryButton"/> class
        /// </summary>
        /// <param name="query">Inline query that will be inserted with bot's username in the current chat. Can be empty, in which case only the bot’s username will be inserted.</param>
        /// <param name="text">Label text on the button</param>
        public InlineKeyboardSwitchInlineQueryCurrentChatButton(string text, string query = "")
            : base(text)
        {
            SwitchInlineQueryCurrentChat = query;
        }
    }
}

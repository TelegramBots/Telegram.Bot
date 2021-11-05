using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types.ReplyMarkups
{
    /// <summary>
    /// This object represents one button of an inline keyboard.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class InlineKeyboardButton : IKeyboardButton
    {
        /// <inheritdoc />
        [JsonProperty(Required = Required.Always)]
        public string Text { get; set; }

        /// <summary>
        /// Optional. HTTP or tg:// url to be opened when button is pressed
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string? Url { get; set; }

        /// <summary>
        /// Optional. An HTTP URL used to automatically authorize the user. Can be used as a replacement for the <see href="https://core.telegram.org/widgets/login">Telegram Login Widget</see>.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public LoginUrl? LoginUrl { get; set; }

        /// <summary>
        /// Optional. Data to be sent in a <see cref="CallbackQuery">callback query</see> to the bot when button is pressed, 1-64 bytes
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string? CallbackData { get; set; }

        /// <summary>
        /// Optional. If set, pressing the button will prompt the user to select one of their chats, open that chat and insert the bot's username and the specified inline query in the input field. Can be empty, in which case just the bot's username will be inserted.
        /// </summary>
        /// <remarks>
        /// Note: This offers an easy way for users to start using your bot in <see href="https://core.telegram.org/bots/inline">inline mode</see> when they are currently in a private chat with it. Especially useful when combined with <see cref="Requests.AnswerInlineQueryRequest">SwitchPm…</see> actions – in this case the user will be automatically returned to the chat they switched from, skipping the chat selection screen.
        /// </remarks>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string? SwitchInlineQuery { get; set; }

        /// <summary>
        /// Optional. If set, pressing the button will insert the bot's username and the specified inline query in the current chat's input field. Can be empty, in which case only the bot's username will be inserted.
        /// </summary>
        /// <remarks>
        /// This offers a quick way for the user to open your bot in inline mode in the same chat – good for selecting something from multiple options.
        /// </remarks>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string? SwitchInlineQueryCurrentChat { get; set; }

        /// <summary>
        /// Optional. Description of the game that will be launched when the user presses the button.
        /// </summary>
        /// <remarks>
        /// <b>NOTE:</b> This type of button <b>must</b> always be the first button in the first row.
        /// </remarks>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public CallbackGame? CallbackGame { get; set; }

        /// <summary>
        /// Optional. Specify True, to send a <see href="https://core.telegram.org/bots/api#payments">Pay button</see>.
        /// </summary>
        /// <remarks>
        /// <b>NOTE:</b> This type of button <b>must</b> always be the first button in the first row.
        /// </remarks>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? Pay { get; set; }

        /// <summary>
        /// Instantiates new Inline Keyboard object
        /// </summary>
        /// <param name="text">Label text on the button</param>
        [JsonConstructor]
        public InlineKeyboardButton(string text)
        {
            Text = text;
        }

        /// <summary>
        /// Creates an inline keyboard button that opens a HTTP url when pressed
        /// </summary>
        /// <param name="text">Label text on the button</param>
        /// <param name="url">HTTP or tg:// url to be opened when button is pressed</param>
        public static InlineKeyboardButton WithUrl(string text, string url) =>
            new(text) { Url = url };

        /// <summary>
        /// Creates an inline keyboard button that opens a HTTP url to automatically authorize the user
        /// </summary>
        /// <param name="text">Label text on the button</param>
        /// <param name="loginUrl">An HTTP URL used to automatically authorize the user. Can be used as a replacement for the <see href="https://core.telegram.org/widgets/login">Telegram Login Widget</see>.</param>
        /// <returns></returns>
        public static InlineKeyboardButton WithLoginUrl(string text, LoginUrl loginUrl) =>
            new(text) { LoginUrl = loginUrl };

        /// <summary>
        /// Creates an inline keyboard button that sends <see cref="CallbackQuery"/> to bot when pressed
        /// </summary>
        /// <param name="textAndCallbackData">Text and data of the button to be sent in a <see cref="CallbackQuery">callback query</see> to the bot when button is pressed, 1-64 bytes</param>
        public static InlineKeyboardButton WithCallbackData(string textAndCallbackData) =>
            new(textAndCallbackData) { CallbackData = textAndCallbackData };

        /// <summary>
        /// Creates an inline keyboard button that sends <see cref="CallbackQuery"/> to bot when pressed
        /// </summary>
        /// <param name="text">Label text on the button</param>
        /// <param name="callbackData">Data to be sent in a <see cref="CallbackQuery">callback query</see> to the bot when button is pressed, 1-64 bytes</param>
        public static InlineKeyboardButton WithCallbackData(string text, string callbackData) =>
            new(text) { CallbackData = callbackData };

        /// <summary>
        /// Creates an inline keyboard button. Pressing the button will prompt the user to select one of their chats, open that chat and insert the bot's username and the specified inline query in the input field.
        /// </summary>
        /// <param name="text">Label text on the button</param>
        /// <param name="query">If set, pressing the button will prompt the user to select one of their chats, open that chat and insert the bot's username and the specified inline query in the input field. Can be empty, in which case just the bot's username will be inserted.</param>
        /// <returns></returns>
        public static InlineKeyboardButton WithSwitchInlineQuery(string text, string query = "") =>
            new(text) { SwitchInlineQuery = query };

        /// <summary>
        /// Creates an inline keyboard button. Pressing the button will insert the bot's username and the specified inline query in the current chat's input field.
        /// </summary>
        /// <param name="text">Label text on the button</param>
        /// <param name="query">If set, pressing the button will insert the bot's username and the specified inline query in the current chat's input field. Can be empty, in which case only the bot's username will be inserted.</param>
        public static InlineKeyboardButton WithSwitchInlineQueryCurrentChat(string text, string query = "") =>
            new(text) { SwitchInlineQueryCurrentChat = query };

        /// <summary>
        /// Creates an inline keyboard button. Pressing the button will launch the game.
        /// </summary>
        /// <param name="text">Label text on the button</param>
        /// <param name="callbackGame">Description of the game that will be launched when the user presses the button.</param>
        public static InlineKeyboardButton WithCallBackGame(string text, CallbackGame? callbackGame = default) =>
            new(text) { CallbackGame = callbackGame ?? new() };

        /// <summary>
        /// Creates an inline keyboard button for a PayButton
        /// </summary>
        /// <param name="text">Label text on the button</param>
        public static InlineKeyboardButton WithPayment(string text) =>
            new(text) { Pay = true };

        /// <summary>
        /// Performs an implicit conversion from <see cref="string"/> to <see cref="InlineKeyboardButton"/> with callback data
        /// </summary>
        /// <param name="textAndCallbackData">Label text and callback data of the button</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator InlineKeyboardButton?(string? textAndCallbackData) =>
            textAndCallbackData is null
                ? default
                : WithCallbackData(textAndCallbackData);
    }
}

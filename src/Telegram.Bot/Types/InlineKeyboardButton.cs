using Newtonsoft.Json;
using System;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents one button of an inline keyboard. You must use exactly one of the optional fields.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class InlineKeyboardButton
    {
        /// <summary>
        /// Label text on the button
        /// </summary>
        [JsonProperty(PropertyName = "text", Required = Required.Always)]
        public string Text { get; set; }

        /// <summary>
        /// Optional. HTTP url to be opened when button is pressed
        /// </summary>
        [JsonProperty(PropertyName = "url", Required = Required.Default, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string Url { get; set; }

        /// <summary>
        /// Optional. Data to be sent in a callback query to the bot when button is pressed
        /// </summary>
        [JsonProperty(PropertyName = "callback_data", Required = Required.Default, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string CallbackData { get; set; }

        /// <summary>
        /// Optional. If set, pressing the button will prompt the user to select one of their chats, open that chat and insert the bot's username and the specified <see cref="InlineQuery"/> in the input field. Can be empty, in which case just the bot's username will be inserted.
        /// </summary>
        /// <remarks>
        /// Note: This offers an easy way for users to start using your bot in inline mode when they are currently in a private chat with it. Especially useful when combined with switchPm[...] parameters (see <see cref="TelegramBotClient.AnswerInlineQueryAsync"/>)  – in this case the user will be automatically returned to the chat they switched from, skipping the chat selection screen.
        /// </remarks>
        [JsonProperty(PropertyName = "switch_inline_query", Required = Required.Default, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string SwitchInlineQuery { get; set; }

        /// <summary>
        /// Optional. If set, pressing the button will insert the bot‘s username and the specified inline query in the current chat's input field. Can be empty, in which case only the bot’s username will be inserted.
        /// </summary>
        /// <remarks>
        /// Note: This offers a quick way for the user to open your bot in inline mode in the same chat – good for selecting something from multiple options.
        /// </remarks>
        [JsonProperty(PropertyName = "switch_inline_query_current_chat", Required = Required.Default, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string SwitchInlineQueryCurrentChat { get; set; }

        /// <summary>
        /// Optimal. Description of the game that will be launched when the user presses the button.
        /// </summary>
        /// <remarks>
        /// Note: This type of button must always be the first button in the first row.
        /// </remarks>
        [JsonProperty("callback_game", Required = Required.Default, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public CallbackGame CallbackGame { get; set; }

        /// <summary>
        /// Optional. Specify True, to send a Pay button.
        /// </summary>
        /// <remarks>
        /// Note: This type of button must always be the first button in the first row.
        /// </remarks>
        [JsonProperty("pay", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public bool Pay { get; set; }

        /// <summary>
        /// Performs an implicit conversion from <see cref="string"/> to <see cref="InlineKeyboardButton"/>.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator InlineKeyboardButton(string key) => new InlineKeyboardButton(key, key);

        /// <summary>
        /// Performs an implicit conversion from <see cref="KeyboardButton"/> to <see cref="InlineKeyboardButton"/>.
        /// </summary>
        /// <param name="button">The <see cref="KeyboardButton"/></param>
        public static implicit operator InlineKeyboardButton(KeyboardButton button) => new InlineKeyboardButton(button.Text, button.Text);

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardButton"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="callbackData">The callback data.</param>
        public InlineKeyboardButton(string text, string callbackData) : this(text)
        {
            CallbackData = callbackData;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardButton"/> class.
        /// </summary>
        /// <param name="text">Text of the button</param>
        /// <param name="callbackGame"></param>
        public InlineKeyboardButton(string text, CallbackGame callbackGame) : this(text)
        {
            CallbackGame = callbackGame;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardButton"/> class.
        /// </summary>
        /// <param name="text">Text of the button</param>
        private InlineKeyboardButton(string text)
        {
            Text = text;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardButton"/> class.
        /// </summary>
        /// <param name="text">Text of the button</param>
        /// <param name="url">HTTP url to be opened when button is pressed</param>
        public static InlineKeyboardButton WithUrl(string text, string url)
            => new InlineKeyboardButton(text) { Url = url };

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardButton"/> class.
        /// </summary>
        /// <param name="text">Text of the button</param>
        /// <param name="url">HTTP url to be opened when button is pressed</param>
        public static InlineKeyboardButton WithUrl(string text, Uri url)
            => new InlineKeyboardButton(text) { Url = url.AbsoluteUri };

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardButton"/> class.
        /// </summary>
        /// <param name="textAndCallbackData">Text of the button and data to be sent in a <see cref="CallbackQuery"/> to the bot when button is pressed</param>
        public static InlineKeyboardButton WithCallbackData(string textAndCallbackData)
            => new InlineKeyboardButton(textAndCallbackData, textAndCallbackData);

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardButton"/> class.
        /// </summary>
        /// <param name="text">Text of the button</param>
        /// <param name="callbackData">Data to be sent in a <see cref="CallbackQuery"/> to the bot when button is pressed, 1-64 bytes</param>
        public static InlineKeyboardButton WithCallbackData(string text, string callbackData)
            => new InlineKeyboardButton(text, callbackData);

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardButton"/> class.
        /// </summary>
        /// <param name="text">Text of the button</param>
        /// <param name="switchInlineQuery">Pressing the button will prompt the user to select one of their chats, open that chat and insert the bot‘s username and the specified inline query in the input field. Can be empty, in which case just the bot’s username will be inserted.</param>
        /// <returns></returns>
        public static InlineKeyboardButton WithSwitchInlineQuery(string text, string switchInlineQuery = "")
            => new InlineKeyboardButton(text) { SwitchInlineQuery = switchInlineQuery };

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardButton"/> class.
        /// </summary>
        /// <param name="text">Text of the button</param>
        /// <param name="switchInlineQueryCurrentChat">Pressing the button will insert the bot‘s username and the specified inline query in the current chat's input field. Can be empty, in which case only the bot’s username will be inserted.</param>
        public static InlineKeyboardButton WithSwitchInlineQueryCurrentChat(string text, string switchInlineQueryCurrentChat = "")
            => new InlineKeyboardButton(text) { SwitchInlineQueryCurrentChat = switchInlineQueryCurrentChat };

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardButton"/> class.
        /// </summary>
        /// <param name="text">Text of the button</param>
        /// <param name="callbackGame">Description of the game that will be launched when the user presses the button.</param>
        public static InlineKeyboardButton WithCallBackGame(string text, CallbackGame callbackGame)
            => new InlineKeyboardButton(text, callbackGame);
    }
}

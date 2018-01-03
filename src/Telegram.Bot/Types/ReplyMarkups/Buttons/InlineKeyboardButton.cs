using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types.ReplyMarkups.Buttons
{
    /// <summary>
    /// This object represents one button of an inline keyboard.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public abstract class InlineKeyboardButton : IKeyboardButton
    {
        /// <inheritdoc />
        [JsonProperty(Required = Required.Always)]
        public string Text { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardButton"/> class.
        /// </summary>
        /// <param name="text">Label text on the button</param>
        protected InlineKeyboardButton(string text)
        {
            Text = text;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardUrlButton"/> class.
        /// </summary>
        /// <param name="text">Label text on the button</param>
        /// <param name="url">HTTP url to be opened when button is pressed</param>
        public static InlineKeyboardButton WithUrl(string text, string url)
            => new InlineKeyboardUrlButton(text, url);

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardUrlButton"/> class.
        /// </summary>
        /// <param name="text">Label text on the button</param>
        /// <param name="url">HTTP url to be opened when button is pressed</param>
        public static InlineKeyboardButton WithUrl(string text, Uri url)
            => new InlineKeyboardUrlButton(text, url);

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardCallbackButton"/> class.
        /// </summary>
        /// <param name="textAndCallbackData">Text of the button and data to be sent in a <see cref="CallbackQuery"/> to the bot when button is pressed</param>
        public static InlineKeyboardButton WithCallbackData(string textAndCallbackData)
            => new InlineKeyboardCallbackButton(textAndCallbackData, textAndCallbackData);

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardCallbackButton"/> class.
        /// </summary>
        /// <param name="text">Label text on the button</param>
        /// <param name="callbackData">Data to be sent in a <see cref="CallbackQuery"/> to the bot when button is pressed, 1-64 bytes</param>
        public static InlineKeyboardButton WithCallbackData(string text, string callbackData)
            => new InlineKeyboardCallbackButton(text, callbackData);

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardSwitchInlineQueryButton"/> class.
        /// </summary>
        /// <param name="text">Label text on the button</param>
        /// <param name="query">Pressing the button will prompt the user to select one of their chats, open that chat and insert the bot‘s username and the specified inline query in the input field. Can be empty, in which case just the bot's username will be inserted.</param>
        /// <returns></returns>
        public static InlineKeyboardButton WithSwitchInlineQuery(string text, string query = default) =>
            query is default
                ? new InlineKeyboardSwitchInlineQueryButton(text)
                : new InlineKeyboardSwitchInlineQueryButton(text, query);

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardSwitchInlineQueryCurrentChatButton"/> class.
        /// </summary>
        /// <param name="text">Label text on the button</param>
        /// <param name="query">Pressing the button will insert the bot‘s username and the specified inline query in the current chat's input field. Can be empty, in which case only the bot's username will be inserted.</param>
        public static InlineKeyboardButton WithSwitchInlineQueryCurrentChat(string text, string query = default) =>
            query is default
                ? new InlineKeyboardSwitchInlineQueryCurrentChatButton(text)
                : new InlineKeyboardSwitchInlineQueryCurrentChatButton(text, query);

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardCallbackGameButton"/> class.
        /// </summary>
        /// <param name="text">Label text on the button</param>
        /// <param name="callbackGame">Description of the game that will be launched when the user presses the button.</param>
        public static InlineKeyboardButton WithCallBackGame(string text, CallbackGame callbackGame)
            => new InlineKeyboardCallbackGameButton(text, callbackGame);

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardPayButton"/> class.
        /// </summary>
        /// <param name="text">Label text on the button</param>
        public static InlineKeyboardButton WithPayment(string text)
            => new InlineKeyboardPayButton(text);

        /// <summary>
        /// Performs an implicit conversion from <see cref="string"/> to <see cref="InlineKeyboardCallbackButton"/>.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator InlineKeyboardButton(string key)
            => new InlineKeyboardCallbackButton(key, key);

        /// <summary>
        /// Performs an implicit conversion from <see cref="KeyboardButton"/> to <see cref="InlineKeyboardCallbackButton"/>.
        /// </summary>
        /// <param name="button">The <see cref="KeyboardButton"/></param>
        public static implicit operator InlineKeyboardButton(KeyboardButton button)
            => new InlineKeyboardCallbackButton(button.Text, button.Text);
    }
}

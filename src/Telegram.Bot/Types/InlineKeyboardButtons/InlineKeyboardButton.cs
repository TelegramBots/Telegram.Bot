using Newtonsoft.Json;
using System;

namespace Telegram.Bot.Types.InlineKeyboardButtons
{
    /// <summary>
    /// This object represents one button of an inline keyboard.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class InlineKeyboardButton
    {
        /// <summary>
        /// Label text on the button
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Text { get; set; }

        /// <summary>
        /// Performs an implicit conversion from <see cref="string"/> to <see cref="InlineKeyboardButton"/>.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator InlineKeyboardButton(string key) => new InlineKeyboardCallbackButton(key, key);

        /// <summary>
        /// Performs an implicit conversion from <see cref="KeyboardButton"/> to <see cref="InlineKeyboardButton"/>.
        /// </summary>
        /// <param name="button">The <see cref="KeyboardButton"/></param>
        public static implicit operator InlineKeyboardButton(KeyboardButton button) => new InlineKeyboardCallbackButton(button.Text, button.Text);

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardButton"/> class.
        /// </summary>
        /// <param name="text">Text of the button</param>
        protected InlineKeyboardButton(string text)
        {
            Text = text;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardButton"/> class.
        /// </summary>
        /// <param name="text">Text of the button</param>
        /// <param name="url">HTTP url to be opened when button is pressed</param>
        public static InlineKeyboardButton WithUrl(string text, string url)
            => new InlineKeyboardUrlButton(text, url);

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardButton"/> class.
        /// </summary>
        /// <param name="text">Text of the button</param>
        /// <param name="url">HTTP url to be opened when button is pressed</param>
        public static InlineKeyboardButton WithUrl(string text, Uri url)
            => new InlineKeyboardUrlButton(text, url);

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardButton"/> class.
        /// </summary>
        /// <param name="textAndCallbackData">Text of the button and data to be sent in a <see cref="CallbackQuery"/> to the bot when button is pressed</param>
        public static InlineKeyboardButton WithCallbackData(string textAndCallbackData)
            => new InlineKeyboardCallbackButton(textAndCallbackData, textAndCallbackData);

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardButton"/> class.
        /// </summary>
        /// <param name="text">Text of the button</param>
        /// <param name="callbackData">Data to be sent in a <see cref="CallbackQuery"/> to the bot when button is pressed, 1-64 bytes</param>
        public static InlineKeyboardButton WithCallbackData(string text, string callbackData)
            => new InlineKeyboardCallbackButton(text, callbackData);

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardButton"/> class.
        /// </summary>
        /// <param name="text">Text of the button</param>
        /// <param name="switchInlineQuery">Pressing the button will prompt the user to select one of their chats, open that chat and insert the bot‘s username and the specified inline query in the input field. Can be empty, in which case just the bot’s username will be inserted.</param>
        /// <returns></returns>
        public static InlineKeyboardButton WithSwitchInlineQuery(string text, string switchInlineQuery = "")
            => new InlineKeyboardSwitchInlineQueryButton(text, switchInlineQuery);

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardButton"/> class.
        /// </summary>
        /// <param name="text">Text of the button</param>
        /// <param name="switchInlineQueryCurrentChat">Pressing the button will insert the bot‘s username and the specified inline query in the current chat's input field. Can be empty, in which case only the bot’s username will be inserted.</param>
        public static InlineKeyboardButton WithSwitchInlineQueryCurrentChat(string text, string switchInlineQueryCurrentChat = "")
            => new InlineKeyboardSwitchInlineQueryCurrentChatButton(text, switchInlineQueryCurrentChat);

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardButton"/> class.
        /// </summary>
        /// <param name="text">Text of the button</param>
        /// <param name="callbackGame">Description of the game that will be launched when the user presses the button.</param>
        public static InlineKeyboardButton WithCallBackGame(string text, CallbackGame callbackGame)
            => new InlineKeyboardCallbackGameButton(text, callbackGame);

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardButton"/> class.
        /// </summary>
        /// <param name="text">The text on the button.</param>
        /// <param name="pay">Specify true to send a pay button.</param>
        public static InlineKeyboardButton WithPayment(string text, bool pay)
            => new InlineKeyboardPayButton(text, pay);
    }
}

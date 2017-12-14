using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types.ReplyMarkups.Buttons
{
    /// <summary>
    /// This object represents one button of an inline keyboard that opens an url when pressed.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn,
        NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class InlineKeyboardUrlButton : InlineKeyboardButton
    {
        /// <summary>
        /// Optional. HTTP url to be opened when button is pressed
        /// </summary>
        [JsonProperty]
        public string Url { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardUrlButton"/> class.
        /// </summary>
        /// <param name="text">Text of the button</param>
        /// <param name="url">HTTP url to be opened when button is pressed</param>
        public InlineKeyboardUrlButton(string text, string url)
            : base(text)
        {
            Url = url;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardUrlButton"/> class.
        /// </summary>
        /// <param name="text">Text of the button</param>
        /// <param name="url">HTTP url to be opened when button is pressed</param>
        public InlineKeyboardUrlButton(string text, Uri url)
            : base(text)
        {
            Url = url.AbsoluteUri;
        }
    }
}

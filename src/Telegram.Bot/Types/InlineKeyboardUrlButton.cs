using Newtonsoft.Json;
using System;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents one button of an inline keyboard that opens an url when pressed.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class InlineKeyboardUrlButton : InlineKeyboardButton
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardButton"/> class.
        /// </summary>
        /// <param name="text">Text of the button</param>
        /// <param name="url">HTTP url to be opened when button is pressed</param>
        public InlineKeyboardUrlButton(string text, string url) : base(text)
        {
            Url = url;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardButton"/> class.
        /// </summary>
        /// <param name="text">Text of the button</param>
        /// <param name="url">HTTP url to be opened when button is pressed</param>
        public InlineKeyboardUrlButton(string text, Uri url) : base(text)
        {
            Url = url.AbsoluteUri;
        }
    }
}

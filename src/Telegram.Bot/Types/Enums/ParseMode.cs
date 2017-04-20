using Newtonsoft.Json;
using System.Collections.Generic;
using Telegram.Bot.Converters;

namespace Telegram.Bot.Types.Enums
{
    /// <summary>
    /// Text parsing mode
    /// 
    /// The Bot API supports basic formatting for messages. You can use bold and italic text, as well as inline links and pre-formatted code in your bots' messages.
    /// Telegram clients will render them accordingly. You can use either markdown-style or HTML-style formatting.
    /// </summary>
    /// <see href="https://core.telegram.org/bots/api#formatting-options"/>
    [JsonConverter(typeof(ParseModeConverter))]
    public enum ParseMode
    {
        /// <summary>
        /// <see cref="Message.Text"/> is plain text
        /// </summary>
        Default = 0,

        /// <summary>
        /// <see cref="Message.Text"/> is formated in Markdown
        /// </summary>
        Markdown,

        /// <summary>
        /// <see cref="Message.Text"/> is formated in HTML
        /// </summary>
        Html,
    }

    internal static class ParseModeExtensions
    {
        internal static readonly Dictionary<ParseMode, string> StringMap =
            new Dictionary<ParseMode, string>
            {
                {ParseMode.Default, null },
                {ParseMode.Markdown, "Markdown" },
                {ParseMode.Html, "HTML" },
            };

        public static string ToModeString(this ParseMode mode) => StringMap[mode];
    }
}

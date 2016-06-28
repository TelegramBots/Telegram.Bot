using System.Collections.Generic;

namespace Telegram.Bot.Types.Enums
{
    /// <summary>
    /// Text parsing mode
    /// </summary>
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

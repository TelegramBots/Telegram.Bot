using Newtonsoft.Json;
using Telegram.Bot.Converters;

namespace Telegram.Bot.Types.Enums
{
    /// <summary>
    /// <para>
    /// Text parsing mode
    /// </para>
    /// <para>
    /// The Bot API supports basic formatting for messages. You can use bold and italic text, as well as inline
    /// links and pre-formatted code in your bots' messages. Telegram clients will render them accordingly.
    /// You can use either markdown-style or HTML-style formatting.
    /// </para>
    /// </summary>
    /// <see href="https://core.telegram.org/bots/api#formatting-options"/>
    [JsonConverter(typeof(ParseModeConverter))]
    public enum ParseMode
    {
        /// <summary>
        /// <see cref="Message.Text"/> is formatted in Markdown
        /// </summary>
        Markdown = 1,

        /// <summary>
        /// <see cref="Message.Text"/> is formatted in HTML
        /// </summary>
        Html,

        /// <summary>
        /// <see cref="Message.Text"/> is formatted in MarkdownV2
        /// </summary>
        MarkdownV2,
    }
}

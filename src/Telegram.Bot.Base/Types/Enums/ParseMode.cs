using System.ComponentModel.DataAnnotations;

namespace Telegram.Bot.Types.Enums;

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
/// <a href="https://core.telegram.org/bots/api#formatting-options"/>
[JsonConverter(typeof(ParseModeConverter))]
public enum ParseMode
{
    /// <summary>
    /// Markdown-formatted A <see cref="Message.Text"/>
    /// </summary>
    /// <remarks>
    /// This is a legacy mode, retained for backward compatibility
    /// </remarks>
    [Display(Name = "Markdown")]
    Markdown = 1,

    /// <summary>
    /// HTML-formatted <see cref="Message.Text"/>
    /// </summary>
    [Display(Name = "Html")]
    Html,

    /// <summary>
    /// MarkdownV2-formatted <see cref="Message.Text"/>
    /// </summary>
    [Display(Name = "MarkdownV2")]
    MarkdownV2,
}

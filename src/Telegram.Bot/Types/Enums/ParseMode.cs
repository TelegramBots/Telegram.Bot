namespace Telegram.Bot.Types.Enums;

/// <summary>Text parsing mode. See <a href="https://core.telegram.org/bots/api#formatting-options"/>
/// <para>The Bot API supports basic formatting for messages. You can use bold, italic, underlined, strikethrough,
/// spoiler text, block quotations as well as inline links and pre-formatted code in your bots' messages.
/// Telegram clients will render them accordingly.
/// You can specify text entities directly, or use markdown-style or HTML-style formatting.</para>
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<ParseMode>))]
public enum ParseMode
{
    /// <summary>The message text is plain text, possibly with explicit entities</summary>
    None = 0,
    /// <summary>The message text is Markdown-formatted</summary>
    /// <remarks>This is a legacy mode, retained for backward compatibility</remarks>
    Markdown,
    /// <summary>The message text is HTML-formatted</summary>
    Html,
    /// <summary>The message text is MarkdownV2-formatted</summary>
    MarkdownV2,
}

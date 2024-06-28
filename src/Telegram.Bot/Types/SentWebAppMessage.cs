namespace Telegram.Bot.Types;

/// <summary>Describes an inline message sent by a <a href="https://core.telegram.org/bots/webapps">Web App</a> on behalf of a user.</summary>
public partial class SentWebAppMessage
{
    /// <summary><em>Optional</em>. Identifier of the sent inline message. Available only if there is an <see cref="InlineKeyboardMarkup">inline keyboard</see> attached to the message.</summary>
    public string? InlineMessageId { get; set; }
}

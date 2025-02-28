// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes an inline message sent by a <a href="https://core.telegram.org/bots/webapps">Web App</a> on behalf of a user.</summary>
public partial class SentWebAppMessage
{
    /// <summary><em>Optional</em>. Identifier of the sent inline message. Available only if there is an <see cref="InlineKeyboardMarkup">inline keyboard</see> attached to the message.</summary>
    [JsonPropertyName("inline_message_id")]
    public string? InlineMessageId { get; set; }

    /// <summary>Implicit conversion to string (InlineMessageId)</summary>
    public static implicit operator string?(SentWebAppMessage self) => self.InlineMessageId;
    /// <summary>Implicit conversion from string (InlineMessageId)</summary>
    public static implicit operator SentWebAppMessage(string? inlineMessageId) => new() { InlineMessageId = inlineMessageId };
}

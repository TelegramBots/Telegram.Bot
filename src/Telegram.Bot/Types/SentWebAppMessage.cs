namespace Telegram.Bot.Types;

/// <summary>
/// Contains information about an inline message sent by a
/// <a href="https://core.telegram.org/bots/webapps">Web App</a> on behalf of a user.
/// </summary>
public class SentWebAppMessage
{
    /// <summary>
    /// Optional. Identifier of the sent inline message. Available only if there is an inline keyboard attached
    /// to the message.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? InlineMessageId { get; set; }
}

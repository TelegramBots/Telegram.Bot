namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a service message about a user allowing a bot to write messages after adding
/// it to the attachment menu, launching a Web App from a link, or accepting an explicit request from
/// a Web App sent by the method
/// <a href="https://core.telegram.org/bots/webapps#initializing-mini-apps">requestWriteAccess</a>.
/// </summary>
public class WriteAccessAllowed
{
    /// <summary>
    /// Optional. <see langword="true"/>, if the access was granted after the user accepted an explicit request
    /// from a Web App sent by the method
    /// <a href="https://core.telegram.org/bots/webapps#initializing-mini-apps">requestWriteAccess</a>
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? FromRequest { get; set; }

    /// <summary>
    /// Optional. Name of the Web App which was launched from a link
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? WebAppName { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if the access was granted when the bot was added to the attachment
    /// or side menu
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? FromAttachmentMenu  { get; set; }
}

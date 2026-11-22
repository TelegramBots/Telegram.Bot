// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents a service message about a user allowing a bot to write messages after adding it to the attachment menu, launching a Web App from a link, or accepting an explicit request from a Web App sent by the method <a href="https://core.telegram.org/bots/webapps#initializing-mini-apps">requestWriteAccess</a>, or about a user being invited to the private chat with the bot by the owner of the bot.</summary>
public partial class WriteAccessAllowed
{
    /// <summary><em>Optional</em>. <see langword="true"/>, if the access was granted after the user accepted an explicit request from a Web App sent by the method <a href="https://core.telegram.org/bots/webapps#initializing-mini-apps">requestWriteAccess</a></summary>
    [JsonPropertyName("from_request")]
    public bool FromRequest { get; set; }

    /// <summary><em>Optional</em>. Name of the Web App, if the access was granted when the Web App was launched from a link</summary>
    [JsonPropertyName("web_app_name")]
    public string? WebAppName { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the access was granted when the bot was added to the attachment or side menu</summary>
    [JsonPropertyName("from_attachment_menu")]
    public bool FromAttachmentMenu { get; set; }

    /// <summary><em>Optional</em>. The owner of the bot, if the access was granted when the owner invited the user to the private chat with the bot. Bot owners can invite up to 10 users at once by pressing the link <c>https://t.me/botinvite/&lt;BotUsername&gt;</c>.</summary>
    public User? Inviter { get; set; }
}

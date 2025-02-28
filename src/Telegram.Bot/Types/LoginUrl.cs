// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents a parameter of the inline keyboard button used to automatically authorize a user. Serves as a great replacement for the <a href="https://core.telegram.org/widgets/login">Telegram Login Widget</a> when the user is coming from Telegram. All the user needs to do is tap/click a button and confirm that they want to log in:<br/><a href="https://core.telegram.org/file/811140015/1734/8VZFkwWXalM.97872/6127fa62d8a0bf2b3c"/><br/><br/>Telegram apps support these buttons as of <a href="https://telegram.org/blog/privacy-discussions-web-bots#meet-seamless-web-bots">version 5.7</a>.</summary>
/// <remarks>Sample bot: <a href="https://t.me/discussbot">@discussbot</a></remarks>
public partial class LoginUrl
{
    /// <summary>An HTTPS URL to be opened with user authorization data added to the query string when the button is pressed. If the user refuses to provide authorization data, the original URL without information about the user will be opened. The data added is the same as described in <a href="https://core.telegram.org/widgets/login#receiving-authorization-data">Receiving authorization data</a>.<br/><br/><b>NOTE:</b> You <b>must</b> always check the hash of the received data to verify the authentication and the integrity of the data as described in <a href="https://core.telegram.org/widgets/login#checking-authorization">Checking authorization</a>.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Url { get; set; } = default!;

    /// <summary><em>Optional</em>. New text of the button in forwarded messages.</summary>
    [JsonPropertyName("forward_text")]
    public string? ForwardText { get; set; }

    /// <summary><em>Optional</em>. Username of a bot, which will be used for user authorization. See <a href="https://core.telegram.org/widgets/login#setting-up-a-bot">Setting up a bot</a> for more details. If not specified, the current bot's username will be assumed. The <see cref="Url">Url</see>'s domain must be the same as the domain linked with the bot. See <a href="https://core.telegram.org/widgets/login#linking-your-domain-to-the-bot">Linking your domain to the bot</a> for more details.</summary>
    [JsonPropertyName("bot_username")]
    public string? BotUsername { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/> to request the permission for your bot to send messages to the user.</summary>
    [JsonPropertyName("request_write_access")]
    public bool RequestWriteAccess { get; set; }
}

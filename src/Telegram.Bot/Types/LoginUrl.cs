namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a parameter of the inline keyboard button used to automatically authorize a user.
/// Serves as a great replacement for the
/// <a href="https://core.telegram.org/widgets/login">Telegram Login Widget</a> when the user is coming from
/// Telegram. All the user needs to do is tap/click a button and confirm that they want to log in.
/// <para>
/// Telegram apps support these buttons as of
/// <a href="https://telegram.org/blog/privacy-discussions-web-bots#meet-seamless-web-bots">version 5.7</a>.
/// </para>
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class LoginUrl
{

    /// <summary>
    /// An HTTP URL to be opened with user authorization data added to the query string when the button is pressed.
    /// If the user refuses to provide authorization data, the original URL without information about the user will
    /// be opened. The data added is the same as described in
    /// <a href="https://core.telegram.org/widgets/login#receiving-authorization-data">
    /// Receiving authorization data
    /// </a>.
    /// <para>
    /// <b>NOTE:</b> You <b>must</b> always check the hash of the received data to verify the authentication and
    /// the integrity of the data as described in
    /// <a href="https://core.telegram.org/widgets/login#checking-authorization">Checking authorization</a>.
    /// </para>
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Url { get; set; } = default!;

    /// <summary>
    /// Optional. New text of the button in forwarded messages
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? ForwardText { get; set; }

    /// <summary>
    /// Optional. Username of a bot, which will be used for user authorization. See
    /// <a href="https://core.telegram.org/widgets/login#setting-up-a-bot">Setting up a bot</a> for more
    /// details. If not specified, the current bot’s username will be assumed. The url's domain must be the same
    /// as the domain linked with the bot. See
    /// <a href="https://core.telegram.org/widgets/login#linking-your-domain-to-the-bot">
    /// Linking your domain to the bot</a> for more details.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? BotUsername { get; set; }

    /// <summary>
    /// Optional. Pass <see langword="true"/> to request the permission for your bot to send messages to the user
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? RequestWriteAccess { get; set; }
}

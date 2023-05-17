namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a service message about a user allowing a bot to write messages
/// after adding the bot to the attachment menu or launching a Web App from a link.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class WriteAccessAllowed
{
    /// <summary>
    /// Optional. Name of the Web App which was launched from a link
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? WebAppName { get; set; }
}

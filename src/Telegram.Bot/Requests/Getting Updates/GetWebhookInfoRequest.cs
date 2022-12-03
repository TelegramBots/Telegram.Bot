// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to get current webhook status. Requires no parameters. On success, returns
/// a <see cref="WebhookInfo"/> object. If the bot is using <see cref="GetUpdatesRequest"/>,
/// will return an object with the <see cref="WebhookInfo.Url"/> field empty.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class GetWebhookInfoRequest : ParameterlessRequest<WebhookInfo>
{
    /// <summary>
    /// Initializes a new request
    /// </summary>
    public GetWebhookInfoRequest()
        : base("getWebhookInfo")
    { }
}

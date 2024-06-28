namespace Telegram.Bot.Requests;

/// <summary>Use this method to get current webhook status.<para>Returns: A <see cref="WebhookInfo"/> object. If the bot is using <see cref="TelegramBotClientExtensions.GetUpdatesAsync">GetUpdates</see>, will return an object with the <em>url</em> field empty.</para></summary>
public partial class GetWebhookInfoRequest : ParameterlessRequest<WebhookInfo>
{
    /// <summary>Instantiates a new <see cref="GetWebhookInfoRequest"/></summary>
    public GetWebhookInfoRequest() : base("getWebhookInfo") { }
}

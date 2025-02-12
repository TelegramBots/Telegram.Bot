// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to get current webhook status.<para>Returns: A <see cref="WebhookInfo"/> object. If the bot is using <see cref="TelegramBotClientExtensions.GetUpdates">GetUpdates</see>, will return an object with the <em>url</em> field empty.</para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class GetWebhookInfoRequest() : ParameterlessRequest<WebhookInfo>("getWebhookInfo")
{}

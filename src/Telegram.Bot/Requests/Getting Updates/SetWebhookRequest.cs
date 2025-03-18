// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to specify a URL and receive incoming updates via an outgoing webhook. Whenever there is an update for the bot, we will send an HTTPS POST request to the specified URL, containing a JSON-serialized <see cref="Update"/>. In case of an unsuccessful request (a request with response <a href="https://en.wikipedia.org/wiki/List_of_HTTP_status_codes">HTTP status code</a> different from <c>2XY</c>), we will repeat the request and give up after a reasonable amount of attempts.<br/>If you'd like to make sure that the webhook was set by you, you can specify secret data in the parameter <see cref="SecretToken">SecretToken</see>. If specified, the request will contain a header “X-Telegram-Bot-Api-Secret-Token” with the secret token as content.</summary>
/// <remarks><b>Notes</b><br/><b>1.</b> You will not be able to receive updates using <see cref="TelegramBotClientExtensions.GetUpdates">GetUpdates</see> for as long as an outgoing webhook is set up.<br/><b>2.</b> To use a self-signed certificate, you need to upload your <a href="https://core.telegram.org/bots/self-signed">public key certificate</a> using <see cref="Certificate">Certificate</see> parameter. Please upload as InputFile, sending a String will not work.<br/><b>3.</b> Ports currently supported <em>for webhooks</em>: <b>443, 80, 88, 8443</b>.<br/>If you're having any trouble setting up webhooks, please check out this <a href="https://core.telegram.org/bots/webhooks">amazing guide to webhooks</a>.<br/></remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SetWebhookRequest() : FileRequestBase<bool>("setWebhook")
{
    /// <summary>HTTPS URL to send updates to. Use an empty string to remove webhook integration</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Url { get; set; }

    /// <summary>Upload your public key certificate so that the root certificate in use can be checked. See our <a href="https://core.telegram.org/bots/self-signed">self-signed guide</a> for details.</summary>
    public InputFileStream? Certificate { get; set; }

    /// <summary>The fixed IP address which will be used to send webhook requests instead of the IP address resolved through DNS</summary>
    [JsonPropertyName("ip_address")]
    public string? IpAddress { get; set; }

    /// <summary>The maximum allowed number of simultaneous HTTPS connections to the webhook for update delivery, 1-100. Defaults to <em>40</em>. Use lower values to limit the load on your bot's server, and higher values to increase your bot's throughput.</summary>
    [JsonPropertyName("max_connections")]
    public int? MaxConnections { get; set; }

    /// <summary>A list of the update types you want your bot to receive. For example, specify <c>["message", "EditedChannelPost", "CallbackQuery"]</c> to only receive updates of these types. See <see cref="Update"/> for a complete list of available update types. Specify an empty list to receive all update types except <em>ChatMember</em>, <em>MessageReaction</em>, and <em>MessageReactionCount</em> (default). If not specified, the previous setting will be used.<br/>Please note that this parameter doesn't affect updates created before the call to the setWebhook, so unwanted updates may be received for a short period of time.</summary>
    [JsonPropertyName("allowed_updates")]
    public IEnumerable<UpdateType>? AllowedUpdates { get; set; }

    /// <summary>Pass <see langword="true"/> to drop all pending updates</summary>
    [JsonPropertyName("drop_pending_updates")]
    public bool DropPendingUpdates { get; set; }

    /// <summary>A secret token to be sent in a header “X-Telegram-Bot-Api-Secret-Token” in every webhook request, 1-256 characters. Only characters <c>A-Z</c>, <c>a-z</c>, <c>0-9</c>, <c>_</c> and <c>-</c> are allowed. The header is useful to ensure that the request comes from a webhook set by you.</summary>
    [JsonPropertyName("secret_token")]
    public string? SecretToken { get; set; }
}

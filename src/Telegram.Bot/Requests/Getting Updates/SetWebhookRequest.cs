using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to specify a url and receive incoming updates via an outgoing webhook.
/// Whenever there is an update for the bot, we will send an HTTPS POST request to the
/// specified url, containing a JSON-serialized <see cref="Types.Update"/>. In case of
/// an unsuccessful request, we will give up after a reasonable amount of attempts.
/// Returns <c>true</c> on success.
/// <para>
/// If you'd like to make sure that the Webhook request comes from Telegram, we recommend
/// using a secret path in the URL, e.g. <c>https://www.example.com/&lt;token&gt;</c>.
/// Since nobody else knows your bot’s token, you can be pretty sure it's us.
/// </para>
/// <remarks>
/// <list type="number">
/// <item>
/// You will not be able to receive updates using <see cref="GetUpdatesRequest"/> for as long as an outgoing
/// webhook is set up.</item>
/// <item>
/// To use a self-signed certificate, you need to upload your
/// <a href="https://core.telegram.org/bots/self-signed">public key certificate</a> using
/// <see cref="Certificate"/> parameter. Please upload as <see cref="InputFileStream"/>, sending
/// a String will not work.
/// </item>
/// <item>Ports currently supported for Webhooks: <b>443, 80, 88, 8443</b></item>
/// </list>
/// If you're having any trouble setting up webhooks, please check out this
/// <a href="https://core.telegram.org/bots/webhooks">amazing guide to Webhooks</a>.
/// </remarks>
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class SetWebhookRequest : FileRequestBase<bool>
{
    /// <summary>
    /// HTTPS url to send updates to. Use an empty string to remove webhook integration
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Url { get; }

    /// <summary>
    /// Upload your public key certificate so that the root certificate in use can be checked. See
    /// our <a href="https://core.telegram.org/bots/self-signed">self-signed guide</a> for details
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public InputFileStream? Certificate { get; set; }

    /// <summary>
    /// The fixed IP address which will be used to send webhook requests instead of the
    /// IP address resolved through DNS
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? IpAddress { get; set; }

    /// <summary>
    /// Maximum allowed number of simultaneous HTTPS connections to the webhook for update
    /// delivery, 1-100. Defaults to <i>40</i>. Use lower values to limit the load on your
    /// bot’s server, and higher values to increase your bot’s throughput
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? MaxConnections { get; set; }

    /// <summary>
    /// A list of the update types you want your bot to receive. For example, specify
    /// [<see cref="UpdateType.Message"/>, <see cref="UpdateType.EditedChannelPost"/>,
    /// <see cref="UpdateType.CallbackQuery"/>] to only receive updates of these types.
    /// See <see cref="UpdateType"/> for a complete list of available update types.
    /// Specify an empty list to receive all update types except
    /// <see cref="UpdateType.ChatMember"/> (default). If not specified,
    /// the previous setting will be used
    /// </summary>
    /// <remarks>
    /// Please note that this parameter doesn't affect updates created before the call to the
    /// <see cref="SetWebhookRequest"/>, so unwanted updates may be received for a short period of time.
    /// </remarks>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public IEnumerable<UpdateType>? AllowedUpdates { get; set; }

    /// <summary>
    /// Pass <c>true</c> to drop all pending updates
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? DropPendingUpdates { get; set; }

    /// <summary>
    /// Initializes a new request with uri
    /// </summary>
    /// <param name="url">
    /// HTTPS url to send updates to. Use an empty string to remove webhook integration
    /// </param>
    public SetWebhookRequest(string url)
        : base("setWebhook")
    {
        Url = url;
    }

    /// <inheritdoc cref="RequestBase{TResponse}.ToHttpContent"/>
    public override HttpContent? ToHttpContent() =>
        Certificate is null
            ? base.ToHttpContent()
            : ToMultipartFormDataContent("certificate", Certificate);
}

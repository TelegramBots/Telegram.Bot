using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to specify a URL and receive incoming updates via an outgoing webhook.
/// Whenever there is an update for the bot, we will send an HTTPS POST request to the
/// specified URL, containing an <see cref="Types.Update"/>. In case of
/// an unsuccessful request, we will give up after a reasonable amount of attempts.
/// Returns <see langword="true"/> on success.
/// <para>
/// If you'd like to make sure that the webhook was set by you, you can specify secret data
/// in the parameter <see cref="SecretToken"/> . If specified, the request
/// will contain a header "X-Telegram-Bot-Api-Secret-Token" with the secret token as content.
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
/// <item>Ports currently supported for webhooks: <b>443, 80, 88, 8443</b></item>
/// </list>
/// If you're having any trouble setting up webhooks, please check out this
/// <a href="https://core.telegram.org/bots/webhooks">amazing guide to Webhooks</a>.
/// </remarks>
/// </summary>
public class SetWebhookRequest : FileRequestBase<bool>
{
    /// <summary>
    /// HTTPS URL to send updates to. Use an empty string to remove webhook integration
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Url { get; init; }

    /// <summary>
    /// Upload your public key certificate so that the root certificate in use can be checked. See
    /// our <a href="https://core.telegram.org/bots/self-signed">self-signed guide</a> for details
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public InputFileStream? Certificate { get; set; }

    /// <summary>
    /// The fixed IP address which will be used to send webhook requests instead of the
    /// IP address resolved through DNS
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? IpAddress { get; set; }

    /// <summary>
    /// Maximum allowed number of simultaneous HTTPS connections to the webhook for update
    /// delivery, 1-100. Defaults to <i>40</i>. Use lower values to limit the load on your
    /// bot's server, and higher values to increase your bot's throughput.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
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
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<UpdateType>? AllowedUpdates { get; set; }

    /// <summary>
    /// Pass <see langword="true"/> to drop all pending updates
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? DropPendingUpdates { get; set; }

    /// <summary>
    /// A secret token to be sent in a header "<c>X-Telegram-Bot-Api-Secret-Token</c>" in every webhook request,
    /// 1-256 characters. Only characters <c>A-Z</c>, <c>a-z</c>, <c>0-9</c>, <c>_</c> and <c>-</c>
    /// are allowed. The header is useful to ensure that the request comes from a webhook set by you.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? SecretToken { get; set; }

    /// <summary>
    /// Initializes a new request with uri
    /// </summary>
    /// <param name="url">
    /// HTTPS url to send updates to. Use an empty string to remove webhook integration
    /// </param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public SetWebhookRequest(string url) : base("setWebhook")
        => Url = url;

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public SetWebhookRequest() : base("setWebhook")
    { }

    /// <inheritdoc cref="RequestBase{TResponse}.ToHttpContent"/>
    public override HttpContent? ToHttpContent() =>
        Certificate switch
        {
            not null => ToMultipartFormDataContent("certificate", Certificate),
            _        => base.ToHttpContent()
        };
}

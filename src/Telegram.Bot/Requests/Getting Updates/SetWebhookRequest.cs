using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Specify a url and receive incoming updates via an outgoing webhook
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class SetWebhookRequest : FileRequestBase<bool>
    {
        /// <summary>
        /// HTTPS url to send updates to. Use an empty string to remove webhook integration.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Url { get; }

        /// <summary>
        /// Public key certificate so that the root certificate in use can be checked
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public InputFileStream Certificate { get; }

        /// <summary>
        /// The fixed IP address which will be used to send webhook requests instead of the IP address resolved through DNS
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string IpAddress { get; set; }

        /// <summary>
        /// Maximum allowed number of simultaneous HTTPS connections to the webhook for update delivery
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int MaxConnections { get; set; }

        /// <summary>
        /// List the types of updates you want your bot to receive
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<UpdateType> AllowedUpdates { get; set; }

        /// <summary>
        /// Pass True to drop all pending updates
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool DropPendingUpdates { get; set; }

        /// <summary>
        /// Initializes a new request with uri
        /// </summary>
        /// <param name="url">HTTPS url to send updates to</param>
        /// <param name="certificate">ToDo</param>
        public SetWebhookRequest(string url, InputFileStream certificate)
            : base("setWebhook")
        {
            Url = url;
            Certificate = certificate;
        }

        /// <inheritdoc cref="RequestBase{TResponse}.ToHttpContent"/>
        public override HttpContent ToHttpContent() =>
            Certificate == null
                ? base.ToHttpContent()
                : ToMultipartFormDataContent("certificate", Certificate);
    }
}

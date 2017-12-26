using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using Telegram.Bot.Helpers;
using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Specify a url and receive incoming updates via an outgoing webhook
    /// </summary>
    public class SetWebhookRequest : RequestBase<bool>
    {
        /// <summary>
        /// HTTPS url to send updates to. Use an empty string to remove webhook integration.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Url { get; set; }

        /// <summary>
        /// Public key certificate so that the root certificate in use can be checked
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Stream Certificate { get; set; }

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
        /// Initializes a new request with uri
        /// </summary>
        /// <param name="url">HTTPS url to send updates to</param>
        public SetWebhookRequest(string url)
            : base("setWebhook")
        {
            Url = url;
        }

        /// <summary>
        /// Initializes a new request with uri and certificate
        /// </summary>
        /// <param name="url">HTTPS url to send updates to</param>
        /// <param name="certificate">Public key certificate so that the root certificate in use can be checked</param>
        public SetWebhookRequest(string url, Stream certificate)
            : base("setWebhook")
        {
            Url = url;
            Certificate = certificate;
        }

        /// <summary>
        /// Initializes a new request
        /// </summary>
        public SetWebhookRequest()
            : base("setWebhook")
        { }

        /// <summary>
        /// Generate content of HTTP message
        /// </summary>
        /// <param name="serializerSettings">JSON serialization setting</param>
        /// <returns>Content of HTTP request</returns>
        public override HttpContent ToHttpContent(JsonSerializerSettings serializerSettings)
        {
            var multipartContent = new MultipartFormDataContent(Guid.NewGuid().ToString() + DateTime.UtcNow.Ticks)
            {
                { new StringContent(Url), nameof(Url).ToSnakeCased() }
            };

            if (MaxConnections != default)
            {
                var maxConnectionsName = nameof(MaxConnections).ToSnakeCased();
                multipartContent.Add(
                    new StringContent(MaxConnections.ToString(), System.Text.Encoding.UTF8),
                    maxConnectionsName);
            }

            var allowedUpdatesName = nameof(AllowedUpdates).ToSnakeCased();
            var allowedUpdatesValue = new StringContent(
                JsonConvert.SerializeObject(AllowedUpdates, serializerSettings));
            multipartContent.Add(allowedUpdatesValue, allowedUpdatesName);

            if (Certificate != null)
            {
                multipartContent.AddStreamContent(Certificate, nameof(Certificate).ToSnakeCased());
            }

            return multipartContent;
        }
    }
}

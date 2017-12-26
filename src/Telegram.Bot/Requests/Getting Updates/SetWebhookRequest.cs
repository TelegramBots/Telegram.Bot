using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Telegram.Bot.Helpers;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

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
        public string Url { get; }

        /// <summary>
        /// Public key certificate so that the root certificate in use can be checked
        /// </summary>
        public InputFileStream Certificate { get; }

        /// <summary>
        /// Maximum allowed number of simultaneous HTTPS connections to the webhook for update delivery
        /// </summary>
        public int MaxConnections { get; set; }

        /// <summary>
        /// List the types of updates you want your bot to receive
        /// </summary>
        public IEnumerable<UpdateType> AllowedUpdates { get; set; }

        /// <summary>
        /// Initializes a new request with uri
        /// </summary>
        /// <param name="url">HTTPS url to send updates to</param>
        /// <param name="certificate">ToDo</param>
        public SetWebhookRequest(string url, Stream certificate = default)
            : base("setWebhook")
        {
            Url = url;
            if (certificate != null)
            {
                Certificate = certificate;
            }
        }

        /// <summary>
        /// Generate content of HTTP message
        /// </summary>
        /// <param name="serializerSettings">JSON serialization setting</param>
        /// <returns>Content of HTTP request</returns>
        public override HttpContent ToHttpContent(JsonSerializerSettings serializerSettings = default)
        {
            var multipartContent = new MultipartFormDataContent(Guid.NewGuid().ToString() + DateTime.UtcNow.Ticks)
            {
                { new StringContent(Url), "url" }
            };

            if (Certificate != null)
            {
                multipartContent.AddStreamContent(Certificate.Content, "certificate");
            }

            if (MaxConnections != default)
            {
                multipartContent.Add(new StringContent(MaxConnections.ToString()), "max_connections");
            }

            if (AllowedUpdates != null)
            {
                multipartContent.Add(
                    new StringContent(
                        JsonConvert.SerializeObject(AllowedUpdates, serializerSettings),
                        Encoding.UTF8, "application/json"
                    ), "allowed_updates");
            }

            return multipartContent;
        }
    }
}

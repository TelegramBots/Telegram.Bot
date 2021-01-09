using System.Collections.Generic;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.SetWebhookAsync" /> method.
    /// </summary>
    public class SetWebhookParameters : ParametersBase
    {
        /// <summary>
        ///     HTTPS url to send updates to. Use an empty string to remove webhook integration
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        ///     Upload your public key certificate so that the root certificate in use can be checked.
        ///     See the
        /// </summary>
        public InputFileStream Certificate { get; set; }

        /// <summary>
        ///     Maximum allowed number of simultaneous HTTPS connections to the webhook for update delivery, 1-100. Defaults to 40.
        ///     Use lower values to limit the load on your bot's server, and higher values to increase your bot's throughput.
        /// </summary>
        public int MaxConnections { get; set; }

        /// <summary>
        ///     List the
        /// </summary>
        public IEnumerable<UpdateType> AllowedUpdates { get; set; }

        /// <summary>
        ///     Sets <see cref="Url" /> property.
        /// </summary>
        /// <param name="url">HTTPS url to send updates to. Use an empty string to remove webhook integration</param>
        public SetWebhookParameters WithUrl(string url)
        {
            Url = url;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Certificate" /> property.
        /// </summary>
        /// <param name="certificate">
        ///     Upload your public key certificate so that the root certificate in use can be checked.
        ///     See the
        /// </param>
        public SetWebhookParameters WithCertificate(InputFileStream certificate)
        {
            Certificate = certificate;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="MaxConnections" /> property.
        /// </summary>
        /// <param name="maxConnections">
        ///     Maximum allowed number of simultaneous HTTPS connections to the webhook for update
        ///     delivery, 1-100. Defaults to 40. Use lower values to limit the load on your bot's server, and higher values to
        ///     increase your bot's throughput.
        /// </param>
        public SetWebhookParameters WithMaxConnections(int maxConnections)
        {
            MaxConnections = maxConnections;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="AllowedUpdates" /> property.
        /// </summary>
        /// <param name="allowedUpdates">
        ///     List the
        /// </param>
        public SetWebhookParameters WithAllowedUpdates(IEnumerable<UpdateType> allowedUpdates)
        {
            AllowedUpdates = allowedUpdates;
            return this;
        }
    }
}
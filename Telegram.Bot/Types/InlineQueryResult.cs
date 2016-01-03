using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    public class InlineQueryResult
    {
        /// <summary>
        /// Unique identifier of this result
        /// </summary>
        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; set; }

        /// <summary>
        /// Type of the result
        /// </summary>
        [JsonProperty("type", Required = Required.Always)]
        public InlineQueryResultType Type { get; set; }

        /// <summary>
        /// Title of the result
        /// </summary>
        [JsonProperty("title", Required = Required.Always)]
        public string Title { get; set; }

        /// <summary>
        /// Text of a message to be sent
        /// </summary>
        [JsonProperty("message_text", Required = Required.Always)]
        public string MessageText { get; set; }

        //TODO: Change to enum or boolean

        /// <summary>
        /// Optional. Send "Markdown", if you want Telegram apps to show bold, italic and inline URLs in your bot's message.
        /// </summary>
        [JsonProperty("parse_mode", Required = Required.Default)]
        public string ParseMode { get; set; }

        /// <summary>
        /// Optional. Url of the thumbnail for the result
        /// </summary>
        [JsonProperty("thumb_url", Required = Required.Default)]
        public string ThumbUrl { get; set; }

        /// <summary>
        /// Optional. Disables link previews for links in the sent message
        /// </summary>
        [JsonProperty("disable_web_page_preview", Required = Required.Default)]
        public bool DisableWebPagePreview { get; set; } = false;
    }
}

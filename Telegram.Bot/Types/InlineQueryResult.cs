using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Telegram.Bot.Helpers;

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
        [JsonConverter(typeof (InlineQueryResultTypeConverter))]
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

        /// <summary>
        /// Set true if you want Telegram apps to show bold, italic and inline URLs in your bot's message.
        /// </summary>
        [JsonIgnore]
        public bool Markdown { get; set; } = false;
        
        [JsonProperty("parse_mode", Required = Required.Default)]
        internal string ParseMode
        {
            get { return Markdown ? "Markdown" : null; }
            set { Markdown = (value == "Markdown"); }
        }

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

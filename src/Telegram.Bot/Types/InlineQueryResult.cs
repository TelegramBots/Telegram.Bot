using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Telegram.Bot.Helpers;

namespace Telegram.Bot.Types
{
    public class InlineQueryResult
    {
        private static readonly Dictionary<Type, InlineQueryResultType> TypeMap =
            new Dictionary<Type, InlineQueryResultType>
            {
                {typeof (InlineQueryResultArticle), InlineQueryResultType.Article},
                {typeof (InlineQueryResultPhoto), InlineQueryResultType.Photo},
                {typeof (InlineQueryResultGif), InlineQueryResultType.Gif},
                {typeof (InlineQueryResultMpeg4Gif), InlineQueryResultType.Mpeg4Gif},
                {typeof (InlineQueryResultVideo), InlineQueryResultType.Video},
            };

        /// <summary>
        /// Unique identifier of this result
        /// </summary>
        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; set; }

        /// <summary>
        /// Type of the result
        /// </summary>
        [JsonConverter(typeof(InlineQueryResultTypeConverter))]
        [JsonProperty("type", Required = Required.Always)]
        public InlineQueryResultType Type => TypeMap[GetType()];

        /// <summary>
        /// Title of the result
        /// </summary>
        [JsonProperty("title", Required = Required.Default)]
        public string Title { get; set; }

        /// <summary>
        /// Text of a message to be sent
        /// </summary>
        [JsonProperty("message_text", Required = Required.Default)]
        public string MessageText { get; set; }

        /// <summary>
        /// Optional. If you want Telegram apps to show bold, italic, fixed-width text or inline URLs in your bot's message.
        /// </summary>
        [JsonConverter(typeof(ParseModeConverter))]
        [JsonProperty("parse_mode", Required = Required.Default)]
        public ParseMode ParseMode { get; set; }

        /// <summary>
        /// Optional. Url of the thumbnail for the result
        /// </summary>
        [JsonProperty("thumb_url", Required = Required.Always)]
        public string ThumbUrl { get; set; }

        /// <summary>
        /// Optional. Disables link previews for links in the sent message
        /// </summary>
        [JsonProperty("disable_web_page_preview", Required = Required.Default)]
        public bool DisableWebPagePreview { get; set; } = false;
    }
}

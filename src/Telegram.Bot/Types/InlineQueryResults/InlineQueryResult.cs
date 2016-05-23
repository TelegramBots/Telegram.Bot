using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Telegram.Bot.Converters;
using Telegram.Bot.Types.InputMessageContents;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Types.InlineQueryResults
{
    public class InlineQueryResult
    {
        private static readonly Dictionary<Type, InlineQueryResultType> TypeMap =
            new Dictionary<Type, InlineQueryResultType>
            {
                {typeof(InlineQueryResultArticle), InlineQueryResultType.Article},
                {typeof(InlineQueryResultAudio), InlineQueryResultType.Audio},
                {typeof(InlineQueryResultContact), InlineQueryResultType.Contact},
                {typeof(InlineQueryResultDocument), InlineQueryResultType.Document},
                {typeof(InlineQueryResultGif), InlineQueryResultType.Gif},
                {typeof(InlineQueryResultLocation), InlineQueryResultType.Location},
                {typeof(InlineQueryResultMpeg4Gif), InlineQueryResultType.Mpeg4Gif},
                {typeof(InlineQueryResultPhoto), InlineQueryResultType.Photo},
                {typeof(InlineQueryResultVenue), InlineQueryResultType.Venue},
                {typeof(InlineQueryResultVideo), InlineQueryResultType.Video},
                {typeof(InlineQueryResultVoice), InlineQueryResultType.Voice},

                {typeof(InlineQueryResultCachedAudio), InlineQueryResultType.CachedAudio },
                {typeof(InlineQueryResultCachedDocument), InlineQueryResultType.CachedDocument },
                {typeof(InlineQueryResultCachedGif), InlineQueryResultType.CachedGif },
                {typeof(InlineQueryResultCachedMpeg4Gif), InlineQueryResultType.CachedMpeg4Gif },
                {typeof(InlineQueryResultCachedPhoto), InlineQueryResultType.CachedPhoto },
                {typeof(InlineQueryResultCachedSticker), InlineQueryResultType.CachedSticker },
                {typeof(InlineQueryResultCachedVideo), InlineQueryResultType.CachedVideo },
                {typeof(InlineQueryResultCachedVoice), InlineQueryResultType.CachedVoice },
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
        /// Content of the message to be sent
        /// </summary>
        [JsonProperty("input_message_content", Required = Required.Default)]
        public InputMessageContent InputMessageContent { get; set; }

        /// <summary>
        /// Inline keyboard attached to the message
        /// </summary>
        [JsonProperty("reply_markup", Required = Required.Default)]
        public InlineKeyboardMarkup ReplyMarkup { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Telegram.Bot.Types.InputMessageContents;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Base Class for inline results send in response to an <see cref="InlineQuery"/>
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class InlineQueryResult
    {
        private static readonly Dictionary<Type, InlineQueryResultType> TypeMap =
            new Dictionary<Type, InlineQueryResultType>
            {
                {typeof(InlineQueryResultArticle), InlineQueryResultType.Article},
                {typeof(InlineQueryResultAudio), InlineQueryResultType.Audio},
                {typeof(InlineQueryResultContact), InlineQueryResultType.Contact},
                {typeof(InlineQueryResultDocument), InlineQueryResultType.Document},
                {typeof(InlineQueryResultGame), InlineQueryResultType.Game},
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
        [JsonProperty(Required = Required.Always)]
        public string Id { get; set; }

        /// <summary>
        /// Type of the result
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public InlineQueryResultType Type => TypeMap[GetType()];

        /// <summary>
        /// Title of the result
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string Title { get; set; }

        /// <summary>
        /// Content of the message to be sent
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public InputMessageContent InputMessageContent { get; set; }

        /// <summary>
        /// Inline keyboard attached to the message
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public InlineKeyboardMarkup ReplyMarkup { get; set; }
    }
}

using System.Collections.Generic;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Type of the InlineQueryResult
    /// </summary>
    public enum InlineQueryResultType
    {
        Unknown = 0,
        Article,
        Photo,
        Gif,
        Mpeg4Gif,
        Video,

        Audio,
        Contact,
        Document,
        Location,
        Venue,
        Voice,

        CachedPhoto = 102,
        CachedGif = 103,
        CachedMpeg4Gif = 104,
        CachedVideo = 105,
        CachedAudio = 106,
        CachedDocument = 108,
        CachedVoice = 111,

        CachedSticker = 112,
    }

    internal static class InlineQueryResultTypeExtension
    {
        private static readonly Dictionary<InlineQueryResultType, string> StringMap =
            new Dictionary<InlineQueryResultType, string>
            {
                {InlineQueryResultType.Article, "article" },
                {InlineQueryResultType.Photo, "photo" },
                {InlineQueryResultType.Gif, "gif" },
                {InlineQueryResultType.Mpeg4Gif, "mpeg4_gif" },
                {InlineQueryResultType.Video, "video" },

                {InlineQueryResultType.Audio, "audio" },
                {InlineQueryResultType.Voice,  "voice" },
                {InlineQueryResultType.Document, "document" },
                {InlineQueryResultType.Location, "location" },
                {InlineQueryResultType.Venue, "venue" },
                {InlineQueryResultType.Contact, "contact" },
            };

        internal static string ToTypeString(this InlineQueryResultType type) => StringMap[type];
    }
}

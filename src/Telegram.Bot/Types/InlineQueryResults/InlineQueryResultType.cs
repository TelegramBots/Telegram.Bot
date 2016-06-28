using System.Collections.Generic;

namespace Telegram.Bot.Types.InlineQueryResults
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
                {InlineQueryResultType.Audio, "audio" },
                {InlineQueryResultType.Contact, "contact" },
                {InlineQueryResultType.Document, "document" },
                {InlineQueryResultType.Gif, "gif" },
                {InlineQueryResultType.Location, "location" },
                {InlineQueryResultType.Mpeg4Gif, "mpeg4_gif" },
                {InlineQueryResultType.Photo, "photo" },
                {InlineQueryResultType.Venue, "venue" },
                {InlineQueryResultType.Video, "video" },
                {InlineQueryResultType.Voice,  "voice" },

                {InlineQueryResultType.CachedAudio, "audio" },
                {InlineQueryResultType.CachedDocument, "document" },
                {InlineQueryResultType.CachedGif, "gif" },
                {InlineQueryResultType.CachedMpeg4Gif, "mpeg4_gif" },
                {InlineQueryResultType.CachedPhoto, "photo" },
                {InlineQueryResultType.CachedSticker, "sticker" },
                {InlineQueryResultType.CachedVideo, "video" },
                {InlineQueryResultType.CachedVoice, "voice" },
            };

        internal static string ToTypeString(this InlineQueryResultType type) => StringMap[type];
    }
}

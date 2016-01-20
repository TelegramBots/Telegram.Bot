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
            };

        internal static string ToTypeString(this InlineQueryResultType type) => StringMap[type];
    }
}

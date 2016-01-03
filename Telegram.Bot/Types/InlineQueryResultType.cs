using System;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Type of the InlineQueryResult
    /// </summary>
    public enum InlineQueryResultType
    {
        Article,
        Photo,
        Gif,
        Mpeg4Gif,
        Video,
    }

    internal static class InlineQueryResultTypeExtension
    {
        internal static string ToTypeString(this InlineQueryResultType type)
        {
            switch (type)
            {
                case InlineQueryResultType.Article:
                    return "article";
                case InlineQueryResultType.Photo:
                    return "photo";
                case InlineQueryResultType.Gif:
                    return "gif";
                case InlineQueryResultType.Mpeg4Gif:
                    return "mpeg4_gif";
                case InlineQueryResultType.Video:
                    return "video";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}

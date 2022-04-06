using System.Collections.Generic;
using Telegram.Bot.Types.InlineQueryResults;

namespace Telegram.Bot.Converters;

internal class InlineQueryResultTypeConverter : EnumConverter<InlineQueryResultType>
{
    static readonly IReadOnlyDictionary<string, InlineQueryResultType> StringToEnum =
        new Dictionary<string, InlineQueryResultType>
        {
            {"unknown", InlineQueryResultType.Unknown},
            {"article", InlineQueryResultType.Article},
            {"photo", InlineQueryResultType.Photo},
            {"gif", InlineQueryResultType.Gif},
            {"mpeg4_gif", InlineQueryResultType.Mpeg4Gif},
            {"video", InlineQueryResultType.Video},
            {"audio", InlineQueryResultType.Audio},
            {"contact", InlineQueryResultType.Contact},
            {"document", InlineQueryResultType.Document},
            {"location", InlineQueryResultType.Location},
            {"venue", InlineQueryResultType.Venue},
            {"voice", InlineQueryResultType.Voice},
            {"game", InlineQueryResultType.Game},
            {"sticker", InlineQueryResultType.Sticker},
        };

    static readonly IReadOnlyDictionary<InlineQueryResultType, string> EnumToString =
        new Dictionary<InlineQueryResultType, string>
        {
            {InlineQueryResultType.Unknown, "unknown"},
            {InlineQueryResultType.Article, "article"},
            {InlineQueryResultType.Photo, "photo"},
            {InlineQueryResultType.Gif, "gif"},
            {InlineQueryResultType.Mpeg4Gif, "mpeg4_gif"},
            {InlineQueryResultType.Video, "video"},
            {InlineQueryResultType.Audio, "audio"},
            {InlineQueryResultType.Contact, "contact"},
            {InlineQueryResultType.Document, "document"},
            {InlineQueryResultType.Location, "location"},
            {InlineQueryResultType.Venue, "venue"},
            {InlineQueryResultType.Voice, "voice"},
            {InlineQueryResultType.Game, "game"},
            {InlineQueryResultType.Sticker, "sticker"},
        };

    protected override InlineQueryResultType GetEnumValue(string value) =>
        StringToEnum.TryGetValue(value, out var enumValue)
            ? enumValue
            : 0;

    protected override string GetStringValue(InlineQueryResultType value) =>
        EnumToString.TryGetValue(value, out var stringValue)
            ? stringValue
            : "unknown";
}


// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types.InlineQueryResults;

/// <summary>
/// Type of the InlineQueryResult
/// </summary>
[JsonConverter(typeof(InlineQueryResultTypeConverter))]
public enum InlineQueryResultType
{
    /// <summary>
    /// <see cref="InlineQueryResultArticle"/>
    /// </summary>
    Article = 1,

    /// <summary>
    /// <see cref="InlineQueryResultPhoto"/>
    /// <see cref="InlineQueryResultCachedPhoto"/>
    /// </summary>
    Photo,

    /// <summary>
    /// <see cref="InlineQueryResultGif"/>
    /// <see cref="InlineQueryResultCachedMpeg4Gif"/>
    /// </summary>
    Gif,

    /// <summary>
    /// <see cref="InlineQueryResultMpeg4Gif"/>
    /// <see cref="InlineQueryResultCachedVideo"/>
    /// </summary>
    Mpeg4Gif,

    /// <summary>
    /// <see cref="InlineQueryResultVideo"/>
    /// /// <see cref="InlineQueryResultCachedVideo"/>
    /// </summary>
    Video,

    /// <summary>
    /// <see cref="InlineQueryResultAudio"/>
    /// <see cref="InlineQueryResultCachedAudio"/>
    /// </summary>
    Audio,

    /// <summary>
    /// <see cref="InlineQueryResultContact"/>
    /// </summary>
    Contact,

    /// <summary>
    /// <see cref="InlineQueryResultDocument"/>
    /// /// <see cref="InlineQueryResultCachedDocument"/>
    /// </summary>
    Document,

    /// <summary>
    /// <see cref="InlineQueryResultLocation"/>
    /// </summary>
    Location,

    /// <summary>
    /// <see cref="InlineQueryResultVenue"/>
    /// </summary>
    Venue,

    /// <summary>
    /// <see cref="InlineQueryResultVoice"/>
    /// <see cref="InlineQueryResultCachedVoice"/>
    /// </summary>
    Voice,

    /// <summary>
    /// <see cref="InlineQueryResultGame"/>
    /// </summary>
    Game,

    /// <summary>
    /// <see cref="InlineQueryResultCachedSticker"/>
    /// </summary>
    Sticker,
}

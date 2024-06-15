using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Serialization;
using Telegram.Bot.Types.ReplyMarkups;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types.InlineQueryResults;

/// <summary>
/// Base Class for inline results send in response to an <see cref="InlineQuery"/>
/// </summary>
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType(typeof(InlineQueryResultArticle))]
[CustomJsonDerivedType(typeof(InlineQueryResultAudio))]
[CustomJsonDerivedType(typeof(InlineQueryResultCachedAudio))]
[CustomJsonDerivedType(typeof(InlineQueryResultCachedDocument))]
[CustomJsonDerivedType(typeof(InlineQueryResultCachedGif))]
[CustomJsonDerivedType(typeof(InlineQueryResultCachedMpeg4Gif))]
[CustomJsonDerivedType(typeof(InlineQueryResultCachedPhoto))]
[CustomJsonDerivedType(typeof(InlineQueryResultCachedSticker))]
[CustomJsonDerivedType(typeof(InlineQueryResultCachedVideo))]
[CustomJsonDerivedType(typeof(InlineQueryResultCachedVoice))]
[CustomJsonDerivedType(typeof(InlineQueryResultContact))]
[CustomJsonDerivedType(typeof(InlineQueryResultDocument))]
[CustomJsonDerivedType(typeof(InlineQueryResultGame))]
[CustomJsonDerivedType(typeof(InlineQueryResultGif))]
[CustomJsonDerivedType(typeof(InlineQueryResultLocation))]
[CustomJsonDerivedType(typeof(InlineQueryResultMpeg4Gif))]
[CustomJsonDerivedType(typeof(InlineQueryResultPhoto))]
[CustomJsonDerivedType(typeof(InlineQueryResultVenue))]
[CustomJsonDerivedType(typeof(InlineQueryResultVideo))]
[CustomJsonDerivedType(typeof(InlineQueryResultVoice))]
public abstract class InlineQueryResult
{
    /// <summary>
    /// Type of the result
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public abstract InlineQueryResultType Type { get; }

    /// <summary>
    /// Unique identifier for this result, 1-64 Bytes
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Id { get; init; }

    /// <summary>
    /// Optional. Inline keyboard attached to the message
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public InlineKeyboardMarkup? ReplyMarkup { get; set; }

    /// <summary>
    /// Initializes a new inline query result
    /// </summary>
    /// <param name="id">Unique identifier for this result, 1-64 Bytes</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    protected InlineQueryResult(string id) => Id = id;

    /// <summary>
    /// Initializes a new inline query result
    /// </summary>
    protected InlineQueryResult()
    { }
}

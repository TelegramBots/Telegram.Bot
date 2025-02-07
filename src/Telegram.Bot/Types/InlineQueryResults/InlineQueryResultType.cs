// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.InlineQueryResults;

/// <summary>Type of the result</summary>
[JsonConverter(typeof(EnumConverter<InlineQueryResultType>))]
public enum InlineQueryResultType
{
    /// <summary>Represents a link to an article or web page.<br/><br/><i>(<see cref="InlineQueryResult"/> can be cast into <see cref="InlineQueryResultArticle"/>)</i></summary>
    Article = 1,
    /// <summary>Represents a link to a photo. By default, this photo will be sent by the user with optional caption. Alternatively, you can use <see cref="InputMessageContent">InputMessageContent</see> to send a message with the specified content instead of the photo.<br/><br/><i>(<see cref="InlineQueryResult"/> can be cast into <see cref="InlineQueryResultPhoto"/> or <see cref="InlineQueryResultCachedPhoto"/>)</i></summary>
    Photo,
    /// <summary>Represents a link to an animated GIF file. By default, this animated GIF file will be sent by the user with optional caption. Alternatively, you can use <see cref="InputMessageContent">InputMessageContent</see> to send a message with the specified content instead of the animation.<br/><br/><i>(<see cref="InlineQueryResult"/> can be cast into <see cref="InlineQueryResultGif"/> or <see cref="InlineQueryResultCachedGif"/>)</i></summary>
    Gif,
    /// <summary>Represents a link to a video animation (H.264/MPEG-4 AVC video without sound). By default, this animated MPEG-4 file will be sent by the user with optional caption. Alternatively, you can use <see cref="InputMessageContent">InputMessageContent</see> to send a message with the specified content instead of the animation.<br/><br/><i>(<see cref="InlineQueryResult"/> can be cast into <see cref="InlineQueryResultMpeg4Gif"/> or <see cref="InlineQueryResultCachedMpeg4Gif"/>)</i></summary>
    Mpeg4Gif,
    /// <summary>Represents a link to a page containing an embedded video player or a video file. By default, this video file will be sent by the user with an optional caption. Alternatively, you can use <see cref="InputMessageContent">InputMessageContent</see> to send a message with the specified content instead of the video.<br/><br/><i>(<see cref="InlineQueryResult"/> can be cast into <see cref="InlineQueryResultVideo"/> or <see cref="InlineQueryResultCachedVideo"/>)</i></summary>
    Video,
    /// <summary>Represents a link to an MP3 audio file. By default, this audio file will be sent by the user. Alternatively, you can use <see cref="InputMessageContent">InputMessageContent</see> to send a message with the specified content instead of the audio.<br/><br/><i>(<see cref="InlineQueryResult"/> can be cast into <see cref="InlineQueryResultAudio"/> or <see cref="InlineQueryResultCachedAudio"/>)</i></summary>
    Audio,
    /// <summary>Represents a contact with a phone number. By default, this contact will be sent by the user. Alternatively, you can use <see cref="InputMessageContent">InputMessageContent</see> to send a message with the specified content instead of the contact.<br/><br/><i>(<see cref="InlineQueryResult"/> can be cast into <see cref="InlineQueryResultContact"/>)</i></summary>
    Contact,
    /// <summary>Represents a link to a file. By default, this file will be sent by the user with an optional caption. Alternatively, you can use <see cref="InputMessageContent">InputMessageContent</see> to send a message with the specified content instead of the file. Currently, only <b>.PDF</b> and <b>.ZIP</b> files can be sent using this method.<br/><br/><i>(<see cref="InlineQueryResult"/> can be cast into <see cref="InlineQueryResultDocument"/> or <see cref="InlineQueryResultCachedDocument"/>)</i></summary>
    Document,
    /// <summary>Represents a location on a map. By default, the location will be sent by the user. Alternatively, you can use <see cref="InputMessageContent">InputMessageContent</see> to send a message with the specified content instead of the location.<br/><br/><i>(<see cref="InlineQueryResult"/> can be cast into <see cref="InlineQueryResultLocation"/>)</i></summary>
    Location,
    /// <summary>Represents a venue. By default, the venue will be sent by the user. Alternatively, you can use <see cref="InputMessageContent">InputMessageContent</see> to send a message with the specified content instead of the venue.<br/><br/><i>(<see cref="InlineQueryResult"/> can be cast into <see cref="InlineQueryResultVenue"/>)</i></summary>
    Venue,
    /// <summary>Represents a link to a voice recording in an .OGG container encoded with OPUS. By default, this voice recording will be sent by the user. Alternatively, you can use <see cref="InputMessageContent">InputMessageContent</see> to send a message with the specified content instead of the the voice message.<br/><br/><i>(<see cref="InlineQueryResult"/> can be cast into <see cref="InlineQueryResultVoice"/> or <see cref="InlineQueryResultCachedVoice"/>)</i></summary>
    Voice,
    /// <summary>Represents a <a href="https://core.telegram.org/bots/api#games">Game</a>.<br/><br/><i>(<see cref="InlineQueryResult"/> can be cast into <see cref="InlineQueryResultGame"/>)</i></summary>
    Game,
    /// <summary>Represents a link to a sticker stored on the Telegram servers. By default, this sticker will be sent by the user. Alternatively, you can use <see cref="InputMessageContent">InputMessageContent</see> to send a message with the specified content instead of the sticker.<br/><br/><i>(<see cref="InlineQueryResult"/> can be cast into <see cref="InlineQueryResultCachedSticker"/>)</i></summary>
    Sticker,
}

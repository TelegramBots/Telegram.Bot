using System.Net.Http;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this request to add a new video sticker to a set created by the bot. Static sticker sets
/// can have up to 120 stickers. Returns <see langword="true"/> on success.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class AddVideoStickerToSetRequest : AddStickerToSetRequest
{
    /// <summary>
    /// <b>WEBM</b> video with the sticker, uploaded using multipart/form-data.
    /// <a href="https://core.telegram.org/stickers#video-sticker-requirements"/>
    /// for technical requirements
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public InputFile WebmSticker { get; }

    /// <inheritdoc />
    /// <param name="webmSticker">
    /// <b>WEBM</b> video with the sticker, uploaded using multipart/form-data.
    /// <a href="https://core.telegram.org/stickers#video-sticker-requirements"/>
    /// for technical requirements
    /// </param>
#pragma warning disable CS1573
    public AddVideoStickerToSetRequest(
        long userId,
        string name,
        InputFile webmSticker,
        string emojis
    ) : base(userId, name, emojis)
        => WebmSticker = webmSticker;
#pragma warning restore CS1573

    /// <inheritdoc />
    public override HttpContent? ToHttpContent()
        => ToMultipartFormDataContent(fileParameterName: "webm_sticker", inputFile: WebmSticker);
}

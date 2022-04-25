using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.InputFiles;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this request to add a new video sticker to a set created by the bot. Static sticker sets
/// can have up to 120 stickers. Returns <c>true</c> on success.
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
    public InputFileStream WebmSticker { get; }

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
        InputFileStream webmSticker,
        string emojis)
        : base(userId, name, emojis) =>
        WebmSticker = webmSticker;
#pragma warning restore CS1573

    /// <inheritdoc />
    public override HttpContent? ToHttpContent() =>
        WebmSticker.Content is not null
            ? ToMultipartFormDataContent(fileParameterName: "webm_sticker", inputFile: WebmSticker)
            : base.ToHttpContent();
}

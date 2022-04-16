using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.InputFiles;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this request to add a new animated sticker to a set created by the bot. Static sticker sets
/// can have up to 120 stickers. Returns <c>true</c> on success.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class AddAnimatedStickerToSetRequest : AddStickerToSetRequest
{
    /// <summary>
    /// <b>WEBM</b> video with the sticker, uploaded using multipart/form-data.
    /// <a href="https://core.telegram.org/stickers#animated-sticker-requirements"/>
    /// for technical requirements
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public InputFileStream TgsSticker { get; }

    /// <inheritdoc />
    /// <param name="tgsSticker">
    /// <b>WEBM</b> video with the sticker, uploaded using multipart/form-data.
    /// <a href="https://core.telegram.org/stickers#animated-sticker-requirements"/>
    /// for technical requirements
    /// </param>
#pragma warning disable CS1573
    public AddAnimatedStickerToSetRequest(
        long userId,
        string name,
        InputFileStream tgsSticker,
        string emojis)
        : base(userId, name, emojis) =>
        TgsSticker = tgsSticker;
#pragma warning restore CS1573

    /// <inheritdoc />
    public override HttpContent? ToHttpContent() =>
        TgsSticker.Content is not null
            ? ToMultipartFormDataContent(fileParameterName: "tgs_sticker", inputFile: TgsSticker)
            : base.ToHttpContent();
}

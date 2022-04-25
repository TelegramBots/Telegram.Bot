using System;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to create a new video sticker set owned by a user. The bot will be able to edit
/// the sticker set thus created. Returns <c>true</c> on success.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class CreateNewVideoStickerSetRequest : CreateNewStickerSetRequest
{
    /// <summary>
    /// <b>WEBM</b> animation with the sticker, uploaded using multipart/form-data. See
    /// <a href="https://core.telegram.org/animated_stickers#technical-requirements"/>
    /// for technical requirements
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public InputFileStream WebmSticker { get; }

    /// <inheritdoc />
    /// <param name="webmSticker">
    /// <b>WEBM</b> animation with the sticker, uploaded using multipart/form-data. See
    /// <a href="https://core.telegram.org/animated_stickers#technical-requirements"/>
    /// for technical requirements
    /// </param>
#pragma warning disable CS1573
    public CreateNewVideoStickerSetRequest(
        long userId,
        string name,
        string title,
        InputFileStream webmSticker,
        string emojis) : base(userId, name, title, emojis)
    {
        WebmSticker = webmSticker ?? throw new ArgumentNullException(nameof(webmSticker), "Sticker is null");
    }
#pragma warning restore CS1573

    /// <inheritdoc />
    public override HttpContent? ToHttpContent() =>
        WebmSticker.FileType == FileType.Stream
            ? ToMultipartFormDataContent(fileParameterName: "webm_sticker", inputFile: WebmSticker)
            : base.ToHttpContent();
}

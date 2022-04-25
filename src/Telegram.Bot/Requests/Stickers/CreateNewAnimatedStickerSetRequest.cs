using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.InputFiles;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to create a new animated sticker set owned by a user. The bot will be able to
/// edit the sticker set thus created. Returns <c>true</c> on success.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class CreateNewAnimatedStickerSetRequest : CreateNewStickerSetRequest
{
    /// <summary>
    /// <b>TGS</b> animation with the sticker, uploaded using multipart/form-data. See
    /// <a href="https://core.telegram.org/animated_stickers#technical-requirements"/>
    /// for technical requirements
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public InputFileStream TgsSticker { get; }

    /// <inheritdoc />
    /// <param name="tgsSticker">
    /// <b>TGS</b> animation with the sticker, uploaded using multipart/form-data. See
    /// <a href="https://core.telegram.org/animated_stickers#technical-requirements"/>
    /// for technical requirements
    /// </param>
#pragma warning disable CS1573
    public CreateNewAnimatedStickerSetRequest(
        long userId,
        string name,
        string title,
        InputFileStream tgsSticker,
        string emojis) : base(userId, name, title, emojis)
    {
        TgsSticker = tgsSticker;
    }
#pragma warning restore CS1573

    /// <inheritdoc />
    public override HttpContent? ToHttpContent() =>
        TgsSticker.Content is not null
            ? ToMultipartFormDataContent(fileParameterName: "tgs_sticker", inputFile: TgsSticker)
            : base.ToHttpContent();
}

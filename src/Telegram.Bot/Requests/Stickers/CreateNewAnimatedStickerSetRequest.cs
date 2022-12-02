using System.Net.Http;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to create a new animated sticker set owned by a user. The bot will be able to
/// edit the sticker set thus created. Returns <see langword="true"/> on success.
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
    public InputFile TgsSticker { get; }

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
        InputFile tgsSticker,
        string emojis
    ) : base(userId, name, title, emojis)
        => TgsSticker = tgsSticker;
#pragma warning restore CS1573

    /// <inheritdoc />
    public override HttpContent? ToHttpContent()
        => ToMultipartFormDataContent(fileParameterName: "tgs_sticker", inputFile: TgsSticker);
}

using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this request to add a new static sticker to a set created by the bot. Static sticker sets
/// can have up to 120 stickers. Returns <c>true</c> on success.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class AddStaticStickerToSetRequest : AddStickerToSetRequest
{
    /// <summary>
    /// <para>
    /// <b>PNG</b> image with the sticker, must be up to 512 kilobytes in size, dimensions must
    /// not exceed 512px, and either width or height must be exactly 512px.
    /// </para>
    /// <para>
    /// Pass a <see cref="Types.InputFiles.InputTelegramFile.FileId"/> as a String to send a file that already
    /// exists on the Telegram servers, pass an HTTP URL as a String for Telegram to get a file from the Internet,
    /// or upload a new one using multipart/form-data
    /// </para>
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public InputOnlineFile PngSticker { get; }
        
    /// <inheritdoc />
    /// <param name="pngSticker">
    /// <b>PNG</b> image with the sticker, must be up to 512 kilobytes in size, dimensions must not
    /// exceed 512px, and either width or height must be exactly 512px. Pass a
    /// <see cref="Types.InputFiles.InputTelegramFile.FileId"/> as a String to send a file that
    /// already exists on the Telegram servers, pass an HTTP URL as a String for Telegram to get
    /// a file from the Internet, or upload a new one using multipart/form-data
    /// </param>
#pragma warning disable CS1573
    public AddStaticStickerToSetRequest(
        long userId,
        string name,
        InputOnlineFile pngSticker,
        string emojis)
        : base(userId, name, emojis) =>
        PngSticker = pngSticker;
#pragma warning restore CS1573

    /// <inheritdoc />
    public override HttpContent? ToHttpContent() =>
        PngSticker.FileType == FileType.Stream
            ? ToMultipartFormDataContent(fileParameterName: "png_sticker", inputFile: PngSticker)
            : base.ToHttpContent();
}
using System.Net.Http;
using Telegram.Bot.Requests.Abstractions;
using File = Telegram.Bot.Types.File;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to upload a .PNG file with a sticker for later use in
/// <see cref="CreateNewStaticStickerSetRequest"/>/<see cref="CreateNewAnimatedStickerSetRequest"/> and
/// <see cref="AddStaticStickerToSetRequest"/>/<see cref="AddAnimatedStickerToSetRequest"/> methods
/// (can be used multiple times). Returns the uploaded <see cref="File"/> on success.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class UploadStickerFileRequest : FileRequestBase<File>, IUserTargetable
{
    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public long UserId { get; }

    /// <summary>
    /// <b>PNG</b> image with the sticker, must be up to 512 kilobytes in size, dimensions must not
    /// exceed 512px, and either width or height must be exactly 512px
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public InputFile PngSticker { get; }

    /// <summary>
    /// Initializes a new request with userId and pngSticker
    /// </summary>
    /// <param name="userId">User identifier of sticker file owner</param>
    /// <param name="pngSticker">
    /// <b>PNG</b> image with the sticker, must be up to 512 kilobytes in size, dimensions must not
    /// exceed 512px, and either width or height must be exactly 512px
    /// </param>
    public UploadStickerFileRequest(long userId, InputFile pngSticker)
        : base("uploadStickerFile")
    {
        UserId = userId;
        PngSticker = pngSticker;
    }

    /// <inheritdoc />
    public override HttpContent? ToHttpContent()
        => ToMultipartFormDataContent(fileParameterName: "png_sticker", inputFile: PngSticker);
}

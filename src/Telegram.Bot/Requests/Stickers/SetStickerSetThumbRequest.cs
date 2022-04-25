using System.Net.Http;
 using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types.Enums;
 using Telegram.Bot.Types.InputFiles;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to set the thumbnail of a sticker set. Animated thumbnails can be set for
/// animated sticker sets only. Returns <c>true</c> on success.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class SetStickerSetThumbRequest : FileRequestBase<bool>, IUserTargetable
{
    /// <summary>
    /// Sticker set name
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Name { get; }

    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public long UserId { get; }

    /// <summary>
    /// A <b>PNG</b> image with the thumbnail, must be up to 128 kilobytes in size and have width
    /// and height exactly 100px, or a <b>TGS</b> animation with the thumbnail up to 32 kilobytes in
    /// size; see <a href="https://core.telegram.org/animated_stickers#technical-requirements"/>
    /// for animated sticker technical requirements. Pass a <see cref="InputTelegramFile.FileId"/>
    /// as a String to send a file that already exists on the Telegram servers, pass an HTTP URL as
    /// a String for Telegram to get a file from the Internet, or upload a new one using
    /// multipart/form-data. Animated sticker set thumbnail can't be uploaded via HTTP URL
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public InputOnlineFile? Thumb { get; set; }

    /// <summary>
    /// Initializes a new request with sticker and position
    /// </summary>
    /// <param name="name">Sticker set name</param>
    /// <param name="userId">User identifier of the sticker set owner</param>
    public SetStickerSetThumbRequest(string name, long userId)
        : base("setStickerSetThumb")
    {
        Name = name;
        UserId = userId;
    }

    /// <inheritdoc />
    public override HttpContent? ToHttpContent() =>
        Thumb?.FileType switch {
            FileType.Stream => ToMultipartFormDataContent(fileParameterName: "thumb", inputFile: Thumb),
            _               => base.ToHttpContent()
        };
}

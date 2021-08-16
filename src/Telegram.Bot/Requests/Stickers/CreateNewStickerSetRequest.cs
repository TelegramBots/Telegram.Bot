using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Use this method to create a new static sticker set owned by a user. The bot will be able to edit
    /// the sticker set thus created. Returns True on success.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class CreateNewStickerSetRequest : FileRequestBase<bool>, IUserTargetable
    {
        /// <inheritdoc />
        [JsonProperty(Required = Required.Always)]
        public long UserId { get; }

        /// <summary>
        /// Short name of sticker set, to be used in <c>t.me/addstickers/</c> URLs (e.g., <i>animals</i>).
        /// Can contain only english letters, digits and underscores. Must begin with a letter, can't
        /// contain consecutive underscores and must end in <i>"_by_&lt;bot username&gt;"</i>.
        /// <i>&lt;bot_username&gt;</i> is case insensitive. 1-64 characters
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Name { get; }

        /// <summary>
        /// Sticker set title, 1-64 characters
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Title { get; }

        /// <summary>
        /// <b>PNG</b> image with the sticker, must be up to 512 kilobytes in size, dimensions must
        /// not exceed 512px, and either width or height must be exactly 512px. Pass a
        /// <see cref="Types.InputFiles.InputTelegramFile.FileId"/> as a String to send a file that
        /// already exists on the Telegram servers, pass an HTTP URL as a String for Telegram to
        /// get a file from the Internet, or upload a new one using multipart/form-data
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public InputOnlineFile PngSticker { get; }

        /// <summary>
        /// One or more emoji corresponding to the sticker
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Emojis { get; }

        /// <summary>
        /// Pass True, if a set of mask stickers should be created
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? ContainsMasks { get; set; }

        /// <summary>
        /// An object for position where the mask should be placed on faces
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public MaskPosition? MaskPosition { get; set; }

        /// <summary>
        /// Initializes a new request with userId, name, pngSticker and emojis
        /// </summary>
        /// <param name="userId">User identifier of sticker set owner</param>
        /// <param name="name">
        /// Short name of sticker set, to be used in <c>t.me/addstickers/</c> URLs (e.g., <i>animals</i>).
        /// Can contain only english letters, digits and underscores. Must begin with a letter, can't
        /// contain consecutive underscores and must end in <i>"_by_&lt;bot username&gt;"</i>.
        /// <i>&lt;bot_username&gt;</i> is case insensitive. 1-64 characters
        /// </param>
        /// <param name="title">Sticker set title, 1-64 characters</param>
        /// <param name="pngSticker">
        /// <b>PNG</b> image with the sticker, must be up to 512 kilobytes in size, dimensions must not
        /// exceed 512px, and either width or height must be exactly 512px. Pass a
        /// <see cref="Types.InputFiles.InputTelegramFile.FileId"/> as a String to send a file that
        /// already exists on the Telegram servers, pass an HTTP URL as a String for Telegram to get
        /// a file from the Internet, or upload a new one using multipart/form-data
        /// </param>
        /// <param name="emojis">One or more emoji corresponding to the sticker</param>
        public CreateNewStickerSetRequest(
            long userId,
            string name,
            string title,
            InputOnlineFile pngSticker,
            string emojis) : base("createNewStickerSet")
        {
            UserId = userId;
            Name = name;
            Title = title;
            PngSticker = pngSticker;
            Emojis = emojis;
        }

        /// <inheritdoc />
        public override HttpContent? ToHttpContent() =>
            PngSticker.FileType == FileType.Stream
                ? ToMultipartFormDataContent(fileParameterName: "png_sticker", inputFile: PngSticker)
                : base.ToHttpContent();
    }
}

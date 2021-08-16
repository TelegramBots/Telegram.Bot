using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Use this method to create a new animated sticker set owned by a user. The bot will be able to
    /// edit the sticker set thus created. Returns <c>true</c> on success.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class CreateNewAnimatedStickerSetRequest : FileRequestBase<bool>, IUserTargetable
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
        /// <b>TGS</b> animation with the sticker, uploaded using multipart/form-data. See
        /// <see href="https://core.telegram.org/animated_stickers#technical-requirements"/>
        /// for technical requirements
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public InputFileStream TgsSticker { get; }

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
        /// Initializes a new request with userId, name, tgsSticker and emojis
        /// </summary>
        /// <param name="userId">User identifier of sticker set owner</param>
        /// <param name="name">
        /// Short name of sticker set, to be used in <c>t.me/addstickers/</c> URLs (e.g., <i>animals</i>).
        /// Can contain only english letters, digits and underscores. Must begin with a letter, can't
        /// contain consecutive underscores and must end in <i>"_by_&lt;bot username&gt;"</i>.
        /// <i>&lt;bot_username&gt;</i> is case insensitive. 1-64 characters
        /// </param>
        /// <param name="title">Sticker set title, 1-64 characters</param>
        /// <param name="tgsSticker">
        /// <b>TGS</b> animation with the sticker, uploaded using multipart/form-data. See
        /// <see href="https://core.telegram.org/animated_stickers#technical-requirements"/>
        /// for technical requirements
        /// </param>
        /// <param name="emojis">One or more emoji corresponding to the sticker</param>
        public CreateNewAnimatedStickerSetRequest(
            long userId,
            string name,
            string title,
            InputFileStream tgsSticker,
            string emojis) : base("createNewStickerSet")
        {
            UserId = userId;
            Name = name;
            Title = title;
            TgsSticker = tgsSticker;
            Emojis = emojis;
        }

        /// <inheritdoc />
        public override HttpContent? ToHttpContent() =>
            TgsSticker.Content is not null
                ? ToMultipartFormDataContent(fileParameterName: "tgs_sticker", inputFile: TgsSticker)
                : base.ToHttpContent();
    }
}

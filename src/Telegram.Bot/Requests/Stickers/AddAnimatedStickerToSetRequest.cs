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
    /// Use this request to add a new sticker to a set created by the bot. Animated stickers can be
    /// added to animated sticker sets and only to them. Animated sticker sets can have up to 50
    /// stickers. Returns <c>true</c> on success.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class AddAnimatedStickerToSetRequest : FileRequestBase<bool>, IUserTargetable
    {
        /// <inheritdoc />
        [JsonProperty(Required = Required.Always)]
        public long UserId { get; }

        /// <summary>
        /// Sticker set name
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Name { get; }

        /// <summary>
        /// <b>TGS</b> animation with the sticker, uploaded using multipart/form-data.
        /// See <see href="https://core.telegram.org/animated_stickers#technical-requirements"/>
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
        /// An object for position where the mask should be placed on faces
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public MaskPosition? MaskPosition { get; set; }

        /// <summary>
        /// Initializes a new request with userId, name, tgsSticker and emojis
        /// </summary>
        /// <param name="userId">User identifier of sticker set owner</param>
        /// <param name="name">Sticker set name</param>
        /// <param name="tgsSticker">
        /// <b>TGS</b> animation with the sticker, uploaded using multipart/form-data.
        /// See <see href="https://core.telegram.org/animated_stickers#technical-requirements"/>
        /// for technical requirements
        /// </param>
        /// <param name="emojis">One or more emoji corresponding to the sticker</param>
        public AddAnimatedStickerToSetRequest(
            long userId,
            string name,
            InputFileStream tgsSticker,
            string emojis) : base("addStickerToSet")
        {
            UserId = userId;
            Name = name;
            TgsSticker = tgsSticker;
            Emojis = emojis;
        }

        /// <inheritdoc />
        public override HttpContent? ToHttpContent() =>
            TgsSticker.Content is null
                ? base.ToHttpContent()
                : ToMultipartFormDataContent("tgs_sticker", TgsSticker);
    }
}

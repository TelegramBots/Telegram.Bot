using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Telegram.Bot.Helpers;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Create new sticker set owned by a user. The bot will be able to edit the created sticker set. Returns True on success.
    /// </summary>
    public class CreateNewStickerSetRequest : FileRequestBase<bool>
    {
        /// <summary>
        /// User identifier of sticker set owner
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Short name of sticker set, to be used in t.me/addstickers/ URLs (e.g., animals). Can contain only english letters, digits and underscores. Must begin with a letter, can't contain consecutive underscores and must end in “_by_[bot username]”. [bot_username] is case insensitive. 1-64 characters.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Sticker set title, 1-64 characters
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Png image with the sticker, must be up to 512 kilobytes in size, dimensions must not exceed 512px, and either width or height must be exactly 512px.
        /// </summary>
        public FileToSend PngSticker { get; set; }

        /// <summary>
        /// One or more emoji corresponding to the sticker
        /// </summary>
        public string Emojis { get; set; }

        /// <summary>
        /// Pass True, if a set of mask stickers should be created
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool ContainsMasks { get; set; }

        /// <summary>
        /// One or more emoji corresponding to the sticker
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public MaskPosition MaskPosition { get; set; }

        /// <summary>
        /// Initializes a new request
        /// </summary>
        public CreateNewStickerSetRequest()
            : base("createNewStickerSet")
        { }

        /// <summary>
        /// Initializes a new request with userId, name, pngSticker and emojis
        /// </summary>
        /// <param name="userId">User identifier of sticker set owner</param>
        /// <param name="name">Sticker set name</param>
        /// <param name="title">Sticker set title, 1-64 characters</param>
        /// <param name="pngSticker">Png image with the sticker</param>
        /// <param name="emojis">One or more emoji corresponding to the sticker</param>
        public CreateNewStickerSetRequest(int userId, string name, string title, FileToSend pngSticker, string emojis)
            : this()
        {
            UserId = userId;
            Name = name;
            Title = title;
            PngSticker = pngSticker;
            Emojis = emojis;
        }

        /// <summary>
        /// Generate content of HTTP message
        /// </summary>
        /// <param name="serializerSettings">JSON serialization setting</param>
        /// <returns>Content of HTTP request</returns>
        public override HttpContent ToHttpContent(JsonSerializerSettings serializerSettings)
        {
            HttpContent content;

            if (PngSticker.Type == FileType.Stream)
            {
                var parameters = new Dictionary<string, object>
                {
                    { nameof(UserId).ToSnakeCased(), UserId },
                    { nameof(Name).ToSnakeCased(), Name },
                    { nameof(Title).ToSnakeCased(), Title },
                    { nameof(PngSticker).ToSnakeCased(), PngSticker },
                    { nameof(Emojis).ToSnakeCased(), Emojis },
                    { nameof(MaskPosition).ToSnakeCased(), MaskPosition },
                };

                if (ContainsMasks)
                {
                    parameters.Add(nameof(ContainsMasks).ToSnakeCased(), ContainsMasks);
                }

                content = GetMultipartContent(parameters, serializerSettings);
            }
            else
            {
                content = base.ToHttpContent(serializerSettings);
            }

            return content;
        }
    }
}

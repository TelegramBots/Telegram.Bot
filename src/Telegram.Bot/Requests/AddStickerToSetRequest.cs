using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Telegram.Bot.Helpers;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Add a new sticker to a set created by the bot. Returns True on success.
    /// </summary>
    public class AddStickerToSetRequest : FileRequestBase<bool>
    {
        /// <summary>
        /// User identifier of sticker set owner
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Sticker set name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Png image with the sticker, must be up to 512 kilobytes in size, dimensions must not exceed 512px, and either width or height must be exactly 512px
        /// </summary>
        public FileToSend PngSticker { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Emojis { get; set; }

        /// <summary>
        /// One or more emoji corresponding to the sticker
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public MaskPosition MaskPosition { get; set; }

        /// <summary>
        /// Initializes a new request
        /// </summary>
        public AddStickerToSetRequest()
            : base("addStickerToSet")
        { }

        /// <summary>
        /// Initializes a new request with userId, name pngSticker and emojis
        /// </summary>
        /// <param name="userId">User identifier of sticker set owner</param>
        /// <param name="name">Sticker set name</param>
        /// <param name="pngSticker">Png image with the sticker</param>
        /// <param name="emojis">One or more emoji corresponding to the sticker</param>
        public AddStickerToSetRequest(int userId, string name, FileToSend pngSticker, string emojis)
            : this()
        {
            UserId = userId;
            Name = name;
            PngSticker = pngSticker;
            Emojis = emojis;
        }

        /// <summary>
        /// Generate content of HTTP message
        /// </summary>
        /// <returns>Content of HTTP request</returns>
        public override HttpContent ToHttpContent()
        {
            HttpContent content;

            if (PngSticker.Type == FileType.Stream)
            {
                var parameters = new Dictionary<string, object>
                {
                    { nameof(UserId).ToSnakeCased(), UserId },
                    { nameof(Name).ToSnakeCased(), Name },
                    { nameof(PngSticker).ToSnakeCased(), PngSticker },
                    { nameof(Emojis).ToSnakeCased(), Emojis },
                    { nameof(MaskPosition).ToSnakeCased(), MaskPosition },
                };

                content = GetMultipartContent(parameters);
            }
            else
            {
                content = base.ToHttpContent();
            }

            return content;
        }
    }
}

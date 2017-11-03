using Newtonsoft.Json;
using System.Collections.Generic;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a sticker set.
    /// <see href="https://core.telegram.org/bots/api#stickerset"/>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class StickerSet
    {
        /// <summary>
        /// Sticker set name
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }

        /// <summary>
        /// Sticker set title
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Title { get; set; }

        /// <summary>
        /// True, if the sticker set contains masks
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public bool ContainsMasks { get; set; }

        /// <summary>
        /// List of all set stickers
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public List<Sticker> Stickers { get; set; } = new List<Sticker>();
    }
}

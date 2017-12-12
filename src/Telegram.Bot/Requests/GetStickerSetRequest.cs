using Telegram.Bot.Types;

namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Get a sticker set. On success, a <see cref="StickerSet"/> object is returned.
    /// </summary>
    public class GetStickerSetRequest : RequestBase<StickerSet>
    {
        /// <summary>
        /// Name of the sticker set
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Initializes a new request
        /// </summary>
        public GetStickerSetRequest()
            : base("getStickerSet")
        { }

        /// <summary>
        /// Initializes a new request with name
        /// </summary>
        /// <param name="name">Name of the sticker set</param>
        public GetStickerSetRequest(string name)
            : this()
        {
            Name = name;
        }
    }
}

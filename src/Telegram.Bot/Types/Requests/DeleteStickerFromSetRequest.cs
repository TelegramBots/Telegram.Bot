using System.Collections.Generic;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to delete a sticker from a set
    /// </summary>
    public class DeleteStickerFromSetRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteStickerFromSetRequest"/> class
        /// </summary>
        /// <param name="sticker">File identifier of the sticker</param>
        public DeleteStickerFromSetRequest(string sticker) : base("deleteStickerFromSet",
            new Dictionary<string, object>()
            {
                { "sticker", sticker }
            })
        {

        }
    }
}

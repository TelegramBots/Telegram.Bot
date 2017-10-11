using System.Collections.Generic;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to get a sticker set
    /// </summary>
    public class GetStickerSetRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetStickerSetRequest"/> class
        /// </summary>
        /// <param name="name">Short name of the sticker set that is used in t.me/addstickers/ URLs (e.g., animals)</param>
        public GetStickerSetRequest(string name) : base("getStickerSet",
            new Dictionary<string, object> { { "name", name } })
        {

        }
    }
}

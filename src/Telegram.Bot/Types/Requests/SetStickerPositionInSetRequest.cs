using System.Collections.Generic;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to set a sticker's position in a set
    /// </summary>
    public class SetStickerPositionInSetRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetStickerPositionInSetRequest"/> class
        /// </summary>
        /// <param name="sticker">File identifier of the sticker</param>
        /// <param name="position">New sticker position in the set, zero-based</param>
        public SetStickerPositionInSetRequest(string sticker, int position) : base("setStickerPositionInSet",
            new Dictionary<string, object>()
            {
                { "sticker", sticker },
                { "position", position }
            })
        {

        }
    }
}

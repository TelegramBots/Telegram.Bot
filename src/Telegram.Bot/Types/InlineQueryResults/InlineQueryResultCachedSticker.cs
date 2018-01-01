using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.ComponentModel;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a link to a sticker stored on the Telegram servers. By default, this sticker will be sent by the user. Alternatively, you can use input_message_content to send a message with the specified content instead of the sticker.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn,
                NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class InlineQueryResultCachedSticker : InlineQueryResult
    {
        /// <summary>
        /// Initializes a new inline query result
        /// </summary>
        public InlineQueryResultCachedSticker()
        {
            Type = InlineQueryResultType.CachedSticker;
        }

        /// <summary>
        /// A valid file identifier of the sticker
        /// </summary>
        [JsonProperty("sticker_file_id", Required = Required.Always)]
        public string FileId { get; set; }

        /// <summary>
        /// Title of the result
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new string Title { get; set; }
    }
}

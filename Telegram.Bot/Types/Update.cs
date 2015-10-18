using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents an incoming update.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Update
    {
        /// <summary>
        /// The update‘s unique identifier. Update identifiers start from a certain positive number and increase sequentially.
        /// This ID becomes especially handy if you’re using Webhooks, since it allows you to ignore repeated updates or to
        /// restore the correct update sequence, should they get out of order.
        /// </summary>
        [JsonProperty(PropertyName = "update_id", Required = Required.Always)]
        public int Id { get; internal set; }

        /// <summary>
        /// Optional. New incoming message of any kind — text, photo, sticker, etc.
        /// </summary>
        [JsonProperty(PropertyName = "message", Required = Required.Default)]
        public Message Message { get; internal set; }

        public static Update FromString(string data)
        {
            return JsonConvert.DeserializeObject<Update>(data);
        }
    }
}

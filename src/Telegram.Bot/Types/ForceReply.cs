using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Upon receiving a message with this object, Telegram clients will display a reply interface to the user (act as if the user has selected the bot‘s message and tapped ’Reply'). This can be extremely useful if you want to create user-friendly step-by-step interfaces without having to sacrifice privacy mode.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class ForceReply : ReplyMarkup
    {
        /// <summary>
        /// Shows reply interface to the user, as if they manually selected the bot‘s message and tapped ’Reply'
        /// </summary>
        [JsonProperty(PropertyName = "force_reply", Required = Required.Always)]
        public bool Force { get; set; }
    }
}

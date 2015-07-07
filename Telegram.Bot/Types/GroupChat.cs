using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a group chat.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class GroupChat : IConversation
    {
        /// <summary>
        /// Unique identifier for this group chat
        /// </summary>
        /// <returns></returns>
        [JsonProperty(PropertyName = "id", Required = Required.Always)]
        public int Id { get; set; }

        /// <summary>
        /// Group name
        /// </summary>
        [JsonProperty(PropertyName = "title", Required = Required.Always)]
        public string Title;
    }
}

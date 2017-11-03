using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a phone contact.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Contact
    {
        /// <summary>
        /// Contact's phone number
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Contact's first name
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string FirstName { get; set; }

        /// <summary>
        /// Optional. Contact's last name
        /// </summary>
        [JsonProperty]
        public string LastName { get; set; }

        /// <summary>
        /// Optional. Contact's user identifier in Telegram
        /// </summary>
        [JsonProperty]
        public int UserId { get; set; }
    }
}

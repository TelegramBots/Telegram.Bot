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
        [JsonProperty(PropertyName = "phone_number", Required = Required.Always)]
        public string PhoneNumber { get; internal set; }

        /// <summary>
        /// Contact's first name
        /// </summary>
        [JsonProperty(PropertyName = "first_name", Required = Required.Always)]
        public string FirstName { get; internal set; }

        /// <summary>
        /// Optional. Contact's last name
        /// </summary>
        [JsonProperty(PropertyName = "last_name", Required = Required.Default)]
        public string LastName { get; internal set; }

        /// <summary>
        /// Optional. Contact's user identifier in Telegram
        /// </summary>
        [JsonProperty(PropertyName = "user_id", Required = Required.Default)]
        public int UserId { get; internal set; }
    }
}

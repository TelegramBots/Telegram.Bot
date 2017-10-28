using Newtonsoft.Json;

namespace Telegram.Bot.Types.InputMessageContents
{
    /// <summary>
    /// Represents the content of a contact message to be sent as the result of an <see cref="InlineQuery"/>.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class InputContactMessageContent : InputMessageContent
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
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string LastName { get; set; }
    }
}

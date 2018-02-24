using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents the content of a contact message to be sent as the result of an <see cref="InlineQuery"/>.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class InputContactMessageContent : InputMessageContentBase
    {
        /// <summary>
        /// Contact's phone number
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string PhoneNumber { get; private set; }

        /// <summary>
        /// Contact's first name
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string FirstName { get; private set; }

        /// <summary>
        /// Optional. Contact's last name
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string LastName { get; set; }

        private InputContactMessageContent()
        { }

        /// <summary>
        /// Initializes a new input contact message content
        /// </summary>
        /// <param name="phoneNumber">The phone number of the contact</param>
        /// <param name="firstName">The first name of the contact</param>
        public InputContactMessageContent(string phoneNumber, string firstName)
        {
            PhoneNumber = phoneNumber;
            FirstName = firstName;
        }
    }
}
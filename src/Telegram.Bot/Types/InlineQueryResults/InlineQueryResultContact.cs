using Newtonsoft.Json;
using System.ComponentModel;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a contact with a phone number. By default, this contact will be sent by the user. Alternatively, you can use input_message_content to send a message with the specified content instead of the contact.
    /// </summary>
    /// <remarks>
    /// This will only work in Telegram versions released after 9 April, 2016. Older clients will ignore them.
    /// </remarks>
    [JsonObject(MemberSerialization.OptIn)]
    public class InlineQueryResultContact : InlineQueryResultNew
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

        /// <summary>
        /// Title of the result
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new string Title { get; set; }
    }
}

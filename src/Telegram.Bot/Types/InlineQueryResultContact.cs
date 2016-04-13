using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents a contact with a phone number. By default, this contact will be sent by the user. Alternatively, you can use input_message_content to send a message with the specified content instead of the contact.
    /// </summary>
    /// <remarks>
    /// This will only work in Telegram versions released after 9 April, 2016. Older clients will ignore them.
    /// </remarks>
    [JsonObject(MemberSerialization.OptIn)]
    public class InlineQueryResultContact : InlineQueryResult
    {
        /// <summary>
        /// Contact's phone number
        /// </summary>
        [JsonProperty("phone_number", Required = Required.Always)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Contact's first name
        /// </summary>
        [JsonProperty("first_name", Required = Required.Always)]
        public string FirstName { get; set; }

        /// <summary>
        /// Optional. Contact's last name
        /// </summary>
        [JsonProperty("last_name", Required = Required.Default)]
        public string LastName { get; set; }
        
        /// <summary>
        /// Optional. Content of the message to be sent instead of the audio
        /// </summary>
        [JsonProperty("input_message_content", Required = Required.Default)]
        public InputMessageContent InputMessageContent { get; set; }

        /// <summary>
        /// Optional. Thumbnail width
        /// </summary>
        [JsonProperty("thumb_width", Required = Required.Default)]
        public string ThumbWidth { get; set; }
        
        /// <summary>
        /// Optional. Thumbnail height
        /// </summary>
        [JsonProperty("thumb_height", Required = Required.Default)]
        public string ThumbHeight { get; set; }
        
        [JsonIgnore]
        public new string Title { get; set; }

        [JsonIgnore]
        public new string MessageText { get; set; }

        [JsonIgnore]
        public new ParseMode ParseMode { get; set; }

        [JsonIgnore]
        public new bool DisableWebPagePreview { get; set; }
    }
}

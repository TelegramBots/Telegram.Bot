using Newtonsoft.Json;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types.InputMessageContents
{
    /// <summary>
    /// Represents the content of a text message to be sent as the result of an <see cref="InlineQuery"/>.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class InputTextMessageContent : InputMessageContent
    {
        /// <summary>
        /// Text of the message to be sent, 1-4096 characters
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string MessageText { get; set; }

        /// <summary>
        /// Optional. Send Markdown or HTML, if you want Telegram apps to show bold, italic, fixed-width text or inline URLs in your bot's message.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public ParseMode ParseMode { get; set; } = ParseMode.Default;

        /// <summary>
        /// Optional. Disables link previews for links in the sent message
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public bool DisableWebPagePreview { get; set; }
    }
}

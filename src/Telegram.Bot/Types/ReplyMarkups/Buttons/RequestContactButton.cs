using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types.ReplyMarkups.Buttons
{
    /// <summary>
    /// The user's phone number will be sent as a contact when the button is pressed. Available in private chats only.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn,
        NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class RequestContactButton : KeyboardButton
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestContactButton"/> class.
        /// </summary>
        /// <param name="text">Label text on the button</param>
        public RequestContactButton(string text)
            : base(text)
        { }

        /// <summary>
        /// If <c>true</c>, the user's phone number will be sent as a contact when the button is pressed. Available in private chats only
        /// </summary>
        [JsonProperty]
        public bool RequestContact => true;
    }
}

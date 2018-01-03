using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types.ReplyMarkups.Buttons
{
    /// <summary>
    /// The user's current location will be sent when the button is pressed. Available in private chats only.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class RequestLocationButton : KeyboardButton
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestLocationButton"/> class.
        /// </summary>
        /// <param name="text">Label text on the button</param>
        public RequestLocationButton(string text)
            : base(text)
        { }

        /// <summary>
        /// If <c>true</c>, the user's current location will be sent when the button is pressed. Available in private chats only
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public bool RequestLocation => true;
    }
}

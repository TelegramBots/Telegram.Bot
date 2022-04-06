using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types.ReplyMarkups;

/// <summary>
/// This object represents one button of the reply keyboard. For simple text buttons String can be used instead of this object to specify text of the button.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class KeyboardButton : IKeyboardButton
{
    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public string Text { get; set; }

    /// <summary>
    /// Optional. If <c>true</c>, the user's phone number will be sent as a contact when the button is pressed. Available in private chats only
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? RequestContact { get; set; }

    /// <summary>
    /// Optional. If <c>true</c>, the user's current location will be sent when the button is pressed. Available in private chats only
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? RequestLocation { get; set; }

    /// <summary>
    /// Optional. If specified, the user will be asked to create a poll and send it to the bot when the button is pressed. Available in private chats only
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public KeyboardButtonPollType? RequestPoll { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="KeyboardButton"/> class.
    /// </summary>
    /// <param name="text">Label text on the button</param>
    [JsonConstructor]
    public KeyboardButton(string text)
    {
        Text = text;
    }

    /// <summary>
    /// Generate a keyboard button to request for contact
    /// </summary>
    /// <param name="text">Button's text</param>
    /// <returns>Keyboard button</returns>
    public static KeyboardButton WithRequestContact(string text) =>
        new(text) { RequestContact = true };

    /// <summary>
    /// Generate a keyboard button to request for location
    /// </summary>
    /// <param name="text">Button's text</param>
    /// <returns>Keyboard button</returns>
    public static KeyboardButton WithRequestLocation(string text) =>
        new(text) { RequestLocation = true };

    /// <summary>
    /// Generate a keyboard button to request a poll
    /// </summary>
    /// <param name="text">Button's text</param>
    /// <param name="type">Poll's type</param>
    /// <returns>Keyboard button</returns>
    public static KeyboardButton WithRequestPoll(string text, string? type = default) =>
        new(text) { RequestPoll = new() { Type = type }};

    /// <summary>
    /// Generate a keyboard button from text
    /// </summary>
    /// <param name="text">Button's text</param>
    /// <returns>Keyboard button</returns>
    public static implicit operator KeyboardButton(string text)
        => new(text);
}
namespace Telegram.Bot.Types.ReplyMarkups;

/// <summary>
/// This object represents one button of the reply keyboard. For simple text buttons <see cref="string"/> can be
/// used instead of this object to specify text of the button.
/// </summary>
/// <remarks>
/// <para>
/// <b>Note</b>: <see cref="RequestContact"/> and <see cref="RequestLocation"/> options will only work in Telegram
/// versions released after 9 April, 2016. Older clients will display unsupported message.
/// </para>
/// <para>
/// <b>Note</b>: <see cref="RequestPoll"/> option will only work in Telegram versions released after 23 January, 2020.
/// Older clients will display unsupported message.
/// </para>
/// <para>
/// <b>Note</b>: <see cref="WebApp"/> option will only work in Telegram versions released after 16 April, 2022. Older
/// clients will display unsupported message.
/// </para>
/// </remarks>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class KeyboardButton : IKeyboardButton
{
    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public string Text { get; set; }

    /// <summary>
    /// Optional. If specified, pressing the button will open a list of suitable users. Tapping on any user will send
    /// their identifier to the bot in a “user_shared” service message. Available in private chats only.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public KeyboardButtonRequestUser? RequestUser { get; set; }

    /// <summary>
    /// Optional. If specified, pressing the button will open a list of suitable chats. Tapping on a chat will send
    /// its identifier to the bot in a “chat_shared” service message. Available in private chats only.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public KeyboardButtonRequestChat? RequestChat { get; set; }

    /// <summary>
    /// Optional. If <see langword="true"/>, the user's phone number will be sent as a contact when the button
    /// is pressed. Available in private chats only
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? RequestContact { get; set; }

    /// <summary>
    /// Optional. If <see langword="true"/>, the user's current location will be sent when the button is pressed.
    /// Available in private chats only
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? RequestLocation { get; set; }

    /// <summary>
    /// Optional. If specified, the user will be asked to create a poll and send it to the bot when the button
    /// is pressed. Available in private chats only
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public KeyboardButtonPollType? RequestPoll { get; set; }

    /// <summary>
    /// Optional. If specified, the described Web App will be launched when the button is pressed. The Web App will
    /// be able to send a “web_app_data” service message. Available in private chats only.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public WebAppInfo? WebApp { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="KeyboardButton"/> class.
    /// </summary>
    /// <param name="text">Label text on the button</param>
    [JsonConstructor]
    public KeyboardButton(string text) => Text = text;

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
    /// Generate a keyboard button to request a web app
    /// </summary>
    /// <param name="text">Button's text</param>
    /// <param name="webAppInfo">Web app information</param>
    /// <returns></returns>
    public static KeyboardButton WithWebApp(string text, WebAppInfo webAppInfo) =>
        new(text) { WebApp = webAppInfo };

    /// <summary>
    /// Generate a keyboard button to request user info
    /// </summary>
    /// <param name="text">Button's text</param>
    /// <param name="requestUser">Criteria used to request a suitable user</param>
    /// <returns></returns>
    public static KeyboardButton WithRequestUser(string text, KeyboardButtonRequestUser requestUser) =>
        new(text) { RequestUser = requestUser };

    /// <summary>
    /// Generate a keyboard button to request chat info
    /// </summary>
    /// <param name="text">Button's text</param>
    /// <param name="requestChat">Criteria used to request a suitable chat</param>
    /// <returns></returns>
    public static KeyboardButton WithRequestChat(string text, KeyboardButtonRequestChat requestChat) =>
        new(text) { RequestChat = requestChat };

    /// <summary>
    /// Generate a keyboard button from text
    /// </summary>
    /// <param name="text">Button's text</param>
    /// <returns>Keyboard button</returns>
    public static implicit operator KeyboardButton(string text)
        => new(text);
}

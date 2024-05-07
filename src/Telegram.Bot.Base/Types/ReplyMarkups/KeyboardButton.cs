using System.Diagnostics.CodeAnalysis;

namespace Telegram.Bot.Types.ReplyMarkups;

/// <summary>
/// This object represents one button of the reply keyboard.
/// For simple text buttons, <see cref="string"/> can be used instead of this object to specify the button text.
/// The optional fields <see cref="WebApp"/>, <see cref="RequestUsers"/>, <see cref="RequestChat"/>,
/// <see cref="RequestContact"/>, <see cref="RequestLocation"/>, and <see cref="RequestPoll"/> are mutually exclusive.
/// </summary>
public class KeyboardButton : IKeyboardButton
{
    /// <inheritdoc />
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Text { get; init; }

    /// <summary>
    /// Optional. If specified, pressing the button will open a list of suitable users. Identifiers of selected users
    /// will be sent to the bot in a "<see cref="UsersShared"/>" service message. Available in private chats only.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public KeyboardButtonRequestUsers? RequestUsers { get; set; }

    /// <summary>
    /// Optional. If specified, pressing the button will open a list of suitable users. Identifiers of selected users
    /// will be sent to the bot in a "<see cref="UsersShared"/>" service message. Available in private chats only.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [Obsolete($"This property is deprecated, use {nameof(RequestUsers)} instead")]
    public KeyboardButtonRequestUser? RequestUser { get; set; }

    /// <summary>
    /// Optional. If specified, pressing the button will open a list of suitable chats. Tapping on a chat will send
    /// its identifier to the bot in a “chat_shared” service message. Available in private chats only.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public KeyboardButtonRequestChat? RequestChat { get; set; }

    /// <summary>
    /// Optional. If <see langword="true"/>, the user's phone number will be sent as a contact when the button
    /// is pressed. Available in private chats only
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? RequestContact { get; set; }

    /// <summary>
    /// Optional. If <see langword="true"/>, the user's current location will be sent when the button is pressed.
    /// Available in private chats only
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? RequestLocation { get; set; }

    /// <summary>
    /// Optional. If specified, the user will be asked to create a poll and send it to the bot when the button
    /// is pressed. Available in private chats only
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public KeyboardButtonPollType? RequestPoll { get; set; }

    /// <summary>
    /// Optional. If specified, the described Web App will be launched when the button is pressed. The Web App will
    /// be able to send a “web_app_data” service message. Available in private chats only.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public WebAppInfo? WebApp { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="KeyboardButton"/> class.
    /// </summary>
    /// <param name="text">Label text on the button</param>
    [JsonConstructor]
    [SetsRequiredMembers]
    public KeyboardButton(string text) => Text = text;

    /// <summary>
    /// Initializes a new instance of the <see cref="KeyboardButton"/> class.
    /// </summary>
    public KeyboardButton()
    { }

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
    /// <param name="url">
    /// An HTTPS URL of a Web App to be opened with additional data as specified in
    /// <a href="https://core.telegram.org/bots/webapps#initializing-web-apps">Initializing Web Apps</a>
    /// </param>
    /// <returns></returns>
    public static KeyboardButton WithWebApp(string text, string url) =>
        new(text) { WebApp = new(url) };

    /// <summary>
    /// Generate a keyboard button to request a web app
    /// </summary>
    /// <param name="text">Button's text</param>
    /// <param name="webAppInfo">Web app information</param>
    public static KeyboardButton WithWebApp(string text, WebAppInfo webAppInfo) =>
        new(text) { WebApp = webAppInfo };

    /// <summary>
    /// Generate a keyboard button to request users
    /// </summary>
    /// <param name="text">Button's text</param>
    /// <param name="requestUsers">Criteria used to request a suitable users</param>
    /// <returns></returns>
    public static KeyboardButton WithRequestUsers(string text, KeyboardButtonRequestUsers requestUsers) =>
        new(text) { RequestUsers = requestUsers };

    /// <summary>
    /// Generate a keyboard button to request user info
    /// </summary>
    /// <param name="text">Button's text</param>
    /// <param name="requestUser">Criteria used to request a suitable user</param>
    /// <returns></returns>
    [Obsolete($"This method is deprecated, use {nameof(WithRequestUsers)} instead")]
    public static KeyboardButton WithRequestUser(string text, KeyboardButtonRequestUser requestUser) =>
        new(text) { RequestUser = requestUser };

    /// <summary>
    /// Generate a keyboard button to request users
    /// </summary>
    /// <param name="text">Button's text</param>
    /// <param name="requestId">
    /// Signed 32-bit identifier of the request that will be received back in the <see cref="UsersShared"/> object.
    /// Must be unique within the message
    /// </param>
    public static KeyboardButton WithRequestUsers(string text, int requestId) =>
        new(text) { RequestUsers = new(requestId) };

    /// <summary>
    /// Generate a keyboard button to request a chat
    /// </summary>
    /// <param name="text">Button's text</param>
    /// <param name="requestChat">Criteria used to request a suitable chat</param>
    public static KeyboardButton WithRequestChat(string text, KeyboardButtonRequestChat requestChat) =>
        new(text) { RequestChat = requestChat };

    /// <summary>
    /// Generate a keyboard button to request a chat
    /// </summary>
    /// <param name="text">Button's text</param>
    /// <param name="requestId">
    /// Signed 32-bit identifier of the request, which will be received back in the <see cref="ChatShared"/> object.
    /// Must be unique within the message
    /// </param>
    /// <param name="chatIsChannel">
    /// Pass <see langword="true"/> to request a channel chat, pass <see langword="false"/> to request a group or a supergroup chat.
    /// </param>
    public static KeyboardButton WithRequestChat(string text, int requestId, bool chatIsChannel) =>
        new(text) { RequestChat = new(requestId, chatIsChannel) };

    /// <summary>
    /// Generate a keyboard button from text
    /// </summary>
    /// <param name="text">Button's text</param>
    /// <returns>Keyboard button</returns>
    public static implicit operator KeyboardButton(string text)
        => new(text);
}

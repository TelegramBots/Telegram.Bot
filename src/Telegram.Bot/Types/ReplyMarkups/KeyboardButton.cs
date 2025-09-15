// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.ReplyMarkups;

/// <summary>This object represents one button of the reply keyboard. At most one of the optional fields must be used to specify type of the button. For simple text buttons, <em>String</em> can be used instead of this object to specify the button text.<br/><b>Note:</b> <see cref="RequestUsers">RequestUsers</see> and <see cref="RequestChat">RequestChat</see> options will only work in Telegram versions released after 3 February, 2023. Older clients will display <em>unsupported message</em>.</summary>
public partial class KeyboardButton : IKeyboardButton
{
    /// <summary>Text of the button. If none of the optional fields are used, it will be sent as a message when the button is pressed</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Text { get; set; }

    /// <summary><em>Optional</em>. If specified, pressing the button will open a list of suitable users. Identifiers of selected users will be sent to the bot in a <see cref="UsersShared"/> service message. Available in private chats only.</summary>
    [JsonPropertyName("request_users")]
    public KeyboardButtonRequestUsers? RequestUsers { get; set; }

    /// <summary><em>Optional</em>. If specified, pressing the button will open a list of suitable chats. Tapping on a chat will send its identifier to the bot in a <see cref="ChatShared"/> service message. Available in private chats only.</summary>
    [JsonPropertyName("request_chat")]
    public KeyboardButtonRequestChat? RequestChat { get; set; }

    /// <summary><em>Optional</em>. If <see langword="true"/>, the user's phone number will be sent as a contact when the button is pressed. Available in private chats only.</summary>
    [JsonPropertyName("request_contact")]
    public bool RequestContact { get; set; }

    /// <summary><em>Optional</em>. If <see langword="true"/>, the user's current location will be sent when the button is pressed. Available in private chats only.</summary>
    [JsonPropertyName("request_location")]
    public bool RequestLocation { get; set; }

    /// <summary><em>Optional</em>. If specified, the user will be asked to create a poll and send it to the bot when the button is pressed. Available in private chats only.</summary>
    [JsonPropertyName("request_poll")]
    public KeyboardButtonPollType? RequestPoll { get; set; }

    /// <summary><em>Optional</em>. If specified, the described <a href="https://core.telegram.org/bots/webapps">Web App</a> will be launched when the button is pressed. The Web App will be able to send a <see cref="WebAppData"/> service message. Available in private chats only.</summary>
    [JsonPropertyName("web_app")]
    public WebAppInfo? WebApp { get; set; }

    /// <summary>Initializes an instance of <see cref="KeyboardButton"/></summary>
    /// <param name="text">Text of the button. If none of the optional fields are used, it will be sent as a message when the button is pressed</param>
    [SetsRequiredMembers]
    public KeyboardButton(string text) => Text = text;

    /// <summary>Instantiates a new <see cref="KeyboardButton"/></summary>
    public KeyboardButton() { }

    /// <summary>Creates a keyboard button. Pressing the button will open a list of suitable users. Identifiers of selected users will be sent to the bot in a <see cref="UsersShared"/> service message. Available in private chats only.</summary>
    /// <param name="text">Button's text</param>
    /// <param name="requestUsers">If specified, pressing the button will open a list of suitable users. Identifiers of selected users will be sent to the bot in a <see cref="UsersShared"/> service message. Available in private chats only.</param>
    public static KeyboardButton WithRequestUsers(string text, KeyboardButtonRequestUsers requestUsers) =>
        new(text) { RequestUsers = requestUsers };

    /// <summary>Creates a keyboard button. Pressing the button will open a list of suitable chats. Tapping on a chat will send its identifier to the bot in a <see cref="ChatShared"/> service message. Available in private chats only.</summary>
    /// <param name="text">Button's text</param>
    /// <param name="requestChat">If specified, pressing the button will open a list of suitable chats. Tapping on a chat will send its identifier to the bot in a <see cref="ChatShared"/> service message. Available in private chats only.</param>
    public static KeyboardButton WithRequestChat(string text, KeyboardButtonRequestChat requestChat) =>
        new(text) { RequestChat = requestChat };

    /// <summary>Creates a keyboard button. The user's phone number will be sent as a contact when the button is pressed. Available in private chats only.</summary>
    /// <param name="text">Button's text</param>
    public static KeyboardButton WithRequestContact(string text) =>
        new(text) { RequestContact = true };

    /// <summary>Creates a keyboard button. The user's current location will be sent when the button is pressed. Available in private chats only.</summary>
    /// <param name="text">Button's text</param>
    public static KeyboardButton WithRequestLocation(string text) =>
        new(text) { RequestLocation = true };

    /// <summary>Creates a keyboard button. The user will be asked to create a poll and send it to the bot when the button is pressed. Available in private chats only.</summary>
    /// <param name="text">Button's text</param>
    /// <param name="requestPoll">If specified, the user will be asked to create a poll and send it to the bot when the button is pressed. Available in private chats only.</param>
    public static KeyboardButton WithRequestPoll(string text, KeyboardButtonPollType requestPoll) =>
        new(text) { RequestPoll = requestPoll };

    /// <summary>Creates a keyboard button. The described <a href="https://core.telegram.org/bots/webapps">Web App</a> will be launched when the button is pressed. The Web App will be able to send a <see cref="WebAppData"/> service message. Available in private chats only.</summary>
    /// <param name="text">Button's text</param>
    /// <param name="webApp">If specified, the described <a href="https://core.telegram.org/bots/webapps">Web App</a> will be launched when the button is pressed. The Web App will be able to send a <see cref="WebAppData"/> service message. Available in private chats only.</param>
    public static KeyboardButton WithWebApp(string text, WebAppInfo webApp) =>
        new(text) { WebApp = webApp };
}

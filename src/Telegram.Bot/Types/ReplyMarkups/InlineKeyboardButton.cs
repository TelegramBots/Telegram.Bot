// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.ReplyMarkups;

/// <summary>This object represents one button of an inline keyboard. Exactly one of the optional fields must be used to specify type of the button.</summary>
public partial class InlineKeyboardButton : IKeyboardButton
{
    /// <summary>Label text on the button</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Text { get; set; }

    /// <summary><em>Optional</em>. HTTP or tg:// URL to be opened when the button is pressed. Links <c>tg://user?id=&lt;UserId&gt;</c> can be used to mention a user by their identifier without using a username, if this is allowed by their privacy settings.</summary>
    public string? Url { get; set; }

    /// <summary><em>Optional</em>. Data to be sent in a <see cref="CallbackQuery">callback query</see> to the bot when the button is pressed, 1-64 bytes</summary>
    [JsonPropertyName("callback_data")]
    public string? CallbackData { get; set; }

    /// <summary><em>Optional</em>. Description of the <a href="https://core.telegram.org/bots/webapps">Web App</a> that will be launched when the user presses the button. The Web App will be able to send an arbitrary message on behalf of the user using the method <see cref="TelegramBotClientExtensions.AnswerWebAppQuery">AnswerWebAppQuery</see>. Available only in private chats between a user and the bot. Not supported for messages sent on behalf of a Telegram Business account.</summary>
    [JsonPropertyName("web_app")]
    public WebAppInfo? WebApp { get; set; }

    /// <summary><em>Optional</em>. An HTTPS URL used to automatically authorize the user. Can be used as a replacement for the <a href="https://core.telegram.org/widgets/login">Telegram Login Widget</a>.</summary>
    [JsonPropertyName("login_url")]
    public LoginUrl? LoginUrl { get; set; }

    /// <summary><em>Optional</em>. If set, pressing the button will prompt the user to select one of their chats, open that chat and insert the bot's username and the specified inline query in the input field. May be empty, in which case just the bot's username will be inserted. Not supported for messages sent in channel direct messages chats and on behalf of a Telegram Business account.</summary>
    [JsonPropertyName("switch_inline_query")]
    public string? SwitchInlineQuery { get; set; }

    /// <summary><em>Optional</em>. If set, pressing the button will insert the bot's username and the specified inline query in the current chat's input field. May be empty, in which case only the bot's username will be inserted.<br/><br/>This offers a quick way for the user to open your bot in inline mode in the same chat - good for selecting something from multiple options. Not supported in channels and for messages sent in channel direct messages chats and on behalf of a Telegram Business account.</summary>
    [JsonPropertyName("switch_inline_query_current_chat")]
    public string? SwitchInlineQueryCurrentChat { get; set; }

    /// <summary><em>Optional</em>. If set, pressing the button will prompt the user to select one of their chats of the specified type, open that chat and insert the bot's username and the specified inline query in the input field. Not supported for messages sent in channel direct messages chats and on behalf of a Telegram Business account.</summary>
    [JsonPropertyName("switch_inline_query_chosen_chat")]
    public SwitchInlineQueryChosenChat? SwitchInlineQueryChosenChat { get; set; }

    /// <summary><em>Optional</em>. Description of the button that copies the specified text to the clipboard.</summary>
    [JsonPropertyName("copy_text")]
    public CopyTextButton? CopyText { get; set; }

    /// <summary><em>Optional</em>. Description of the game that will be launched when the user presses the button.<br/><br/><b>NOTE:</b> This type of button <b>must</b> always be the first button in the first row.</summary>
    [JsonPropertyName("callback_game")]
    public CallbackGame? CallbackGame { get; set; }

    /// <summary><em>Optional</em>. Specify <see langword="true"/>, to send a <a href="https://core.telegram.org/bots/api#payments">Pay button</a>. Substrings “⭐” and “XTR” in the buttons's text will be replaced with a Telegram Star icon.<br/><br/><b>NOTE:</b> This type of button <b>must</b> always be the first button in the first row and can only be used in invoice messages.</summary>
    public bool Pay { get; set; }

    /// <summary>Initializes an instance of <see cref="InlineKeyboardButton"/></summary>
    /// <param name="text">Label text on the button</param>
    [SetsRequiredMembers]
    public InlineKeyboardButton(string text) => Text = text;

    /// <summary>Instantiates a new <see cref="InlineKeyboardButton"/></summary>
    public InlineKeyboardButton() { }

    /// <summary>Creates an inline keyboard button with HTTP or tg:// URL to be opened when the button is pressed. Links <c>tg://user?id=&lt;UserId&gt;</c> can be used to mention a user by their identifier without using a username, if this is allowed by their privacy settings.</summary>
    /// <param name="text">Label text on the button</param>
    /// <param name="url">HTTP or tg:// URL to be opened when the button is pressed. Links <c>tg://user?id=&lt;UserId&gt;</c> can be used to mention a user by their identifier without using a username, if this is allowed by their privacy settings.</param>
    public static InlineKeyboardButton WithUrl(string text, string url) =>
        new(text) { Url = url };

    /// <summary>Creates an inline keyboard button with data to be sent in a <see cref="CallbackQuery">callback query</see> to the bot when the button is pressed, 1-64 bytes</summary>
    /// <param name="text">Label text on the button</param>
    /// <param name="callbackData">Data to be sent in a <see cref="CallbackQuery">callback query</see> to the bot when the button is pressed, 1-64 bytes</param>
    public static InlineKeyboardButton WithCallbackData(string text, string callbackData) =>
        new(text) { CallbackData = callbackData };

    /// <summary>Creates an inline keyboard button with description of the <a href="https://core.telegram.org/bots/webapps">Web App</a> that will be launched when the user presses the button. The Web App will be able to send an arbitrary message on behalf of the user using the method <see cref="TelegramBotClientExtensions.AnswerWebAppQuery">AnswerWebAppQuery</see>. Available only in private chats between a user and the bot. Not supported for messages sent on behalf of a Telegram Business account.</summary>
    /// <param name="text">Label text on the button</param>
    /// <param name="webApp">Description of the <a href="https://core.telegram.org/bots/webapps">Web App</a> that will be launched when the user presses the button. The Web App will be able to send an arbitrary message on behalf of the user using the method <see cref="TelegramBotClientExtensions.AnswerWebAppQuery">AnswerWebAppQuery</see>. Available only in private chats between a user and the bot. Not supported for messages sent on behalf of a Telegram Business account.</param>
    public static InlineKeyboardButton WithWebApp(string text, WebAppInfo webApp) =>
        new(text) { WebApp = webApp };

    /// <summary>Creates an inline keyboard button with an HTTPS URL used to automatically authorize the user. Can be used as a replacement for the <a href="https://core.telegram.org/widgets/login">Telegram Login Widget</a>.</summary>
    /// <param name="text">Label text on the button</param>
    /// <param name="loginUrl">An HTTPS URL used to automatically authorize the user. Can be used as a replacement for the <a href="https://core.telegram.org/widgets/login">Telegram Login Widget</a>.</param>
    public static InlineKeyboardButton WithLoginUrl(string text, LoginUrl loginUrl) =>
        new(text) { LoginUrl = loginUrl };

    /// <summary>Creates an inline keyboard button. Pressing the button will prompt the user to select one of their chats, open that chat and insert the bot's username and the specified inline query in the input field. May be empty, in which case just the bot's username will be inserted. Not supported for messages sent in channel direct messages chats and on behalf of a Telegram Business account.</summary>
    /// <param name="text">Label text on the button</param>
    /// <param name="switchInlineQuery">If set, pressing the button will prompt the user to select one of their chats, open that chat and insert the bot's username and the specified inline query in the input field. May be empty, in which case just the bot's username will be inserted. Not supported for messages sent in channel direct messages chats and on behalf of a Telegram Business account.</param>
    public static InlineKeyboardButton WithSwitchInlineQuery(string text, string switchInlineQuery = "") =>
        new(text) { SwitchInlineQuery = switchInlineQuery };

    /// <summary>Creates an inline keyboard button. Pressing the button will insert the bot's username and the specified inline query in the current chat's input field. May be empty, in which case only the bot's username will be inserted.<br/><br/>This offers a quick way for the user to open your bot in inline mode in the same chat - good for selecting something from multiple options. Not supported in channels and for messages sent in channel direct messages chats and on behalf of a Telegram Business account.</summary>
    /// <param name="text">Label text on the button</param>
    /// <param name="switchInlineQueryCurrentChat">If set, pressing the button will insert the bot's username and the specified inline query in the current chat's input field. May be empty, in which case only the bot's username will be inserted.<br/><br/>This offers a quick way for the user to open your bot in inline mode in the same chat - good for selecting something from multiple options. Not supported in channels and for messages sent in channel direct messages chats and on behalf of a Telegram Business account.</param>
    public static InlineKeyboardButton WithSwitchInlineQueryCurrentChat(string text, string switchInlineQueryCurrentChat = "") =>
        new(text) { SwitchInlineQueryCurrentChat = switchInlineQueryCurrentChat };

    /// <summary>Creates an inline keyboard button. Pressing the button will prompt the user to select one of their chats of the specified type, open that chat and insert the bot's username and the specified inline query in the input field. Not supported for messages sent in channel direct messages chats and on behalf of a Telegram Business account.</summary>
    /// <param name="text">Label text on the button</param>
    /// <param name="switchInlineQueryChosenChat">If set, pressing the button will prompt the user to select one of their chats of the specified type, open that chat and insert the bot's username and the specified inline query in the input field. Not supported for messages sent in channel direct messages chats and on behalf of a Telegram Business account.</param>
    public static InlineKeyboardButton WithSwitchInlineQueryChosenChat(string text, SwitchInlineQueryChosenChat switchInlineQueryChosenChat) =>
        new(text) { SwitchInlineQueryChosenChat = switchInlineQueryChosenChat };

    /// <summary>Creates an inline keyboard button with description of the button that copies the specified text to the clipboard.</summary>
    /// <param name="text">Label text on the button</param>
    /// <param name="copyText">Description of the button that copies the specified text to the clipboard.</param>
    public static InlineKeyboardButton WithCopyText(string text, CopyTextButton copyText) =>
        new(text) { CopyText = copyText };

    /// <summary>Creates an inline keyboard button with description of the game that will be launched when the user presses the button.<br/><br/><b>NOTE:</b> This type of button <b>must</b> always be the first button in the first row.</summary>
    /// <param name="text">Label text on the button</param>
    public static InlineKeyboardButton WithCallbackGame(string text) =>
        new(text) { CallbackGame = new() };

    /// <summary>Creates an inline keyboard button <a href="https://core.telegram.org/bots/api#payments">Pay button</a>. Substrings “⭐” and “XTR” in the buttons's text will be replaced with a Telegram Star icon.<br/><br/><b>NOTE:</b> This type of button <b>must</b> always be the first button in the first row and can only be used in invoice messages.</summary>
    /// <param name="text">Label text on the button</param>
    public static InlineKeyboardButton WithPay(string text) =>
        new(text) { Pay = true };
}

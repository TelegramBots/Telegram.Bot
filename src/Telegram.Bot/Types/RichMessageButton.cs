// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents a button in a <see cref="RichMessage"/>. Exactly one of the fields other than <see cref="Text">Text</see> and <see cref="Style">Style</see> must be used to specify the type of the button.</summary>
public partial class RichMessageButton
{
    /// <summary>Text of the button. May contain only plain text, <see cref="RichTextCustomEmoji"/> and <see cref="RichTextDateTime"/> entities.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public RichText Text { get; set; } = default!;

    /// <summary><em>Optional</em>. Style of the button. Must be one of <see cref="RichMessageButtonStyle.Danger">Danger</see> (red), <see cref="RichMessageButtonStyle.Success">Success</see> (green), <see cref="RichMessageButtonStyle.Primary">Primary</see> (blue) or <see cref="RichMessageButtonStyle.Link">Link</see> (the button is shown as a regular link without borders). If omitted, then an app-specific style is used. The style <see cref="RichMessageButtonStyle.Link">Link</see> is allowed only for callback buttons.</summary>
    public RichMessageButtonStyle? Style { get; set; }

    /// <summary><em>Optional</em>. HTTP or tg:// URL to be opened when the button is pressed. Links <c>tg://user?id=&lt;UserId&gt;</c> can be used to mention a user by their identifier without using a username, if this is allowed by their privacy settings.</summary>
    public string? Url { get; set; }

    /// <summary><em>Optional</em>. Data to be sent in a <see cref="CallbackQuery">callback query</see> to the bot when the button is pressed, 1-64 bytes</summary>
    [JsonPropertyName("callback_data")]
    public string? CallbackData { get; set; }

    /// <summary><em>Optional</em>. Description of the <a href="https://core.telegram.org/bots/webapps">Web App</a> that will be launched when the user presses the button. The Web App will be able to send an arbitrary message on behalf of the user using the method <see cref="TelegramBotClientExtensions.AnswerWebAppQuery">AnswerWebAppQuery</see>. Available only in private chats between a user and the bot. Not supported for messages sent on behalf of a business account.</summary>
    [JsonPropertyName("web_app")]
    public WebAppInfo? WebApp { get; set; }

    /// <summary><em>Optional</em>. An HTTPS URL used to automatically authorize the user. Can be used as a replacement for the <a href="https://core.telegram.org/widgets/login">Telegram Login Widget</a>. Not supported for ephemeral messages.</summary>
    [JsonPropertyName("login_url")]
    public LoginUrl? LoginUrl { get; set; }

    /// <summary><em>Optional</em>. If set, pressing the button will prompt the user to select one of their chats, open that chat and insert the bot's username and the specified inline query in the input field. May be empty, in which case just the bot's username will be inserted. Not supported for messages sent in channel direct messages chats and on behalf of a business account.</summary>
    [JsonPropertyName("switch_inline_query")]
    public string? SwitchInlineQuery { get; set; }

    /// <summary><em>Optional</em>. If set, pressing the button will insert the bot's username and the specified inline query in the current chat's input field. May be empty, in which case only the bot's username will be inserted. Not supported in channels and for messages sent in channel direct messages chats and on behalf of a business account.</summary>
    [JsonPropertyName("switch_inline_query_current_chat")]
    public string? SwitchInlineQueryCurrentChat { get; set; }

    /// <summary><em>Optional</em>. If set, pressing the button will prompt the user to select one of their chats of the specified type, open that chat and insert the bot's username and the specified inline query in the input field. Not supported for messages sent in channel direct messages chats and on behalf of a business account.</summary>
    [JsonPropertyName("switch_inline_query_chosen_chat")]
    public SwitchInlineQueryChosenChat? SwitchInlineQueryChosenChat { get; set; }

    /// <summary><em>Optional</em>. A button that copies the specified text to the clipboard</summary>
    [JsonPropertyName("copy_text")]
    public CopyTextButton? CopyText { get; set; }

    /// <summary><em>Optional</em>. If set, then the button is disabled and does nothing</summary>
    public DisabledButton? Disabled { get; set; }
}

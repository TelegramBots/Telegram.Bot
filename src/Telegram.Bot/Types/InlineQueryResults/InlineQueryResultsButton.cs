// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.InlineQueryResults;

/// <summary>This object represents a button to be shown above inline query results. You <b>must</b> use exactly one of the optional fields.</summary>
public partial class InlineQueryResultsButton
{
    /// <summary>Label text on the button</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Text { get; set; }

    /// <summary><em>Optional</em>. Description of the <a href="https://core.telegram.org/bots/webapps">Web App</a> that will be launched when the user presses the button. The Web App will be able to switch back to the inline mode using the method <a href="https://core.telegram.org/bots/webapps#initializing-mini-apps">switchInlineQuery</a> inside the Web App.</summary>
    [JsonPropertyName("web_app")]
    public WebAppInfo? WebApp { get; set; }

    /// <summary><em>Optional</em>. <a href="https://core.telegram.org/bots/features#deep-linking">Deep-linking</a> parameter for the /start message sent to the bot when a user presses the button. 1-64 characters, only <c>A-Z</c>, <c>a-z</c>, <c>0-9</c>, <c>_</c> and <c>-</c> are allowed.<br/><br/><em>Example:</em> An inline bot that sends YouTube videos can ask the user to connect the bot to their YouTube account to adapt search results accordingly. To do this, it displays a 'Connect your YouTube account' button above the results, or even before showing any. The user presses the button, switches to a private chat with the bot and, in doing so, passes a start parameter that instructs the bot to return an OAuth link. Once done, the bot can offer a <see cref="InlineKeyboardMarkup"><em>SwitchInline</em></see> button so that the user can easily return to the chat where they wanted to use the bot's inline capabilities.</summary>
    [JsonPropertyName("start_parameter")]
    public string? StartParameter { get; set; }

    /// <summary>Initializes an instance of <see cref="InlineQueryResultsButton"/></summary>
    /// <param name="text">Label text on the button</param>
    [SetsRequiredMembers]
    public InlineQueryResultsButton(string text) => Text = text;

    /// <summary>Instantiates a new <see cref="InlineQueryResultsButton"/></summary>
    public InlineQueryResultsButton() { }
}

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types.InlineQueryResults;

/// <summary>
/// This object represents a button to be shown above inline query results.
/// You <b>must</b> use exactly one of the optional fields.
/// </summary>
public class InlineQueryResultsButton
{
    /// <summary>
    /// Label text on the button
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Text { get; init; }

    /// <summary>
    /// Optional. Description of the Web App that will be launched when the user presses
    /// the button. The Web App will be able to switch back to the inline mode using
    /// the method <see href="https://core.telegram.org/bots/webapps#initializing-web-apps">switchInlineQuery</see>
    /// inside the Web App.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public WebAppInfo? WebApp { get; set; }

    /// <summary>
    /// Optional. <a href="https://core.telegram.org/bots#deep-linking">Deep-linking</a> parameter
    /// for the /start message sent to the bot when a user presses the button.
    /// 1-64 characters, only <c>A-Z</c>, <c>a-z</c>, <c>0-9</c>, <c>_</c> and <c>-</c> are allowed.
    /// </summary>
    /// <remarks>
    /// Example: An inline bot that sends YouTube videos can ask the user to connect the bot to their YouTube account
    /// to adapt search results accordingly. To do this, it displays a 'Connect your YouTube account' button above
    /// the results, or even before showing any.The user presses the button, switches to a private chat with the bot and,
    /// in doing so, passes a start parameter that instructs the bot to return an OAuth link. Once done,
    /// the bot can offer a switch_inline button so that the user can easily return to the chat
    /// where they wanted to use the bot's inline capabilities.
    /// </remarks>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? StartParameter { get; set; }

    /// <summary>
    /// Initializes a new <see cref="InlineQueryResultsButton"/> object
    /// </summary>
    /// <param name="text">
    /// Label text on the button
    /// </param>
    public InlineQueryResultsButton(string text)
    {
        Text = text;
    }

    /// <summary>
    /// Initializes a new <see cref="InlineQueryResultsButton"/> object
    /// </summary>
    public InlineQueryResultsButton()
    { }
}

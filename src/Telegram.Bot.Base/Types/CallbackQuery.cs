using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Types;

/// <summary>
/// This object represents an incoming callback query from a callback button in an
/// <see cref="InlineKeyboardButton">inline keyboard</see>. If the button that originated the query was attached to
/// a message sent by the bot, the field <see cref="Message"/> will be present. If the button was attached to a
/// message sent via the bot (in inline mode), the field <see cref="InlineMessageId"/> will be present. Exactly one
/// of the fields data or <see cref="GameShortName"/> will be present.
/// </summary>
/// <remarks>
/// <b>NOTE:</b> After the user presses a callback button, Telegram clients will display a progress bar until
/// you call <see cref="Requests.AnswerCallbackQueryRequest"/>. It is, therefore, necessary to react by calling
/// <see cref="Requests.AnswerCallbackQueryRequest"/> even if no notification to the user is needed (e.g., without
/// specifying any of the optional parameters).
/// </remarks>
public class CallbackQuery
{
    /// <summary>
    /// Unique identifier for this query
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Id { get; set; } = default!;

    /// <summary>
    /// Sender
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public User From { get; set; } = default!;

    /// <summary>
    /// Optional. Message sent by the bot with the callback button that originated the query
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public MaybeInaccessibleMessage? Message { get; set; }

    /// <summary>
    /// Optional. Identifier of the message sent via the bot in inline mode, that originated the query
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? InlineMessageId { get; set; }

    /// <summary>
    /// Global identifier, uniquely corresponding to the chat to which the message with the callback button was
    /// sent. Useful for high scores in games.
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string ChatInstance { get; set; } = default!;

    /// <summary>
    /// Optional. Data associated with the callback button.
    /// </summary>
    /// <remarks>
    /// Be aware that a bad client can send arbitrary data in this field.
    /// </remarks>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Data { get; set; }

    /// <summary>
    /// Optional. Short name of a <see cref="Game"/> to be returned, serves as the unique identifier for the game.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? GameShortName { get; set; }

    /// <summary>
    /// Indicates if the User requests a Game
    /// </summary>
    public bool IsGameQuery => GameShortName != default;
}

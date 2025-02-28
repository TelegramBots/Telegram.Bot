// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents an incoming callback query from a callback button in an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>. If the button that originated the query was attached to a message sent by the bot, the field <see cref="Message">Message</see> will be present. If the button was attached to a message sent via the bot (in <a href="https://core.telegram.org/bots/api#inline-mode">inline mode</a>), the field <see cref="InlineMessageId">InlineMessageId</see> will be present. Exactly one of the fields <see cref="Data">Data</see> or <see cref="GameShortName">GameShortName</see> will be present.</summary>
/// <remarks><b>NOTE:</b> After the user presses a callback button, Telegram clients will display a progress bar until you call <see cref="TelegramBotClientExtensions.AnswerCallbackQuery">AnswerCallbackQuery</see>. It is, therefore, necessary to react by calling <see cref="TelegramBotClientExtensions.AnswerCallbackQuery">AnswerCallbackQuery</see> even if no notification to the user is needed (e.g., without specifying any of the optional parameters).</remarks>
public partial class CallbackQuery
{
    /// <summary>Unique identifier for this query</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Id { get; set; } = default!;

    /// <summary>Sender</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public User From { get; set; } = default!;

    /// <summary><em>Optional</em>. Message sent by the bot with the callback button that originated the query</summary>
    public Message? Message { get; set; }

    /// <summary><em>Optional</em>. Identifier of the message sent via the bot in inline mode, that originated the query.</summary>
    [JsonPropertyName("inline_message_id")]
    public string? InlineMessageId { get; set; }

    /// <summary>Global identifier, uniquely corresponding to the chat to which the message with the callback button was sent. Useful for high scores in <a href="https://core.telegram.org/bots/api#games">games</a>.</summary>
    [JsonPropertyName("chat_instance")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string ChatInstance { get; set; } = default!;

    /// <summary><em>Optional</em>. Data associated with the callback button. Be aware that the message originated the query can contain no callback buttons with this data.</summary>
    public string? Data { get; set; }

    /// <summary><em>Optional</em>. Short name of a <a href="https://core.telegram.org/bots/api#games">Game</a> to be returned, serves as the unique identifier for the game</summary>
    [JsonPropertyName("game_short_name")]
    public string? GameShortName { get; set; }
}

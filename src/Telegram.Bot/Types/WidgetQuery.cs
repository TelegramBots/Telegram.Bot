// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents an incoming query from a widget in a message sent by the bot or via the bot. Use <see cref="TelegramBotClientExtensions.AnswerWidgetQuery">AnswerWidgetQuery</see> to answer the query. Each user can send at most 5 queries per 5 seconds to the bot from all its widgets.</summary>
public partial class WidgetQuery
{
    /// <summary>Unique identifier for this query</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Id { get; set; } = default!;

    /// <summary>Sender</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public User From { get; set; } = default!;

    /// <summary><em>Optional</em>. Chat with the message containing the widget; omitted if the widget is in an inline message</summary>
    public Chat? Chat { get; set; }

    /// <summary><em>Optional</em>. Identifier of the message containing the widget; omitted if the widget is in an inline message</summary>
    [JsonPropertyName("message_id")]
    public int? MessageId { get; set; }

    /// <summary><em>Optional</em>. Identifier of the inline message containing the widget</summary>
    [JsonPropertyName("inline_message_id")]
    public string? InlineMessageId { get; set; }

    /// <summary>Global identifier, uniquely corresponding to the chat to which the message with the widget was sent</summary>
    [JsonPropertyName("chat_instance")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string ChatInstance { get; set; } = default!;

    /// <summary><em>Optional</em>. For widgets in inline messages, type of the chat to which the message was sent. Can be either “sender” for a private chat with the inline query sender, “private”, “group”, “supergroup”, or “channel”.</summary>
    [JsonPropertyName("chat_type")]
    public string? ChatType { get; set; }

    /// <summary>Type of the query</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public WidgetQueryType Type { get; set; } = default!;

    /// <summary>Data of the query, 1-64 bytes</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Data { get; set; } = default!;

    /// <summary><em>Optional</em>. Unique identifier of the shared widget, which sent the query; omitted if the widget isn't shared</summary>
    [JsonPropertyName("widget_id")]
    public string? WidgetId { get; set; }
}

// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents an incoming query from a forwarded message with a shared widget created by the bot. Use <see cref="TelegramBotClientExtensions.AnswerWidgetQuery">AnswerWidgetQuery</see> to answer the query. Each user can send at most 5 queries per 5 seconds to the bot from all its widgets.</summary>
public partial class SharedWidgetQuery
{
    /// <summary>Unique identifier for this query</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Id { get; set; } = default!;

    /// <summary>Sender</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public User From { get; set; } = default!;

    /// <summary>Type of the query</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public WidgetQueryType Type { get; set; } = default!;

    /// <summary>Data of the query, 1-64 bytes</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Data { get; set; } = default!;

    /// <summary>Unique identifier of the shared widget, which sent the query</summary>
    [JsonPropertyName("widget_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string WidgetId { get; set; } = default!;
}

// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to send an answer to a query from a widget. The query must be answered within 15 seconds. The answer is received only by the widget that sent the query and doesn't change data of the widget; use <see cref="TelegramBotClientExtensions.EditMessageWidgetData">EditMessageWidgetData</see> or <see cref="TelegramBotClientExtensions.EditSharedWidgetData">EditSharedWidgetData</see> to change it</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class AnswerWidgetQueryRequest() : RequestBase<bool>("answerWidgetQuery")
{
    /// <summary>Unique identifier for the query to be answered</summary>
    [JsonPropertyName("widget_query_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string WidgetQueryId { get; set; }

    /// <summary>Data to be returned to the widget, 0-1024 bytes</summary>
    public string? Data { get; set; }
}

// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to edit public data of a shared widget created by the bot. A shared widget can be edited at most once per second</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class EditSharedWidgetDataRequest() : RequestBase<bool>("editSharedWidgetData")
{
    /// <summary>Unique identifier of the shared widget</summary>
    [JsonPropertyName("widget_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string WidgetId { get; set; }

    /// <summary>New public data of the widget, 0-1024 bytes; pass an empty string to remove the public data</summary>
    [JsonPropertyName("public_data")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string PublicData { get; set; }

    /// <summary>The maximum amount of time in seconds that the public data of the widget may be cached client-side; 5-3600. If omitted, then the current value is kept, which is 30 by default.</summary>
    [JsonPropertyName("cache_time")]
    public int? CacheTime { get; set; }
}

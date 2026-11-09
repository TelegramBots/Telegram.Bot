// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to edit data of a widget in a message sent by the bot or via the bot (for inline bots). Public data of a shared widget can be changed at most once per second</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class EditMessageWidgetDataRequest() : RequestBase<bool>("editMessageWidgetData"), IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target supergroup in the format <c>@username</c>.</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Identifier of the message with the widget.</summary>
    [JsonPropertyName("message_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MessageId { get; set; }

    /// <summary>New public data of the widget, 0-1024 bytes; pass an empty string to remove the public data. If omitted, then the public data isn't changed.</summary>
    [JsonPropertyName("public_data")]
    public string? PublicData { get; set; }

    /// <summary>New data of the widget, which is specific to the message, 0-1024 bytes; pass an empty string to remove the data. If omitted, then the data isn't changed.</summary>
    [JsonPropertyName("chat_data")]
    public string? ChatData { get; set; }
}

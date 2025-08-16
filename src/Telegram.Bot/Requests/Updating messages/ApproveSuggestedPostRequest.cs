// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to approve a suggested post in a direct messages chat. The bot must have the 'CanPostMessages' administrator right in the corresponding channel chat.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class ApproveSuggestedPostRequest() : RequestBase<bool>("approveSuggestedPost"), IChatTargetable
{
    /// <summary>Unique identifier for the target direct messages chat</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long ChatId { get; set; }

    /// <summary>Identifier of a suggested post message to approve</summary>
    [JsonPropertyName("message_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MessageId { get; set; }

    /// <summary>Point in time when the post is expected to be published; omit if the date has already been specified when the suggested post was created. If specified, then the date must be not more than 2678400 seconds (30 days) in the future</summary>
    [JsonPropertyName("send_date")]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime? SendDate { get; set; }

    /// <inheritdoc/>
    ChatId IChatTargetable.ChatId => ChatId;
}

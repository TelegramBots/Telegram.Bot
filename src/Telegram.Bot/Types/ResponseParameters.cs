// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes why a request was unsuccessful.</summary>
public partial class ResponseParameters
{
    /// <summary><em>Optional</em>. The group has been migrated to a supergroup with the specified identifier.</summary>
    [JsonPropertyName("migrate_to_chat_id")]
    public long? MigrateToChatId { get; set; }

    /// <summary><em>Optional</em>. In case of exceeding flood control, the number of seconds left to wait before the request can be repeated</summary>
    [JsonPropertyName("retry_after")]
    public int? RetryAfter { get; set; }
}

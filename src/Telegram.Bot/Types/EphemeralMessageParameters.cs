// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary></summary>
public partial class EphemeralMessageParameters
{
    /// <summary>Identifier of the user who will receive the message. It is not guaranteed that the user will receive the message, especially if they are offline. See <a href="https://core.telegram.org/bots/api#ephemeral-messages-and-commands">here</a> for more details.</summary>
    [JsonPropertyName("receiver_user_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public long ReceiverUserId { get; set; }

    /// <summary><em>Optional</em>. Identifier of the callback query which triggered the message, if any</summary>
    [JsonPropertyName("callback_query_id")]
    public string? CallbackQueryId { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/> if the ephemeral message must be shown in place of the original message. Must be <see langword="false"/> for callback queries from ephemeral messages, which must be edited using regular <em>editEphemeralMessage…</em> methods.</summary>
    [JsonPropertyName("replace_callback_query_message")]
    public bool ReplaceCallbackQueryMessage { get; set; }
}

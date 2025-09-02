// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes a service message about a change in the price of direct messages sent to a channel chat.</summary>
public partial class DirectMessagePriceChanged
{
    /// <summary><see langword="true"/>, if direct messages are enabled for the channel chat; false otherwise</summary>
    [JsonPropertyName("are_direct_messages_enabled")]
    public bool AreDirectMessagesEnabled { get; set; }

    /// <summary><em>Optional</em>. The new number of Telegram Stars that must be paid by users for each direct message sent to the channel. Does not apply to users who have been exempted by administrators. Defaults to 0.</summary>
    [JsonPropertyName("direct_message_star_count")]
    public long? DirectMessageStarCount { get; set; }
}

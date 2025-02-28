// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Represents a join request sent to a chat.</summary>
public partial class ChatJoinRequest
{
    /// <summary>Chat to which the request was sent</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Chat Chat { get; set; } = default!;

    /// <summary>User that sent the join request</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public User From { get; set; } = default!;

    /// <summary>Identifier of a private chat with the user who sent the join request. The bot can use this identifier for 5 minutes to send messages until the join request is processed, assuming no other administrator contacted the user.</summary>
    [JsonPropertyName("user_chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public long UserChatId { get; set; }

    /// <summary>Date the request was sent</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime Date { get; set; }

    /// <summary><em>Optional</em>. Bio of the user.</summary>
    public string? Bio { get; set; }

    /// <summary><em>Optional</em>. Chat invite link that was used by the user to send the join request</summary>
    [JsonPropertyName("invite_link")]
    public ChatInviteLink? InviteLink { get; set; }
}

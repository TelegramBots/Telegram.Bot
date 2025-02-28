// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents changes in the status of a chat member.</summary>
public partial class ChatMemberUpdated
{
    /// <summary>Chat the user belongs to</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Chat Chat { get; set; } = default!;

    /// <summary>Performer of the action, which resulted in the change</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public User From { get; set; } = default!;

    /// <summary>Date the change was done</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime Date { get; set; }

    /// <summary>Previous information about the chat member</summary>
    [JsonPropertyName("old_chat_member")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public ChatMember OldChatMember { get; set; } = default!;

    /// <summary>New information about the chat member</summary>
    [JsonPropertyName("new_chat_member")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public ChatMember NewChatMember { get; set; } = default!;

    /// <summary><em>Optional</em>. Chat invite link, which was used by the user to join the chat; for joining by invite link events only.</summary>
    [JsonPropertyName("invite_link")]
    public ChatInviteLink? InviteLink { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the user joined the chat after sending a direct join request without using an invite link and being approved by an administrator</summary>
    [JsonPropertyName("via_join_request")]
    public bool ViaJoinRequest { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the user joined the chat via a chat folder invite link</summary>
    [JsonPropertyName("via_chat_folder_invite_link")]
    public bool ViaChatFolderInviteLink { get; set; }
}

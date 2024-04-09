using Telegram.Bot.Serialization;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a chat.
/// </summary>
public class Chat
{
    /// <summary>
    /// Unique identifier for this chat. This number may have more
    /// than 32 significant bits and some programming languages may have
    /// difficulty/silent defects in interpreting it. But it has
    /// at most 52 significant bits, so a signed 64-bit integer
    /// or double-precision float type are safe for storing this identifier.
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public long Id { get; set; }

    /// <summary>
    /// Type of chat, can be either “private”, “group”, “supergroup” or “channel”
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public ChatType Type { get; set; }

    /// <summary>
    /// Optional. Title, for supergroups, channels and group chats
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Title { get; set; }

    /// <summary>
    /// Optional. Username, for private chats, supergroups and channels if available
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Username { get; set; }

    /// <summary>
    /// Optional. First name of the other party in a private chat
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? FirstName { get; set; }

    /// <summary>
    /// Optional. Last name of the other party in a private chat
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? LastName { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if the supergroup chat is a forum (has topics enabled)
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? IsForum { get; set; }

    /// <summary>
    /// Optional. Chat photo. Returned only in <see cref="Requests.GetChatRequest"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ChatPhoto? Photo { get; set; }

    /// <summary>
    /// Optional. If non-empty, the list of all active chat usernames; for private chats, supergroups and channels.
    /// Returned only in <see cref="Requests.GetChatRequest"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? ActiveUsernames { get; set; }

    /// <summary>
    /// Optional. For private chats, the date of birth of the user.
    /// Returned only in <see cref="Requests.GetChatRequest"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Birthday? Birthday { get; set; }

    /// <summary>
    /// Optional. For private chats with business accounts, the intro of the business.
    /// Returned only in <see cref="Requests.GetChatRequest"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public BusinessIntro? BusinessIntro { get; set; }

    /// <summary>
    /// Optional. For private chats with business accounts, the location of the business.
    /// Returned only in <see cref="Requests.GetChatRequest"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public BusinessLocation? BusinessLocation { get; set; }

    /// <summary>
    /// Optional. For private chats with business accounts, the opening hours of the business.
    /// Returned only in <see cref="Requests.GetChatRequest"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public BusinessOpeningHours? BusinessOpeningHours { get; set; }

    /// <summary>
    /// Optional. For private chats, the personal channel of the user.
    /// Returned only in <see cref="Requests.GetChatRequest"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Chat? PersonalChat { get; set; }

    /// <summary>
    /// Optional. List of available reactions allowed in the chat. If omitted, then all <see cref="ReactionTypeEmoji.Emoji">emoji reactions</see> are allowed.
    /// Returned only in <see cref="Requests.GetChatRequest"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ReactionType[]? AvailableReactions { get; set; }

    /// <summary>
    /// Optional. Identifier of the <see href="https://core.telegram.org/bots/api#accent-colors">accent color</see>
    /// for the chat name and backgrounds of the chat photo, reply header, and link preview.
    /// See accent colors for more details. Returned only in <see cref="Requests.GetChatRequest"/>.
    /// Always returned in <see cref="Requests.GetChatRequest"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? AccentColorId { get; set; }

    /// <summary>
    /// Optional. Custom emoji identifier of emoji chosen by the chat for the reply header and link preview background.
    /// Returned only in <see cref="Requests.GetChatRequest"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? BackgroundCustomEmojiId { get; set; }

    /// <summary>
    /// Optional. Identifier of the accent color for the chat's profile background.
    /// See <see href="https://core.telegram.org/bots/api#profile-accent-colors">profile accent colors</see> for more details.
    /// Returned only in <see cref="Requests.GetChatRequest"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? ProfileAccentColorId { get; set; }

    /// <summary>
    /// Optional. Custom emoji identifier of the emoji chosen by the chat for its profile background.
    /// Returned only in <see cref="Requests.GetChatRequest"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ProfileBackgroundCustomEmojiId { get; set; }

    /// <summary>
    /// Optional. Custom emoji identifier of emoji status of the other party in a private chat.
    /// Returned only in <see cref="Requests.GetChatRequest"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? EmojiStatusCustomEmojiId { get; set; }

    /// <summary>
    /// Optional. Expiration date of the emoji status of the other party in a private chat, if any.
    /// Returned only in <see cref="Requests.GetChatRequest"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime? EmojiStatusExpirationDate { get; set; }

    /// <summary>
    /// Optional. Bio of the other party in a private chat. Returned only in <see cref="Requests.GetChatRequest"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Bio { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if privacy settings of the other party in the private chat allows to use
    /// <c>tg://user?id=&lt;user_id&gt;</c> links only in chats with the user.
    /// Returned only in <see cref="Requests.GetChatRequest"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? HasPrivateForwards { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if the privacy settings of the other party restrict sending voice
    /// and video note messages in the private chat.
    /// Returned only in <see cref="Requests.GetChatRequest"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? HasRestrictedVoiceAndVideoMessages { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if users need to join the supergroup before they can send messages.
    /// Returned only in <see cref="Requests.GetChatRequest"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? JoinToSendMessages { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if all users directly joining the supergroup need to be approved by supergroup administrators.
    /// Returned only in <see cref="Requests.GetChatRequest"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? JoinByRequest { get; set; }

    /// <summary>
    /// Optional. Description, for groups, supergroups and channel chats.
    /// Returned only in <see cref="Requests.GetChatRequest"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; set; }

    /// <summary>
    /// Optional. Primary invite link, for groups, supergroups and channel chats.
    /// Returned only in <see cref="Requests.GetChatRequest"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? InviteLink { get; set; }

    /// <summary>
    /// Optional. The most recent pinned message (by sending date).
    /// Returned only in <see cref="Requests.GetChatRequest"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Message? PinnedMessage { get; set; }

    /// <summary>
    /// Optional. Default chat member permissions, for groups and supergroups.
    /// Returned only in <see cref="Requests.GetChatRequest"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ChatPermissions? Permissions { get; set; }

    /// <summary>
    /// Optional. For supergroups, the minimum allowed delay between consecutive messages sent by each
    /// unpriviledged user. Returned only in <see cref="Requests.GetChatRequest"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? SlowModeDelay { get; set; }

    /// <summary>
    /// Optional. For supergroups, the minimum number of boosts that a non-administrator user needs to add in order
    /// to ignore slow mode and chat permissions. Returned only in getChat.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? UnrestrictBoostCount { get; set; }

    /// <summary>
    /// Optional. The time after which all messages sent to the chat will be automatically deleted; in seconds.
    /// Returned only in <see cref="Requests.GetChatRequest"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? MessageAutoDeleteTime { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if aggressive anti-spam checks are enabled in the supergroup. The field is
    /// only available to chat administrators. Returned only in <see cref="Requests.GetChatRequest"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? HasAggressiveAntiSpamEnabled { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if non-administrators can only get the list of bots and administrators in
    /// the chat. Returned only in <see cref="Requests.GetChatRequest"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? HasHiddenMembers { get; set; }

    /// <summary>
    /// Optional.  <see langword="true"/>, if new chat members will have access to old messages; available only to chat administrators.
    /// Returned only in <see cref="Requests.GetChatRequest"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? HasVisibleHistory { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if messages from the chat can't be forwarded to other chats.
    /// Returned only in <see cref="Requests.GetChatRequest"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? HasProtectedContent { get; set; }

    /// <summary>
    /// Optional. For supergroups, name of group sticker set.
    /// Returned only in <see cref="Requests.GetChatRequest"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? StickerSetName { get; set; }

    /// <summary>
    /// Optional. True, if the bot can change the group sticker set.
    /// Returned only in <see cref="Requests.GetChatRequest"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanSetStickerSet { get; set; }

    /// <summary>
    /// Optional. For supergroups, the name of the group's custom emoji sticker set. Custom emoji from this set can be
    /// used by all users and bots in the group.
    /// Returned only in <see cref="Requests.GetChatRequest"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CustomEmojiStickerSetName { get; set; }

    /// <summary>
    /// Optional. Unique identifier for the linked chat, i.e. the discussion group identifier for a channel
    /// and vice versa; for supergroups and channel chats. This identifier may be greater than 32 bits and some
    /// programming languages may have difficulty/silent defects in interpreting it. But it is smaller than
    /// 52 bits, so a signed 64 bit integer or double-precision float type are safe for storing this identifier.
    /// Returned only in <see cref="Requests.GetChatRequest"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? LinkedChatId { get; set; }

    /// <summary>
    /// Optional. For supergroups, the location to which the supergroup is connected.
    /// Returned only in <see cref="Requests.GetChatRequest"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ChatLocation? Location { get; set; }
}

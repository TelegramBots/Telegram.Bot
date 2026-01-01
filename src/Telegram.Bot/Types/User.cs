// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents a Telegram user or bot.</summary>
public partial class User
{
    /// <summary>Unique identifier for this user or bot.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public long Id { get; set; }

    /// <summary><see langword="true"/>, if this user is a bot</summary>
    [JsonPropertyName("is_bot")]
    public bool IsBot { get; set; }

    /// <summary>User's or bot's first name</summary>
    [JsonPropertyName("first_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string FirstName { get; set; } = default!;

    /// <summary><em>Optional</em>. User's or bot's last name</summary>
    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }

    /// <summary><em>Optional</em>. User's or bot's username</summary>
    public string? Username { get; set; }

    /// <summary><em>Optional</em>. <a href="https://en.wikipedia.org/wiki/IETF_language_tag">IETF language tag</a> of the user's language</summary>
    [JsonPropertyName("language_code")]
    public string? LanguageCode { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if this user is a Telegram Premium user</summary>
    [JsonPropertyName("is_premium")]
    public bool IsPremium { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if this user added the bot to the attachment menu</summary>
    [JsonPropertyName("added_to_attachment_menu")]
    public bool AddedToAttachmentMenu { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the bot can be invited to groups. Returned only in <see cref="TelegramBotClientExtensions.GetMe">GetMe</see>.</summary>
    [JsonPropertyName("can_join_groups")]
    public bool CanJoinGroups { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if <a href="https://core.telegram.org/bots/features#privacy-mode">privacy mode</a> is disabled for the bot. Returned only in <see cref="TelegramBotClientExtensions.GetMe">GetMe</see>.</summary>
    [JsonPropertyName("can_read_all_group_messages")]
    public bool CanReadAllGroupMessages { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the bot supports inline queries. Returned only in <see cref="TelegramBotClientExtensions.GetMe">GetMe</see>.</summary>
    [JsonPropertyName("supports_inline_queries")]
    public bool SupportsInlineQueries { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the bot can be connected to a Telegram Business account to receive its messages. Returned only in <see cref="TelegramBotClientExtensions.GetMe">GetMe</see>.</summary>
    [JsonPropertyName("can_connect_to_business")]
    public bool CanConnectToBusiness { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the bot has a main Web App. Returned only in <see cref="TelegramBotClientExtensions.GetMe">GetMe</see>.</summary>
    [JsonPropertyName("has_main_web_app")]
    public bool HasMainWebApp { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the bot has forum topic mode enabled in private chats. Returned only in <see cref="TelegramBotClientExtensions.GetMe">GetMe</see>.</summary>
    [JsonPropertyName("has_topics_enabled")]
    public bool HasTopicsEnabled { get; set; }
}

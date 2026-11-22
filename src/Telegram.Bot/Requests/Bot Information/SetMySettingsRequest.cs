// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to change settings of the bot. Settings that aren't specified remain unchanged.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SetMySettingsRequest() : RequestBase<bool>("setMySettings")
{
    /// <summary>Pass <see langword="true"/> to enable <a href="https://core.telegram.org/bots/api#inline-mode">inline mode</a> for the bot or <see langword="false"/> to disable it</summary>
    [JsonPropertyName("supports_inline_queries")]
    public bool SupportsInlineQueries { get; set; }

    /// <summary>New placeholder for inline queries, which is shown in the input field after the bot's username. Pass an empty string to use the default placeholder.</summary>
    [JsonPropertyName("inline_query_placeholder")]
    public string? InlineQueryPlaceholder { get; set; }

    /// <summary>Pass <see langword="true"/> to request the location of the user with every inline query to the bot or <see langword="false"/> to stop requesting it</summary>
    [JsonPropertyName("needs_inline_query_location")]
    public bool NeedsInlineQueryLocation { get; set; }

    /// <summary>Denominator of the share of <see cref="ChosenInlineResult">chosen inline results</see> received by the bot; the bot receives 1 out of <see cref="InlineFeedbackShareDenominator">InlineFeedbackShareDenominator</see> chosen results. Must be one of 1, 10, 100, or 1000. Pass 0 to stop receiving chosen inline results.</summary>
    [JsonPropertyName("inline_feedback_share_denominator")]
    public int? InlineFeedbackShareDenominator { get; set; }

    /// <summary>Pass <see langword="true"/> to allow adding the bot to groups or <see langword="false"/> to disallow it</summary>
    [JsonPropertyName("can_join_groups")]
    public bool CanJoinGroups { get; set; }

    /// <summary>Pass <see langword="true"/> to disable <a href="https://core.telegram.org/bots/features#privacy-mode">privacy mode</a> for the bot or <see langword="false"/> to enable it</summary>
    [JsonPropertyName("can_read_all_group_messages")]
    public bool CanReadAllGroupMessages { get; set; }

    /// <summary>Pass <see langword="true"/> to allow connecting the bot to user accounts to manage them or <see langword="false"/> to disallow it</summary>
    [JsonPropertyName("can_connect_to_business")]
    public bool CanConnectToBusiness { get; set; }

    /// <summary>Pass <see langword="true"/> to allow creating other bots that will be controlled by the bot or <see langword="false"/> to disallow it</summary>
    [JsonPropertyName("can_manage_bots")]
    public bool CanManageBots { get; set; }

    /// <summary>Pass <see langword="true"/> to allow guest queries to the bot from chats it isn't a member of or <see langword="false"/> to disallow them</summary>
    [JsonPropertyName("supports_guest_queries")]
    public bool SupportsGuestQueries { get; set; }

    /// <summary>Pass <see langword="true"/> to allow assigning the bot to process join request queries or <see langword="false"/> to disallow it</summary>
    [JsonPropertyName("supports_join_request_queries")]
    public bool SupportsJoinRequestQueries { get; set; }

    /// <summary>Pass <see langword="true"/> to enable forum topic mode in private chats with the bot or <see langword="false"/> to disable it</summary>
    [JsonPropertyName("has_topics_enabled")]
    public bool HasTopicsEnabled { get; set; }

    /// <summary>Pass <see langword="true"/> to allow users to create and delete topics in private chats with the bot or <see langword="false"/> to disallow it</summary>
    [JsonPropertyName("allows_users_to_create_topics")]
    public bool AllowsUsersToCreateTopics { get; set; }

    /// <summary>Pass <see langword="true"/> to show topics in private chats with the bot in a sidebar or <see langword="false"/> to stop showing them in a sidebar</summary>
    [JsonPropertyName("has_topic_sidebar")]
    public bool HasTopicSidebar { get; set; }

    /// <summary>Pass <see langword="true"/> to allow the bot to communicate with other bots in groups, private chats, and through business accounts or <see langword="false"/> to disallow it</summary>
    [JsonPropertyName("can_communicate_with_bots")]
    public bool CanCommunicateWithBots { get; set; }

    /// <summary>Mode in which the bot receives <see cref="CallbackQuery">callback queries</see> from buttons in messages sent by the bot. Must be one of <see cref="SetMySettingsCallbackQueryMode.Default">Default</see> (the field <em>message</em> is always used and contains an <a href="https://core.telegram.org/bots/api#inaccessiblemessage">InaccessibleMessage</a> if the message is inaccessible to the bot), <see cref="SetMySettingsCallbackQueryMode.Auto">Auto</see> (the field <em>InlineMessageId</em> is used instead of the field <em>message</em> if the message is inaccessible to the bot; the field <em>message</em> can still contain an inaccessible message if the message has just been deleted), or <see cref="SetMySettingsCallbackQueryMode.Inline">Inline</see> (the field <em>InlineMessageId</em> is always used instead of the field <em>message</em>).</summary>
    [JsonPropertyName("callback_query_mode")]
    public SetMySettingsCallbackQueryMode? CallbackQueryMode { get; set; }
}

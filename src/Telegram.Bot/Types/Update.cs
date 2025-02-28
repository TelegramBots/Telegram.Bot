// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This <a href="https://core.telegram.org/bots/api#available-types">object</a> represents an incoming update.<br/>At most <b>one</b> of the optional parameters can be present in any given update.</summary>
public partial class Update
{
    /// <summary>The update's unique identifier. Update identifiers start from a certain positive number and increase sequentially. This identifier becomes especially handy if you're using <see cref="TelegramBotClientExtensions.SetWebhook">webhooks</see>, since it allows you to ignore repeated updates or to restore the correct update sequence, should they get out of order. If there are no new updates for at least a week, then identifier of the next update will be chosen randomly instead of sequentially.</summary>
    [JsonPropertyName("update_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Id { get; set; }

    /// <summary><em>Optional</em>. New incoming message of any kind - text, photo, sticker, etc.</summary>
    public Message? Message { get; set; }

    /// <summary><em>Optional</em>. New version of a message that is known to the bot and was edited. This update may at times be triggered by changes to message fields that are either unavailable or not actively used by your bot.</summary>
    [JsonPropertyName("edited_message")]
    public Message? EditedMessage { get; set; }

    /// <summary><em>Optional</em>. New incoming channel post of any kind - text, photo, sticker, etc.</summary>
    [JsonPropertyName("channel_post")]
    public Message? ChannelPost { get; set; }

    /// <summary><em>Optional</em>. New version of a channel post that is known to the bot and was edited. This update may at times be triggered by changes to message fields that are either unavailable or not actively used by your bot.</summary>
    [JsonPropertyName("edited_channel_post")]
    public Message? EditedChannelPost { get; set; }

    /// <summary><em>Optional</em>. The bot was connected to or disconnected from a business account, or a user edited an existing connection with the bot</summary>
    [JsonPropertyName("business_connection")]
    public BusinessConnection? BusinessConnection { get; set; }

    /// <summary><em>Optional</em>. New message from a connected business account</summary>
    [JsonPropertyName("business_message")]
    public Message? BusinessMessage { get; set; }

    /// <summary><em>Optional</em>. New version of a message from a connected business account</summary>
    [JsonPropertyName("edited_business_message")]
    public Message? EditedBusinessMessage { get; set; }

    /// <summary><em>Optional</em>. Messages were deleted from a connected business account</summary>
    [JsonPropertyName("deleted_business_messages")]
    public BusinessMessagesDeleted? DeletedBusinessMessages { get; set; }

    /// <summary><em>Optional</em>. A reaction to a message was changed by a user. The bot must be an administrator in the chat and must explicitly specify <c>"<see cref="MessageReaction">MessageReaction</see>"</c> in the list of <em>AllowedUpdates</em> to receive these updates. The update isn't received for reactions set by bots.</summary>
    [JsonPropertyName("message_reaction")]
    public MessageReactionUpdated? MessageReaction { get; set; }

    /// <summary><em>Optional</em>. Reactions to a message with anonymous reactions were changed. The bot must be an administrator in the chat and must explicitly specify <c>"<see cref="MessageReactionCount">MessageReactionCount</see>"</c> in the list of <em>AllowedUpdates</em> to receive these updates. The updates are grouped and can be sent with delay up to a few minutes.</summary>
    [JsonPropertyName("message_reaction_count")]
    public MessageReactionCountUpdated? MessageReactionCount { get; set; }

    /// <summary><em>Optional</em>. New incoming <a href="https://core.telegram.org/bots/api#inline-mode">inline</a> query</summary>
    [JsonPropertyName("inline_query")]
    public InlineQuery? InlineQuery { get; set; }

    /// <summary><em>Optional</em>. The result of an <a href="https://core.telegram.org/bots/api#inline-mode">inline</a> query that was chosen by a user and sent to their chat partner. Please see our documentation on the <a href="https://core.telegram.org/bots/inline#collecting-feedback">feedback collecting</a> for details on how to enable these updates for your bot.</summary>
    [JsonPropertyName("chosen_inline_result")]
    public ChosenInlineResult? ChosenInlineResult { get; set; }

    /// <summary><em>Optional</em>. New incoming callback query</summary>
    [JsonPropertyName("callback_query")]
    public CallbackQuery? CallbackQuery { get; set; }

    /// <summary><em>Optional</em>. New incoming shipping query. Only for invoices with flexible price</summary>
    [JsonPropertyName("shipping_query")]
    public ShippingQuery? ShippingQuery { get; set; }

    /// <summary><em>Optional</em>. New incoming pre-checkout query. Contains full information about checkout</summary>
    [JsonPropertyName("pre_checkout_query")]
    public PreCheckoutQuery? PreCheckoutQuery { get; set; }

    /// <summary><em>Optional</em>. A user purchased paid media with a non-empty payload sent by the bot in a non-channel chat</summary>
    [JsonPropertyName("purchased_paid_media")]
    public PaidMediaPurchased? PurchasedPaidMedia { get; set; }

    /// <summary><em>Optional</em>. New poll state. Bots receive only updates about manually stopped polls and polls, which are sent by the bot</summary>
    public Poll? Poll { get; set; }

    /// <summary><em>Optional</em>. A user changed their answer in a non-anonymous poll. Bots receive new votes only in polls that were sent by the bot itself.</summary>
    [JsonPropertyName("poll_answer")]
    public PollAnswer? PollAnswer { get; set; }

    /// <summary><em>Optional</em>. The bot's chat member status was updated in a chat. For private chats, this update is received only when the bot is blocked or unblocked by the user.</summary>
    [JsonPropertyName("my_chat_member")]
    public ChatMemberUpdated? MyChatMember { get; set; }

    /// <summary><em>Optional</em>. A chat member's status was updated in a chat. The bot must be an administrator in the chat and must explicitly specify <c>"<see cref="ChatMember">ChatMember</see>"</c> in the list of <em>AllowedUpdates</em> to receive these updates.</summary>
    [JsonPropertyName("chat_member")]
    public ChatMemberUpdated? ChatMember { get; set; }

    /// <summary><em>Optional</em>. A request to join the chat has been sent. The bot must have the <em>CanInviteUsers</em> administrator right in the chat to receive these updates.</summary>
    [JsonPropertyName("chat_join_request")]
    public ChatJoinRequest? ChatJoinRequest { get; set; }

    /// <summary><em>Optional</em>. A chat boost was added or changed. The bot must be an administrator in the chat to receive these updates.</summary>
    [JsonPropertyName("chat_boost")]
    public ChatBoostUpdated? ChatBoost { get; set; }

    /// <summary><em>Optional</em>. A boost was removed from a chat. The bot must be an administrator in the chat to receive these updates.</summary>
    [JsonPropertyName("removed_chat_boost")]
    public ChatBoostRemoved? RemovedChatBoost { get; set; }

    /// <summary>Gets the <see cref="UpdateType">type</see> of the <see cref="Update"/></summary>
    /// <value>The <see cref="UpdateType">type</see> of the <see cref="Update"/></value>
    [JsonIgnore]
    public UpdateType Type => this switch
    {
        { Message: not null }                 => UpdateType.Message,
        { EditedMessage: not null }           => UpdateType.EditedMessage,
        { ChannelPost: not null }             => UpdateType.ChannelPost,
        { EditedChannelPost: not null }       => UpdateType.EditedChannelPost,
        { BusinessConnection: not null }      => UpdateType.BusinessConnection,
        { BusinessMessage: not null }         => UpdateType.BusinessMessage,
        { EditedBusinessMessage: not null }   => UpdateType.EditedBusinessMessage,
        { DeletedBusinessMessages: not null } => UpdateType.DeletedBusinessMessages,
        { MessageReaction: not null }         => UpdateType.MessageReaction,
        { MessageReactionCount: not null }    => UpdateType.MessageReactionCount,
        { InlineQuery: not null }             => UpdateType.InlineQuery,
        { ChosenInlineResult: not null }      => UpdateType.ChosenInlineResult,
        { CallbackQuery: not null }           => UpdateType.CallbackQuery,
        { ShippingQuery: not null }           => UpdateType.ShippingQuery,
        { PreCheckoutQuery: not null }        => UpdateType.PreCheckoutQuery,
        { PurchasedPaidMedia: not null }      => UpdateType.PurchasedPaidMedia,
        { Poll: not null }                    => UpdateType.Poll,
        { PollAnswer: not null }              => UpdateType.PollAnswer,
        { MyChatMember: not null }            => UpdateType.MyChatMember,
        { ChatMember: not null }              => UpdateType.ChatMember,
        { ChatJoinRequest: not null }         => UpdateType.ChatJoinRequest,
        { ChatBoost: not null }               => UpdateType.ChatBoost,
        { RemovedChatBoost: not null }        => UpdateType.RemovedChatBoost,
        _                                     => UpdateType.Unknown
    };

    /// <summary>All UpdateTypes, for use with <see cref="TelegramBotClientExtensions.GetUpdates">GetUpdates</see></summary>
    public static readonly UpdateType[] AllTypes =
        [UpdateType.Message, UpdateType.EditedMessage, UpdateType.ChannelPost, UpdateType.EditedChannelPost, UpdateType.BusinessConnection, UpdateType.BusinessMessage, UpdateType.EditedBusinessMessage, UpdateType.DeletedBusinessMessages, UpdateType.MessageReaction, UpdateType.MessageReactionCount, UpdateType.InlineQuery, UpdateType.ChosenInlineResult, UpdateType.CallbackQuery, UpdateType.ShippingQuery, UpdateType.PreCheckoutQuery, UpdateType.PurchasedPaidMedia, UpdateType.Poll, UpdateType.PollAnswer, UpdateType.MyChatMember, UpdateType.ChatMember, UpdateType.ChatJoinRequest, UpdateType.ChatBoost, UpdateType.RemovedChatBoost];
}

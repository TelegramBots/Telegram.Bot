using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Payments;

namespace Telegram.Bot.Types;

/// <summary>
/// This object represents an incoming update.
/// </summary>
/// <remarks>
/// Only <b>one</b> of the optional parameters can be present in any given update.
/// </remarks>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class Update
{
    /// <summary>
    /// The update's unique identifier. Update identifiers start from a certain positive number and increase
    /// sequentially. This ID becomes especially handy if you're using
    /// <a href="https://core.telegram.org/bots/api#setwebhook">Webhooks</a>, since it allows you to ignore repeated
    /// updates or to restore the correct update sequence, should they get out of order. If there are no new updates
    /// for at least a week, then identifier of the next update will be chosen randomly instead of sequentially.
    /// </summary>
    [JsonProperty("update_id", Required = Required.Always)]
    public int Id { get; set; }

    /// <summary>
    /// Optional. New incoming message of any kind — text, photo, sticker, etc.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public Message? Message { get; set; }

    /// <summary>
    /// Optional. New version of a message that is known to the bot and was edited
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public Message? EditedMessage { get; set; }

    /// <summary>
    /// Optional. New incoming channel post of any kind — text, photo, sticker, etc.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public Message? ChannelPost { get; set; }

    /// <summary>
    /// Optional. New version of a channel post that is known to the bot and was edited
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public Message? EditedChannelPost { get; set; }

    /// <summary>
    /// Optional. A reaction to a message was changed by a user. The bot must be an administrator
    /// in the chat and must explicitly specify "<see cref="UpdateType.MessageReaction"/>" in the list
    /// of <c>AllowedUpdates</c> to receive these updates.
    /// The update isn't received for reactions set by bots.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public MessageReactionUpdated? MessageReaction { get; set; }

    /// <summary>
    /// Optional. Reactions to a message with anonymous reactions were changed. The bot must
    /// be an administrator in the chat and must explicitly specify "<see cref="UpdateType.MessageReactionCount"/>"
    /// in the list of <c>AllowedUpdates</c> to receive these updates.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public MessageReactionCountUpdated? MessageReactionCount { get; set; }

    /// <summary>
    /// Optional. New incoming inline query
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public InlineQuery? InlineQuery { get; set; }

    /// <summary>
    /// Optional. The result of a inline query that was chosen by a user and sent to their chat partner
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public ChosenInlineResult? ChosenInlineResult { get; set; }

    /// <summary>
    /// Optional. New incoming callback query
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public CallbackQuery? CallbackQuery { get; set; }

    /// <summary>
    /// Optional. New incoming shipping query. Only for invoices with flexible price
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public ShippingQuery? ShippingQuery { get; set; }

    /// <summary>
    /// Optional. New incoming pre-checkout query. Contains full information about checkout
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public PreCheckoutQuery? PreCheckoutQuery { get; set; }

    /// <summary>
    /// Optional. New poll state. Bots receive only updates about stopped polls and polls, which are sent by the bot
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public Poll? Poll { get; set; }

    /// <summary>
    /// Optional. A user changed their answer in a non-anonymous poll. Bots receive new votes only in polls that were
    /// sent by the bot itself.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public PollAnswer? PollAnswer { get; set; }

    /// <summary>
    /// Optional. The bot’s chat member status was updated in a chat. For private chats, this update is received
    /// only when the bot is blocked or unblocked by the user.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public ChatMemberUpdated? MyChatMember { get; set; }

    /// <summary>
    /// Optional. A chat member's status was updated in a chat. The bot must be an administrator in the chat
    /// and must explicitly specify “<see cref="UpdateType.ChatMember"/>” in the list of <c>AllowedUpdates</c> to
    /// receive these updates.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public ChatMemberUpdated? ChatMember { get; set; }

    /// <summary>
    /// Optional. A request to join the chat has been sent. The bot must have the
    /// <see cref="ChatPermissions.CanInviteUsers"/> administrator right in the chat to receive these updates.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public ChatJoinRequest? ChatJoinRequest { get; set; }

    /// <summary>
    /// Optional. A chat boost was added or changed.
    /// The bot must be an administrator in the chat to receive these updates.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public ChatBoostUpdated? ChatBoost { get; set; }

    /// <summary>
    /// Optional. A boost was removed from a chat.
    /// The bot must be an administrator in the chat to receive these updates.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public ChatBoostRemoved? RemovedChatBoost { get; set; }

    /// <summary>
    /// Gets the update type.
    /// </summary>
    /// <value>
    /// The update type.
    /// </value>
    [JsonIgnore]
    public UpdateType Type => this switch
    {
        { Message: not null }               => UpdateType.Message,
        { EditedMessage: not null }         => UpdateType.EditedMessage,
        { ChannelPost: not null }           => UpdateType.ChannelPost,
        { EditedChannelPost: not null }     => UpdateType.EditedChannelPost,
        { MessageReaction: not null }       => UpdateType.MessageReaction,
        { MessageReactionCount: not null }  => UpdateType.MessageReactionCount,
        { InlineQuery: not null }           => UpdateType.InlineQuery,
        { ChosenInlineResult: not null }    => UpdateType.ChosenInlineResult,
        { CallbackQuery: not null }         => UpdateType.CallbackQuery,
        { ShippingQuery: not null }         => UpdateType.ShippingQuery,
        { PreCheckoutQuery: not null }      => UpdateType.PreCheckoutQuery,
        { Poll: not null }                  => UpdateType.Poll,
        { PollAnswer: not null }            => UpdateType.PollAnswer,
        { MyChatMember: not null }          => UpdateType.MyChatMember,
        { ChatMember: not null }            => UpdateType.ChatMember,
        { ChatJoinRequest: not null }       => UpdateType.ChatJoinRequest,
        { ChatBoost: not null }             => UpdateType.ChatBoost,
        { RemovedChatBoost: not null }      => UpdateType.RemovedChatBoost,
        _                                   => UpdateType.Unknown
    };
}

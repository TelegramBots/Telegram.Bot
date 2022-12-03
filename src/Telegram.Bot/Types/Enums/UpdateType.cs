namespace Telegram.Bot.Types.Enums;

/// <summary>
/// The type of an <see cref="Update"/>
/// </summary>
[JsonConverter(typeof(UpdateTypeConverter))]
public enum UpdateType
{
    /// <summary>
    /// Update Type is unknown
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// The <see cref="Update"/> contains a <see cref="Types.Message"/>.
    /// </summary>
    Message,

    /// <summary>
    /// The <see cref="Update"/> contains an <see cref="Types.InlineQuery"/>.
    /// </summary>
    InlineQuery,

    /// <summary>
    /// The <see cref="Update"/> contains a <see cref="Types.ChosenInlineResult"/>.
    /// </summary>
    ChosenInlineResult,

    /// <summary>
    /// The <see cref="Update"/> contains a <see cref="Types.CallbackQuery"/>
    /// </summary>
    CallbackQuery,

    /// <summary>
    /// The <see cref="Update"/> contains an edited <see cref="Types.Message"/>
    /// </summary>
    EditedMessage,

    /// <summary>
    /// The <see cref="Update"/> contains a channel post <see cref="Types.Message"/>
    /// </summary>
    ChannelPost,

    /// <summary>
    /// The <see cref="Update"/> contains an edited channel post <see cref="Types.Message"/>
    /// </summary>
    EditedChannelPost,

    /// <summary>
    /// The <see cref="Update"/> contains an <see cref="ShippingQuery"/>
    /// </summary>
    ShippingQuery,

    /// <summary>
    /// The <see cref="Update"/> contains an <see cref="PreCheckoutQuery"/>
    /// </summary>
    PreCheckoutQuery,

    /// <summary>
    /// The <see cref="Update"/> contains an <see cref="Poll"/>
    /// </summary>
    Poll,

    /// <summary>
    /// The <see cref="Update"/> contains an <see cref="PollAnswer"/>
    /// </summary>
    PollAnswer,

    /// <summary>
    /// The <see cref="Update"/> contains an <see cref="MyChatMember"/>
    /// </summary>
    MyChatMember,

    /// <summary>
    /// The <see cref="Update"/> contains an <see cref="ChatMember"/>
    /// </summary>
    ChatMember,

    /// <summary>
    /// The <see cref="Update"/> contains an <see cref="ChatJoinRequest"/>
    /// </summary>
    ChatJoinRequest,
}

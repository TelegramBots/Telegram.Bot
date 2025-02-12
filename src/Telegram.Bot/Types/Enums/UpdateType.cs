// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Enums;

/// <summary>The type of <see cref="Update"/></summary>
[JsonConverter(typeof(EnumConverter<UpdateType>))]
public enum UpdateType
{
    /// <summary><see cref="Update"/> type is unknown</summary>
    Unknown = 0,
    /// <summary>The <see cref="Update"/> contains a <see cref="Update.Message"/></summary>
    Message,
    /// <summary>The <see cref="Update"/> contains an <see cref="Update.InlineQuery"/></summary>
    InlineQuery,
    /// <summary>The <see cref="Update"/> contains a <see cref="Update.ChosenInlineResult"/></summary>
    ChosenInlineResult,
    /// <summary>The <see cref="Update"/> contains a <see cref="Update.CallbackQuery"/></summary>
    CallbackQuery,
    /// <summary>The <see cref="Update"/> contains an <see cref="Update.EditedMessage"/></summary>
    EditedMessage,
    /// <summary>The <see cref="Update"/> contains a <see cref="Update.ChannelPost"/></summary>
    ChannelPost,
    /// <summary>The <see cref="Update"/> contains an <see cref="Update.EditedChannelPost"/></summary>
    EditedChannelPost,
    /// <summary>The <see cref="Update"/> contains a <see cref="Update.ShippingQuery"/></summary>
    ShippingQuery,
    /// <summary>The <see cref="Update"/> contains a <see cref="Update.PreCheckoutQuery"/></summary>
    PreCheckoutQuery,
    /// <summary>The <see cref="Update"/> contains a <see cref="Update.Poll"/></summary>
    Poll,
    /// <summary>The <see cref="Update"/> contains a <see cref="Update.PollAnswer"/></summary>
    PollAnswer,
    /// <summary>The <see cref="Update"/> contains a <see cref="Update.MyChatMember"/></summary>
    MyChatMember,
    /// <summary>The <see cref="Update"/> contains a <see cref="Update.ChatMember"/></summary>
    ChatMember,
    /// <summary>The <see cref="Update"/> contains a <see cref="Update.ChatJoinRequest"/></summary>
    ChatJoinRequest,
    /// <summary>The <see cref="Update"/> contains a <see cref="Update.MessageReaction"/></summary>
    MessageReaction,
    /// <summary>The <see cref="Update"/> contains a <see cref="Update.MessageReactionCount"/></summary>
    MessageReactionCount,
    /// <summary>The <see cref="Update"/> contains a <see cref="Update.ChatBoost"/></summary>
    ChatBoost,
    /// <summary>The <see cref="Update"/> contains a <see cref="Update.RemovedChatBoost"/></summary>
    RemovedChatBoost,
    /// <summary>The <see cref="Update"/> contains a <see cref="Update.BusinessConnection"/></summary>
    BusinessConnection,
    /// <summary>The <see cref="Update"/> contains a <see cref="Update.BusinessMessage"/></summary>
    BusinessMessage,
    /// <summary>The <see cref="Update"/> contains an <see cref="Update.EditedBusinessMessage"/></summary>
    EditedBusinessMessage,
    /// <summary>The <see cref="Update"/> contains a <see cref="Update.DeletedBusinessMessages"/></summary>
    DeletedBusinessMessages,
    /// <summary>The <see cref="Update"/> contains a <see cref="Update.PurchasedPaidMedia"/></summary>
    PurchasedPaidMedia,
}

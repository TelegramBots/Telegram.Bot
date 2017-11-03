using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Telegram.Bot.Types.Enums
{
    /// <summary>
    /// The type of an <see cref="Update"/>
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum UpdateType
    {
        /// <summary>
        /// Update Type is unknown
        /// </summary>
        [EnumMember(Value = "unknown_update")]
        UnknownUpdate = 0,

        /// <summary>
        /// The <see cref="Update"/> contains a <see cref="Message"/>.
        /// </summary>
        [EnumMember(Value = "message")]
        MessageUpdate,

        /// <summary>
        /// The <see cref="Update"/> contains an <see cref="InlineQuery"/>.
        /// </summary>
        [EnumMember(Value = "inline_query")]
        InlineQueryUpdate,

        /// <summary>
        /// The <see cref="Update"/> contains a <see cref="ChosenInlineResult"/>.
        /// </summary>
        [EnumMember(Value = "chosen_inline_result")]
        ChosenInlineResultUpdate,

        /// <summary>
        /// The <see cref="Update"/> contains a <see cref="CallbackQuery"/>
        /// </summary>
        [EnumMember(Value = "callback_query")]
        CallbackQueryUpdate,

        /// <summary>
        /// The <see cref="Update"/> contains an edited <see cref="Message"/>
        /// </summary>
        [EnumMember(Value = "edited_message")]
        EditedMessage,

        /// <summary>
        /// The <see cref="Update"/> contains a channel post <see cref="Message"/>
        /// </summary>
        [EnumMember(Value = "channel_post")]
        ChannelPost,

        /// <summary>
        /// The <see cref="Update"/> contains an edited channel post <see cref="Message"/>
        /// </summary>
        [EnumMember(Value = "edited_channel_post")]
        EditedChannelPost,

        /// <summary>
        /// The <see cref="Update"/> contains an <see cref="ShippingQueryUpdate"/>
        /// </summary>
        [EnumMember(Value = "shipping_query")]
        ShippingQueryUpdate,

        /// <summary>
        /// The <see cref="Update"/> contains an <see cref="PreCheckoutQueryUpdate"/>
        /// </summary>
        [EnumMember(Value = "pre_checkout_query")]
        PreCheckoutQueryUpdate,

        /// <summary>
        /// Receive all <see cref="Update"/> Types
        /// </summary>
        [EnumMember(Value = "")]
        All = 255,
    }
}

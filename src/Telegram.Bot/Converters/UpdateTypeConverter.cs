using System.Collections.Generic;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Converters;

internal class UpdateTypeConverter : EnumConverter<UpdateType>
{
    static readonly IReadOnlyDictionary<string, UpdateType> StringToEnum =
        new Dictionary<string, UpdateType>
        {
            {"message", UpdateType.Message},
            {"inline_query", UpdateType.InlineQuery},
            {"chosen_inline_result", UpdateType.ChosenInlineResult},
            {"callback_query", UpdateType.CallbackQuery},
            {"edited_message", UpdateType.EditedMessage},
            {"channel_post", UpdateType.ChannelPost},
            {"edited_channel_post", UpdateType.EditedChannelPost},
            {"shipping_query", UpdateType.ShippingQuery},
            {"pre_checkout_query", UpdateType.PreCheckoutQuery},
            {"poll", UpdateType.Poll},
            {"poll_answer", UpdateType.PollAnswer},
            {"my_chat_member", UpdateType.MyChatMember},
            {"chat_member", UpdateType.ChatMember},
            {"chat_join_request", UpdateType.ChatJoinRequest}
        };

    static readonly IReadOnlyDictionary<UpdateType, string> EnumToString =
        new Dictionary<UpdateType, string>
        {
            {UpdateType.Unknown, "unknown"},
            {UpdateType.Message, "message"},
            {UpdateType.InlineQuery, "inline_query"},
            {UpdateType.ChosenInlineResult, "chosen_inline_result"},
            {UpdateType.CallbackQuery, "callback_query"},
            {UpdateType.EditedMessage, "edited_message"},
            {UpdateType.ChannelPost, "channel_post"},
            {UpdateType.EditedChannelPost, "edited_channel_post"},
            {UpdateType.ShippingQuery, "shipping_query"},
            {UpdateType.PreCheckoutQuery, "pre_checkout_query"},
            {UpdateType.Poll, "poll"},
            {UpdateType.PollAnswer, "poll_answer"},
            {UpdateType.MyChatMember, "my_chat_member"},
            {UpdateType.ChatMember, "chat_member"},
            {UpdateType.ChatJoinRequest, "chat_join_request"}
        };

    protected override UpdateType GetEnumValue(string value) =>
        StringToEnum.TryGetValue(value, out var enumValue)
            ? enumValue
            : 0;

    protected override string GetStringValue(UpdateType value) =>
        EnumToString.TryGetValue(value, out var stringValue)
            ? stringValue
            : "unknown";
}
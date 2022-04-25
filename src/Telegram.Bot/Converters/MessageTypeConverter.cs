using System;
using System.Collections.Generic;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Converters;

internal class MessageTypeConverter : EnumConverter<MessageType>
{
    static readonly IReadOnlyDictionary<string, MessageType> StringToEnum =
        new Dictionary<string, MessageType>
        {
            {"text", MessageType.Text},
            {"photo", MessageType.Photo},
            {"audio", MessageType.Audio},
            {"video", MessageType.Video},
            {"voice", MessageType.Voice},
            {"document", MessageType.Document},
            {"sticker", MessageType.Sticker},
            {"location", MessageType.Location},
            {"contact", MessageType.Contact},
            {"venue", MessageType.Venue},
            {"game", MessageType.Game},
            {"video_note", MessageType.VideoNote},
            {"invoice", MessageType.Invoice},
            {"successful_payment", MessageType.SuccessfulPayment},
            {"website_connected", MessageType.WebsiteConnected},
            {"chat_members_added", MessageType.ChatMembersAdded},
            {"chat_member_left", MessageType.ChatMemberLeft},
            {"chat_title_changed", MessageType.ChatTitleChanged},
            {"chat_photo_changed", MessageType.ChatPhotoChanged},
            {"message_pinned", MessageType.MessagePinned},
            {"chat_photo_deleted", MessageType.ChatPhotoDeleted},
            {"group_created", MessageType.GroupCreated},
            {"supergroup_created", MessageType.SupergroupCreated},
            {"channel_created", MessageType.ChannelCreated},
            {"migrated_to_supergroup", MessageType.MigratedToSupergroup},
            {"migrated_from_group", MessageType.MigratedFromGroup},
            {"poll", MessageType.Poll},
            {"dice", MessageType.Dice},
            {"message_auto_delete_timer_changed", MessageType.MessageAutoDeleteTimerChanged},
            {"proximity_alert_triggered", MessageType.ProximityAlertTriggered},
            {"video_chat_scheduled", MessageType.VideoChatScheduled},
            {"video_chat_started", MessageType.VideoChatStarted},
            {"video_chat_ended", MessageType.VideoChatEnded},
            {"video_chat_participants_invited", MessageType.VideoChatParticipantsInvited}
        };

    static readonly IReadOnlyDictionary<MessageType, string> EnumToString =
        new Dictionary<MessageType, string>
        {
            {MessageType.Unknown, "unknown"},
            {MessageType.Text, "text"},
            {MessageType.Photo, "photo"},
            {MessageType.Audio, "audio"},
            {MessageType.Video, "video"},
            {MessageType.Voice, "voice"},
            {MessageType.Document, "document"},
            {MessageType.Sticker, "sticker"},
            {MessageType.Location, "location"},
            {MessageType.Contact, "contact"},
            {MessageType.Venue, "venue"},
            {MessageType.Game, "game"},
            {MessageType.VideoNote, "video_note"},
            {MessageType.Invoice, "invoice"},
            {MessageType.SuccessfulPayment, "successful_payment"},
            {MessageType.WebsiteConnected, "website_connected"},
            {MessageType.ChatMembersAdded, "chat_members_added"},
            {MessageType.ChatMemberLeft, "chat_member_left"},
            {MessageType.ChatTitleChanged, "chat_title_changed"},
            {MessageType.ChatPhotoChanged, "chat_photo_changed"},
            {MessageType.MessagePinned, "message_pinned"},
            {MessageType.ChatPhotoDeleted, "chat_photo_deleted"},
            {MessageType.GroupCreated, "group_created"},
            {MessageType.SupergroupCreated, "supergroup_created"},
            {MessageType.ChannelCreated, "channel_created"},
            {MessageType.MigratedToSupergroup, "migrated_to_supergroup"},
            {MessageType.MigratedFromGroup, "migrated_from_group"},
            {MessageType.Poll, "poll"},
            {MessageType.Dice, "dice"},
            {MessageType.MessageAutoDeleteTimerChanged, "message_auto_delete_timer_changed"},
            {MessageType.ProximityAlertTriggered, "proximity_alert_triggered"},
            {MessageType.VideoChatScheduled, "video_chat_scheduled"},
            {MessageType.VideoChatStarted, "video_chat_started"},
            {MessageType.VideoChatEnded, "video_chat_ended"},
            {MessageType.VideoChatParticipantsInvited, "video_chat_participants_invited"}
        };

    protected override MessageType GetEnumValue(string value) =>
        StringToEnum.TryGetValue(value, out var enumValue)
            ? enumValue
            : 0;

    protected override string GetStringValue(MessageType value) =>
        EnumToString.TryGetValue(value, out var stringValue)
            ? stringValue
            : throw new NotSupportedException();
}

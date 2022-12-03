using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class MessageTypeConverterTests
{
    [Theory]
    [InlineData(MessageType.Unknown, "unknown")]
    [InlineData(MessageType.Text, "text")]
    [InlineData(MessageType.Photo, "photo")]
    [InlineData(MessageType.Audio, "audio")]
    [InlineData(MessageType.Video, "video")]
    [InlineData(MessageType.Voice, "voice")]
    [InlineData(MessageType.Animation, "animation")]
    [InlineData(MessageType.Document, "document")]
    [InlineData(MessageType.Sticker, "sticker")]
    [InlineData(MessageType.Location, "location")]
    [InlineData(MessageType.Contact, "contact")]
    [InlineData(MessageType.Venue, "venue")]
    [InlineData(MessageType.Game, "game")]
    [InlineData(MessageType.VideoNote, "video_note")]
    [InlineData(MessageType.Invoice, "invoice")]
    [InlineData(MessageType.SuccessfulPayment, "successful_payment")]
    [InlineData(MessageType.WebsiteConnected, "website_connected")]
    [InlineData(MessageType.ChatMembersAdded, "chat_members_added")]
    [InlineData(MessageType.ChatMemberLeft, "chat_member_left")]
    [InlineData(MessageType.ChatTitleChanged, "chat_title_changed")]
    [InlineData(MessageType.ChatPhotoChanged, "chat_photo_changed")]
    [InlineData(MessageType.MessagePinned, "message_pinned")]
    [InlineData(MessageType.ChatPhotoDeleted, "chat_photo_deleted")]
    [InlineData(MessageType.GroupCreated, "group_created")]
    [InlineData(MessageType.SupergroupCreated, "supergroup_created")]
    [InlineData(MessageType.ChannelCreated, "channel_created")]
    [InlineData(MessageType.MigratedToSupergroup, "migrated_to_supergroup")]
    [InlineData(MessageType.MigratedFromGroup, "migrated_from_group")]
    [InlineData(MessageType.Poll, "poll")]
    [InlineData(MessageType.Dice, "dice")]
    [InlineData(MessageType.MessageAutoDeleteTimerChanged, "message_auto_delete_timer_changed")]
    [InlineData(MessageType.ProximityAlertTriggered, "proximity_alert_triggered")]
    [InlineData(MessageType.VideoChatScheduled, "video_chat_scheduled")]
    [InlineData(MessageType.VideoChatStarted, "video_chat_started")]
    [InlineData(MessageType.VideoChatEnded, "video_chat_ended")]
    [InlineData(MessageType.VideoChatParticipantsInvited, "video_chat_participants_invited")]
    public void Should_Convert_UpdateType_To_String(MessageType messageType, string value)
    {
        Message message = new() { Type = messageType };
        string expectedResult = @$"{{""type"":""{value}""}}";

        string result = JsonConvert.SerializeObject(message);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(MessageType.Unknown, "unknown")]
    [InlineData(MessageType.Text, "text")]
    [InlineData(MessageType.Photo, "photo")]
    [InlineData(MessageType.Audio, "audio")]
    [InlineData(MessageType.Video, "video")]
    [InlineData(MessageType.Voice, "voice")]
    [InlineData(MessageType.Animation, "animation")]
    [InlineData(MessageType.Document, "document")]
    [InlineData(MessageType.Sticker, "sticker")]
    [InlineData(MessageType.Location, "location")]
    [InlineData(MessageType.Contact, "contact")]
    [InlineData(MessageType.Venue, "venue")]
    [InlineData(MessageType.Game, "game")]
    [InlineData(MessageType.VideoNote, "video_note")]
    [InlineData(MessageType.Invoice, "invoice")]
    [InlineData(MessageType.SuccessfulPayment, "successful_payment")]
    [InlineData(MessageType.WebsiteConnected, "website_connected")]
    [InlineData(MessageType.ChatMembersAdded, "chat_members_added")]
    [InlineData(MessageType.ChatMemberLeft, "chat_member_left")]
    [InlineData(MessageType.ChatTitleChanged, "chat_title_changed")]
    [InlineData(MessageType.ChatPhotoChanged, "chat_photo_changed")]
    [InlineData(MessageType.MessagePinned, "message_pinned")]
    [InlineData(MessageType.ChatPhotoDeleted, "chat_photo_deleted")]
    [InlineData(MessageType.GroupCreated, "group_created")]
    [InlineData(MessageType.SupergroupCreated, "supergroup_created")]
    [InlineData(MessageType.ChannelCreated, "channel_created")]
    [InlineData(MessageType.MigratedToSupergroup, "migrated_to_supergroup")]
    [InlineData(MessageType.MigratedFromGroup, "migrated_from_group")]
    [InlineData(MessageType.Poll, "poll")]
    [InlineData(MessageType.Dice, "dice")]
    [InlineData(MessageType.MessageAutoDeleteTimerChanged, "message_auto_delete_timer_changed")]
    [InlineData(MessageType.ProximityAlertTriggered, "proximity_alert_triggered")]
    [InlineData(MessageType.VideoChatScheduled, "video_chat_scheduled")]
    [InlineData(MessageType.VideoChatStarted, "video_chat_started")]
    [InlineData(MessageType.VideoChatEnded, "video_chat_ended")]
    [InlineData(MessageType.VideoChatParticipantsInvited, "video_chat_participants_invited")]
    public void Should_Convert_String_To_UpdateType(MessageType messageType, string value)
    {
        Message expectedResult = new() { Type = messageType };
        string jsonData = @$"{{""type"":""{value}""}}";

        Message? result = JsonConvert.DeserializeObject<Message>(jsonData);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Should_Return_Unknown_For_Incorrect_UpdateType()
    {
        string jsonData = @$"{{""type"":""{int.MaxValue}""}}";

        Message? result = JsonConvert.DeserializeObject<Message>(jsonData);

        Assert.NotNull(result);
        Assert.Equal(MessageType.Unknown, result.Type);
    }

    [Fact]
    public void Should_Throw_NotSupportedException_For_Incorrect_MessageType()
    {
        Message message = new() { Type = (MessageType)int.MaxValue };

        Assert.Throws<NotSupportedException>(() => JsonConvert.SerializeObject(message));
    }

    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    class Message
    {
        [JsonProperty(Required = Required.Always)]
        public MessageType Type { get; init; }
    }
}

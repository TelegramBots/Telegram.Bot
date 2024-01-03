using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class MessageTypeConverterTests
{
    [Fact]
    public void Should_Verify_All_MessageType_Members()
    {
        List<string> messageTypeMembers = Enum
            .GetNames<MessageType>()
            .OrderBy(x => x)
            .ToList();
        List<string> messageTypeDataMembers = new MessageTypeData()
            .Select(x => Enum.GetName(typeof(MessageType), x[0]))
            .OrderBy(x => x)
            .ToList()!;

        Assert.Equal(messageTypeMembers.Count, messageTypeDataMembers.Count);
        Assert.Equal(messageTypeMembers, messageTypeDataMembers);
    }

    [Theory]
    [ClassData(typeof(MessageTypeData))]
    public void Should_Convert_UpdateType_To_String(MessageType messageType, string value)
    {
        Message message = new(messageType);
        string expectedResult =
            $$"""
            {"type":"{{value}}"}
            """;

        string result = JsonConvert.SerializeObject(message);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [ClassData(typeof(MessageTypeData))]
    public void Should_Convert_String_To_UpdateType(MessageType messageType, string value)
    {
        Message expectedResult = new(messageType);
        string jsonData =
            $$"""
            {"type":"{{value}}"}
            """;

        Message? result = JsonConvert.DeserializeObject<Message>(jsonData);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Should_Return_Unknown_For_Incorrect_UpdateType()
    {
        string jsonData =
            $$"""
            {"type":"{{int.MaxValue}}"}
            """;

        Message? result = JsonConvert.DeserializeObject<Message>(jsonData);

        Assert.NotNull(result);
        Assert.Equal(MessageType.Unknown, result.Type);
    }

    [Fact]
    public void Should_Throw_NotSupportedException_For_Incorrect_MessageType()
    {
        Message message = new((MessageType)int.MaxValue );

        Assert.Throws<NotSupportedException>(() => JsonConvert.SerializeObject(message));
    }

    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    record Message([property: JsonProperty(Required = Required.Always)] MessageType Type);

    private class MessageTypeData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { MessageType.Unknown, "unknown" };
            yield return new object[] { MessageType.Text, "text" };
            yield return new object[] { MessageType.Animation, "animation" };
            yield return new object[] { MessageType.Audio, "audio" };
            yield return new object[] { MessageType.Document, "document" };
            yield return new object[] { MessageType.Photo, "photo" };
            yield return new object[] { MessageType.Sticker, "sticker" };
            yield return new object[] { MessageType.Story, "story" };
            yield return new object[] { MessageType.Video, "video" };
            yield return new object[] { MessageType.VideoNote, "video_note" };
            yield return new object[] { MessageType.Voice, "voice" };
            yield return new object[] { MessageType.Contact, "contact" };
            yield return new object[] { MessageType.Dice, "dice" };
            yield return new object[] { MessageType.Game, "game" };
            yield return new object[] { MessageType.Poll, "poll" };
            yield return new object[] { MessageType.Venue, "venue" };
            yield return new object[] { MessageType.Location, "location" };
            yield return new object[] { MessageType.NewChatMembers, "new_chat_members" };
            yield return new object[] { MessageType.LeftChatMember, "left_chat_member" };
            yield return new object[] { MessageType.NewChatTitle, "new_chat_title" };
            yield return new object[] { MessageType.NewChatPhoto, "new_chat_photo" };
            yield return new object[] { MessageType.DeleteChatPhoto, "delete_chat_photo" };
            yield return new object[] { MessageType.GroupChatCreated, "group_chat_created" };
            yield return new object[] { MessageType.SupergroupChatCreated, "supergroup_chat_created" };
            yield return new object[] { MessageType.ChannelChatCreated, "channel_chat_created" };
            yield return new object[] { MessageType.MessageAutoDeleteTimerChanged, "message_auto_delete_timer_changed" };
            yield return new object[] { MessageType.MigrateToChatId, "migrate_to_chat_id" };
            yield return new object[] { MessageType.MigrateFromChatId, "migrate_from_chat_id" };
            yield return new object[] { MessageType.PinnedMessage, "pinned_message" };
            yield return new object[] { MessageType.Invoice, "invoice" };
            yield return new object[] { MessageType.SuccessfulPayment, "successful_payment" };
            yield return new object[] { MessageType.UsersShared, "users_shared" };
            yield return new object[] { MessageType.ChatShared, "chat_shared" };
            yield return new object[] { MessageType.ConnectedWebsite, "connected_website" };
            yield return new object[] { MessageType.WriteAccessAllowed, "write_access_allowed" };
            yield return new object[] { MessageType.PassportData, "passport_data" };
            yield return new object[] { MessageType.ProximityAlertTriggered, "proximity_alert_triggered" };
            yield return new object[] { MessageType.ForumTopicCreated, "forum_topic_created" };
            yield return new object[] { MessageType.ForumTopicEdited, "forum_topic_edited" };
            yield return new object[] { MessageType.ForumTopicClosed, "forum_topic_closed" };
            yield return new object[] { MessageType.ForumTopicReopened, "forum_topic_reopened" };
            yield return new object[] { MessageType.GeneralForumTopicHidden, "general_forum_topic_hidden" };
            yield return new object[] { MessageType.GeneralForumTopicUnhidden, "general_forum_topic_unhidden" };
            yield return new object[] { MessageType.GiveawayCreated, "giveaway_created" };
            yield return new object[] { MessageType.Giveaway, "giveaway" };
            yield return new object[] { MessageType.GiveawayWinners, "giveaway_winners" };
            yield return new object[] { MessageType.GiveawayCompleted, "giveaway_completed" };
            yield return new object[] { MessageType.VideoChatScheduled, "video_chat_scheduled" };
            yield return new object[] { MessageType.VideoChatStarted, "video_chat_started" };
            yield return new object[] { MessageType.VideoChatEnded, "video_chat_ended" };
            yield return new object[] { MessageType.VideoChatParticipantsInvited, "video_chat_participants_invited" };
            yield return new object[] { MessageType.WebAppData, "web_app_data" };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

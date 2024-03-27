using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types.Enums;
using Xunit;
using JsonSerializerOptionsProvider = Telegram.Bot.Serialization.JsonSerializerOptionsProvider;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class MessageTypeConverterTests
{
    [Fact]
    public void Should_Verify_All_MessageType_Members()
    {
        List<string> messageTypeMembers = Enum
            .GetNames(typeof(MessageType))
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

        string result = JsonSerializer.Serialize(message, JsonSerializerOptionsProvider.Options);

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

        Message? result = JsonSerializer.Deserialize<Message>(jsonData, JsonSerializerOptionsProvider.Options);

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

        Message? result = JsonSerializer.Deserialize<Message>(jsonData, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(result);
        Assert.Equal(MessageType.Unknown, result.Type);
    }

    [Fact]
    public void Should_Throw_JsonException_For_Incorrect_MessageType()
    {
        Message message = new((MessageType)int.MaxValue );

        Assert.Throws<JsonException>(() => JsonSerializer.Serialize(message, JsonSerializerOptionsProvider.Options));
    }


    record Message([property: JsonRequired] MessageType Type);

    private class MessageTypeData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [MessageType.Unknown, "unknown"];
            yield return [MessageType.Text, "text"];
            yield return [MessageType.Animation, "animation"];
            yield return [MessageType.Audio, "audio"];
            yield return [MessageType.BoostAdded, "boost_added"];
            yield return [MessageType.Document, "document"];
            yield return [MessageType.Photo, "photo"];
            yield return [MessageType.Sticker, "sticker"];
            yield return [MessageType.Story, "story"];
            yield return [MessageType.Video, "video"];
            yield return [MessageType.VideoNote, "video_note"];
            yield return [MessageType.Voice, "voice"];
            yield return [MessageType.Contact, "contact"];
            yield return [MessageType.Dice, "dice"];
            yield return [MessageType.Game, "game"];
            yield return [MessageType.Poll, "poll"];
            yield return [MessageType.Venue, "venue"];
            yield return [MessageType.Location, "location"];
            yield return [MessageType.NewChatMembers, "new_chat_members"];
            yield return [MessageType.LeftChatMember, "left_chat_member"];
            yield return [MessageType.NewChatTitle, "new_chat_title"];
            yield return [MessageType.NewChatPhoto, "new_chat_photo"];
            yield return [MessageType.DeleteChatPhoto, "delete_chat_photo"];
            yield return [MessageType.GroupChatCreated, "group_chat_created"];
            yield return [MessageType.SupergroupChatCreated, "supergroup_chat_created"];
            yield return [MessageType.ChannelChatCreated, "channel_chat_created"];
            yield return [MessageType.MessageAutoDeleteTimerChanged, "message_auto_delete_timer_changed"];
            yield return [MessageType.MigrateToChatId, "migrate_to_chat_id"];
            yield return [MessageType.MigrateFromChatId, "migrate_from_chat_id"];
            yield return [MessageType.PinnedMessage, "pinned_message"];
            yield return [MessageType.Invoice, "invoice"];
            yield return [MessageType.SuccessfulPayment, "successful_payment"];
            yield return [MessageType.UserShared, "user_shared"];
            yield return [MessageType.UsersShared, "users_shared"];
            yield return [MessageType.ChatShared, "chat_shared"];
            yield return [MessageType.ConnectedWebsite, "connected_website"];
            yield return [MessageType.WriteAccessAllowed, "write_access_allowed"];
            yield return [MessageType.PassportData, "passport_data"];
            yield return [MessageType.ProximityAlertTriggered, "proximity_alert_triggered"];
            yield return [MessageType.ForumTopicCreated, "forum_topic_created"];
            yield return [MessageType.ForumTopicEdited, "forum_topic_edited"];
            yield return [MessageType.ForumTopicClosed, "forum_topic_closed"];
            yield return [MessageType.ForumTopicReopened, "forum_topic_reopened"];
            yield return [MessageType.GeneralForumTopicHidden, "general_forum_topic_hidden"];
            yield return [MessageType.GeneralForumTopicUnhidden, "general_forum_topic_unhidden"];
            yield return [MessageType.GiveawayCreated, "giveaway_created"];
            yield return [MessageType.Giveaway, "giveaway"];
            yield return [MessageType.GiveawayWinners, "giveaway_winners"];
            yield return [MessageType.GiveawayCompleted, "giveaway_completed"];
            yield return [MessageType.VideoChatScheduled, "video_chat_scheduled"];
            yield return [MessageType.VideoChatStarted, "video_chat_started"];
            yield return [MessageType.VideoChatEnded, "video_chat_ended"];
            yield return [MessageType.VideoChatParticipantsInvited, "video_chat_participants_invited"];
            yield return [MessageType.WebAppData, "web_app_data"];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

using System.Collections;
using System.Collections.Generic;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class ChatMemberStatusConverterTests
{
    [Theory]
    [ClassData(typeof(ChatMemberData))]
    public void Should_Convert_ChatMemberStatus_To_String(ChatMember chatMember, string value)
    {
        string result = JsonSerializer.Serialize(chatMember, TelegramBotClientJsonSerializerContext.Instance.ChatMember);

        Assert.Equal(value, result);
    }

    [Theory]
    [ClassData(typeof(ChatMemberData))]
    public void Should_Convert_String_To_ChatMemberStatus(ChatMember expectedResult, string value)
    {
        ChatMember? result = JsonSerializer.Deserialize(value, TelegramBotClientJsonSerializerContext.Instance.ChatMember);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.Status, result.Status);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_ChatMemberStatus()
    {
        ChatMemberStatus? result =
            JsonSerializer.Deserialize(int.MaxValue, TelegramBotClientJsonSerializerContext.Instance.ChatMemberStatus);

        Assert.NotNull(result);
        Assert.Equal((ChatMemberStatus)0, result);
    }

    [Fact]
    public void Should_Throw_JsonException_For_Incorrect_ChatMemberStatus()
    {
        Assert.Throws<JsonException>(() =>
            JsonSerializer.Serialize((ChatMemberStatus)int.MaxValue, TelegramBotClientJsonSerializerContext.Instance.ChatMemberStatus));
    }

    private class ChatMemberData : IEnumerable<object[]>
    {
        private static ChatMember NewChatMember(ChatMemberStatus chatMemberStatus)
        {
            return chatMemberStatus switch
            {
                ChatMemberStatus.Creator =>
                    new ChatMemberOwner
                    {
                        IsAnonymous = false,
                        User = NewUser()
                    },
                ChatMemberStatus.Administrator =>
                    new ChatMemberAdministrator
                    {
                        CanBeEdited = false,
                        CanChangeInfo = false,
                        CanDeleteMessages = false,
                        CanInviteUsers = false,
                        CanManageChat = false,
                        CanManageVideoChats = false,
                        CanRestrictMembers = false,
                        CanPromoteMembers = false,
                        User = NewUser()
                    },
                ChatMemberStatus.Member => new ChatMemberMember { User = NewUser() },
                ChatMemberStatus.Left => new ChatMemberLeft { User = NewUser() },
                ChatMemberStatus.Kicked => new ChatMemberBanned { User = NewUser() },
                ChatMemberStatus.Restricted =>
                    new ChatMemberRestricted
                    {
                        IsMember = false,
                        CanChangeInfo = false,
                        CanInviteUsers = false,
                        CanPinMessages = false,
                        CanSendMessages = false,
                        CanSendAudios = false,
                        CanSendDocuments = false,
                        CanSendPhotos = false,
                        CanSendVideos = false,
                        CanSendVideoNotes = false,
                        CanSendVoiceNotes = false,
                        CanSendPolls = false,
                        CanSendOtherMessages = false,
                        CanAddWebPagePreviews = false,
                        User = NewUser()
                    },
                _ => throw new ArgumentOutOfRangeException(nameof(chatMemberStatus), chatMemberStatus, null)
            };
        }

        private static User NewUser()
        {
            return new User
            {
                Id = 1,
                IsBot = false,
                FirstName = "FirstName",
            };
        }
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [NewChatMember(ChatMemberStatus.Creator), """{"status":"creator","is_anonymous":false,"user":{"id":1,"is_bot":false,"first_name":"FirstName"}}"""];
            yield return [NewChatMember(ChatMemberStatus.Administrator), """{"status":"administrator","can_be_edited":false,"is_anonymous":false,"can_manage_chat":false,"can_delete_messages":false,"can_manage_video_chats":false,"can_restrict_members":false,"can_promote_members":false,"can_change_info":false,"can_invite_users":false,"user":{"id":1,"is_bot":false,"first_name":"FirstName"}}"""];
            yield return [NewChatMember(ChatMemberStatus.Member), """{"status":"member","user":{"id":1,"is_bot":false,"first_name":"FirstName"}}"""];
            yield return [NewChatMember(ChatMemberStatus.Left), """{"status":"left","user":{"id":1,"is_bot":false,"first_name":"FirstName"}}"""];
            yield return [NewChatMember(ChatMemberStatus.Kicked), """{"status":"kicked","user":{"id":1,"is_bot":false,"first_name":"FirstName"}}"""];
            yield return [NewChatMember(ChatMemberStatus.Restricted), """{"status":"restricted","is_member":false,"can_change_info":false,"can_invite_users":false,"can_pin_messages":false,"can_send_messages":false,"can_send_audios":false,"can_send_documents":false,"can_send_photos":false,"can_send_videos":false,"can_send_video_notes":false,"can_send_voice_notes":false,"can_send_polls":false,"can_send_other_messages":false,"can_add_web_page_previews":false,"user":{"id":1,"is_bot":false,"first_name":"FirstName"}}"""];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

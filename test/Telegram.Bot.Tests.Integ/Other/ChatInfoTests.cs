using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Tests.Integ.Framework.Fixtures;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Other
{
    [Collection(Constants.TestCollections.ChatInfo)]
    [TestCaseOrderer(Constants.TestCaseOrderer2, Constants.AssemblyName)]
    public class ChatInfoTests : IClassFixture<ChatInfoTests.Fixture>
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly Fixture _classFixture;

        private readonly TestsFixture _fixture;

        public ChatInfoTests(TestsFixture fixture, Fixture classFixture)
        {
            _fixture = fixture;
            _classFixture = classFixture;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldGetPrivateChat)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetChat)]
        public async Task Should_Get_Private_Chat()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldGetPrivateChat);

            Chat privateChat = _classFixture.PrivateChat;

            Chat chat = await BotClient.GetChatAsync(
                chatId: privateChat.Id
            );

            Assert.Equal(ChatType.Private, chat.Type);
            Assert.Equal(privateChat.Id, chat.Id);
            Assert.Equal(privateChat.Username, chat.Username);
            Assert.Equal(privateChat.FirstName, chat.FirstName);
            Assert.Equal(privateChat.LastName, chat.LastName);
            Assert.Null(chat.Title);
            Assert.False(chat.AllMembersAreAdministrators);
            Assert.Null(chat.Description);
            Assert.Null(chat.InviteLink);
            Assert.Null(chat.PinnedMessage);
            Assert.Null(chat.StickerSetName);
            Assert.Null(chat.CanSetStickerSet);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldGetSupergroupChat)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetChat)]
        public async Task Should_Get_Supergroup_Chat()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldGetSupergroupChat);

            Chat supergroupChat = _classFixture.SupergroupChat;

            Chat chat = await BotClient.GetChatAsync(
                chatId: supergroupChat.Id
            );

            Assert.Equal(ChatType.Supergroup, chat.Type);
            Assert.Equal(supergroupChat.Id, chat.Id);
            Assert.Equal(supergroupChat.Title, chat.Title);
            Assert.Equal(supergroupChat.Username, chat.Username);
            Assert.Equal(supergroupChat.Description, chat.Description);
            Assert.Equal(supergroupChat.InviteLink, chat.InviteLink);
            Assert.Equal(supergroupChat.PinnedMessage, chat.PinnedMessage);
            Assert.Equal(supergroupChat.StickerSetName, chat.StickerSetName);
            Assert.Equal(supergroupChat.CanSetStickerSet, chat.CanSetStickerSet);
            Assert.Null(chat.FirstName);
            Assert.Null(chat.LastName);
            Assert.False(chat.AllMembersAreAdministrators);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldGetSupergroupChatAdmins)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetChatAdministrators)]
        public async Task Should_Get_Supergroup_Chat_Admins()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldGetSupergroupChatAdmins);

            ChatMember[] chatAdmins = await BotClient.GetChatAdministratorsAsync(
                chatId: _classFixture.SupergroupChat.Id
            );

            ChatMember memberCreator = Assert.Single(chatAdmins, _ => _.Status == ChatMemberStatus.Creator);
            ChatMember memberBot = Assert.Single(chatAdmins, _ => _.User.IsBot);

            Assert.True(2 <= chatAdmins.Length); // at least, Bot and the Creator
            Assert.Null(memberCreator.UntilDate);
            Assert.Null(memberCreator.CanBeEdited);
            Assert.Null(memberCreator.CanChangeInfo);
            Assert.Null(memberCreator.CanPostMessages);
            Assert.Null(memberCreator.CanEditMessages);
            Assert.Null(memberCreator.CanDeleteMessages);
            Assert.Null(memberCreator.CanInviteUsers);
            Assert.Null(memberCreator.CanRestrictMembers);
            Assert.Null(memberCreator.CanPinMessages);
            Assert.Null(memberCreator.CanPromoteMembers);
            Assert.Null(memberCreator.CanSendMessages);
            Assert.Null(memberCreator.CanSendMediaMessages);
            Assert.Null(memberCreator.CanSendOtherMessages);
            Assert.Null(memberCreator.CanAddWebPagePreviews);
            Assert.True(JToken.DeepEquals(
                JToken.FromObject(_fixture.BotUser),
                JToken.FromObject(memberBot.User)
            ));
        }

        private static class FactTitles
        {
            public const string ShouldGetPrivateChat = "Should get private chat info";

            public const string ShouldGetSupergroupChat = "Should get supergroup chat info";

            public const string ShouldGetSupergroupChatAdmins = "Should get supergroup chat administrators";
        }

        public class Fixture : AllChatsFixture
        {
            public Fixture(TestsFixture testsFixture)
                : base(testsFixture, Constants.TestCollections.ChatInfo)
            {
            }
        }
    }
}
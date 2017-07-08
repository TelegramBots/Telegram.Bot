using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Tests.Integ.AdminBots
{
    public class ChatAdministrationFixture
    {
        public BotClientFixture AssemblyFixture { get; }

        public int RegularMemberId { get; }

        public string RegularMemberName { get; }

        public ChatId RegularMemberPrivateChatId { get; }

        public string GroupInviteLink { get; set; }

        public ChatAdministrationFixture(BotClientFixture assemblyFixture)
        {
            AssemblyFixture = assemblyFixture;

            // ToDo: check for this USERID in appsettings first

            AssemblyFixture.SendTestCollectionNotificationAsync(
                CommonConstants.TestCollections.ChatMemberAdministration,
                "A non-admin chat member should should send `/me` command so bot can use his/her user id during tests.")
                .Wait();

            Message replyInGroup = GetRegularGroupChatMemberUserIdAsync().Result;
            RegularMemberId = int.Parse(replyInGroup.From.Id);
            RegularMemberName = '@' + replyInGroup.From.Username;

            AssemblyFixture.UpdateReceiver.DiscardNewUpdatesAsync().Wait();

            AssemblyFixture.BotClient.SendTextMessageAsync(AssemblyFixture.SuperGroupChatId,
                $"Now, {RegularMemberName} should send bot `/me` command in his/her private chat with bot.",
                ParseMode.Markdown,
                replyToMessageId: replyInGroup.MessageId).Wait();

            Message replyInPrivate = GetRegularMemberPrivateChatIdAsync(RegularMemberId).Result;
            RegularMemberPrivateChatId = replyInPrivate.Chat.Id;
        }

        private async Task<Message> GetRegularGroupChatMemberUserIdAsync()
        {
            var update = (await AssemblyFixture.UpdateReceiver.GetUpdatesAsync(u =>
                    u.Message.Chat.Type == ChatType.Supergroup &&
                    u.Message.Text?.StartsWith("/me", StringComparison.OrdinalIgnoreCase) == true,
                updateTypes: UpdateType.MessageUpdate)).Single();

            // todo: Validate user is non-admin

            return update.Message;
        }

        private async Task<Message> GetRegularMemberPrivateChatIdAsync(ChatId userid)
        {
            var update = (await AssemblyFixture.UpdateReceiver.GetUpdatesAsync(u =>
                    u.Message.Chat.Type == ChatType.Private &&
                    u.Message.From.Id.ToString() == userid.ToString() &&
                    u.Message.Text?.StartsWith("/me", StringComparison.OrdinalIgnoreCase) == true,
                updateTypes: UpdateType.MessageUpdate)).Single();

            return update.Message;
        }
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Tests.Integ.AdminBots
{
    public class ChatMemberAdministrationTestFixture
    {
        public TestsFixture TestsFixture { get; }

        public int RegularMemberUserId { get; }

        public string RegularMemberUserName { get; }

        public ChatId RegularMemberPrivateChatId { get; }

        public string GroupInviteLink { get; set; }

        public ChatMemberAdministrationTestFixture(TestsFixture testsFixture)
        {
            TestsFixture = testsFixture;

            bool isUserIdNull = string.IsNullOrWhiteSpace(ConfigurationProvider.TestConfigurations.RegularMemberUserId);
            bool isUserNameNull =
                string.IsNullOrWhiteSpace(ConfigurationProvider.TestConfigurations.RegularMemberUserName);
            bool isChatIdNull = string.IsNullOrWhiteSpace(ConfigurationProvider.TestConfigurations
                .RegularMemberPrivateChatId);

            // All must have values or all must be null
            if (!(isUserIdNull == isUserNameNull && isUserNameNull == isChatIdNull))
            {
                throw new ArgumentException("All (or none) of Regular Chat Member configurations should be provided");
            }

            bool configValuesExist = !isUserIdNull;

            if (configValuesExist)
            {
                RegularMemberUserId = int.Parse(ConfigurationProvider.TestConfigurations.RegularMemberUserId);
                RegularMemberUserName = ConfigurationProvider.TestConfigurations.RegularMemberUserName;
                RegularMemberPrivateChatId = ConfigurationProvider.TestConfigurations.RegularMemberPrivateChatId;

                TestsFixture.SendTestCollectionNotificationAsync(
                    Constants.TestCollections.ChatMemberAdministration).Wait();
            }
            else
            {
                TestsFixture.SendTestCollectionNotificationAsync(
                        Constants.TestCollections.ChatMemberAdministration,
                        "A non-admin chat member should send /me command so bot can use his/her user id during tests")
                    .Wait();

                Message replyInGroup = GetRegularGroupChatMemberUserIdAsync().Result;
                RegularMemberUserId = replyInGroup.From.Id;
                RegularMemberUserName = replyInGroup.From.Username;

                TestsFixture.UpdateReceiver.DiscardNewUpdatesAsync().Wait();

                TestsFixture.BotClient.SendTextMessageAsync(TestsFixture.SuperGroupChatId,
                    $"Now, @{RegularMemberUserName} should send bot /me command in his/her private chat with bot",
                    ParseMode.Markdown,
                    replyToMessageId: replyInGroup.MessageId).Wait();

                Message replyInPrivate = GetRegularMemberPrivateChatIdAsync(RegularMemberUserId).Result;
                RegularMemberPrivateChatId = replyInPrivate.Chat.Id;
            }
        }

        private async Task<Message> GetRegularGroupChatMemberUserIdAsync()
        {
            Update update = (await TestsFixture.UpdateReceiver.GetUpdatesAsync(u =>
                    u.Message.Chat.Type == ChatType.Supergroup &&
                    u.Message.Text?.StartsWith("/me", StringComparison.OrdinalIgnoreCase) == true,
                updateTypes: UpdateType.MessageUpdate)).Single();

            // todo: Validate user is non-admin

            return update.Message;
        }

        private async Task<Message> GetRegularMemberPrivateChatIdAsync(ChatId userid)
        {
            Update update = (await TestsFixture.UpdateReceiver.GetUpdatesAsync(u =>
                    u.Message.Chat.Type == ChatType.Private &&
                    u.Message.From.Id.ToString() == userid.ToString() &&
                    u.Message.Text?.StartsWith("/me", StringComparison.OrdinalIgnoreCase) == true,
                updateTypes: UpdateType.MessageUpdate)).Single();

            return update.Message;
        }
    }
}

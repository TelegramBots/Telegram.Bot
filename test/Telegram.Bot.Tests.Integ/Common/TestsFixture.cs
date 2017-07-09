using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.AdminBots;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Tests.Integ.Common
{
    public class TestsFixture : IDisposable
    {
        public ITelegramBotClient BotClient { get; }

        public UpdateReceiver UpdateReceiver { get; }

        public string[] AllowedUserNames { get; }

        public ChatId TesterPrivateChatId { get; }

        public ChatId SuperGroupChatId { get; }

        public string PaymentProviderToken { get; set; }

        public TestsFixture()
        {
            string apiToken = ConfigurationProvider.TestConfigurations.ApiToken;
            BotClient = new TelegramBotClient(apiToken);

            BotClient.DeleteWebhookAsync().Wait();

            AllowedUserNames = ConfigurationProvider.TestConfigurations.AllowedUserNamesArray;
            UpdateReceiver = new UpdateReceiver(BotClient, AllowedUserNames);

            PaymentProviderToken = ConfigurationProvider.TestConfigurations.PaymentProviderToken;

            string superGroupChatId = ConfigurationProvider.TestConfigurations.SuperGroupChatId;
            string privateChatId = ConfigurationProvider.TestConfigurations.PrivateChatId;

            #region Validations

            if (string.IsNullOrWhiteSpace(superGroupChatId))
            {
                UpdateReceiver.DiscardNewUpdatesAsync().Wait();
                SuperGroupChatId = GetChatIdFromTesterAsync(ChatType.Supergroup).Result;
            }
            else
            {
                SuperGroupChatId = superGroupChatId;
            }

            // todo test this is payments fixture
            //if (string.IsNullOrWhiteSpace(privateChatId))
            //{
            //    UpdateReceiver.DiscardNewUpdatesAsync().Wait();
            //    TesterPrivateChatId = GetChatIdFromTesterAsync(ChatType.Private).Result;
            //}
            //else
            //{
            //    TesterPrivateChatId = privateChatId;
            //}

            #endregion

            var source = new CancellationTokenSource(TimeSpan.FromSeconds(6));
            BotClient.SendTextMessageAsync(SuperGroupChatId,
                "```\nTest execution is starting...\n```",
                ParseMode.Markdown,
                cancellationToken: source.Token)
                .Wait(source.Token);
        }

        public async Task<Message> SendTestCaseNotificationAsync(string testcase, string instructions = null,
            ChatType chatType = ChatType.Supergroup)
        {
            Message msg = await SendNotificationToChatAsync(false, testcase, instructions, chatType);
            return msg;
        }

        public async Task<Message> SendTestCollectionNotificationAsync(string collectionName,
            string instructions = null, ChatType chatType = ChatType.Supergroup)
        {
            Message msg = await SendNotificationToChatAsync(true, collectionName, instructions, chatType);
            return msg;
        }

        private Task<Message> SendNotificationToChatAsync(bool isForCollection, string name,
            string instructions = null, ChatType chatType = ChatType.Supergroup)
        {
            string textFormat;
            if (isForCollection)
            {
                textFormat = Constants.StartCollectionMessageFormat;
            }
            else
            {
                textFormat = Constants.StartTestCaseMessageFormat;
            }

            string text = string.Format(textFormat, name);

            if (instructions != null)
            {
                text += "\n\n" + string.Format(Constants.InstructionsMessageFormat, instructions);
            }

            ChatId chatid;

            if (chatType == ChatType.Supergroup)
            {
                chatid = SuperGroupChatId;
            }
            else
            {
                chatid = TesterPrivateChatId;
            }

            var task = BotClient.SendTextMessageAsync(chatid, text, ParseMode.Markdown);
            return task;
        }

        private async Task<ChatId> GetChatIdFromTesterAsync(ChatType chatType)
        {
            var update = (await UpdateReceiver.GetUpdatesAsync(u =>
                u.Message.Text?.StartsWith("/test", StringComparison.OrdinalIgnoreCase) == true &&
                u.Message.Chat.Type == chatType,
                updateTypes: UpdateType.MessageUpdate)).Single();

            await UpdateReceiver.DiscardNewUpdatesAsync();

            ChatId chatid = update.Message.Chat.Id;
            return chatid;
        }

        public void Dispose()
        {
            var source = new CancellationTokenSource(TimeSpan.FromSeconds(6));
            BotClient.SendTextMessageAsync(SuperGroupChatId,
                "```\nTest execution is finished.\n```",
                ParseMode.Markdown,
                cancellationToken: source.Token)
                .Wait(source.Token);
        }

        private static class Constants
        {
            public const string StartCollectionMessageFormat = "💬 Test Collection:\n*{0}*";

            public const string StartTestCaseMessageFormat = "🔹 Test Case:\n*{0}*";

            public const string InstructionsMessageFormat = "👉 _Instructions_: 👈\n{0}";
        }
    }
}

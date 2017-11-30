using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Tests.Integ.Common
{
    public class TestsFixture : IDisposable
    {
        public ITelegramBotClient BotClient { get; }

        public User BotUser { get; }

        public UpdateReceiver UpdateReceiver { get; }

        public string[] AllowedUserNames { get; }

        public ChatId SuperGroupChatId { get; }

        public TestsFixture()
        {
            string apiToken = ConfigurationProvider.TestConfigurations.ApiToken;
            BotClient = new TelegramBotClient(apiToken);

            BotUser = BotClient.GetMeAsync().GetAwaiter().GetResult();

            BotClient.DeleteWebhookAsync().GetAwaiter().GetResult();

            AllowedUserNames = ConfigurationProvider.TestConfigurations.AllowedUserNamesArray;
            UpdateReceiver = new UpdateReceiver(BotClient, AllowedUserNames);

            string superGroupChatId = ConfigurationProvider.TestConfigurations.SuperGroupChatId;

            #region Validations

            if (string.IsNullOrWhiteSpace(superGroupChatId))
            {
                UpdateReceiver.DiscardNewUpdatesAsync().GetAwaiter().GetResult();
                SuperGroupChatId = GetChatIdFromTesterAsync(ChatType.Supergroup).GetAwaiter().GetResult();
            }
            else
            {
                SuperGroupChatId = superGroupChatId;
            }

            #endregion

            var source = new CancellationTokenSource(TimeSpan.FromSeconds(20));
            BotClient.SendTextMessageAsync(
                SuperGroupChatId,
                "```\nTest execution is starting...\n```",
                ParseMode.Markdown,
                cancellationToken: source.Token
            ).GetAwaiter().GetResult();
        }

        public async Task<Message> SendTestCaseNotificationAsync(string testcase, string instructions = null,
            ChatId chatid = null)
        {
            Message msg = await SendNotificationToChatAsync(false, testcase, instructions, chatid);
            return msg;
        }

        public async Task<Message> SendTestCollectionNotificationAsync(string collectionName,
            string instructions = null, ChatId chatid = null)
        {
            Message msg = await SendNotificationToChatAsync(true, collectionName, instructions, chatid);
            return msg;
        }

        public async Task<ChatId> GetChatIdFromTesterAsync(ChatType chatType)
        {
            var update = (await UpdateReceiver.GetUpdatesAsync(u =>
                    u.Message.Text?.StartsWith("/test", StringComparison.OrdinalIgnoreCase) == true &&
                    u.Message.Chat.Type == chatType,
                updateTypes: UpdateType.MessageUpdate)).Single();

            await UpdateReceiver.DiscardNewUpdatesAsync();

            ChatId chatid = update.Message.Chat.Id;
            return chatid;
        }

        private Task<Message> SendNotificationToChatAsync(bool isForCollection, string name,
            string instructions = null, ChatId chatid = null)
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

            if (chatid is null)
            {
                chatid = SuperGroupChatId;
            }

            var task = BotClient.SendTextMessageAsync(chatid, text, ParseMode.Markdown);
            return task;
        }

        public void Dispose()
        {
            var source = new CancellationTokenSource(TimeSpan.FromSeconds(15));
            UpdateReceiver.DiscardNewUpdatesAsync(source.Token).GetAwaiter().GetResult();

            source = new CancellationTokenSource(TimeSpan.FromSeconds(20));
            BotClient.SendTextMessageAsync(
                SuperGroupChatId,
                "```\nTest execution is finished.\n```",
                ParseMode.Markdown,
                cancellationToken: source.Token
            ).GetAwaiter().GetResult();
        }

        private static class Constants
        {
            public const string StartCollectionMessageFormat = "💬 Test Collection:\n*{0}*";

            public const string StartTestCaseMessageFormat = "🔹 Test Case:\n*{0}*";

            public const string InstructionsMessageFormat = "👉 _Instructions_: 👈\n{0}";
        }
    }
}

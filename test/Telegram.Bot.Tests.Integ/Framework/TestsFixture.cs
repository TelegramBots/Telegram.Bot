using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit.Sdk;

namespace Telegram.Bot.Tests.Integ.Framework
{
    public class TestsFixture : IDisposable
    {
        public ITelegramBotClient BotClient { get; }

        public User BotUser { get; }

        public UpdateReceiver UpdateReceiver { get; }

        public string[] AllowedUserNames { get; }

        public Chat SupergroupChat { get; }

        public Chat PrivateChat { get; set; }

        public Chat ChannelChat { get; set; }

        public RunSummary RunSummary { get; } = new RunSummary();

        private CancellationToken CancellationToken =>
            new CancellationTokenSource(TimeSpan.FromSeconds(45)).Token;

        public TestsFixture()
        {
            string apiToken = ConfigurationProvider.TestConfigurations.ApiToken;
            BotClient = new TelegramBotClient(apiToken);

            BotUser = BotClient.GetMeAsync().GetAwaiter().GetResult();

            BotClient.DeleteWebhookAsync().GetAwaiter().GetResult();

            AllowedUserNames = ConfigurationProvider.TestConfigurations.AllowedUserNamesArray;
            UpdateReceiver = new UpdateReceiver(BotClient, AllowedUserNames);

            string supergroupChatId = ConfigurationProvider.TestConfigurations.SuperGroupChatId;

            #region Validations

            if (string.IsNullOrWhiteSpace(supergroupChatId))
            {
                UpdateReceiver.DiscardNewUpdatesAsync().GetAwaiter().GetResult();
                SupergroupChat = GetChatFromTesterAsync(ChatType.Supergroup).GetAwaiter().GetResult();
            }
            else
            {
                SupergroupChat = BotClient.GetChatAsync(supergroupChatId)
                    .GetAwaiter().GetResult();
            }

            #endregion

            BotClient.SendTextMessageAsync(
                SupergroupChat.Id,
                "```\nTest execution is starting...\n```",
                ParseMode.Markdown,
                cancellationToken: CancellationToken
            ).GetAwaiter().GetResult();
        }

        public void Dispose()
        {
            UpdateReceiver.DiscardNewUpdatesAsync(CancellationToken).GetAwaiter().GetResult();

            int passed = RunSummary.Total - RunSummary.Skipped - RunSummary.Skipped;

            BotClient.SendTextMessageAsync(
                SupergroupChat.Id,
                string.Format(
                    Constants.TestExecutionResultMessageFormat,
                    RunSummary.Total,
                    passed,
                    RunSummary.Skipped,
                    RunSummary.Failed
                ),
                ParseMode.Markdown,
                cancellationToken: CancellationToken
            ).GetAwaiter().GetResult();
        }

        public async Task<Message> SendTestCaseNotificationAsync(string testcase,
            string instructions = default,
            ChatId chatid = default,
            bool startInlineQuery = default)
        {
            Message msg = await SendNotificationToChatAsync(false, testcase, instructions, chatid, startInlineQuery);
            return msg;
        }

        public async Task<Message> SendTestCollectionNotificationAsync(string collectionName,
            string instructions = null, ChatId chatid = null)
        {
            Message msg = await SendNotificationToChatAsync(true, collectionName, instructions, chatid);
            return msg;
        }

        public async Task<Chat> GetChatFromTesterAsync(ChatType chatType)
        {
            bool IsMatch(Update u) => (
                    u.Message.Chat.Type == chatType &&
                    u.Message.Text?.StartsWith("/test", StringComparison.OrdinalIgnoreCase) == true
                ) || (
                    ChatType.Channel == chatType &&
                    ChatType.Channel == u.Message.ForwardFromChat?.Type
                );

            var update = await UpdateReceiver
                .GetUpdatesAsync(IsMatch, updateTypes: UpdateType.Message)
                .ContinueWith(t => t.Result.Single());

            await UpdateReceiver.DiscardNewUpdatesAsync();

            return chatType == ChatType.Channel
                ? update.Message.ForwardFromChat
                : update.Message.Chat;
        }

        private Task<Message> SendNotificationToChatAsync(bool isForCollection, string name,
            string instructions = default, ChatId chatid = default, bool switchInlineQuery = default)
        {
            var textFormat = isForCollection
                ? Constants.StartCollectionMessageFormat
                : Constants.StartTestCaseMessageFormat;

            string text = string.Format(textFormat, name);

            chatid = chatid ?? SupergroupChat.Id;
            if (instructions != default)
            {
                text += "\n\n" + string.Format(Constants.InstructionsMessageFormat, instructions);
            }

            IReplyMarkup replyMarkup = switchInlineQuery
                ? new InlineKeyboardMarkup(new[] {
                    InlineKeyboardButton.WithSwitchInlineQueryCurrentChat("Start inline query")
                })
                : default;

            var task = BotClient.SendTextMessageAsync(chatid, text, ParseMode.Markdown,
                replyMarkup: replyMarkup,
                cancellationToken: CancellationToken);
            return task;
        }

        private static class Constants
        {
            public const string StartCollectionMessageFormat = "💬 Test Collection:\n*{0}*";

            public const string StartTestCaseMessageFormat = "🔹 Test Case:\n*{0}*";

            public const string InstructionsMessageFormat = "👉 _Instructions_: 👈\n{0}";

            public const string TestExecutionResultMessageFormat =
                "```\nTest execution is finished.\n```" +
                "Total: {0} tests\n" +
                "✅ `{1} passed`\n" +
                "⚠ `{2} skipped`\n" +
                "❎ `{3} failed`";
        }
    }
}

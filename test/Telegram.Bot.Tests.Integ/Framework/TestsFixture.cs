using System;
using System.Collections.Generic;
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
        public ITelegramBotClient BotClient { get; private set; }

        public User BotUser { get; private set; }

        public UpdateReceiver UpdateReceiver { get; private set; }

        public Chat SupergroupChat { get; private set; }

        public Chat PrivateChat { get; set; }

        public Chat ChannelChat { get; set; }

        public RunSummary RunSummary { get; } = new RunSummary();

        private CancellationToken CancellationToken =>
            new CancellationTokenSource(TimeSpan.FromSeconds(45)).Token;

        public TestsFixture()
        {
            InitAsync().GetAwaiter().GetResult();
        }

        public void Dispose()
        {
            UpdateReceiver.DiscardNewUpdatesAsync(CancellationToken).GetAwaiter().GetResult();

            int passed = RunSummary.Total - RunSummary.Skipped - RunSummary.Failed;

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

        public async Task<Chat> GetChatFromTesterAsync(ChatType chatType, CancellationToken cancellationToken = default)
        {
            bool IsMatch(Update u) =>
            (
                u.Message.Chat.Type == chatType &&
                u.Message.Text?.StartsWith("/test", StringComparison.OrdinalIgnoreCase) == true
            ) || (
                ChatType.Channel == chatType &&
                ChatType.Channel == u.Message.ForwardFromChat?.Type
            );

            var updates = await UpdateReceiver
                .GetUpdatesAsync(IsMatch, updateTypes: UpdateType.Message, cancellationToken: cancellationToken);
            var update = updates.Single();

            await UpdateReceiver.DiscardNewUpdatesAsync(cancellationToken);

            return chatType == ChatType.Channel
                ? update.Message.ForwardFromChat
                : update.Message.Chat;
        }

        public async Task<Chat> GetChatFromAdminAsync()
        {
            bool IsMatch(Update u) => (
                u.Message.Type == MessageType.Contact ||
                u.Message.ForwardFrom?.Id != null
            );

            var update = await UpdateReceiver
                .GetUpdatesAsync(IsMatch, updateTypes: UpdateType.Message)
                .ContinueWith(t => t.Result.Single());

            await UpdateReceiver.DiscardNewUpdatesAsync();

            int userId = update.Message.Type == MessageType.Contact
                ? update.Message.Contact.UserId
                : update.Message.ForwardFrom.Id;

            return await BotClient.GetChatAsync(userId);
        }

        private async Task InitAsync()
        {
            string apiToken = ConfigurationProvider.TestConfigurations.ApiToken;
            BotClient = new TelegramBotClient(apiToken);
            BotUser = await BotClient.GetMeAsync(CancellationToken);
            await BotClient.DeleteWebhookAsync(CancellationToken);

            SupergroupChat = await FindSupergroupTestChatAsync();
            var allowedUserNames = await FindAllowedTesterUserNames();
            UpdateReceiver = new UpdateReceiver(BotClient, allowedUserNames);

            await BotClient.SendTextMessageAsync(
                SupergroupChat.Id,
                "```\nTest execution is starting...\n```" +
                "Testers are: \n" + UpdateReceiver.GetTesterMentions(),
                ParseMode.Markdown,
                cancellationToken: CancellationToken
            );
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
                ? new InlineKeyboardMarkup(new[]
                {
                    InlineKeyboardButton.WithSwitchInlineQueryCurrentChat("Start inline query")
                })
                : default;

            var task = BotClient.SendTextMessageAsync(chatid, text, ParseMode.Markdown,
                replyMarkup: replyMarkup,
                cancellationToken: CancellationToken);
            return task;
        }

        private async Task<Chat> FindSupergroupTestChatAsync()
        {
            Chat supergroupChat;
            string supergroupChatId = ConfigurationProvider.TestConfigurations.SuperGroupChatId;
            if (string.IsNullOrWhiteSpace(supergroupChatId))
            {
                supergroupChat = null; // ToDo Find supergroup from a message command /test
                // await UpdateReceiver.DiscardNewUpdatesAsync(CancellationToken);
                // supergroupChat = await GetChatFromTesterAsync(ChatType.Supergroup, CancellationToken);
            }
            else
            {
                supergroupChat = await BotClient.GetChatAsync(supergroupChatId, CancellationToken);
            }

            return supergroupChat;
        }

        private async Task<IEnumerable<string>> FindAllowedTesterUserNames()
        {
            // Try to get user names from test configurations first
            string[] allowedUserNames = ConfigurationProvider.TestConfigurations.AllowedUserNames
                .Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(n => n.Trim())
                .ToArray();

            if (!allowedUserNames.Any())
            {
                // Assume all chat admins are allowed testers
                ChatMember[] admins = await BotClient.GetChatAdministratorsAsync(SupergroupChat, CancellationToken);
                allowedUserNames = admins
                    .Where(member => !member.User.IsBot)
                    .Select(member => member.User.Username)
                    .ToArray();
            }

            return allowedUserNames;
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

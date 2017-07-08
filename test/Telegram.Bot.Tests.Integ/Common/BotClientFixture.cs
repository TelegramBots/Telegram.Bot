using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Tests.Integ.Common
{
    public class BotClientFixture : IDisposable
    {
        public ITelegramBotClient BotClient { get; }

        public UpdateReceiver UpdateReceiver { get; }

        public string[] AllowedUserNames { get; }

        public ChatId PrivateChatId { get; }

        public ChatId SuperGroupChatId { get; }

        public string PaymentProviderToken { get; set; }

        public BotClientFixture()
        {
            string apiToken = ConfigurationProvider.TestConfigurations.ApiToken;
            BotClient = new TelegramBotClient(apiToken);

            BotClient.DeleteWebhookAsync().Wait();

            AllowedUserNames = ConfigurationProvider.TestConfigurations.AllowedUserNamesArray;
            UpdateReceiver = new UpdateReceiver(BotClient, AllowedUserNames);

            PaymentProviderToken = ConfigurationProvider.TestConfigurations.PaymentProviderToken;

            string superGroupChatId = ConfigurationProvider.TestConfigurations.SuperGroupChatId;
            string privateChatId = ConfigurationProvider.TestConfigurations.PrivateChatId;

            if (string.IsNullOrWhiteSpace(superGroupChatId))
            {
                UpdateReceiver.DiscardNewUpdatesAsync().Wait();
                SuperGroupChatId = GetChatIdFromTesterAsync(ChatType.Supergroup).Result;
            }
            else
            {
                SuperGroupChatId = superGroupChatId;
            }

            if (string.IsNullOrWhiteSpace(privateChatId))
            {
                UpdateReceiver.DiscardNewUpdatesAsync().Wait();
                PrivateChatId = GetChatIdFromTesterAsync(ChatType.Private).Result;
            }
            else
            {
                PrivateChatId = privateChatId;
            }

            var source = new CancellationTokenSource(TimeSpan.FromSeconds(6));
            BotClient.SendTextMessageAsync(SuperGroupChatId,
                "```\nTest execution is starting...\n```",
                ParseMode.Markdown,
                cancellationToken: source.Token)
                .Wait(source.Token);
        }

        public async Task SendTestCaseNotificationAsync(string testcase, string instructions = null,
            ChatType chatType = ChatType.Supergroup)
        {
            const string format = "Executing test case:\n*{0}*";
            const string instructionsFromat = "_Instructions_:\n{0}";

            string text = string.Format(format, testcase);
            if (!string.IsNullOrWhiteSpace(instructions))
            {
                text += string.Format("\n\n" + instructionsFromat, instructions);
            }

            ChatId chatid;

            if (chatType == ChatType.Supergroup)
            {
                chatid = SuperGroupChatId;
            }
            else
            {
                chatid = PrivateChatId;
            }

            await BotClient.SendTextMessageAsync(chatid, text, ParseMode.Markdown);
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
    }
}

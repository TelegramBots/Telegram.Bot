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

        public int UserId { get; private set; }

        public ChatId ChatId { get; private set; }

        public BotClientFixture()
        {
            string apiToken = ConfigurationProvider.TelegramBot.ApiToken;
            BotClient = new TelegramBotClient(apiToken);

            BotClient.DeleteWebhookAsync().Wait();

            UpdateReceiver = new UpdateReceiver(BotClient, ConfigurationProvider.TestAnalyst.AllowedUserNames);

            /* ToDo:
             * First check whether any config is provided for userId and chatId.
             * If not, go and wait for `/test` to be initiated is some chat
             * Also allow the config for a list of allowed UserNames, or allowed
             * UserIds to initiate the test
             */

            ChatId = ConfigurationProvider.TestAnalyst.ChatId;

            UpdateReceiver.DiscardNewUpdatesAsync().Wait();

            if (string.IsNullOrWhiteSpace(ChatId))
            {
                WaitForTestAnalystToStart().Wait();
            }

            var source = new CancellationTokenSource(TimeSpan.FromSeconds(6));
            BotClient.SendTextMessageAsync(ChatId,
                "```\nTest execution is starting...\n```",
                ParseMode.Markdown,
                cancellationToken: source.Token)
                .Wait(source.Token);
        }

        public async Task SendTestCaseNotification(string testcase, string instructions = null)
        {
            const string format = "Executing test case:\n*{0}*";
            const string instructionsFromat = "_Instructions_:\n{0}";

            string text = string.Format(format, testcase);
            if (!string.IsNullOrWhiteSpace(instructions))
            {
                text += string.Format("\n\n" + instructionsFromat, instructions);
            }

            await BotClient.SendTextMessageAsync(ChatId, text, ParseMode.Markdown);
        }

        private async Task WaitForTestAnalystToStart()
        {
            var update = (await UpdateReceiver.GetUpdatesAsync(
                u => u.Message?.Text?.Equals("/test", StringComparison.OrdinalIgnoreCase) == true,
                updateTypes: UpdateType.MessageUpdate)).Single();

            UserId = int.Parse(update.Message.From.Id);
            ChatId = update.Message.Chat.Id;

            await UpdateReceiver.DiscardNewUpdatesAsync();
        }

        public void Dispose()
        {
            var source = new CancellationTokenSource(TimeSpan.FromSeconds(6));
            BotClient.SendTextMessageAsync(ChatId,
                "```\nTest execution is finished.\n```",
                ParseMode.Markdown,
                cancellationToken: source.Token)
                .Wait(source.Token);
        }
    }
}

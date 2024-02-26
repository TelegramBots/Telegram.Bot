using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit.Abstractions;
using Xunit.Sdk;

#nullable disable

namespace Telegram.Bot.Tests.Integ.Framework;

public class TestsFixture : IDisposable
{
    readonly IMessageSink _diagnosticMessageSink;
    ConfigurationProvider _configurationProvider;

    public TestConfiguration Configuration => _configurationProvider.Configuration;

    public ITelegramBotClient BotClient { get; private set; }

    public User BotUser { get; private set; }

    public UpdateReceiver UpdateReceiver { get; private set; }

    public Chat SupergroupChat { get; private set; }

    public Chat PrivateChat { get; set; }

    public Chat ChannelChat { get; set; }

    public RunSummary RunSummary { get; } = new();

    public static TestsFixture Instance { get; private set; }

    public TestsFixture(IMessageSink diagnosticMessageSink)
    {
        _diagnosticMessageSink = diagnosticMessageSink;
        InitAsync().GetAwaiter().GetResult();
        Instance = this;
    }

    public void Dispose()
    {
        Ex.WithCancellation(async token =>
        {
            await UpdateReceiver.DiscardNewUpdatesAsync(token);
            var passed = RunSummary.Total - RunSummary.Skipped - RunSummary.Failed;

            await BotClient.SendMessageAsync(
                new()
                {
                    ChatId = SupergroupChat.Id,
                    Text = string.Format(
                        Constants.TestExecutionResultMessageFormat,
                        RunSummary.Total,
                        passed,
                        RunSummary.Skipped,
                        RunSummary.Failed
                    ),
                    ParseMode = ParseMode.Markdown,

                },
                token
            );
        }).GetAwaiter().GetResult();
    }

    public async Task<Message> SendTestInstructionsAsync(
        string instructions,
        ChatId chatId = default,
        bool startInlineQuery = false)
    {
        var text = string.Format(Constants.InstructionsMessageFormat, instructions);
        chatId ??= SupergroupChat.Id;

        IReplyMarkup replyMarkup = startInlineQuery
            ? (InlineKeyboardMarkup)InlineKeyboardButton.WithSwitchInlineQueryCurrentChat("Start inline query")
            : default;

        return await Ex.WithCancellation(async token =>
            await BotClient.SendMessageAsync(
                new()
                {
                    ChatId = chatId,
                    Text = text,
                    ParseMode = ParseMode.Markdown,
                    ReplyMarkup = replyMarkup,
                },
                token
            )
        );
    }

    public async Task<Message> SendTestCaseNotificationAsync(string testCase) =>
        await SendNotificationToChatAsync(false, testCase);

    public async Task<Message> SendTestCollectionNotificationAsync(
        string collectionName,
        string instructions = default,
        ChatId chatId = default
    ) =>
        await Ex.WithCancellation(async token =>
            await SendNotificationToChatAsync(
                isForCollection: true,
                name: collectionName,
                instructions: instructions,
                chatId: chatId,
                cancellationToken: token
            )
        );

    public async Task<Chat> GetChatFromTesterAsync(
        ChatType chatType,
        CancellationToken cancellationToken = default)
    {
        bool IsMatch(Update u) =>
        (
            u.Message?.Chat.Type == chatType &&
            u.Message.Text?.StartsWith("/test", StringComparison.OrdinalIgnoreCase) == true
        ) || (
            ChatType.Channel == chatType &&
            u.Message?.ForwardOrigin is MessageOriginChannel
        );

        var updates = await UpdateReceiver.GetUpdatesAsync(
            IsMatch,
            updateTypes: UpdateType.Message,
            cancellationToken: cancellationToken
        );

        var update = updates.Single();

        await UpdateReceiver.DiscardNewUpdatesAsync(cancellationToken);

        return chatType == ChatType.Channel
            ? ((MessageOriginChannel)update.Message?.ForwardOrigin)!.Chat
            : update.Message?.Chat;
    }

    public async Task<Chat> GetChatFromAdminAsync()
    {
        await UpdateReceiver.DiscardNewUpdatesAsync();

        var update = await UpdateReceiver.GetUpdateAsync(
            IsMatch,
            updateTypes: [UpdateType.Message, UpdateType.ChatMember]
        );

        await UpdateReceiver.DiscardNewUpdatesAsync();

        var userId = update.Message switch
        {
            { Contact.UserId: {} id } => id,
            { ForwardOrigin: MessageOriginUser originUser } => originUser.SenderUser.Id,
            { NewChatMembers: { Length: 1 } members } => members[0].Id,
            _ => throw new InvalidOperationException()
        };

        return await BotClient.GetChatAsync(new GetChatRequest { ChatId = userId! });

        static bool IsMatch(Update u) => u
            is { Message.Type: MessageType.Contact }
            or { Message.NewChatMembers.Length: > 0 }
            or { Message.ForwardOrigin: not null };
    }

    async Task InitAsync()
    {
        _configurationProvider = new();
        var apiToken = Configuration.ApiToken;

        BotClient = new RetryTelegramBotClient(
            options: new(
                retryCount: Configuration.RetryCount,
                defaultTimeout: TimeSpan.FromSeconds(Configuration.DefaultRetryTimeout),
                token: apiToken,
                useTestEnvironment: false,
                baseUrl: default
            ),
            diagnosticMessageSink: _diagnosticMessageSink
        );

        var allowedUserNames = await Ex.WithCancellation(
            async token =>
            {
                BotUser = await BotClient.GetMeAsync(new(), token);
                await BotClient.DeleteWebhookAsync(new DeleteWebhookRequest(), token);

                SupergroupChat = await FindSupergroupTestChatAsync(token);
                return await FindAllowedTesterUserNames(token);
            }
        );

        UpdateReceiver = new(BotClient, allowedUserNames);

        await Ex.WithCancellation(async token => await BotClient.SendMessageAsync(
            new()
            {
                ChatId = SupergroupChat.Id,
                Text = $"""
                       ```
                       Test execution is starting...
                       ```
                       #testers
                       These users are allowed to interact with the bot:

                       {UpdateReceiver.GetTesters()}
                       """,
                ParseMode = ParseMode.Markdown,
                DisableNotification = true,
            },
            token
        ));

#if DEBUG
        BotClient.OnMakingApiRequest += OnMakingApiRequest;
        BotClient.OnApiResponseReceived += OnApiResponseReceived;
#endif
    }

    Task<Message> SendNotificationToChatAsync(
        bool isForCollection,
        string name,
        string instructions = default,
        ChatId chatId = default,
        bool switchInlineQuery = default,
        CancellationToken cancellationToken = default)
    {
        var textFormat = isForCollection
            ? Constants.StartCollectionMessageFormat
            : Constants.StartTestCaseMessageFormat;

        var text = string.Format(textFormat, name);

        chatId ??= SupergroupChat.Id;
        if (instructions != default)
        {
            text += $"\n\n{string.Format(Constants.InstructionsMessageFormat, instructions)}";
        }

        IReplyMarkup replyMarkup = switchInlineQuery
            ? (InlineKeyboardMarkup)InlineKeyboardButton.WithSwitchInlineQueryCurrentChat("Start inline query")
            : default;

        var task = BotClient.SendMessageAsync(
            new()
            {
                ChatId = chatId,
                Text = text,
                ParseMode = ParseMode.Markdown,
                ReplyMarkup = replyMarkup,
            },
            cancellationToken
        );
        return task;
    }

    async Task<Chat> FindSupergroupTestChatAsync(CancellationToken cancellationToken = default)
    {
        var supergroupChatId = Configuration.SuperGroupChatId;
        return await BotClient.GetChatAsync(new GetChatRequest {ChatId = supergroupChatId}, cancellationToken);
    }

    async Task<IEnumerable<string>> FindAllowedTesterUserNames(CancellationToken cancellationToken = default)
    {
        // Try to get user names from test configurations first
        var allowedUserNames = Configuration.AllowedUserNames;

        if (allowedUserNames.Length != 0) return allowedUserNames;

        // Assume all chat admins are allowed testers
        var admins = await BotClient.GetChatAdministratorsAsync(
            new GetChatAdministratorsRequest {ChatId = SupergroupChat}, cancellationToken
        );
        allowedUserNames = admins
            .Where(member => !member.User.IsBot)
            .Select(member => member.User.Username)
            .ToArray();

        return allowedUserNames;
    }

#if DEBUG
    // Disable "The variable ‘x’ is assigned but its value is never used":
#pragma warning disable 219
    // ReSharper disable NotAccessedVariable
    // ReSharper disable RedundantAssignment
    async ValueTask OnMakingApiRequest(
        ITelegramBotClient botClient,
        ApiRequestEventArgs e,
        CancellationToken cancellationToken = default)
    {
        bool hasContent;
        string content;
        string[] multipartContent;
        if (e.HttpRequestMessage?.Content is null)
        {
            hasContent = false;
        }
        else if (e.HttpRequestMessage.Content is MultipartFormDataContent multipartFormDataContent)
        {
            hasContent = true;
            var stringifiedFormContent = new List<string>(multipartFormDataContent.Count());

            foreach (var formContent in multipartFormDataContent)
            {
                if (formContent is StringContent stringContent)
                {
                    var stringifiedContent = await stringContent.ReadAsStringAsync(cancellationToken);
                    stringifiedFormContent.Add(stringifiedContent);
                }
                else
                {
                    stringifiedFormContent.Add(formContent.Headers.ToString());
                }
            }

            multipartContent = [..stringifiedFormContent];
        }
        else
        {
            hasContent = true;
            content = await e.HttpRequestMessage.Content.ReadAsStringAsync(cancellationToken);
        }

        /* Debugging Hint: set breakpoints with conditions here in order to investigate the HTTP request values. */
    }

    // ReSharper disable UnusedVariable
    async ValueTask OnApiResponseReceived(
        ITelegramBotClient botClient,
        ApiResponseEventArgs e,
        CancellationToken cancellationToken = default)
    {
        var content = await e.ResponseMessage.Content.ReadAsStringAsync(cancellationToken)
            .ConfigureAwait(false);

        /* Debugging Hint: set breakpoints with conditions here in order to investigate the HTTP response received. */
    }
#pragma warning restore 219
#endif
    static class Constants
    {
        public const string StartCollectionMessageFormat = "💬 Test Collection:\n*{0}*";

        public const string StartTestCaseMessageFormat = "🔹 Test Case:\n*{0}*";

        public const string InstructionsMessageFormat = "👉 _Instructions_: 👈\n{0}";

        public const string TestExecutionResultMessageFormat =
            """
            Test execution is finished.
            Total: {0} tests
            ✅ `{1} passed`
            ⚠ `{2} skipped`
            ❎ `{3} failed`
            """;
    }
}

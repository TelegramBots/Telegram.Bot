using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

var token = Environment.GetEnvironmentVariable("TELEGRAM_BOT_TOKEN");

if (string.IsNullOrWhiteSpace(token))
{
    Console.WriteLine("Set TELEGRAM_BOT_TOKEN to your bot token before running this sample.");
    return;
}

var botClient = new TelegramBotClient(token);
var me = await botClient.GetMe();

Console.WriteLine($"NativeAOT demo connected as @{me.Username} (id: {me.Id}).");
Console.WriteLine("Send me a message and I will echo it back. Press Ctrl+C to exit.");

using var cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    cts.Cancel();
};

var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = new[] { UpdateType.Message },
    DropPendingUpdates = true,
    Limit = 5,
};

await botClient.ReceiveAsync(HandleUpdateAsync, HandleErrorAsync, receiverOptions, cts.Token);

async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken cancellationToken)
{
    if (update.Message is not { Text: { } messageText, Chat: { } chat })
    {
        return;
    }

    var sender = chat.Username ?? chat.Id.ToString();
    await bot.SendMessage(chat.Id, $"NativeAOT echo: {messageText}", cancellationToken: cancellationToken);
    Console.WriteLine($"Echoed '{messageText}' to @{sender} (chat: {chat.Id}).");
}

Task HandleErrorAsync(ITelegramBotClient bot, Exception exception, CancellationToken cancellationToken)
{
    Console.WriteLine($"Polling error: {exception.Message}");
    return Task.CompletedTask;
}

using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

var configJson = await File.ReadAllTextAsync("appsettings.json");
var config = JsonDocument.Parse(configJson);

var botToken = config.RootElement.GetProperty("BotToken").GetString()
    ?? throw new Exception("BotToken not found in config");

TelegramBotClient bot;
var proxyEnabled = config.RootElement.GetProperty("Proxy").GetProperty("Enabled").GetBoolean();

if (proxyEnabled)
{
    var proxyHost = config.RootElement.GetProperty("Proxy").GetProperty("Host").GetString()!;
    var proxyPort = config.RootElement.GetProperty("Proxy").GetProperty("Port").GetInt32();
    var proxyUsername = config.RootElement.GetProperty("Proxy").GetProperty("Username").GetString();
    var proxyPassword = config.RootElement.GetProperty("Proxy").GetProperty("Password").GetString();

    var httpClientHandler = new HttpClientHandler
    {
        Proxy = new System.Net.WebProxy($"socks5://{proxyHost}:{proxyPort}")
        {
            Credentials = string.IsNullOrEmpty(proxyUsername)
                ? null
                : new System.Net.NetworkCredential(proxyUsername, proxyPassword)
        },
        UseProxy = true
    };

    var httpClient = new HttpClient(httpClientHandler);
    using var cts = new CancellationTokenSource();
    bot = new TelegramBotClient(botToken, httpClient, cancellationToken: cts.Token);
}
else
{
    using var cts = new CancellationTokenSource();
    bot = new TelegramBotClient(botToken, cancellationToken: cts.Token);
}

var me = await bot.GetMe();
bot.OnMessage += OnMessage;

Console.WriteLine($"@{me.Username} is running... Press Enter to terminate");
Console.ReadLine();

async Task OnMessage(Message msg, UpdateType type)
{
    if (msg.Text is null) return;
    Console.WriteLine($"Received {type} '{msg.Text}' in {msg.Chat}");
    await bot.SendMessage(msg.Chat, $"{msg.From} said: {msg.Text}");
}

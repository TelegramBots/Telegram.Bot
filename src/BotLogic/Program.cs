using System.Text.Json;
using BotLogic.Database;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

var configJson = await File.ReadAllTextAsync("appsettings.json");
var config = JsonDocument.Parse(configJson);

var botToken = config.RootElement.GetProperty("BotToken").GetString()
    ?? throw new Exception("BotToken not found in config");

var dbConfig = config.RootElement.GetProperty("Database");
var connectionString = $"Host={dbConfig.GetProperty("Host").GetString()};" +
                      $"Port={dbConfig.GetProperty("Port").GetInt32()};" +
                      $"Database={dbConfig.GetProperty("Database").GetString()};" +
                      $"Username={dbConfig.GetProperty("Username").GetString()};" +
                      $"Password={dbConfig.GetProperty("Password").GetString()}";

DatabaseService? db = null;
try
{
    db = new DatabaseService(connectionString);
    Console.WriteLine("Connected to PostgreSQL database");
}
catch (Exception ex)
{
    Console.WriteLine($"Failed to connect to database: {ex.Message}");
    Console.WriteLine("Bot will continue without database logging");
}

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

db?.Dispose();

async Task OnMessage(Message msg, UpdateType type)
{
    if (msg.Text is null) return;
    Console.WriteLine($"Received {type} '{msg.Text}' in {msg.Chat}");

    if (db != null && msg.From != null)
    {
        try
        {
            await db.UpsertUserAsync(
                msg.From.Id,
                msg.From.Username,
                msg.From.FirstName,
                msg.From.LastName
            );

            await db.SaveMessageAsync(
                msg.From.Id,
                msg.MessageId,
                msg.Chat.Id,
                msg.Text,
                type.ToString()
            );

            var stats = await db.GetUserStatsAsync(msg.From.Id);
            Console.WriteLine($"User {msg.From.Id} stats: {stats.MessageCount} messages total");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error: {ex.Message}");
        }
    }

    await bot.SendMessage(msg.Chat, $"{msg.From} said: {msg.Text}");
}

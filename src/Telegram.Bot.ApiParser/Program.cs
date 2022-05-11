using Telegram.Bot.ApiParser;

TelegramBotDocParser parser = new();
await parser.LoadBotApiPageAsync();
await parser.ParseEnumsAsync();
parser.ParseBotApiPage();

var types = parser.Types;
await TelegramBotDocWriter.WriteTypesAsync(types);

var methods = parser.Methods;
await TelegramBotDocWriter.WriteMethodsAsync(methods);

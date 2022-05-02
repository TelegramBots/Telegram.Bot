using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Telegram.Bot.ApiTypesGenerator;

internal static class TelegramBotDocWriter
{
    public static async Task WriteTypesAsync(IReadOnlyCollection<BotApiType> types)
    {
        await using FileStream file = File.Create("types.json");
        await JsonSerializer.SerializeAsync(file, types);
    }
}
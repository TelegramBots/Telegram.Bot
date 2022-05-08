using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Telegram.Bot.ApiTypesGenerator;

internal static class TelegramBotDocWriter
{
    public static async Task WriteTypesAsync(IReadOnlyCollection<BotApiType> types)
    {
        string fileName = Path.Combine("..", "..", "..", "..", "Telegram.Bot", "Types", "types.json");
        await using FileStream file = File.Create(fileName);
        await JsonSerializer.SerializeAsync(file, types);
    }
}

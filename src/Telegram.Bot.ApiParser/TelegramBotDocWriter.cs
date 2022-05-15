using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Telegram.Bot.ApiParser.Models;

namespace Telegram.Bot.ApiParser;

internal static class TelegramBotDocWriter
{
    public static async Task WriteBotApiDataAsync(BotApiData data)
    {
        string fileName = Path.Combine(GetMainLibraryPath(), "data.json");
        await using FileStream file = File.Create(fileName);
        await JsonSerializer.SerializeAsync(file, data, new JsonSerializerOptions(JsonSerializerDefaults.General)
        {
            WriteIndented = true
        });
    }

    private static string GetMainLibraryPath()
    {
        var dir = new DirectoryInfo(Directory.GetCurrentDirectory());
        do
        {
            dir = dir.Parent!;
        } while (dir.Name != "src");

        return Path.Combine(dir.FullName, "Telegram.Bot");
    }
}

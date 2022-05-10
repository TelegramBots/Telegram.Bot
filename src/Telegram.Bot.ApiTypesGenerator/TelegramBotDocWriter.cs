using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Telegram.Bot.ApiTypesGenerator;

internal static class TelegramBotDocWriter
{
    public static async Task WriteTypesAsync(IReadOnlyCollection<BotApiType> types)
    {
        string fileName = Path.Combine(GetMainLibraryPath(), "Types", "types.json");
        await using FileStream file = File.Create(fileName);
        await JsonSerializer.SerializeAsync(file, types);
    }

    public static async Task WriteMethodsAsync(IReadOnlyCollection<BotApiMethod> methods)
    {
        string fileName = Path.Combine(GetMainLibraryPath(), "Requests", "methods.json");
        await using FileStream file = File.Create(fileName);
        await JsonSerializer.SerializeAsync(file, methods);
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

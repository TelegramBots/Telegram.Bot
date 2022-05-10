namespace Telegram.Bot.Generators.Models;

internal sealed class BotApiMethod
{
    public string MethodName { get; set; }
    public string MethodDescription { get; set; }
    public List<BotApiMethodParameter> Parameters { get; set; }
}

namespace Telegram.Bot.Generators;

internal sealed class BotApiType
{
    public string TypeName { get; set; }
    public string TypeDescription { get; set; }
    public List<BotApiTypeParameter> Parameters { get; set; }
}

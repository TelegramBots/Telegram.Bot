namespace Telegram.Bot.Generators.Models;

internal sealed class BotApiMethodParameter
{
    public string ParameterName { get; set; }
    public string ParameterDescription { get; set; }
    public string ParameterTypeName { get; set; }
    public bool IsEnum { get; set; }
    public bool IsRequired { get; set; }
}

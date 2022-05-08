namespace Telegram.Bot.Generators;

internal sealed record BotApiTypeParameter
{
    public string ParameterName { get; set; }
    public string ParameterDescription { get; set; }
    public string ParameterTypeName { get; set; }
    public bool IsEnum { get; set; }
}

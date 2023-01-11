namespace Telegram.Bot.Options;

public record TelegramBotClientWebhookOptions(string SecretToken)
{
    public const string SectionName = "SecretToken";
    public string SecretToken { get; } = SecretToken;
}

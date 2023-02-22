namespace Telegram.Bot.AspNet.Options;

public class TelegramBotClientWebhookOptions
{
    public const string SectionName = "SecretToken";
    public string SecretToken { get; set; }
}

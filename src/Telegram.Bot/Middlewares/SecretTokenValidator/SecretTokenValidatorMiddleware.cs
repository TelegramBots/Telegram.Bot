using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Options;

namespace Telegram.Bot.Middlewares.SecretTokenValidator;

public class SecretTokenValidatorMiddleware : IMiddleware
{
    private const string SecretTokenHeader = "X-Telegram-Bot-Api-Secret-Token";
    private readonly TelegramBotClientWebhookOptions _options;

    public SecretTokenValidatorMiddleware(IOptions<TelegramBotClientWebhookOptions> options)
    {
        _options = options.Value;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        string? secretToken = context.Request.Headers
                                     .FirstOrDefault(x => x.Key == SecretTokenHeader)
                                     .Value
                                     .ToString();

        if (string.IsNullOrWhiteSpace(secretToken))
            throw new InvalidSecretTokenException($"Header {SecretTokenHeader} not found or empty");

        if (secretToken != _options.SecretToken)
            throw new InvalidSecretTokenException("Secret token is invalid");

        await next(context);
    }
}

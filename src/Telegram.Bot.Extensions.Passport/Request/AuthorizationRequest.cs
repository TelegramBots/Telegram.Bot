using System;
using Newtonsoft.Json;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Passport.Request
{
    public struct AuthorizationRequest
    {
        public int BotId { get; }

        public string PublicKey { get; }

        public string Nonce { get; }

        public PassportScope PassportScope { get; }

        public string Query { get; }

        public string Url => "tg://resolve?" + Query;

        public string AndroidUrl => "tg:resolve?" + Query;

        public AuthorizationRequest(
            int botId,
            string publicKey,
            string nonce,
            PassportScope scope
        )
        {
            BotId = botId;
            PublicKey = publicKey ?? throw new ArgumentNullException(nameof(publicKey));
            Nonce = nonce ?? throw new ArgumentNullException(nameof(nonce));
            PassportScope = scope ?? throw new ArgumentNullException(nameof(PassportScope));

            var scopeJson = JsonConvert.SerializeObject(scope);

            Query = "domain=telegrampassport" +
                    $"&bot_id={Uri.EscapeDataString(botId + "")}" +
                    $"&scope={Uri.EscapeDataString(scopeJson)}" +
                    $"&public_key={Uri.EscapeDataString(publicKey)}" +
                    $"&nonce={Uri.EscapeDataString(nonce)}";
        }

        public override string ToString() => Url;
    }
}

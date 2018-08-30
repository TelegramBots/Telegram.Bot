using System;
using System.Linq;
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

//        /// <summary>
//        /// Requesed scopes. Available only if constructor for Telegram Passport v1.0 is used.
//        /// </summary>
//        [Obsolete("Parameters for Telegram Passport v1.0 are deprected")]
//        public string[] Scopes { get; }

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
            PublicKey = publicKey;
            Nonce = nonce;
            PassportScope = scope;
//            Scopes = null;

            var scopeJson = JsonConvert.SerializeObject(scope);

            Query = "domain=telegrampassport" +
                    $"&bot_id={Uri.EscapeDataString(botId + "")}" +
                    $"&scope={Uri.EscapeDataString(scopeJson)}" +
                    $"&public_key={Uri.EscapeDataString(publicKey)}" +
                    $"&nonce={Uri.EscapeDataString(nonce)}";
        }

//        /// <summary>
//        /// Initialize an instance of <see cref="AuthorizationRequest"/>
//        /// </summary>
//        /// <param name="botId">
//        /// Unique identifier for the bot. You can get it from bot token. For example, for the bot token
//        /// 1234567:4TT8bAc8GHUspu3ERYn-KGcvsvGB9u_n4ddy, the bot id is 1234567.
//        /// </param>
//        /// <param name="publicKey">Public key of the bot</param>
//        /// <param name="payload">
//        /// Bot-specified payload.
//        /// Important: For security purposes it should be a cryptographically secure unique identifier of the
//        /// request. In particular, it should be long enough and it should be generated using a cryptographically
//        /// secure pseudorandom number generator. You should never accept credentials with the same payload twice.
//        /// </param>
//        /// <param name="scope">Types of data you want to request. One of values in
//        /// type <see cref="PassportEnums.Scope"/>.
//        /// </param>
//        /// <exception cref="ArgumentException">If <paramref name="scope"/> is empty or has empty items</exception>
//        [Obsolete("Parameters for Telegram Passport v1.0 are deprected. Use the other constructor instead.")]
//        public AuthorizationRequest(
//            int botId,
//            string publicKey,
//            string payload,
//            params string[] scope
//        )
//        {
//            if (scope == null || scope.Length < 1 || scope.Any(string.IsNullOrWhiteSpace))
//            {
//                throw new ArgumentException(nameof(scope));
//            }
//
//            BotId = botId;
//            PublicKey = publicKey;
//            Nonce = payload;
//            Scopes = scope;
//            PassportScope = null;
//
//            var scopeJson = JsonConvert.SerializeObject(scope);
//
//            Query = "domain=telegrampassport" +
//                    $"&bot_id={Uri.EscapeDataString(botId + "")}" +
//                    $"&scope={Uri.EscapeDataString(scopeJson)}" +
//                    $"&public_key={Uri.EscapeDataString(publicKey)}" +
//                    $"&payload={Uri.EscapeDataString(payload)}";
//        }

        public override string ToString() => Url;
    }
}

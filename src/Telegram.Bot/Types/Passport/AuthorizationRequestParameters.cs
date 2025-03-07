namespace Telegram.Bot.Types.Passport;

/// <summary>Parameters for making a Telegram Passport authorization request</summary>
public class AuthorizationRequestParameters
{
    /// <summary>Unique identifier for the bot. You can get it from bot token. For example, for the bot token "1234567:4TT8bAc8GHUspu3ERYn-KGcvsvGB9u_n4ddy", the bot id is 1234567.</summary>
    public long BotId { get; }

    /// <summary>Public key of the bot</summary>
    public string PublicKey { get; }

    /// <summary>Bot-specified nonce.
    /// Important: For security purposes it should be a cryptographically secure unique identifier of the request.
    /// In particular, it should be long enough and it should be generated using a cryptographically secure
    /// pseudorandom number generator. You should never accept credentials with the same nonce twice.</summary>
    public string Nonce { get; }

    /// <summary>Description of the data you want to request</summary>
    public PassportScope PassportScope { get; }

    /// <summary>Query string part of the URI generated from the parameters</summary>
    public string Query { get; }

    /// <summary>Authorization request URI</summary>
    public string Uri => "tg://resolve?" + Query;

    /// <summary>Authorization request URI for Android devices</summary>
    public string AndroidUri => "tg:resolve?" + Query;

    /// <summary>Converts the parameters to their "tg://" URI string representation</summary>
    /// <returns>URI representation of this request</returns>
    public override string ToString() => Uri;

#pragma warning disable CA2208, MA0015 // names in ArgumentException
    /// <summary>Initializes a new instance of <see cref="AuthorizationRequestParameters"/></summary>
    /// <param name="botId">Unique identifier for the bot. You can get it from bot token. For example, for the bot token "1234567:4TT8bAc8GHUspu3ERYn-KGcvsvGB9u_n4ddy", the bot id is 1234567.</param>
    /// <param name="publicKey">Public key of the bot</param>
    /// <param name="nonce">Bot-specified nonce.
    /// Important: For security purposes it should be a cryptographically secure unique identifier of the request.
    /// In particular, it should be long enough and it should be generated using a cryptographically secure
    /// pseudorandom number generator. You should never accept credentials with the same nonce twice.</param>
    /// <param name="scope">Description of the data you want to request</param>
    public AuthorizationRequestParameters(long botId, string publicKey, string nonce, PassportScope scope)
    {
        BotId = botId;
        PublicKey = publicKey ?? throw new ArgumentNullException(nameof(publicKey));
        Nonce = nonce ?? throw new ArgumentNullException(nameof(nonce));
        PassportScope = scope ?? throw new ArgumentNullException(nameof(PassportScope));

        var scopeJson = JsonSerializer.Serialize(scope, JsonBotAPI.Options);
        Query = $"domain=telegrampassport&bot_id={botId}" +
                $"&scope={System.Uri.EscapeDataString(scopeJson)}" +
                $"&public_key={System.Uri.EscapeDataString(publicKey)}" +
                $"&nonce={System.Uri.EscapeDataString(nonce)}";
    }
}

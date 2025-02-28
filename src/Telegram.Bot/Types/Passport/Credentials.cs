// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Passport;

/// <summary>Credentials is a JSON-serialized object.<br/><b>IMPORTANT:</b> Make sure that the <b>nonce</b> is the same as was passed in the request.</summary>
public partial class Credentials : IDecryptedValue
{
    /// <summary>Credentials for encrypted data</summary>
    [JsonPropertyName("secure_data")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public SecureData SecureData { get; set; } = default!;

    /// <summary>Bot-specified nonce</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Nonce { get; set; } = default!;
}

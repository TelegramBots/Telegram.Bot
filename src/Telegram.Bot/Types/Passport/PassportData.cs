// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Passport;

/// <summary>Describes Telegram Passport data shared with the bot by the user.</summary>
public partial class PassportData
{
    /// <summary>Array with information about documents and other Telegram Passport elements that was shared with the bot</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public EncryptedPassportElement[] Data { get; set; } = default!;

    /// <summary>Encrypted credentials required to decrypt the data</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public EncryptedCredentials Credentials { get; set; } = default!;
}

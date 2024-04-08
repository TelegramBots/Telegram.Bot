using Newtonsoft.Json.Converters;

namespace Telegram.Bot.Types;

/// <summary>
/// Describes the connection of the bot with a business account.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class BusinessConnection
{
    /// <summary>
    /// Unique identifier of the business connection
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Id { get; set; } = default!;

    /// <summary>
    /// Business account user that created the business connection
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public User User { get; set; } = default!;

    /// <summary>
    /// Identifier of a private chat with the user who created the business connection. This number may have more than
    /// 32 significant bits and some programming languages may have difficulty/silent defects in interpreting it. But
    /// it has at most 52 significant bits, so a 64-bit integer or double-precision float type are safe for storing
    /// this identifier.
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public long UserChatId { get; set; }

    /// <summary>
    /// Date the connection was established
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime Date { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the bot can act on behalf of the business account in chats that were active in the last 24 hours
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool CanReply { get; set; }

    /// <summary>
    /// <see langword="true"/>, if the connection is active
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool IsEnabled { get; set; }
}

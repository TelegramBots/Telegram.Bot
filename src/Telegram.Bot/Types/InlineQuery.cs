using Telegram.Bot.Types.Enums;
using static Telegram.Bot.Types.Enums.ChatType;

namespace Telegram.Bot.Types;

/// <summary>
/// This object represents an incoming inline query. When the user sends an empty query, your bot could return
/// some default or trending results.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class InlineQuery
{
    /// <summary>
    /// Unique identifier for this query
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Id { get; set; } = default!;

    /// <summary>
    /// Sender
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public User From { get; set; } = default!;

    /// <summary>
    /// Text of the query (up to 256 characters)
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Query { get; set; } = default!;

    /// <summary>
    /// Offset of the results to be returned, can be controlled by the bot
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Offset { get; set; } = default!;

    /// <summary>
    /// Optional. Type of the chat, from which the inline query was sent. Can be either  <see cref="Sender"/> for
    /// a private chat with the inline query sender, <see cref="Private"/>, <see cref="Group"/>,
    /// <see cref="Supergroup"/>, or <see cref="Channel"/>. The chat type should be always known for requests
    /// sent from official clients and most third-party clients, unless the request was sent from a secret chat
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public ChatType? ChatType { get; set; }

    /// <summary>
    /// Optional. Sender location, only for bots that request user location
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public Location? Location { get; set; }
}

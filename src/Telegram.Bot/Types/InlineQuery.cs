namespace Telegram.Bot.Types;

/// <summary>
/// This object represents an incoming inline query. When the user sends an empty query, your bot could return some default or trending results.
/// </summary>
public partial class InlineQuery
{
    /// <summary>
    /// Unique identifier for this query
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Id { get; set; } = default!;

    /// <summary>
    /// Sender
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public User From { get; set; } = default!;

    /// <summary>
    /// Text of the query (up to 256 characters)
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Query { get; set; } = default!;

    /// <summary>
    /// Offset of the results to be returned, can be controlled by the bot
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Offset { get; set; } = default!;

    /// <summary>
    /// <em>Optional</em>. Type of the chat from which the inline query was sent. Can be either <see cref="Enums.ChatType.Sender">Sender</see> for a private chat with the inline query sender, <see cref="Enums.ChatType.Private">Private</see>, <see cref="Enums.ChatType.Group">Group</see>, <see cref="Enums.ChatType.Supergroup">Supergroup</see>, or <see cref="Enums.ChatType.Channel">Channel</see>. The chat type should be always known for requests sent from official clients and most third-party clients, unless the request was sent from a secret chat
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Enums.ChatType? ChatType { get; set; }

    /// <summary>
    /// <em>Optional</em>. Sender location, only for bots that request user location
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Location? Location { get; set; }
}

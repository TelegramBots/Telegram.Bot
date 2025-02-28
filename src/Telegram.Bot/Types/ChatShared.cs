// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object contains information about a chat that was shared with the bot using a <see cref="KeyboardButtonRequestChat"/> button.</summary>
public partial class ChatShared
{
    /// <summary>Identifier of the request</summary>
    [JsonPropertyName("request_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int RequestId { get; set; }

    /// <summary>Identifier of the shared chat. The bot may not have access to the chat and could be unable to use this identifier, unless the chat is already known to the bot by some other means.</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public long ChatId { get; set; }

    /// <summary><em>Optional</em>. Title of the chat, if the title was requested by the bot.</summary>
    public string? Title { get; set; }

    /// <summary><em>Optional</em>. Username of the chat, if the username was requested by the bot and available.</summary>
    public string? Username { get; set; }

    /// <summary><em>Optional</em>. Available sizes of the chat photo, if the photo was requested by the bot</summary>
    public PhotoSize[]? Photo { get; set; }
}

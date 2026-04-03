// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.ReplyMarkups;

/// <summary>This object defines the parameters for the creation of a managed bot. Information about the created bot will be shared with the bot using the update <em>ManagedBot</em> and a <see cref="Message"/> with the field <em>ManagedBotCreated</em>.</summary>
public partial class KeyboardButtonRequestManagedBot
{
    /// <summary>Signed 32-bit identifier of the request. Must be unique within the message</summary>
    [JsonPropertyName("request_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int RequestId { get; set; }

    /// <summary><em>Optional</em>. Suggested name for the bot</summary>
    [JsonPropertyName("suggested_name")]
    public string? SuggestedName { get; set; }

    /// <summary><em>Optional</em>. Suggested username for the bot</summary>
    [JsonPropertyName("suggested_username")]
    public string? SuggestedUsername { get; set; }

    /// <summary>Initializes an instance of <see cref="KeyboardButtonRequestManagedBot"/></summary>
    /// <param name="requestId">Signed 32-bit identifier of the request. Must be unique within the message</param>
    [SetsRequiredMembers]
    public KeyboardButtonRequestManagedBot(int requestId) => RequestId = requestId;

    /// <summary>Instantiates a new <see cref="KeyboardButtonRequestManagedBot"/></summary>
    public KeyboardButtonRequestManagedBot() { }
}
